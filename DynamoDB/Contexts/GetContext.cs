// Copyright 2015 Adamantworks.  All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Syntax;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	// See Overloads.tt and Overloads.cs for more methods of this class
	internal partial class GetContext : IConsistentGetSyntax
	{
		private readonly Table table;
		private readonly ProjectionExpression projection;
		private bool? consistentRead;

		public GetContext(Table table, ProjectionExpression projection)
		{
			this.table = table;
			this.projection = projection;
		}
		public GetContext(Table table, ProjectionExpression projection, bool consistentRead)
		{
			this.table = table;
			this.projection = projection;
			this.consistentRead = consistentRead;
		}

		public IGetSyntax Consistent { get { return ConsistentIf(true); } }
		public IGetSyntax ConsistentIf(bool consistent)
		{
			if(consistentRead != null)
				throw new InvalidOperationException("Can't set Consistent twice");
			consistentRead = consistent;
			return this;
		}

		#region Get
		public async Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken)
		{
			var request = BuildGetRequest(key);
			var result = await table.Region.DB.GetItemAsync(request, cancellationToken).ConfigureAwait(false);
			return result.GetItem();
		}

		public DynamoDBMap Get(ItemKey key)
		{
			var request = BuildGetRequest(key);
			var result = table.Region.DB.GetItem(request);
			return result.GetItem();
		}

		private Aws.GetItemRequest BuildGetRequest(ItemKey key)
		{
			var request = new Aws.GetItemRequest()
			{
				TableName = table.Name,
				Key = key.ToAws(table.Schema.Key),
				ConsistentRead = consistentRead ?? false,
			};
			if(projection != null)
			{
				request.ProjectionExpression = projection.Expression;
				request.ExpressionAttributeNames = AwsAttributeNames.Get(projection);
			}
			return request;
		}
		#endregion

		#region BatchGet
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			var awsKeys = keys.Select(k => k.ToAws(table.Schema.Key));

			return AsyncEnumerable.Using(awsKeys.GetEnumerator, enumerator =>
			{
				// These must be in the using so they are deferred until GetEnumerator() is called on us (need one per enumerator)
				var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(Limits.BatchGetSize);
				var request = BuildBatchGetItemRequest(batchKeys);

				return AsyncEnumerableEx.GenerateChunked<Aws.BatchGetItemResponse, DynamoDBMap>(null,
					async (lastResponse, cancellationToken) =>
					{
						while(batchKeys.Count < Limits.BatchGetSize && await enumerator.MoveNext().ConfigureAwait(false))
							batchKeys.Add(enumerator.Current);

						if(batchKeys.Count == 0) // No more items to get
							return null;

						var response = await table.Region.DB.BatchGetItemAsync(request, cancellationToken).ConfigureAwait(false);

						ReBatchUnprocessedKeys(response, batchKeys);

						return response;
					},
					lastResponse => lastResponse != null ? lastResponse.Responses[table.Name].Select(item => item.ToMap()) : Enumerable.Empty<DynamoDBMap>(),
					lastResponse => lastResponse == null,
					readAhead);
			});
		}

		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys)
		{
			var awsKeys = keys.Select(k => k.ToAws(table.Schema.Key));

			var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(Limits.BatchGetSize);
			var request = BuildBatchGetItemRequest(batchKeys);

			using(var enumerator = awsKeys.GetEnumerator())
			{
				for(; ; )
				{
					while(batchKeys.Count < Limits.BatchGetSize && enumerator.MoveNext())
						batchKeys.Add(enumerator.Current);

					if(batchKeys.Count == 0) // No more items to get
						yield break;

					var response = table.Region.DB.BatchGetItem(request);

					ReBatchUnprocessedKeys(response, batchKeys);

					foreach(var item in response.Responses[table.Name])
						yield return item.ToMap();
				}
			}
		}

		private Aws.BatchGetItemRequest BuildBatchGetItemRequest(List<Dictionary<string, Aws.AttributeValue>> batchKeys)
		{
			var keysAndAttributes = new Aws.KeysAndAttributes()
			{
				Keys = batchKeys,
				ConsistentRead = consistentRead ?? false,
			};
			if(projection != null)
			{
				keysAndAttributes.ProjectionExpression = projection.Expression;
				keysAndAttributes.ExpressionAttributeNames = AwsAttributeNames.Get(projection);
			}
			var request = new Aws.BatchGetItemRequest()
			{
				RequestItems = new Dictionary<string, Aws.KeysAndAttributes>()
				{
					{table.Name, keysAndAttributes}
				},
			};
			return request;
		}
		private void ReBatchUnprocessedKeys(Aws.BatchGetItemResponse response, List<Dictionary<string, Aws.AttributeValue>> batchKeys)
		{
			batchKeys.Clear();
			if(response.UnprocessedKeys.Count > 0)
				batchKeys.AddRange(response.UnprocessedKeys[table.Name].Keys);
		}
		#endregion

		#region BatchGetJoin
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return AsyncEnumerable.Using(outerItems.GetEnumerator, enumerator =>
			{
				// These must be in the using so they are deferred until GetEnumerator() is called on us (need one per enumerator)
				var innerItems = new Dictionary<ItemKey, DynamoDBMap>();
				var batchItems = new Dictionary<ItemKey, IList<T>>(Limits.BatchGetSize);
				var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(Limits.BatchGetSize);
				var request = BuildBatchGetItemRequest(batchKeys);

				return AsyncEnumerableEx.GenerateChunked<Tuple<List<TResult>, bool>, TResult>(null,
					async (lastResults, cancellationToken) =>
					{
						var batchResults = new List<TResult>(Limits.BatchGetSize);

						while(batchItems.Count < Limits.BatchGetSize && await enumerator.MoveNext()) // Need context for later calls to keySelector and resultSelector
						{
							var outerItem = enumerator.Current;
							var key = keySelector(outerItem);
							DynamoDBMap innerItem;
							if(innerItems.TryGetValue(key, out innerItem))
								batchResults.Add(resultSelector(outerItem, innerItem));
							else
								BatchItem(batchItems, key, outerItem);
						}

						if(batchItems.Count == 0) // No more items to get
							return Tuple.Create(batchResults, false); // we could still have picked up some values in the above loop

						batchKeys.AddRange(batchItems.Keys.Select(k => k.ToAws(table.Schema.Key)));

						var response = await table.Region.DB.BatchGetItemAsync(request, cancellationToken); // Need context for later calls to keySelector and resultSelector
						var unprocessedItems = ProcessResponse(response, innerItems, batchItems);

						foreach(var batchItem in batchItems)
						{
							var batchItemOuterItems = batchItem.Value;
							var innerItem = GetInnerItem(batchItem, innerItems);
							foreach(var outerItem in batchItemOuterItems)
								batchResults.Add(resultSelector(outerItem, innerItem));
						}

						ReBatchUnprocessedKeys(batchItems, batchKeys, unprocessedItems);

						return Tuple.Create(batchResults, true);
					},
					lastResponse => lastResponse.Item1 ?? Enumerable.Empty<TResult>(),
					lastResponse => lastResponse.Item2,
					readAhead,
					true);
			});
		}

		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			var innerItems = new Dictionary<ItemKey, DynamoDBMap>();
			var batchItems = new Dictionary<ItemKey, IList<T>>(Limits.BatchGetSize);
			var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(Limits.BatchGetSize);
			var request = BuildBatchGetItemRequest(batchKeys);

			using(var enumerator = outerItems.GetEnumerator())
			{
				for(; ; )
				{
					while(batchItems.Count < Limits.BatchGetSize && enumerator.MoveNext())
					{
						var outerItem = enumerator.Current;
						var key = keySelector(outerItem);
						DynamoDBMap innerItem;
						if(innerItems.TryGetValue(key, out innerItem))
							yield return resultSelector(outerItem, innerItem);
						else
							BatchItem(batchItems, key, outerItem);
					}

					if(batchItems.Count == 0) // No more items to get
						yield break;

					batchKeys.AddRange(batchItems.Keys.Select(k => k.ToAws(table.Schema.Key)));

					var response = table.Region.DB.BatchGetItem(request);
					var unprocessedItems = ProcessResponse(response, innerItems, batchItems);

					foreach(var batchItem in batchItems)
					{
						var batchItemOuterItems = batchItem.Value;
						var innerItem = GetInnerItem(batchItem, innerItems);
						foreach(var outerItem in batchItemOuterItems)
							yield return resultSelector(outerItem, innerItem);
					}

					ReBatchUnprocessedKeys(batchItems, batchKeys, unprocessedItems);
				}
			}
		}

		private static void BatchItem<T>(IDictionary<ItemKey, IList<T>> batchItems, ItemKey key, T item)
		{
			IList<T> items;
			if(!batchItems.TryGetValue(key, out items))
			{
				items = new List<T>();
				batchItems.Add(key, items);
			}
			items.Add(item);
		}

		private IDictionary<ItemKey, IList<T>> ProcessResponse<T>(Aws.BatchGetItemResponse response, IDictionary<ItemKey, DynamoDBMap> innerItems, IDictionary<ItemKey, IList<T>> batchItems)
		{
			foreach(var item in response.Responses[table.Name])
			{
				var innerItem = item.ToMap();
				if(innerItem != null)
					innerItems.Add(table.GetKey(innerItem), innerItem);
			}

			var unprocessedKeys = response.UnprocessedKeys.Count > 0
				? response.UnprocessedKeys[table.Name].Keys.Select(k => k.ToItemKey(table.Schema.Key))
				: Enumerable.Empty<ItemKey>();
			return unprocessedKeys.ToDictionary(k => k, k =>
			{
				var item1 = batchItems[k];
				batchItems.Remove(k);
				return item1;
			});
		}
		private static DynamoDBMap GetInnerItem<T>(KeyValuePair<ItemKey, IList<T>> batchItem, IDictionary<ItemKey, DynamoDBMap> innerItems)
		{
			var key = batchItem.Key;
			DynamoDBMap innerItem;
			if(!innerItems.TryGetValue(key, out innerItem))
				innerItems.Add(key, null); // Add that the key was not found, for future lookups
			return innerItem; // innerItem == null if TryGetValue returned false
		}
		private static void ReBatchUnprocessedKeys<T>(IDictionary<ItemKey, IList<T>> batchItems, IList<Dictionary<string, Aws.AttributeValue>> batchKeys, IDictionary<ItemKey, IList<T>> unprocessedItems)
		{
			batchItems.Clear();
			batchKeys.Clear();

			foreach(var item in unprocessedItems)
				batchItems.Add(item.Key, item.Value);
		}
		#endregion
	}
}
