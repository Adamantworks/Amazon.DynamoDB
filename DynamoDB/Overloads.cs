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

using Adamantworks.Amazon.DynamoDB.Contexts;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Adamantworks.Amazon.DynamoDB
{
	public partial interface IDynamoDBRegion
	{
		IAsyncEnumerable<string> ListTablesAsync();

		Task<ITable> CreateTableAsync(string tableName, TableSchema schema);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, CancellationToken cancellationToken);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken);
		ITable CreateTable(string tableName, TableSchema schema);
		ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput);
		ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
	}

	internal partial class Region
	{
		#region ListTablesAsync
		public IAsyncEnumerable<string> ListTablesAsync()
		{
			return ListTablesAsync(ReadAhead.Some);
		}
		#endregion

		#region CreateTableAsync
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema)
		{
			return CreateTableAsync(tableName, schema, null, null, CancellationToken.None);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput)
		{
			return CreateTableAsync(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, null, CancellationToken.None);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			return CreateTableAsync(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs, CancellationToken.None);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, CancellationToken cancellationToken)
		{
			return CreateTableAsync(tableName, schema, null, null, cancellationToken);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken)
		{
			return CreateTableAsync(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, null, cancellationToken);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			return CreateTableAsync(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs, cancellationToken);
		}
		#endregion

		#region CreateTable
		public ITable CreateTable(string tableName, TableSchema schema)
		{
			return CreateTable(tableName, schema, null, null);
		}
		public ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput)
		{
			return CreateTable(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, null);
		}
		public ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			return CreateTable(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs);
		}
		#endregion
	}

	public partial interface ITable
	{
		Task ReloadAsync();

		Task WaitUntilNotAsync(TableStatus status);
		Task WaitUntilNotAsync(TableStatus status, TimeSpan timeout);

		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput);
		Task UpdateTableAsync(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken);
		Task UpdateTableAsync(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken);
		void UpdateTable(ProvisionedThroughput provisionedThroughput);
		void UpdateTable(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		void UpdateTable(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);

		IReverseSyntax Query(DynamoDBKeyValue hashKey);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, bool consistent);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent);

		IScanLimitToSyntax Scan();
		IScanLimitToSyntax Scan(ProjectionExpression projection);
		IScanLimitToSyntax Scan(PredicateExpression filter);
		IScanLimitToSyntax Scan(ProjectionExpression projection, PredicateExpression filter);
		IScanLimitToSyntax Scan(PredicateExpression filter, Values values);
		IScanLimitToSyntax Scan(ProjectionExpression projection, PredicateExpression filter, Values values);
	}

	internal partial class Table
	{
		#region ReloadAsync
		public Task ReloadAsync()
		{
			return ReloadAsync(CancellationToken.None);
		}
		#endregion

		#region WaitUntilNotAsync
		public Task WaitUntilNotAsync(TableStatus status)
		{
			return WaitUntilNotAsync(status, CancellationToken.None);
		}
		public Task WaitUntilNotAsync(TableStatus status, TimeSpan timeout)
		{
			return WaitUntilNotAsync(status, timeout, CancellationToken.None);
		}
		#endregion

		#region UpdateTableAsync
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput)
		{
			return UpdateTableAsync((ProvisionedThroughput?)provisionedThroughput, null, CancellationToken.None);
		}
		public Task UpdateTableAsync(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			return UpdateTableAsync(null, indexProvisionedThroughputs, CancellationToken.None);
		}
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			return UpdateTableAsync((ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs, CancellationToken.None);
		}
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken)
		{
			return UpdateTableAsync((ProvisionedThroughput?)provisionedThroughput, null, cancellationToken);
		}
		public Task UpdateTableAsync(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			return UpdateTableAsync(null, indexProvisionedThroughputs, cancellationToken);
		}
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			return UpdateTableAsync((ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs, cancellationToken);
		}
		#endregion

		#region UpdateTable
		public void UpdateTable(ProvisionedThroughput provisionedThroughput)
		{
			UpdateTable((ProvisionedThroughput?)provisionedThroughput, null);
		}
		public void UpdateTable(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			UpdateTable(null, indexProvisionedThroughputs);
		}
		public void UpdateTable(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			UpdateTable((ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs);
		}
		#endregion

		#region GetAsync
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey)
		{
			return eventuallyConsistentContext.GetAsync(new ItemKey(hashKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(new ItemKey(hashKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentContext.GetAsync(new ItemKey(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(new ItemKey(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key)
		{
			return eventuallyConsistentContext.GetAsync(key, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(key, cancellationToken);
		}
		#endregion

		#region Get
		public DynamoDBMap Get(DynamoDBKeyValue hashKey)
		{
			return eventuallyConsistentContext.Get(new ItemKey(hashKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentContext.Get(new ItemKey(hashKey, rangeKey));
		}
		public DynamoDBMap Get(ItemKey key)
		{
			return eventuallyConsistentContext.Get(key);
		}
		#endregion

		#region BatchGetAsync
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys)
		{
			return eventuallyConsistentContext.BatchGetAsync(keys.ToAsyncEnumerable(), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return eventuallyConsistentContext.BatchGetAsync(keys.ToAsyncEnumerable(), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys)
		{
			return eventuallyConsistentContext.BatchGetAsync(keys, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return eventuallyConsistentContext.BatchGetAsync(keys, readAhead);
		}
		#endregion

		#region BatchGet
		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys)
		{
			return eventuallyConsistentContext.BatchGet(keys);
		}
		#endregion

		#region BatchGetJoinAsync<T, TResult>
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return eventuallyConsistentContext.BatchGetJoinAsync(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return eventuallyConsistentContext.BatchGetJoinAsync(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return eventuallyConsistentContext.BatchGetJoinAsync(outerItems, keySelector, resultSelector, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return eventuallyConsistentContext.BatchGetJoinAsync(outerItems, keySelector, resultSelector, readAhead);
		}
		#endregion

		#region BatchGetJoin<T, TResult>
		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return eventuallyConsistentContext.BatchGetJoin(outerItems, keySelector, resultSelector);
		}
		#endregion

		#region PutAsync
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item)
		{
			return putContext.PutAsync(item, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem)
		{
			return putContext.PutAsync(item, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			return putContext.PutAsync(item, false, cancellationToken);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem, CancellationToken cancellationToken)
		{
			return putContext.PutAsync(item, returnOldItem, cancellationToken);
		}
		#endregion

		#region Put
		public DynamoDBMap Put(DynamoDBMap item)
		{
			return putContext.Put(item, false);
		}
		public DynamoDBMap Put(DynamoDBMap item, bool returnOldItem)
		{
			return putContext.Put(item, returnOldItem);
		}
		#endregion

		#region Query
		public IReverseSyntax Query(DynamoDBKeyValue hashKey)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, null, null, false);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, null, null, false);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, null, false);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, null, false);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, values, false);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, values, false);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, null, null, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, null, null, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, null, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, null, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, values, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, values, consistent);
		}
		#endregion

		#region Scan
		public IScanLimitToSyntax Scan()
		{
			return new ScanContext(Region, Name, null, null, null);
		}
		public IScanLimitToSyntax Scan(ProjectionExpression projection)
		{
			return new ScanContext(Region, Name, projection, null, null);
		}
		public IScanLimitToSyntax Scan(PredicateExpression filter)
		{
			return new ScanContext(Region, Name, null, filter, null);
		}
		public IScanLimitToSyntax Scan(ProjectionExpression projection, PredicateExpression filter)
		{
			return new ScanContext(Region, Name, projection, filter, null);
		}
		public IScanLimitToSyntax Scan(PredicateExpression filter, Values values)
		{
			return new ScanContext(Region, Name, null, filter, values);
		}
		public IScanLimitToSyntax Scan(ProjectionExpression projection, PredicateExpression filter, Values values)
		{
			return new ScanContext(Region, Name, projection, filter, values);
		}
		#endregion
	}
}

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public partial interface IGetSyntax
	{
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(ItemKey key);
		DynamoDBMap Get(DynamoDBKeyValue hashKey);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);

		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys);

		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector);
	}

	public partial interface IModifySyntax
	{
		Task<DynamoDBMap> UpdateAsync(UpdateExpression update);
		Task<DynamoDBMap> UpdateAsync(UpdateExpression update, Values values);
		Task<DynamoDBMap> UpdateAsync(UpdateExpression update, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(UpdateExpression update, Values values, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(UpdateExpression update, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(UpdateExpression update, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		DynamoDBMap Update(UpdateExpression update);
		DynamoDBMap Update(UpdateExpression update, Values values);
		DynamoDBMap Update(UpdateExpression update, UpdateReturnValue returnValue);

		Task<bool> TryUpdateAsync(UpdateExpression update);
		Task<bool> TryUpdateAsync(UpdateExpression update, Values values);
		Task<bool> TryUpdateAsync(UpdateExpression update, CancellationToken cancellationToken);
		bool TryUpdate(UpdateExpression update);

		Task<DynamoDBMap> DeleteAsync();
		Task<DynamoDBMap> DeleteAsync(bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(CancellationToken cancellationToken);
		DynamoDBMap Delete();
	}

	public partial interface IPutSyntax
	{
		Task<DynamoDBMap> PutAsync(DynamoDBMap item);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken);
		DynamoDBMap Put(DynamoDBMap item);
	}
}

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class GetContext
	{
		#region GetAsync
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey)
		{
			return GetAsync(new ItemKey(hashKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key)
		{
			return GetAsync(key, CancellationToken.None);
		}
		#endregion

		#region Get
		public DynamoDBMap Get(DynamoDBKeyValue hashKey)
		{
			return Get(new ItemKey(hashKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return Get(new ItemKey(hashKey, rangeKey));
		}
		#endregion

		#region BatchGetAsync
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys)
		{
			return BatchGetAsync(keys, ReadAhead.Some);
		}
		#endregion

		#region BatchGet
		#endregion

		#region BatchGetJoinAsync<T, TResult>
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems, keySelector, resultSelector, ReadAhead.Some);
		}
		#endregion

		#region BatchGetJoin<T, TResult>
		#endregion
	}

	internal partial class ModifyContext
	{
		#region UpdateAsync
		public Task<DynamoDBMap> UpdateAsync(UpdateExpression update)
		{
			return UpdateAsync(update, null, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(UpdateExpression update, Values values)
		{
			return UpdateAsync(update, values, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(UpdateExpression update, UpdateReturnValue returnValue)
		{
			return UpdateAsync(update, null, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return UpdateAsync(update, values, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(UpdateExpression update, CancellationToken cancellationToken)
		{
			return UpdateAsync(update, null, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(update, values, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(update, null, returnValue, cancellationToken);
		}
		#endregion

		#region Update
		public DynamoDBMap Update(UpdateExpression update)
		{
			return Update(update, null, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(UpdateExpression update, Values values)
		{
			return Update(update, values, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(UpdateExpression update, UpdateReturnValue returnValue)
		{
			return Update(update, null, returnValue);
		}
		#endregion

		#region TryUpdateAsync
		public Task<bool> TryUpdateAsync(UpdateExpression update)
		{
			return TryUpdateAsync(update, null, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(UpdateExpression update, Values values)
		{
			return TryUpdateAsync(update, values, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(UpdateExpression update, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(update, null, cancellationToken);
		}
		#endregion

		#region TryUpdate
		public bool TryUpdate(UpdateExpression update)
		{
			return TryUpdate(update, null);
		}
		#endregion

		#region DeleteAsync
		public Task<DynamoDBMap> DeleteAsync()
		{
			return DeleteAsync(false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(bool returnOldItem)
		{
			return DeleteAsync(returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(CancellationToken cancellationToken)
		{
			return DeleteAsync(false, cancellationToken);
		}
		#endregion

		#region Delete
		public DynamoDBMap Delete()
		{
			return Delete(false);
		}
		#endregion
	}

	internal partial class PutContext
	{
		#region PutAsync
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item)
		{
			return PutAsync(item, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem)
		{
			return PutAsync(item, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			return PutAsync(item, false, cancellationToken);
		}
		#endregion

		#region Put
		public DynamoDBMap Put(DynamoDBMap item)
		{
			return Put(item, false);
		}
		#endregion
	}
}
