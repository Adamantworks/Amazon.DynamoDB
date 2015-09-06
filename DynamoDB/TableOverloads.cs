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
using Adamantworks.Amazon.DynamoDB.Contexts;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Syntax;

namespace Adamantworks.Amazon.DynamoDB
{
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
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key)
		{
			return eventuallyConsistentReadContext.GetAsync(key, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(object hashKey, object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(ItemKey.Create(hashKey, rangeKey, converter), cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken)
		{
			return eventuallyConsistentReadContext.GetAsync(key, cancellationToken);
		}
		#endregion

		#region Get
		public DynamoDBMap Get(DynamoDBKeyValue hashKey)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, converter));
		}
		public DynamoDBMap Get(object hashKey)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey));
		}
		public DynamoDBMap Get(object hashKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, converter));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(object hashKey, object rangeKey)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, rangeKey));
		}
		public DynamoDBMap Get(object hashKey, object rangeKey, IValueConverter converter)
		{
			return eventuallyConsistentReadContext.Get(ItemKey.Create(hashKey, rangeKey, converter));
		}
		public DynamoDBMap Get(ItemKey key)
		{
			return eventuallyConsistentReadContext.Get(key);
		}
		#endregion

		#region BatchGetAsync
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys)
		{
			return eventuallyConsistentReadContext.BatchGetAsync(keys.ToAsyncEnumerable(), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return eventuallyConsistentReadContext.BatchGetAsync(keys.ToAsyncEnumerable(), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys)
		{
			return eventuallyConsistentReadContext.BatchGetAsync(keys, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return eventuallyConsistentReadContext.BatchGetAsync(keys, readAhead);
		}
		#endregion

		#region BatchGet
		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys)
		{
			return eventuallyConsistentReadContext.BatchGet(keys);
		}
		#endregion

		#region BatchGetJoinAsync<T, TResult>
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return eventuallyConsistentReadContext.BatchGetJoinAsync(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return eventuallyConsistentReadContext.BatchGetJoinAsync(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return eventuallyConsistentReadContext.BatchGetJoinAsync(outerItems, keySelector, resultSelector, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return eventuallyConsistentReadContext.BatchGetJoinAsync(outerItems, keySelector, resultSelector, readAhead);
		}
		#endregion

		#region BatchGetJoin<T, TResult>
		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return eventuallyConsistentReadContext.BatchGetJoin(outerItems, keySelector, resultSelector);
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
			return writeContext.PutAsync(item, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem)
		{
			return writeContext.PutAsync(item, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			return writeContext.PutAsync(item, false, cancellationToken);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem, CancellationToken cancellationToken)
		{
			return writeContext.PutAsync(item, returnOldItem, cancellationToken);
		}
		#endregion

		#region Put
		public DynamoDBMap Put(DynamoDBMap item)
		{
			return writeContext.Put(item, false);
		}
		public DynamoDBMap Put(DynamoDBMap item, bool returnOldItem)
		{
			return writeContext.Put(item, returnOldItem);
		}
		#endregion

		#region Update
		public IForKeySyntax Update(UpdateExpression update)
		{
			return new UpdateContext(this, update, null, UpdateReturnValue.None);
		}
		public IForKeySyntax Update(UpdateExpression update, Values values)
		{
			return new UpdateContext(this, update, values, UpdateReturnValue.None);
		}
		public IForKeySyntax Update(UpdateExpression update, UpdateReturnValue returnValue)
		{
			return new UpdateContext(this, update, null, returnValue);
		}
		public IForKeySyntax Update(UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return new UpdateContext(this, update, values, returnValue);
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

		#region QueryCount
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey)
		{
			return new QueryCountContext(this, null, false, hashKey, null, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryCountContext(this, null, false, hashKey, filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(this, null, false, hashKey, filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new QueryCountContext(this, null, false, hashKey, null, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryCountContext(this, null, false, hashKey, filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(this, null, false, hashKey, filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey)
		{
			return new QueryCountContext(this, null, false, DynamoDBKeyValue.Convert(hashKey), null, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, PredicateExpression filter)
		{
			return new QueryCountContext(this, null, false, DynamoDBKeyValue.Convert(hashKey), filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(this, null, false, DynamoDBKeyValue.Convert(hashKey), filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter)
		{
			return new QueryCountContext(this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), null, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryCountContext(this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, values);
		}
		#endregion

		#region Scan
		public IScanLimitToOrPagedSyntax Scan()
		{
			return new ScanContext(this, null, null, false, null, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter)
		{
			return new ScanContext(this, null, null, false, filter, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter, Values values)
		{
			return new ScanContext(this, null, null, false, filter, values);
		}
		#endregion

		#region ScanCount
		public IScanCountOptionsSyntax ScanCount()
		{
			return new ScanCountContext(this, null, false, null, null);
		}
		public IScanCountOptionsSyntax ScanCount(PredicateExpression filter)
		{
			return new ScanCountContext(this, null, false, filter, null);
		}
		public IScanCountOptionsSyntax ScanCount(PredicateExpression filter, Values values)
		{
			return new ScanCountContext(this, null, false, filter, values);
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
}