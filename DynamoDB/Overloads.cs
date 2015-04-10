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
using Adamantworks.Amazon.DynamoDB.Converters;
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

		IAsyncEnumerable<string> ListTablesWithPrefixAsync(string tableNameBegins);

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

		#region ListTablesWithPrefixAsync
		public IAsyncEnumerable<string> ListTablesWithPrefixAsync(string tableNameBegins)
		{
			return ListTablesWithPrefixAsync(tableNameBegins, ReadAhead.Some);
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

		Task WaitUntilNotAsync(CollectionStatus status);
		Task WaitUntilNotAsync(CollectionStatus status, TimeSpan timeout);

		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput);
		Task UpdateTableAsync(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken);
		Task UpdateTableAsync(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken);
		void UpdateTable(ProvisionedThroughput provisionedThroughput);
		void UpdateTable(IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		void UpdateTable(ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);

		IIfSyntax ForKey(DynamoDBKeyValue hashKey);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, IValueConverter converter);
		IIfSyntax ForKey(object hashKey);
		IIfSyntax ForKey(object hashKey, IValueConverter converter);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		IIfSyntax ForKey(object hashKey, DynamoDBKeyValue rangeKey);
		IIfSyntax ForKey(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, object rangeKey);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter);
		IIfSyntax ForKey(object hashKey, object rangeKey);
		IIfSyntax ForKey(object hashKey, object rangeKey, IValueConverter converter);
		IIfSyntax ForKey(ItemKey key);

		void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey);
		void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, IValueConverter converter);
		void Delete(IBatchWriteAsync batch, object hashKey);
		void Delete(IBatchWriteAsync batch, object hashKey, IValueConverter converter);
		void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		void Delete(IBatchWriteAsync batch, object hashKey, DynamoDBKeyValue rangeKey);
		void Delete(IBatchWriteAsync batch, object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, object rangeKey);
		void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter);
		void Delete(IBatchWriteAsync batch, object hashKey, object rangeKey);
		void Delete(IBatchWriteAsync batch, object hashKey, object rangeKey, IValueConverter converter);
		void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey);
		void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, IValueConverter converter);
		void Delete(IBatchWrite batch, object hashKey);
		void Delete(IBatchWrite batch, object hashKey, IValueConverter converter);
		void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		void Delete(IBatchWrite batch, object hashKey, DynamoDBKeyValue rangeKey);
		void Delete(IBatchWrite batch, object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, object rangeKey);
		void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter);
		void Delete(IBatchWrite batch, object hashKey, object rangeKey);
		void Delete(IBatchWrite batch, object hashKey, object rangeKey, IValueConverter converter);
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
		public Task WaitUntilNotAsync(CollectionStatus status)
		{
			return WaitUntilNotAsync(status, CancellationToken.None);
		}
		public Task WaitUntilNotAsync(CollectionStatus status, TimeSpan timeout)
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
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key)
		{
			return eventuallyConsistentContext.GetAsync(key, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken)
		{
			return eventuallyConsistentContext.GetAsync(key, cancellationToken);
		}
		#endregion

		#region Get
		public DynamoDBMap Get(DynamoDBKeyValue hashKey)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, converter));
		}
		public DynamoDBMap Get(object hashKey)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey));
		}
		public DynamoDBMap Get(object hashKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, converter));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(object hashKey, object rangeKey)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(object hashKey, object rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentContext.Get(ItemKey.Create(hashKey, rangeKey, converter));
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

		#region ForKey
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey));
		}
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, converter));
		}
		public IIfSyntax ForKey(object hashKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey));
		}
		public IIfSyntax ForKey(object hashKey, IValueConverter converter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, converter));
		}
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey));
		}
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public IIfSyntax ForKey(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey));
		}
		public IIfSyntax ForKey(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey));
		}
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public IIfSyntax ForKey(object hashKey, object rangeKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey));
		}
		public IIfSyntax ForKey(object hashKey, object rangeKey, IValueConverter converter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public IIfSyntax ForKey(ItemKey key)
		{
			return new ModifyContext(this, key);
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
			return new QueryContext(this, null, null, false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryContext(this, null, null, false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(this, null, null, false, hashKey, filter, values);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new QueryContext(this, null, null, false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(this, null, null, false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(this, null, null, false, hashKey, filter, values);
		}
		public IReverseSyntax Query(object hashKey)
		{
			return new QueryContext(this, null, null, false, DynamoDBKeyValue.Convert(hashKey), null, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter)
		{
			return new QueryContext(this, null, null, false, DynamoDBKeyValue.Convert(hashKey), filter, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(this, null, null, false, DynamoDBKeyValue.Convert(hashKey), filter, values);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter)
		{
			return new QueryContext(this, null, null, false, DynamoDBKeyValue.Convert(hashKey, converter), null, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(this, null, null, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(this, null, null, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, values);
		}
		#endregion

		#region Scan
		public IScanLimitToOrPagedSyntax Scan()
		{
			return new ScanContext(this, null, null, null, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter)
		{
			return new ScanContext(this, null, null, filter, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter, Values values)
		{
			return new ScanContext(this, null, null, filter, values);
		}
		#endregion

		#region Delete
		public void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey)
		{
			Delete(batch, ItemKey.Create(hashKey));
		}
		public void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, converter));
		}
		public void Delete(IBatchWriteAsync batch, object hashKey)
		{
			Delete(batch, ItemKey.Create(hashKey));
		}
		public void Delete(IBatchWriteAsync batch, object hashKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, converter));
		}
		public void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey));
		}
		public void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public void Delete(IBatchWriteAsync batch, object hashKey, DynamoDBKeyValue rangeKey)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey));
		}
		public void Delete(IBatchWriteAsync batch, object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, object rangeKey)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey));
		}
		public void Delete(IBatchWriteAsync batch, DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public void Delete(IBatchWriteAsync batch, object hashKey, object rangeKey)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey));
		}
		public void Delete(IBatchWriteAsync batch, object hashKey, object rangeKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion

		#region Delete
		public void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey)
		{
			Delete(batch, ItemKey.Create(hashKey));
		}
		public void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, converter));
		}
		public void Delete(IBatchWrite batch, object hashKey)
		{
			Delete(batch, ItemKey.Create(hashKey));
		}
		public void Delete(IBatchWrite batch, object hashKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, converter));
		}
		public void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey));
		}
		public void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public void Delete(IBatchWrite batch, object hashKey, DynamoDBKeyValue rangeKey)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey));
		}
		public void Delete(IBatchWrite batch, object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, object rangeKey)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey));
		}
		public void Delete(IBatchWrite batch, DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey, converter));
		}
		public void Delete(IBatchWrite batch, object hashKey, object rangeKey)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey));
		}
		public void Delete(IBatchWrite batch, object hashKey, object rangeKey, IValueConverter converter)
		{
			Delete(batch, ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion
	}

	internal partial class Index
	{
		#region Query
		public IReverseSyntax Query(DynamoDBKeyValue hashKey)
		{
			return new QueryContext(Table, this, null, false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryContext(Table, this, null, false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(Table, this, null, false, hashKey, filter, values);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new QueryContext(Table, this, null, false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(Table, this, null, false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(Table, this, null, false, hashKey, filter, values);
		}
		public IReverseSyntax Query(object hashKey)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey), null, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey), filter, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey), filter, values);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), null, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, values);
		}
		#endregion

		#region Scan
		public IScanLimitToOrPagedSyntax Scan()
		{
			return new ScanContext(Table, this, null, null, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter)
		{
			return new ScanContext(Table, this, null, filter, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter, Values values)
		{
			return new ScanContext(Table, this, null, filter, values);
		}
		#endregion
	}
}

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public partial interface IGetSyntax
	{
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, IValueConverter converter);
		Task<DynamoDBMap> GetAsync(object hashKey);
		Task<DynamoDBMap> GetAsync(object hashKey, IValueConverter converter);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey);
		Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter);
		Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey);
		Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, IValueConverter converter);
		Task<DynamoDBMap> GetAsync(ItemKey key);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(object hashKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(object hashKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		DynamoDBMap Get(DynamoDBKeyValue hashKey);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, IValueConverter converter);
		DynamoDBMap Get(object hashKey);
		DynamoDBMap Get(object hashKey, IValueConverter converter);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		DynamoDBMap Get(object hashKey, DynamoDBKeyValue rangeKey);
		DynamoDBMap Get(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, object rangeKey);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter);
		DynamoDBMap Get(object hashKey, object rangeKey);
		DynamoDBMap Get(object hashKey, object rangeKey, IValueConverter converter);

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

		Task<DynamoDBMap> DeleteAsync();
		Task<DynamoDBMap> DeleteAsync(bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(CancellationToken cancellationToken);
		DynamoDBMap Delete();
	}

	public partial interface IPagedQueryRangeSyntax
	{
		Task<ItemPage> AllKeysAsync();

		Task<ItemPage> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyBeginsWithAsync(object rangeKey);
		Task<ItemPage> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBeginsWithAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyBeginsWith(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyBeginsWith(object rangeKey);
		ItemPage RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyEqualsAsync(object rangeKey);
		Task<ItemPage> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyEqualsAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyEquals(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyEquals(object rangeKey);
		ItemPage RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyLessThanAsync(object rangeKey);
		Task<ItemPage> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyLessThanAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyLessThan(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyLessThan(object rangeKey);
		ItemPage RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(object rangeKey);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyLessThanOrEqualTo(object rangeKey);
		ItemPage RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyGreaterThanAsync(object rangeKey);
		Task<ItemPage> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyGreaterThanAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyGreaterThan(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyGreaterThan(object rangeKey);
		ItemPage RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(object rangeKey);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyGreaterThanOrEqualTo(object rangeKey);
		ItemPage RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, object endExclusive);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, object endExclusive, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter);
		ItemPage RangeKeyBetween(object startInclusive, object endExclusive);
		ItemPage RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		ItemPage RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive);
		ItemPage RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		ItemPage RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive);
		ItemPage RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
	}

	public partial interface IPagedScanOptionsSyntax
	{
		Task<ItemPage> AllAsync();

		Task<ItemPage> ParallelAsync(int segment, int totalSegments);
	}

	public partial interface IPutSyntax
	{
		Task<DynamoDBMap> PutAsync(DynamoDBMap item);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken);
		DynamoDBMap Put(DynamoDBMap item);
	}

	public partial interface IQueryRangeSyntax
	{
		IAsyncEnumerable<DynamoDBMap> AllKeysAsync();

		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyBeginsWith(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyBeginsWith(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyEquals(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyEquals(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyLessThan(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyLessThan(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThan(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThan(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, object endExclusive);
		IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive);
		IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive);
		IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
	}

	public partial interface IQuerySyntax
	{
		IReverseSyntax Query(DynamoDBKeyValue hashKey);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values);
		IReverseSyntax Query(object hashKey);
		IReverseSyntax Query(object hashKey, PredicateExpression filter);
		IReverseSyntax Query(object hashKey, PredicateExpression filter, Values values);
		IReverseSyntax Query(object hashKey, IValueConverter converter);
		IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter);
		IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter, Values values);
	}

	public partial interface IScanOptionsSyntax
	{
		IAsyncEnumerable<DynamoDBMap> AllAsync();

		IAsyncEnumerable<DynamoDBMap> ParallelAsync(int segment, int totalSegments);
	}

	public partial interface IScanSyntax
	{
		IScanLimitToOrPagedSyntax Scan();
		IScanLimitToOrPagedSyntax Scan(PredicateExpression filter);
		IScanLimitToOrPagedSyntax Scan(PredicateExpression filter, Values values);
	}

	public partial interface ITryModifySyntax
	{
		Task<bool> TryUpdateAsync(UpdateExpression update);
		Task<bool> TryUpdateAsync(UpdateExpression update, Values values);
		Task<bool> TryUpdateAsync(UpdateExpression update, CancellationToken cancellationToken);
		bool TryUpdate(UpdateExpression update);
	}
}

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class GetContext
	{
		#region GetAsync
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey)
		{
			return GetAsync(ItemKey.Create(hashKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return GetAsync(ItemKey.Create(hashKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey)
		{
			return GetAsync(ItemKey.Create(hashKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, IValueConverter converter)
		{
			return GetAsync(ItemKey.Create(hashKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, IValueConverter converter)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key)
		{
			return GetAsync(key, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		#endregion

		#region Get
		public DynamoDBMap Get(DynamoDBKeyValue hashKey)
		{
			return Get(ItemKey.Create(hashKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return Get(ItemKey.Create(hashKey, converter));
		}
		public DynamoDBMap Get(object hashKey)
		{
			return Get(ItemKey.Create(hashKey));
		}
		public DynamoDBMap Get(object hashKey, IValueConverter converter)
		{
			return Get(ItemKey.Create(hashKey, converter));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(object hashKey, object rangeKey)
		{
			return Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(object hashKey, object rangeKey, IValueConverter converter)
		{
			return Get(ItemKey.Create(hashKey, rangeKey, converter));
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

		#region Query
		public IReverseSyntax Query(DynamoDBKeyValue hashKey)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, hashKey, filter, values);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, hashKey, filter, values);
		}
		public IReverseSyntax Query(object hashKey)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), null, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), filter, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), filter, values);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), null, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), filter, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(table, null, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), filter, values);
		}
		#endregion

		#region Scan
		public IScanLimitToOrPagedSyntax Scan()
		{
			return new ScanContext(table, null, projection, null, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter)
		{
			return new ScanContext(table, null, projection, filter, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter, Values values)
		{
			return new ScanContext(table, null, projection, filter, values);
		}
		#endregion
	}

	internal partial class IndexContext
	{
		#region Query
		public IReverseSyntax Query(DynamoDBKeyValue hashKey)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, hashKey, filter, values);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, hashKey, filter, values);
		}
		public IReverseSyntax Query(object hashKey)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), null, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), filter, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), filter, values);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), null, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), filter, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(index.Table, index, projection, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), filter, values);
		}
		#endregion

		#region Scan
		public IScanLimitToOrPagedSyntax Scan()
		{
			return new ScanContext(index.Table, index, projection, null, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter)
		{
			return new ScanContext(index.Table, index, projection, filter, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter, Values values)
		{
			return new ScanContext(index.Table, index, projection, filter, values);
		}
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

	internal partial class QueryContext
	{
		#region AllKeysAsync
		public IAsyncEnumerable<DynamoDBMap> AllKeysAsync()
		{
			return AllKeysAsync(ReadAhead.Some);
		}
		#endregion

		#region RangeKeyBeginsWithAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWithAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyBeginsWithAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBeginsWithAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyBeginsWith
		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(object rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(object rangeKey)
		{
			return RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWith(rangeKey);
		}
		#endregion

		#region RangeKeyEqualsAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyEqualsAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyEqualsAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyEqualsAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyEquals
		public IEnumerable<DynamoDBMap> RangeKeyEquals(object rangeKey, IValueConverter converter)
		{
			return RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyEquals(object rangeKey)
		{
			return RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyEquals(rangeKey);
		}
		#endregion

		#region RangeKeyLessThanAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyLessThanAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyLessThanAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyLessThan
		public IEnumerable<DynamoDBMap> RangeKeyLessThan(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyLessThan(object rangeKey)
		{
			return RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThan(rangeKey);
		}
		#endregion

		#region RangeKeyLessThanOrEqualToAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyLessThanOrEqualTo
		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(object rangeKey)
		{
			return RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualTo(rangeKey);
		}
		#endregion

		#region RangeKeyGreaterThanAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyGreaterThanAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyGreaterThan
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(object rangeKey)
		{
			return RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThan(rangeKey);
		}
		#endregion

		#region RangeKeyGreaterThanOrEqualToAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyGreaterThanOrEqualTo
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(object rangeKey)
		{
			return RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualTo(rangeKey);
		}
		#endregion

		#region RangeKeyBetweenAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, readAhead);
		}
		#endregion

		#region RangeKeyBetween
		public IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, object endExclusive)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive);
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), endExclusive);
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(startInclusive, endExclusive);
		}
		#endregion

		#region IPagedQueryRangeSyntax.AllKeysAsync
		Task<ItemPage> IPagedQueryRangeSyntax.AllKeysAsync()
		{
			return ((IPagedQueryRangeSyntax)this).AllKeysAsync(CancellationToken.None);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyBeginsWith
		ItemPage IPagedQueryRangeSyntax.RangeKeyBeginsWith(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBeginsWith(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWith(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyEqualsAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyEquals
		ItemPage IPagedQueryRangeSyntax.RangeKeyEquals(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyEquals(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEquals(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyLessThanAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyLessThan
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThan(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThan(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThan(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualTo(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyGreaterThan
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThan(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThan(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThan(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualTo(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyBetweenAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, object endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, endExclusive, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, endExclusive, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, object endExclusive, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, endExclusive, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyBetween
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(object startInclusive, object endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), endExclusive);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(startInclusive, endExclusive);
		}
		#endregion
	}

	internal partial class ScanContext
	{
		#region AllAsync
		public IAsyncEnumerable<DynamoDBMap> AllAsync()
		{
			return AllAsync(ReadAhead.Some);
		}
		#endregion

		#region IPagedScanOptionsSyntax.AllAsync
		Task<ItemPage> IPagedScanOptionsSyntax.AllAsync()
		{
			return ((IPagedScanOptionsSyntax)this).AllAsync(CancellationToken.None);
		}
		#endregion

		#region ParallelAsync
		public IAsyncEnumerable<DynamoDBMap> ParallelAsync(int segment, int totalSegments)
		{
			return ParallelAsync(segment, totalSegments, ReadAhead.Some);
		}
		#endregion

		#region IPagedScanOptionsSyntax.ParallelAsync
		Task<ItemPage> IPagedScanOptionsSyntax.ParallelAsync(int segment, int totalSegments)
		{
			return ((IPagedScanOptionsSyntax)this).ParallelAsync(segment, totalSegments, CancellationToken.None);
		}
		#endregion
	}
}
