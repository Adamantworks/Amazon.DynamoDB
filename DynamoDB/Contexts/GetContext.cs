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
using Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class GetContext : IConsistentSyntax
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

		public IConsistentSyntax Consistent
		{
			get
			{
				if(consistentRead != null)
					throw new InvalidOperationException("Can't set Consistent twice");
				consistentRead = true;
				return this;
			}
		}

		#region Get
		public async Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken)
		{
			var request = BuildGetRequest(key);
			var result = await table.Region.DB.GetItemAsync(request, cancellationToken).ConfigureAwait(false);
			return result.Item.ToGetValue();
		}

		public DynamoDBMap Get(ItemKey key)
		{
			var request = BuildGetRequest(key);
			var result = table.Region.DB.GetItem(request);
			return result.Item.ToGetValue();
		}

		private global::Amazon.DynamoDBv2.Model.GetItemRequest BuildGetRequest(ItemKey key)
		{
			var request = new global::Amazon.DynamoDBv2.Model.GetItemRequest()
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
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys,  ReadAhead readAhead)
		{
			var awsKeys = keys.Select(k => k.ToAws(table.Schema.Key));

			return AsyncEnumerable.Using(awsKeys.GetEnumerator, enumerator =>
			{
				// These must be in the using so they are deferred until GetEnumerator() is called on us (need one per enumerator)
				var batchKeys = new List<Dictionary<string, AttributeValue>>(Limits.BatchGetSize);
				var request = BuildBatchGetItemRequest(batchKeys);

				return AsyncEnumerableEx.GenerateChunked<global::Amazon.DynamoDBv2.Model.BatchGetItemResponse, DynamoDBMap>(null,
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
					lastResponse => lastResponse != null ? lastResponse.Responses[table.Name].Select(item => item.ToGetValue()) : Enumerable.Empty<DynamoDBMap>(),
					lastResponse => lastResponse == null,
					readAhead);
			});
		}

		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys)
		{
			var awsKeys = keys.Select(k => k.ToAws(table.Schema.Key));

			var batchKeys = new List<Dictionary<string, AttributeValue>>(Limits.BatchGetSize);
			var request = BuildBatchGetItemRequest(batchKeys );

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
						yield return item.ToGetValue();
				}
			}
		}

		private global::Amazon.DynamoDBv2.Model.BatchGetItemRequest BuildBatchGetItemRequest(List<Dictionary<string, AttributeValue>> batchKeys)
		{
			var keysAndAttributes = new global::Amazon.DynamoDBv2.Model.KeysAndAttributes()
			{
				Keys = batchKeys,
				ConsistentRead = consistentRead ?? false,
			};
			if(projection != null)
			{
				keysAndAttributes.ProjectionExpression = projection.Expression;
				keysAndAttributes.ExpressionAttributeNames = AwsAttributeNames.Get(projection);
			}
			var request = new global::Amazon.DynamoDBv2.Model.BatchGetItemRequest()
			{
				RequestItems = new Dictionary<string, KeysAndAttributes>()
				{
					{table.Name, keysAndAttributes}
				},
			};
			return request;
		}
		private void ReBatchUnprocessedKeys(global::Amazon.DynamoDBv2.Model.BatchGetItemResponse response, List<Dictionary<string, AttributeValue>> batchKeys)
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
				var batchItems = new Dictionary<ItemKey, T>(Limits.BatchGetSize);
				var batchKeys = new List<Dictionary<string, AttributeValue>>(Limits.BatchGetSize);
				var request = BuildBatchGetItemRequest(batchKeys);

				return AsyncEnumerableEx.GenerateChunked<Tuple<List<TResult>, bool>, TResult>(null,
					async (lastResults, cancellationToken) =>
					{
						var batchResults = new List<TResult>(Limits.BatchGetSize);

						while(batchItems.Count < Limits.BatchGetSize && await enumerator.MoveNext().ConfigureAwait(false))
						{
							var outerItem = enumerator.Current;
							var key = keySelector(outerItem);
							DynamoDBMap innerItem;
							if(innerItems.TryGetValue(key, out innerItem))
								batchResults.Add(resultSelector(outerItem, innerItem));
							else
								batchItems.Add(key, outerItem);
						}

						if(batchItems.Count == 0) // No more items to get
							return Tuple.Create(batchResults, false); // we could still have picked up some values in the above loop

						batchKeys.AddRange(batchItems.Keys.Select(k => k.ToAws(table.Schema.Key)));

						var response = await table.Region.DB.BatchGetItemAsync(request, cancellationToken).ConfigureAwait(false);
						var unprocessedItems = ProcessResponse(response, innerItems, batchItems);

						foreach(var batchItem in batchItems)
						{
							var outerItem = batchItem.Value;
							var innerItem = GetInnerItem(batchItem, innerItems);
							batchResults.Add(resultSelector(outerItem, innerItem));
						}

						ReBatchUnprocessedKeys(batchItems, batchKeys, unprocessedItems);

						return Tuple.Create(batchResults, true);
					},
					lastResponse => lastResponse.Item1 ?? Enumerable.Empty<TResult>(),
					lastResponse => lastResponse.Item2,
					readAhead);
			});
		}

		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			var innerItems = new Dictionary<ItemKey, DynamoDBMap>();
			var batchItems = new Dictionary<ItemKey, T>(Limits.BatchGetSize);
			var batchKeys = new List<Dictionary<string, AttributeValue>>(Limits.BatchGetSize);
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
							batchItems.Add(key, outerItem);
					}

					if(batchItems.Count == 0) // No more items to get
						yield break;

					batchKeys.AddRange(batchItems.Keys.Select(k => k.ToAws(table.Schema.Key)));

					var response = table.Region.DB.BatchGetItem(request);
					var unprocessedItems = ProcessResponse(response, innerItems, batchItems);

					foreach(var batchItem in batchItems)
					{
						var outerItem = batchItem.Value;
						var innerItem = GetInnerItem(batchItem, innerItems);
						yield return resultSelector(outerItem, innerItem);
					}

					ReBatchUnprocessedKeys(batchItems, batchKeys, unprocessedItems);
				}
			}
		}

		private IDictionary<ItemKey, T> ProcessResponse<T>(global::Amazon.DynamoDBv2.Model.BatchGetItemResponse response, IDictionary<ItemKey, DynamoDBMap> innerItems, IDictionary<ItemKey, T> batchItems)
		{
			foreach(var item in response.Responses[table.Name])
			{
				var innerItem = item.ToGetValue();
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
		private static DynamoDBMap GetInnerItem<T>(KeyValuePair<ItemKey, T> batchItem, IDictionary<ItemKey, DynamoDBMap> innerItems)
		{
			var key = batchItem.Key;
			DynamoDBMap innerItem;
			if(!innerItems.TryGetValue(key, out innerItem))
				innerItems.Add(key, null); // Add that the key was not found, for future lookups
			return innerItem; // innerItem == null if TryGetValue returned false
		}
		private static void ReBatchUnprocessedKeys<T>(IDictionary<ItemKey, T> batchItems, IList<Dictionary<string, AttributeValue>> batchKeys, IDictionary<ItemKey, T> unprocessedItems)
		{
			batchItems.Clear();
			batchKeys.Clear();

			foreach(var item in unprocessedItems)
				batchItems.Add(item.Key, item.Value);
		}
		#endregion
	}
}
