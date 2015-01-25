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

using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB
{
	// See Overloads.tt and Overloads.cs for more method overloads of this interface
	public partial interface ITable
	{
		string Name { get; }
		TableSchema Schema { get; }
		IReadOnlyDictionary<string, IIndex> Indexes { get; }
		DateTime CreationDateTime { get; }
		long ItemCount { get; }
		long SizeInBytes { get; }
		TableStatus Status { get; }
		IProvisionedThroughputInfo ProvisionedThroughput { get; }

		Task ReloadAsync(CancellationToken cancellationToken);
		void Reload();

		Task WaitUntilNotAsync(TableStatus status, CancellationToken cancellationToken);
		Task WaitUntilNotAsync(TableStatus status, TimeSpan timeout, CancellationToken cancellationToken);
		void WaitUntilNot(TableStatus status);
		void WaitUntilNot(TableStatus status, TimeSpan timeout);

		// The overloads of these methods are in Overloads.tt and call the private implementations
		// Task UpdateTableAsync(...);
		// void UpdateTable(...);

		ItemKey GetKey(DynamoDBMap item);

		Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken);
		DynamoDBMap Get(ItemKey key, ProjectionExpression projection, bool consistent);

		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent);

		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent, ReadAhead readAhead);
		IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent);

		void PutAsync(IBatchWriteAsync batch, DynamoDBMap item);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, Values values, bool returnOldItem, CancellationToken cancellationToken);
		void Put(IBatchWrite batch, DynamoDBMap item);
		DynamoDBMap Put(DynamoDBMap item, PredicateExpression condition, Values values, bool returnOldItem);

		Task InsertAsync(DynamoDBMap item);
		Task InsertAsync(DynamoDBMap item, CancellationToken cancellationToken);
		void Insert(DynamoDBMap item);

		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		DynamoDBMap Update(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue);

		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		bool TryUpdate(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values);

		void DeleteAsync(IBatchWriteAsync batch, ItemKey key);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, Values values, bool returnOldItem, CancellationToken cancellationToken);
		void Delete(IBatchWrite batch, ItemKey key);
		DynamoDBMap Delete(ItemKey key, PredicateExpression condition, Values values, bool returnOldItem);

		// The overloads of these methods are in Overloads.tt
		// IQueryContext Query(...);
		// IScanContext Scan(...);

		// TODO: QueryCount
		// TODO: ScanCount()

		// TODO: bool ReportConsumedCapacity
		// TODO: CapacityConsumedEvent
	}

	// See Overloads.tt and Overloads.cs for more method overloads of this class
	internal partial class Table : ITable
	{
		internal readonly Region Region;

		public Table(Region region, Aws.TableDescription tableDescription)
		{
			Region = region;
			UpdateTableDescription(tableDescription);
		}

		private void UpdateTableDescription(Aws.TableDescription tableDescription)
		{
			Name = tableDescription.TableName;
			Status = tableDescription.TableStatus.ToTableStatus();
			// Handle state where all schema info is gone
			if(Status == TableStatus.Deleting && tableDescription.AttributeDefinitions.Count == 0) return;
			Schema = tableDescription.ToSchema();
			CreationDateTime = tableDescription.CreationDateTime;
			ItemCount = tableDescription.ItemCount;
			SizeInBytes = tableDescription.TableSizeBytes;
			ProvisionedThroughput = tableDescription.ProvisionedThroughput.ToInfo();

			// Indexes
			var existingIndexes = Indexes;
			var newIndexes = new Dictionary<string, IIndex>();
			foreach(var indexDescription in tableDescription.GlobalSecondaryIndexes)
			{
				IIndex existingIndex = null;
				if(existingIndexes != null)
					existingIndexes.TryGetValue(indexDescription.IndexName, out existingIndex);
				var newIndex = (Index)existingIndex ?? new Index(this, indexDescription.IndexName);
				newIndex.UpdateDescription(indexDescription, Schema.Indexes[indexDescription.IndexName]);
				newIndexes.Add(indexDescription.IndexName, newIndex);
			}
			foreach(var indexDescription in tableDescription.LocalSecondaryIndexes)
			{
				IIndex existingIndex = null;
				if(existingIndexes != null)
					existingIndexes.TryGetValue(indexDescription.IndexName, out existingIndex);
				var newIndex = (Index)existingIndex ?? new Index(this, indexDescription.IndexName);
				newIndex.UpdateDescription(indexDescription, Schema.Indexes[indexDescription.IndexName]);
				newIndexes.Add(indexDescription.IndexName, newIndex);
			}
			Indexes = newIndexes;
		}

		public string Name { get; private set; }
		public TableSchema Schema { get; private set; }
		public IReadOnlyDictionary<string, IIndex> Indexes { get; private set; }
		public DateTime CreationDateTime { get; private set; }
		public long ItemCount { get; private set; }
		public long SizeInBytes { get; private set; }
		public TableStatus Status { get; private set; }
		public IProvisionedThroughputInfo ProvisionedThroughput { get; private set; }

		#region Reload
		public async Task ReloadAsync(CancellationToken cancellationToken)
		{
			var response = await Region.DB.DescribeTableAsync(new Aws.DescribeTableRequest(Name), cancellationToken).ConfigureAwait(false);
			UpdateTableDescription(response.Table);
		}

		public void Reload()
		{
			try
			{
				var response = Region.DB.DescribeTable(Name);
				UpdateTableDescription(response.Table);
			}
			catch(Aws.ResourceNotFoundException)
			{
				Status = TableStatus.Deleted;
			}
		}
		#endregion

		#region WaitUntilNot
		public async Task WaitUntilNotAsync(TableStatus status, CancellationToken cancellationToken)
		{
			CheckCanWaitUntilNot(status);
			await ReloadAsync(cancellationToken).ConfigureAwait(false);
			while((Status == status || Indexes.Values.Any(i => i.Status == status)) && !cancellationToken.IsCancellationRequested)
			{
				await Task.Delay(Region.WaitStatusPollingInterval, cancellationToken).ConfigureAwait(false);
				await ReloadAsync(cancellationToken).ConfigureAwait(false);
			}
		}
		public async Task WaitUntilNotAsync(TableStatus status, TimeSpan timeout, CancellationToken cancellationToken)
		{
			CheckCanWaitUntilNot(status);
			await ReloadAsync(cancellationToken).ConfigureAwait(false);
			var start = DateTime.UtcNow;
			TimeSpan timeRemaining;
			while((Status == status || Indexes.Values.Any(i => i.Status == status)) && !cancellationToken.IsCancellationRequested && (timeRemaining = DateTime.UtcNow - start) < timeout)
			{
				await Task.Delay(TimeSpanEx.Min(Region.WaitStatusPollingInterval, timeRemaining), cancellationToken).ConfigureAwait(false);
				await ReloadAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		public void WaitUntilNot(TableStatus status)
		{
			CheckCanWaitUntilNot(status);
			Reload();
			while(Status == status || Indexes.Values.Any(i => i.Status == status))
			{
				Thread.Sleep(Region.WaitStatusPollingInterval);
				Reload();
			}
		}
		public void WaitUntilNot(TableStatus status, TimeSpan timeout)
		{
			CheckCanWaitUntilNot(status);
			Reload();
			var start = DateTime.UtcNow;
			TimeSpan timeRemaining;
			while((Status == status || Indexes.Values.Any(i => i.Status == status)) && (timeRemaining = DateTime.UtcNow - start) < timeout)
			{
				Thread.Sleep(TimeSpanEx.Min(Region.WaitStatusPollingInterval, timeRemaining));
				Reload();
			}
		}

		private static void CheckCanWaitUntilNot(TableStatus status)
		{
			switch(status)
			{
				case TableStatus.Creating:
				case TableStatus.Updating:
				case TableStatus.Deleting:
					return;
				default:
					throw new ArgumentException("Can only wait on transient status (Creating, Updating, Deleting)", "status");
			}
		}
		#endregion

		#region UpdateTable
		private Task UpdateTableAsync(ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			var request = BuildUpdateTableRequest(provisionedThroughput, indexProvisionedThroughputs);
			return Region.DB.UpdateTableAsync(request, cancellationToken);
		}

		private void UpdateTable(ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = BuildUpdateTableRequest(provisionedThroughput, indexProvisionedThroughputs);
			Region.DB.UpdateTable(request);
		}

		private Aws.UpdateTableRequest BuildUpdateTableRequest(ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = new Aws.UpdateTableRequest()
			{
				TableName = Name,
			};
			if(provisionedThroughput != null)
				request.ProvisionedThroughput = provisionedThroughput.Value.ToAws();
			if(indexProvisionedThroughputs != null && indexProvisionedThroughputs.Count > 0)
				request.GlobalSecondaryIndexUpdates = indexProvisionedThroughputs.Select(i => new Aws.GlobalSecondaryIndexUpdate()
				{
					Update = new Aws.UpdateGlobalSecondaryIndexAction()
					{
						IndexName = i.Key,
						ProvisionedThroughput = i.Value.ToAws(),
					}
				}).ToList();

			return request;
		}
		#endregion

		public ItemKey GetKey(DynamoDBMap item)
		{
			return Schema.Key.GetKey(item);
		}

		#region Get
		public async Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			var request = BuildGetRequest(key, projection, consistent);
			var result = await Region.DB.GetItemAsync(request, cancellationToken).ConfigureAwait(false);
			return result.Item.ToGetValue();
		}

		public DynamoDBMap Get(ItemKey key, ProjectionExpression projection, bool consistent)
		{
			var request = BuildGetRequest(key, projection, consistent);
			var result = Region.DB.GetItem(request);
			return result.Item.ToGetValue();
		}

		private Aws.GetItemRequest BuildGetRequest(ItemKey key, ProjectionExpression projection, bool consistent)
		{
			var request = new Aws.GetItemRequest()
			{
				TableName = Name,
				Key = key.ToAws(Schema.Key),
				ConsistentRead = consistent,
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
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			var awsKeys = keys.Select(k => k.ToAws(Schema.Key));

			return AsyncEnumerable.Using(awsKeys.GetEnumerator, enumerator =>
			{
				// These must be in the using so they are deferred until GetEnumerator() is called on us (need one per enumerator)
				var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(Limits.BatchGetSize);
				var request = BuildBatchGetItemRequest(batchKeys, projection, consistent);

				return AsyncEnumerableEx.GenerateChunked<Aws.BatchGetItemResponse, DynamoDBMap>(null,
					async (lastResponse, cancellationToken) =>
					{
						while(batchKeys.Count < Limits.BatchGetSize && await enumerator.MoveNext().ConfigureAwait(false))
							batchKeys.Add(enumerator.Current);

						if(batchKeys.Count == 0) // No more items to get
							return null;

						var response = await Region.DB.BatchGetItemAsync(request, cancellationToken).ConfigureAwait(false);

						ReBatchUnprocessedKeys(response, batchKeys);

						return response;
					},
					lastResponse => lastResponse != null ? lastResponse.Responses[Name].Select(item => item.ToGetValue()) : Enumerable.Empty<DynamoDBMap>(),
					lastResponse => lastResponse == null,
					readAhead);
			});
		}

		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent)
		{
			var awsKeys = keys.Select(k => k.ToAws(Schema.Key));

			var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(Limits.BatchGetSize);
			var request = BuildBatchGetItemRequest(batchKeys, projection, consistent);

			using(var enumerator = awsKeys.GetEnumerator())
			{
				for(; ; )
				{
					while(batchKeys.Count < Limits.BatchGetSize && enumerator.MoveNext())
						batchKeys.Add(enumerator.Current);

					if(batchKeys.Count == 0) // No more items to get
						yield break;

					var response = Region.DB.BatchGetItem(request);

					ReBatchUnprocessedKeys(response, batchKeys);

					foreach(var item in response.Responses[Name])
						yield return item.ToGetValue();
				}
			}
		}

		private Aws.BatchGetItemRequest BuildBatchGetItemRequest(List<Dictionary<string, Aws.AttributeValue>> batchKeys, ProjectionExpression projection, bool consistent)
		{
			var keysAndAttributes = new Aws.KeysAndAttributes()
			{
				Keys = batchKeys,
				ConsistentRead = consistent,
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
					{Name, keysAndAttributes}
				},
			};
			return request;
		}
		private void ReBatchUnprocessedKeys(Aws.BatchGetItemResponse response, List<Dictionary<string, Aws.AttributeValue>> batchKeys)
		{
			batchKeys.Clear();
			if(response.UnprocessedKeys.Count > 0)
				batchKeys.AddRange(response.UnprocessedKeys[Name].Keys);
		}
		#endregion

		#region BatchGetJoin
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			return AsyncEnumerable.Using(outerItems.GetEnumerator, enumerator =>
			{
				// These must be in the using so they are deferred until GetEnumerator() is called on us (need one per enumerator)
				var innerItems = new Dictionary<ItemKey, DynamoDBMap>();
				var batchItems = new Dictionary<ItemKey, T>(Limits.BatchGetSize);
				var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(Limits.BatchGetSize);
				var request = BuildBatchGetItemRequest(batchKeys, projection, consistent);

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

						batchKeys.AddRange(batchItems.Keys.Select(k => k.ToAws(Schema.Key)));

						var response = await Region.DB.BatchGetItemAsync(request, cancellationToken).ConfigureAwait(false);
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

		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent)
		{
			var innerItems = new Dictionary<ItemKey, DynamoDBMap>();
			var batchItems = new Dictionary<ItemKey, T>(Limits.BatchGetSize);
			var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(Limits.BatchGetSize);
			var request = BuildBatchGetItemRequest(batchKeys, projection, consistent);

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

					batchKeys.AddRange(batchItems.Keys.Select(k => k.ToAws(Schema.Key)));

					var response = Region.DB.BatchGetItem(request);
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

		private IDictionary<ItemKey, T> ProcessResponse<T>(Aws.BatchGetItemResponse response, IDictionary<ItemKey, DynamoDBMap> innerItems, IDictionary<ItemKey, T> batchItems)
		{
			foreach(var item in response.Responses[Name])
			{
				var innerItem = item.ToGetValue();
				if(innerItem != null)
					innerItems.Add(GetKey(innerItem), innerItem);
			}

			var unprocessedKeys = response.UnprocessedKeys.Count > 0
				? response.UnprocessedKeys[Name].Keys.Select(k => k.ToItemKey(Schema.Key))
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
		private static void ReBatchUnprocessedKeys<T>(IDictionary<ItemKey, T> batchItems, IList<Dictionary<string, Aws.AttributeValue>> batchKeys, IDictionary<ItemKey, T> unprocessedItems)
		{
			batchItems.Clear();
			batchKeys.Clear();

			foreach(var item in unprocessedItems)
				batchItems.Add(item.Key, item.Value);
		}
		#endregion

		#region Put
		public void PutAsync(IBatchWriteAsync batch, DynamoDBMap item)
		{
			((IBatchWriteOperations)batch).Put(this, item);
		}
		public async Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, Values values, bool returnOldItem, CancellationToken cancellationToken)
		{
			var request = BuildPutItemRequest(item, condition, values, returnOldItem);
			var response = await Region.DB.PutItemAsync(request, cancellationToken).ConfigureAwait(false);
			if(!returnOldItem) return null;
			return response.Attributes.ToGetValue();
		}

		public void Put(IBatchWrite batch, DynamoDBMap item)
		{
			((IBatchWriteOperations)batch).Put(this, item);
		}
		public DynamoDBMap Put(DynamoDBMap item, PredicateExpression condition, Values values, bool returnOldItem)
		{
			var request = BuildPutItemRequest(item, condition, values, returnOldItem);
			var response = Region.DB.PutItem(request);
			if(!returnOldItem) return null;
			return response.Attributes.ToGetValue();
		}

		private Aws.PutItemRequest BuildPutItemRequest(DynamoDBMap item, PredicateExpression condition, Values values, bool returnOldItem)
		{
			var request = new Aws.PutItemRequest()
			{
				TableName = Name,
				Item = item.ToAwsDictionary(),
				ReturnValues = returnOldItem ? AwsEnums.ReturnValue.ALL_OLD : AwsEnums.ReturnValue.NONE,
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(condition, values);
			return request;
		}
		#endregion

		#region Insert
		public Task InsertAsync(DynamoDBMap item)
		{
			return PutAsync(item, GetInsertExpression(), null, false, CancellationToken.None);
		}
		public Task InsertAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			return PutAsync(item, GetInsertExpression(), null, false, cancellationToken);
		}

		public void Insert(DynamoDBMap item)
		{
			Put(item, GetInsertExpression(), null, false);
		}

		private PredicateExpression insertExpression;
		private PredicateExpression GetInsertExpression()
		{
			if(insertExpression == null)
				insertExpression = new PredicateExpression("attribute_not_exists(#key)", "key", Schema.Key.HashKey.Name);
			return insertExpression;
		}
		#endregion

		#region Update
		public async Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			var request = BuildUpdateRequest(key, update, condition, values, returnValue);
			var response = await Region.DB.UpdateItemAsync(request, cancellationToken).ConfigureAwait(false);
			if(returnValue == UpdateReturnValue.None) return null;
			return response.Attributes.ToGetValue();
		}

		public DynamoDBMap Update(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue)
		{
			var request = BuildUpdateRequest(key, update, condition, values, returnValue);
			var response = Region.DB.UpdateItem(request);
			if(returnValue == UpdateReturnValue.None) return null;
			return response.Attributes.ToGetValue();
		}

		private Aws.UpdateItemRequest BuildUpdateRequest(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue)
		{
			var request = new Aws.UpdateItemRequest()
			{
				TableName = Name,
				Key = key.ToAws(Schema.Key),
				UpdateExpression = update.Expression,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(update, condition),
				ReturnValues = returnValue.ToAws(),
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(update, condition, values);

			return request;
		}
		#endregion

		#region TryUpdate
		public async Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			try
			{
				await UpdateAsync(key, update, condition, values, UpdateReturnValue.None, cancellationToken).ConfigureAwait(false);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}

		public bool TryUpdate(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values)
		{
			try
			{
				Update(key, update, condition, values, UpdateReturnValue.None);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}
		#endregion

		#region Delete
		public void DeleteAsync(IBatchWriteAsync batch, ItemKey key)
		{
			((IBatchWriteOperations)batch).Delete(this, key);
		}
		public async Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, Values values, bool returnOldItem, CancellationToken cancellationToken)
		{
			var request = BuildDeleteRequest(key, condition, values, returnOldItem);
			var response = await Region.DB.DeleteItemAsync(request, cancellationToken).ConfigureAwait(false);
			if(!returnOldItem) return null;
			return response.Attributes.ToGetValue();
		}

		public void Delete(IBatchWrite batch, ItemKey key)
		{
			((IBatchWriteOperations)batch).Delete(this, key);
		}
		public DynamoDBMap Delete(ItemKey key, PredicateExpression condition, Values values, bool returnOldItem)
		{
			var request = BuildDeleteRequest(key, condition, values, returnOldItem);
			var response = Region.DB.DeleteItem(request);
			if(!returnOldItem) return null;
			return response.Attributes.ToGetValue();
		}

		private Aws.DeleteItemRequest BuildDeleteRequest(ItemKey key, PredicateExpression condition, Values values, bool returnOldItem)
		{
			var request = new Aws.DeleteItemRequest()
			{
				TableName = Name,
				Key = key.ToAws(Schema.Key),
				ReturnValues = returnOldItem ? AwsEnums.ReturnValue.ALL_OLD : AwsEnums.ReturnValue.NONE,
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(condition, values);
			return request;
		}
		#endregion
	}
}
