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

namespace Adamantworks.Amazon.DynamoDB
{
	public interface ITable
	{
		string Name { get; }
		TableSchema Schema { get; }
		IReadOnlyDictionary<string, IIndex> Indexes { get; }
		DateTime CreationDateTime { get; }
		long ItemCount { get; }
		long SizeInBytes { get; }
		TableStatus Status { get; }
		IProvisionedThroughputInfo ProvisionedThroughput { get; }

		// In the following interface API, the only one argument per method group should have a default value. The priority of those arguments is:
		//     cancellationToken for async methods (not returning IAsyncEnumerable)
		//     readAhead for methods returning IAsyncEnumerable
		//     consistent (either non-async or not returning IAsyncEnumerable)
		// That will ensure a consistent set of overloads and prevent users from needing to specify parameter names

		Task ReloadAsync(CancellationToken cancellationToken = default(CancellationToken));
		void Reload();

		Task WaitUntilNotAsync(TableStatus status, CancellationToken cancellationToken = default(CancellationToken));
		Task WaitUntilNotAsync(TableStatus status, TimeSpan timeout, CancellationToken cancellationToken = default(CancellationToken));
		void WaitUntilNot(TableStatus status);
		void WaitUntilNot(TableStatus status, TimeSpan timeout);

		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken = default(CancellationToken));
		Task UpdateTableAsync(Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken = default(CancellationToken));
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken = default(CancellationToken));
		void UpdateTable(ProvisionedThroughput provisionedThroughput);
		void UpdateTable(Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		void UpdateTable(ProvisionedThroughput provisionedThroughput, Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);

		ItemKey GetKey(DynamoDBMap item);

		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, bool consistent, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(ItemKey key, bool consistent, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken = default(CancellationToken));
		DynamoDBMap Get(DynamoDBKeyValue hashKey, bool consistent = false);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent = false);
		DynamoDBMap Get(ItemKey key, bool consistent = false);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent = false);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent = false);
		DynamoDBMap Get(ItemKey key, ProjectionExpression projection, bool consistent = false);

		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, bool consistent = false);
		IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent = false);

		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, ReadAhead readAhead = ReadAhead.Some);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent = false);
		IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent = false);

		// TODO:Task<Item> GetAsync(IBatchGetAsync batch);
		// TODO:Task PutAsync(Item item);
		// TODO:Task PutAsync(IBatchWriteAsync batch, Item item);
		// TODO:Task InsertAsync() // does a put with a condition that the row doesn't exist

		Task UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task UpdateAsync(ItemKey key, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		void Update(DynamoDBKeyValue hashKey, UpdateExpression update, Values values = null);
		void Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values = null);
		void Update(ItemKey key, UpdateExpression update, Values values = null);
		void Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values = null);
		void Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values = null);
		void Update(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values = null);

		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, Values values = null);
		bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values = null);
		bool TryUpdate(ItemKey key, UpdateExpression update, Values values = null);
		bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values = null);
		bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values = null);
		bool TryUpdate(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values = null);

		// TODO:Task DeleteAsync();
		// TODO:Task DeleteAsync(IBatchWriteAsync batch);

		IQueryContext Query(DynamoDBKeyValue hashKey, bool consistent = false);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent = false);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent = false);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent = false);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent = false);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent = false);

		IScanContext Scan();
		IScanContext Scan(PredicateExpression filter);
		IScanContext Scan(PredicateExpression filter, Values values);
		IScanContext Scan(ProjectionExpression projection);
		IScanContext Scan(ProjectionExpression projection, PredicateExpression filter);
		IScanContext Scan(ProjectionExpression projection, PredicateExpression filter, Values values);

		// TODO: Count()
		// TODO: Task GetStatus(); // TODO maybe this is part of some larger op
		// TODO: Task UpdateProvisionedThroughput();

		// TODO: bool ReportConsumedCapacity
		// TODO: CapacityConsumedEvent
	}

	internal class Table : ITable
	{
		private const int BatchGetBatchSizeLimit = 100;
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
		public async Task ReloadAsync(CancellationToken cancellationToken = new CancellationToken())
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
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken)
		{
			var request = BuildUpdateTableRequest(provisionedThroughput, null);
			return Region.DB.UpdateTableAsync(request, cancellationToken);
		}
		public Task UpdateTableAsync(Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			var request = BuildUpdateTableRequest(null, indexProvisionedThroughputs);
			return Region.DB.UpdateTableAsync(request, cancellationToken);
		}
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			var request = BuildUpdateTableRequest(provisionedThroughput, indexProvisionedThroughputs);
			return Region.DB.UpdateTableAsync(request, cancellationToken);
		}

		public void UpdateTable(ProvisionedThroughput provisionedThroughput)
		{
			var request = BuildUpdateTableRequest(provisionedThroughput, null);
			Region.DB.UpdateTable(request);
		}
		public void UpdateTable(Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = BuildUpdateTableRequest(null, indexProvisionedThroughputs);
			Region.DB.UpdateTable(request);
		}
		public void UpdateTable(ProvisionedThroughput provisionedThroughput, Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = BuildUpdateTableRequest(provisionedThroughput, indexProvisionedThroughputs);
			Region.DB.UpdateTable(request);
		}

		private Aws.UpdateTableRequest BuildUpdateTableRequest(ProvisionedThroughput? provisionedThroughput, Dictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
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
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), null, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), null, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), null, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), null, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken)
		{
			return GetAsync(key, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(key, null, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), projection, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), projection, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), projection, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), projection, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, CancellationToken cancellationToken)
		{
			return GetAsync(key, projection, false, cancellationToken);
		}
		public async Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			var request = BuildGetRequest(key, projection, consistent);
			var result = await Region.DB.GetItemAsync(request, cancellationToken).ConfigureAwait(false);
			return result.Item.ToGetValue();
		}

		public DynamoDBMap Get(DynamoDBKeyValue hashKey, bool consistent)
		{
			return Get(new ItemKey(hashKey), null, consistent);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent)
		{
			return Get(new ItemKey(hashKey, rangeKey), null, consistent);
		}
		public DynamoDBMap Get(ItemKey key, bool consistent)
		{
			return Get(key, null, consistent);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent)
		{
			return Get(new ItemKey(hashKey), projection, consistent);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent)
		{
			return Get(new ItemKey(hashKey, rangeKey), projection, consistent);
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
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), null, false, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, bool consistent, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), null, consistent, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), projection, false, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), projection, consistent, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return BatchGetAsync(keys, null, false, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, bool consistent, ReadAhead readAhead)
		{
			return BatchGetAsync(keys, null, consistent, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, ReadAhead readAhead)
		{
			return BatchGetAsync(keys, projection, false, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			var awsKeys = keys.Select(k => k.ToAws(Schema.Key));

			return AsyncEnumerable.Using(awsKeys.GetEnumerator, enumerator =>
			{
				// These must be in the using so they are deferred until GetEnumerator() is called on us
				var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(BatchGetBatchSizeLimit);
				var request = BuildBatchGetItemRequest(batchKeys, projection, consistent);

				return AsyncEnumerableEx.GenerateChunked<Aws.BatchGetItemResponse, DynamoDBMap>(null,
					async (lastResponse, cancellationToken) =>
					{
						while(batchKeys.Count < BatchGetBatchSizeLimit && await enumerator.MoveNext().ConfigureAwait(false))
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

		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, bool consistent = false)
		{
			return BatchGet(keys, null, consistent);
		}
		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent = false)
		{
			var awsKeys = keys.Select(k => k.ToAws(Schema.Key));

			var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(BatchGetBatchSizeLimit);
			var request = BuildBatchGetItemRequest(batchKeys, projection, consistent);

			using(var enumerator = awsKeys.GetEnumerator())
			{
				for(; ; )
				{
					while(batchKeys.Count < BatchGetBatchSizeLimit && enumerator.MoveNext())
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
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return BatchGetJoinAsync(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, null, false, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent, ReadAhead readAhead)
		{
			return BatchGetJoinAsync(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, null, consistent, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, ReadAhead readAhead)
		{
			return BatchGetJoinAsync(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, projection, false, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			return BatchGetJoinAsync(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, projection, consistent, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return BatchGetJoinAsync(outerItems, keySelector, resultSelector, null, false, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent, ReadAhead readAhead)
		{
			return BatchGetJoinAsync(outerItems, keySelector, resultSelector, null, consistent, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, ReadAhead readAhead)
		{
			return BatchGetJoinAsync(outerItems, keySelector, resultSelector, projection, false, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			return AsyncEnumerable.Using(outerItems.GetEnumerator, enumerator =>
			{
				// These must be in the using so they are deferred until GetEnumerator() is called on us
				var innerItems = new Dictionary<ItemKey, DynamoDBMap>();
				var batchItems = new Dictionary<ItemKey, T>(BatchGetBatchSizeLimit);
				var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(BatchGetBatchSizeLimit);
				var request = BuildBatchGetItemRequest(batchKeys, projection, consistent);

				return AsyncEnumerableEx.GenerateChunked<Tuple<List<TResult>, bool>, TResult>(null,
					async (lastResults, cancellationToken) =>
					{
						var batchResults = new List<TResult>(BatchGetBatchSizeLimit);

						while(batchItems.Count < BatchGetBatchSizeLimit && await enumerator.MoveNext().ConfigureAwait(false))
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

		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent)
		{
			return BatchGetJoin(outerItems, keySelector, resultSelector, null, consistent);
		}
		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent)
		{
			var innerItems = new Dictionary<ItemKey, DynamoDBMap>();
			var batchItems = new Dictionary<ItemKey, T>(BatchGetBatchSizeLimit);
			var batchKeys = new List<Dictionary<string, Aws.AttributeValue>>(BatchGetBatchSizeLimit);
			var request = BuildBatchGetItemRequest(batchKeys, projection, consistent);

			using(var enumerator = outerItems.GetEnumerator())
			{
				for(; ; )
				{
					while(batchItems.Count < BatchGetBatchSizeLimit && enumerator.MoveNext())
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

		#region Update
		public Task UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, values, cancellationToken);
		}
		public Task UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, values, cancellationToken);
		}
		public Task UpdateAsync(ItemKey key, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(key, update, null, values, cancellationToken);
		}
		public Task UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, values, cancellationToken);
		}
		public Task UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, values, cancellationToken); ;
		}
		public async Task UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			var request = BuildUpdateRequest(key, update, condition, values);
			await Region.DB.UpdateItemAsync(request, cancellationToken).ConfigureAwait(false);
		}

		public void Update(DynamoDBKeyValue hashKey, UpdateExpression update, Values values)
		{
			Update(new ItemKey(hashKey), update, null, values);
		}
		public void Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values)
		{
			Update(new ItemKey(hashKey, rangeKey), update, null, values);
		}
		public void Update(ItemKey key, UpdateExpression update, Values values)
		{
			Update(key, update, null, values);
		}
		public void Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			Update(new ItemKey(hashKey), update, condition, values);
		}
		public void Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			Update(new ItemKey(hashKey, rangeKey), update, condition, values);
		}
		public void Update(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values)
		{
			var request = BuildUpdateRequest(key, update, condition, values);
			Region.DB.UpdateItem(request);
		}

		private Aws.UpdateItemRequest BuildUpdateRequest(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values)
		{
			var request = new Aws.UpdateItemRequest()
			{
				TableName = Name,
				Key = key.ToAws(Schema.Key),
				UpdateExpression = update.Expression,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(update, condition),
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(update, condition, values);

			return request;
		}
		#endregion

		#region TryUpdate
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, null, values, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, null, values, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(key, update, null, values, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, condition, values, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, values, cancellationToken);
		}
		public async Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			try
			{
				await UpdateAsync(key, update, condition, values, cancellationToken);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}

		public bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, Values values)
		{
			return TryUpdate(new ItemKey(hashKey), update, null, values);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values)
		{
			return TryUpdate(new ItemKey(hashKey, rangeKey), update, null, values);
		}
		public bool TryUpdate(ItemKey key, UpdateExpression update, Values values)
		{
			return TryUpdate(key, update, null, values);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return TryUpdate(new ItemKey(hashKey), update, condition, values);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return TryUpdate(new ItemKey(hashKey, rangeKey), update, condition, values);
		}
		public bool TryUpdate(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values)
		{
			try
			{
				Update(key, update, condition, values);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}
		#endregion

		#region Query
		public IQueryContext Query(DynamoDBKeyValue hashKey, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, null, null, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, null, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, values, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, null, null, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, null, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, values, consistent);
		}
		#endregion

		#region Scan
		public IScanContext Scan()
		{
			return new ScanContext(Region, Name, null, null, null);
		}
		public IScanContext Scan(PredicateExpression filter)
		{
			return new ScanContext(Region, Name, null, filter, null);
		}
		public IScanContext Scan(PredicateExpression filter, Values values)
		{
			return new ScanContext(Region, Name, null, filter, values);
		}
		public IScanContext Scan(ProjectionExpression projection)
		{
			return new ScanContext(Region, Name, projection, null, null);
		}
		public IScanContext Scan(ProjectionExpression projection, PredicateExpression filter)
		{
			return new ScanContext(Region, Name, projection, filter, null);
		}
		public IScanContext Scan(ProjectionExpression projection, PredicateExpression filter, Values values)
		{
			return new ScanContext(Region, Name, projection, filter, values);
		}
		#endregion
	}
}
