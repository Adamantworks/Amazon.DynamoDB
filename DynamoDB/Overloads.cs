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
using Adamantworks.Amazon.DynamoDB.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Adamantworks.Amazon.DynamoDB
{
	public partial interface ITable
	{
		Task ReloadAsync();

		Task WaitUntilNotAsync(TableStatus status);
		Task WaitUntilNotAsync(TableStatus status, TimeSpan timeout);

		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput);
		Task UpdateTableAsync(IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken);
		Task UpdateTableAsync(IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken);
		Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken);
		void UpdateTable(ProvisionedThroughput provisionedThroughput);
		void UpdateTable(IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		void UpdateTable(ProvisionedThroughput provisionedThroughput, IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);

		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, bool consistent);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, bool consistent, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(ItemKey key);
		Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection);
		Task<DynamoDBMap> GetAsync(ItemKey key, bool consistent);
		Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, bool consistent);
		Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, CancellationToken cancellationToken);
		Task<DynamoDBMap> GetAsync(ItemKey key, bool consistent, CancellationToken cancellationToken);
		DynamoDBMap Get(DynamoDBKeyValue hashKey);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, ProjectionExpression projection);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, bool consistent);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent);
		DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent);
		DynamoDBMap Get(ItemKey key);
		DynamoDBMap Get(ItemKey key, ProjectionExpression projection);
		DynamoDBMap Get(ItemKey key, bool consistent);

		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, bool consistent);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, bool consistent, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, bool consistent);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, bool consistent, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys);
		IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, ProjectionExpression projection);
		IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, bool consistent);

		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, ReadAhead readAhead);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent, ReadAhead readAhead);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent, ReadAhead readAhead);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, ReadAhead readAhead);
		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent, ReadAhead readAhead);
		IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector);
		IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection);
		IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent);

		Task<DynamoDBMap> PutAsync(DynamoDBMap item);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, Values values);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, bool returnOldItem);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, Values values, bool returnOldItem);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, CancellationToken cancellationToken);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem, CancellationToken cancellationToken);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, bool returnOldItem, CancellationToken cancellationToken);
		DynamoDBMap Put(DynamoDBMap item);
		DynamoDBMap Put(DynamoDBMap item, PredicateExpression condition);
		DynamoDBMap Put(DynamoDBMap item, PredicateExpression condition, Values values);
		DynamoDBMap Put(DynamoDBMap item, bool returnOldItem);
		DynamoDBMap Put(DynamoDBMap item, PredicateExpression condition, bool returnOldItem);

		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, Values values);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, Values values, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, Values values);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, UpdateReturnValue returnValue);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, UpdateReturnValue returnValue);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, UpdateReturnValue returnValue);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, UpdateReturnValue returnValue);
		DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue);
		DynamoDBMap Update(ItemKey key, UpdateExpression update);
		DynamoDBMap Update(ItemKey key, UpdateExpression update, PredicateExpression condition);
		DynamoDBMap Update(ItemKey key, UpdateExpression update, Values values);
		DynamoDBMap Update(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values);
		DynamoDBMap Update(ItemKey key, UpdateExpression update, UpdateReturnValue returnValue);
		DynamoDBMap Update(ItemKey key, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue);
		DynamoDBMap Update(ItemKey key, UpdateExpression update, Values values, UpdateReturnValue returnValue);

		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update);
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition);
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, Values values);
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values);
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken);
		Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, Values values, CancellationToken cancellationToken);
		bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update);
		bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition);
		bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, Values values);
		bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values);
		bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update);
		bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition);
		bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values);
		bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values);
		bool TryUpdate(ItemKey key, UpdateExpression update);
		bool TryUpdate(ItemKey key, UpdateExpression update, PredicateExpression condition);
		bool TryUpdate(ItemKey key, UpdateExpression update, Values values);

		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, bool returnOldItem, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, bool returnOldItem, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values, bool returnOldItem, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool returnOldItem, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, bool returnOldItem, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values, bool returnOldItem, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(ItemKey key);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, Values values);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, Values values, bool returnOldItem);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, Values values, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, bool returnOldItem, CancellationToken cancellationToken);
		Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, bool returnOldItem, CancellationToken cancellationToken);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, PredicateExpression condition);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, bool returnOldItem);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, PredicateExpression condition, bool returnOldItem);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values, bool returnOldItem);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool returnOldItem);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, bool returnOldItem);
		DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values, bool returnOldItem);
		DynamoDBMap Delete(ItemKey key);
		DynamoDBMap Delete(ItemKey key, PredicateExpression condition);
		DynamoDBMap Delete(ItemKey key, PredicateExpression condition, Values values);
		DynamoDBMap Delete(ItemKey key, bool returnOldItem);
		DynamoDBMap Delete(ItemKey key, PredicateExpression condition, bool returnOldItem);

		IQueryContext Query(DynamoDBKeyValue hashKey);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values);
		IQueryContext Query(DynamoDBKeyValue hashKey, bool consistent);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent);
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
		public Task UpdateTableAsync(IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			return UpdateTableAsync(null, indexProvisionedThroughputs, CancellationToken.None);
		}
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			return UpdateTableAsync((ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs, CancellationToken.None);
		}
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken)
		{
			return UpdateTableAsync((ProvisionedThroughput?)provisionedThroughput, null, cancellationToken);
		}
		public Task UpdateTableAsync(IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			return UpdateTableAsync(null, indexProvisionedThroughputs, cancellationToken);
		}
		public Task UpdateTableAsync(ProvisionedThroughput provisionedThroughput, IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			return UpdateTableAsync((ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs, cancellationToken);
		}
		#endregion

		#region UpdateTable
		public void UpdateTable(ProvisionedThroughput provisionedThroughput)
		{
			UpdateTable((ProvisionedThroughput?)provisionedThroughput, null);
		}
		public void UpdateTable(IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			UpdateTable(null, indexProvisionedThroughputs);
		}
		public void UpdateTable(ProvisionedThroughput provisionedThroughput, IDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			UpdateTable((ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs);
		}
		#endregion

		#region GetAsync
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey)
		{
			return GetAsync(new ItemKey(hashKey), null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection)
		{
			return GetAsync(new ItemKey(hashKey), projection, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, bool consistent)
		{
			return GetAsync(new ItemKey(hashKey), null, consistent, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent)
		{
			return GetAsync(new ItemKey(hashKey), projection, consistent, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), null, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), projection, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), null, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey), projection, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), projection, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), null, consistent, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), projection, consistent, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), null, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), projection, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), null, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), projection, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key)
		{
			return GetAsync(key, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection)
		{
			return GetAsync(key, projection, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, bool consistent)
		{
			return GetAsync(key, null, consistent, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, bool consistent)
		{
			return GetAsync(key, projection, consistent, CancellationToken.None);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken)
		{
			return GetAsync(key, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, CancellationToken cancellationToken)
		{
			return GetAsync(key, projection, false, cancellationToken);
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, bool consistent, CancellationToken cancellationToken)
		{
			return GetAsync(key, null, consistent, cancellationToken);
		}
		#endregion

		#region Get
		public DynamoDBMap Get(DynamoDBKeyValue hashKey)
		{
			return Get(new ItemKey(hashKey), null, false);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, ProjectionExpression projection)
		{
			return Get(new ItemKey(hashKey), projection, false);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, bool consistent)
		{
			return Get(new ItemKey(hashKey), null, consistent);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent)
		{
			return Get(new ItemKey(hashKey), projection, consistent);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return Get(new ItemKey(hashKey, rangeKey), null, false);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection)
		{
			return Get(new ItemKey(hashKey, rangeKey), projection, false);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent)
		{
			return Get(new ItemKey(hashKey, rangeKey), null, consistent);
		}
		public DynamoDBMap Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent)
		{
			return Get(new ItemKey(hashKey, rangeKey), projection, consistent);
		}
		public DynamoDBMap Get(ItemKey key)
		{
			return Get(key, null, false);
		}
		public DynamoDBMap Get(ItemKey key, ProjectionExpression projection)
		{
			return Get(key, projection, false);
		}
		public DynamoDBMap Get(ItemKey key, bool consistent)
		{
			return Get(key, null, consistent);
		}
		#endregion

		#region BatchGetAsync
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), null, false, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), projection, false, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, bool consistent)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), null, consistent, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), projection, consistent, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), null, false, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), projection, false, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, bool consistent, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), null, consistent, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			return BatchGetAsync(keys.ToAsyncEnumerable(), projection, consistent, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys)
		{
			return BatchGetAsync(keys, null, false, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection)
		{
			return BatchGetAsync(keys, projection, false, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, bool consistent)
		{
			return BatchGetAsync(keys, null, consistent, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, bool consistent)
		{
			return BatchGetAsync(keys, projection, consistent, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ReadAhead readAhead)
		{
			return BatchGetAsync(keys, null, false, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ProjectionExpression projection, ReadAhead readAhead)
		{
			return BatchGetAsync(keys, projection, false, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, bool consistent, ReadAhead readAhead)
		{
			return BatchGetAsync(keys, null, consistent, readAhead);
		}
		#endregion

		#region BatchGet
		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys)
		{
			return BatchGet(keys, null, false);
		}
		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, ProjectionExpression projection)
		{
			return BatchGet(keys, projection, false);
		}
		public IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys, bool consistent)
		{
			return BatchGet(keys, null, consistent);
		}
		#endregion

		#region BatchGetJoinAsync<T, TResult>
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, null, false, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, projection, false, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, null, consistent, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, projection, consistent, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, null, false, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, ReadAhead readAhead)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, projection, false, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent, ReadAhead readAhead)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, null, consistent, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems.ToAsyncEnumerable(), keySelector, resultSelector, projection, consistent, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems, keySelector, resultSelector, null, false, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems, keySelector, resultSelector, projection, false, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems, keySelector, resultSelector, null, consistent, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, bool consistent)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems, keySelector, resultSelector, projection, consistent, ReadAhead.Some);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems, keySelector, resultSelector, null, false, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection, ReadAhead readAhead)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems, keySelector, resultSelector, projection, false, readAhead);
		}
		public IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent, ReadAhead readAhead)
		{
			return BatchGetJoinAsync<T, TResult>(outerItems, keySelector, resultSelector, null, consistent, readAhead);
		}
		#endregion

		#region BatchGetJoin<T, TResult>
		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector)
		{
			return BatchGetJoin<T, TResult>(outerItems, keySelector, resultSelector, null, false);
		}
		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ProjectionExpression projection)
		{
			return BatchGetJoin<T, TResult>(outerItems, keySelector, resultSelector, projection, false);
		}
		public IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, bool consistent)
		{
			return BatchGetJoin<T, TResult>(outerItems, keySelector, resultSelector, null, consistent);
		}
		#endregion

		#region PutAsync
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item)
		{
			return PutAsync(item, null, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition)
		{
			return PutAsync(item, condition, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, Values values)
		{
			return PutAsync(item, condition, values, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem)
		{
			return PutAsync(item, null, null, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, bool returnOldItem)
		{
			return PutAsync(item, condition, null, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, Values values, bool returnOldItem)
		{
			return PutAsync(item, condition, values, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			return PutAsync(item, null, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return PutAsync(item, condition, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return PutAsync(item, condition, values, false, cancellationToken);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem, CancellationToken cancellationToken)
		{
			return PutAsync(item, null, null, returnOldItem, cancellationToken);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, PredicateExpression condition, bool returnOldItem, CancellationToken cancellationToken)
		{
			return PutAsync(item, condition, null, returnOldItem, cancellationToken);
		}
		#endregion

		#region Put
		public DynamoDBMap Put(DynamoDBMap item)
		{
			return Put(item, null, null, false);
		}
		public DynamoDBMap Put(DynamoDBMap item, PredicateExpression condition)
		{
			return Put(item, condition, null, false);
		}
		public DynamoDBMap Put(DynamoDBMap item, PredicateExpression condition, Values values)
		{
			return Put(item, condition, values, false);
		}
		public DynamoDBMap Put(DynamoDBMap item, bool returnOldItem)
		{
			return Put(item, null, null, returnOldItem);
		}
		public DynamoDBMap Put(DynamoDBMap item, PredicateExpression condition, bool returnOldItem)
		{
			return Put(item, condition, null, returnOldItem);
		}
		#endregion

		#region UpdateAsync
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, null, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, null, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, values, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, values, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, UpdateReturnValue returnValue)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, null, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, null, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, values, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, values, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, null, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, null, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, values, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, values, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, null, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, null, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, null, values, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey), update, condition, values, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, null, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, null, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, values, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, values, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, UpdateReturnValue returnValue)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, null, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, null, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, values, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, values, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, null, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, null, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, values, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, values, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, null, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, null, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, null, values, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, values, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update)
		{
			return UpdateAsync(key, update, null, null, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition)
		{
			return UpdateAsync(key, update, condition, null, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, Values values)
		{
			return UpdateAsync(key, update, null, values, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return UpdateAsync(key, update, condition, values, UpdateReturnValue.None, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, UpdateReturnValue returnValue)
		{
			return UpdateAsync(key, update, null, null, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue)
		{
			return UpdateAsync(key, update, condition, null, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return UpdateAsync(key, update, null, values, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue)
		{
			return UpdateAsync(key, update, condition, values, returnValue, CancellationToken.None);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, CancellationToken cancellationToken)
		{
			return UpdateAsync(key, update, null, null, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return UpdateAsync(key, update, condition, null, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(key, update, null, values, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(key, update, condition, values, UpdateReturnValue.None, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(key, update, null, null, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(key, update, condition, null, returnValue, cancellationToken);
		}
		public Task<DynamoDBMap> UpdateAsync(ItemKey key, UpdateExpression update, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(key, update, null, values, returnValue, cancellationToken);
		}
		#endregion

		#region Update
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update)
		{
			return Update(new ItemKey(hashKey), update, null, null, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition)
		{
			return Update(new ItemKey(hashKey), update, condition, null, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, Values values)
		{
			return Update(new ItemKey(hashKey), update, null, values, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return Update(new ItemKey(hashKey), update, condition, values, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, UpdateReturnValue returnValue)
		{
			return Update(new ItemKey(hashKey), update, null, null, returnValue);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue)
		{
			return Update(new ItemKey(hashKey), update, condition, null, returnValue);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return Update(new ItemKey(hashKey), update, null, values, returnValue);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue)
		{
			return Update(new ItemKey(hashKey), update, condition, values, returnValue);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, null, null, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, condition, null, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, null, values, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, condition, values, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, UpdateReturnValue returnValue)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, null, null, returnValue);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, condition, null, returnValue);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, null, values, returnValue);
		}
		public DynamoDBMap Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, UpdateReturnValue returnValue)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, condition, values, returnValue);
		}
		public DynamoDBMap Update(ItemKey key, UpdateExpression update)
		{
			return Update(key, update, null, null, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(ItemKey key, UpdateExpression update, PredicateExpression condition)
		{
			return Update(key, update, condition, null, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(ItemKey key, UpdateExpression update, Values values)
		{
			return Update(key, update, null, values, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return Update(key, update, condition, values, UpdateReturnValue.None);
		}
		public DynamoDBMap Update(ItemKey key, UpdateExpression update, UpdateReturnValue returnValue)
		{
			return Update(key, update, null, null, returnValue);
		}
		public DynamoDBMap Update(ItemKey key, UpdateExpression update, PredicateExpression condition, UpdateReturnValue returnValue)
		{
			return Update(key, update, condition, null, returnValue);
		}
		public DynamoDBMap Update(ItemKey key, UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return Update(key, update, null, values, returnValue);
		}
		#endregion

		#region TryUpdateAsync
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, null, null, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, condition, null, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, null, values, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, condition, values, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, null, null, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, condition, null, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, null, values, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey), update, condition, values, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, null, null, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, null, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, null, values, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, values, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, null, null, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, null, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, null, values, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(new ItemKey(hashKey, rangeKey), update, condition, values, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update)
		{
			return TryUpdateAsync(key, update, null, null, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition)
		{
			return TryUpdateAsync(key, update, condition, null, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, Values values)
		{
			return TryUpdateAsync(key, update, null, values, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return TryUpdateAsync(key, update, condition, values, CancellationToken.None);
		}
		public Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(key, update, null, null, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(key, update, condition, null, cancellationToken);
		}
		public Task<bool> TryUpdateAsync(ItemKey key, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(key, update, null, values, cancellationToken);
		}
		#endregion

		#region TryUpdate
		public bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update)
		{
			return TryUpdate(new ItemKey(hashKey), update, null, null);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition)
		{
			return TryUpdate(new ItemKey(hashKey), update, condition, null);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, Values values)
		{
			return TryUpdate(new ItemKey(hashKey), update, null, values);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return TryUpdate(new ItemKey(hashKey), update, condition, values);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update)
		{
			return TryUpdate(new ItemKey(hashKey, rangeKey), update, null, null);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition)
		{
			return TryUpdate(new ItemKey(hashKey, rangeKey), update, condition, null);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values)
		{
			return TryUpdate(new ItemKey(hashKey, rangeKey), update, null, values);
		}
		public bool TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values)
		{
			return TryUpdate(new ItemKey(hashKey, rangeKey), update, condition, values);
		}
		public bool TryUpdate(ItemKey key, UpdateExpression update)
		{
			return TryUpdate(key, update, null, null);
		}
		public bool TryUpdate(ItemKey key, UpdateExpression update, PredicateExpression condition)
		{
			return TryUpdate(key, update, condition, null);
		}
		public bool TryUpdate(ItemKey key, UpdateExpression update, Values values)
		{
			return TryUpdate(key, update, null, values);
		}
		#endregion

		#region DeleteAsync
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey)
		{
			return DeleteAsync(new ItemKey(hashKey), null, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition)
		{
			return DeleteAsync(new ItemKey(hashKey), condition, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values)
		{
			return DeleteAsync(new ItemKey(hashKey), condition, values, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, bool returnOldItem)
		{
			return DeleteAsync(new ItemKey(hashKey), null, null, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, bool returnOldItem)
		{
			return DeleteAsync(new ItemKey(hashKey), condition, null, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values, bool returnOldItem)
		{
			return DeleteAsync(new ItemKey(hashKey), condition, values, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey), null, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey), condition, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey), condition, values, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, bool returnOldItem, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey), null, null, returnOldItem, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, bool returnOldItem, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey), condition, null, returnOldItem, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values, bool returnOldItem, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey), condition, values, returnOldItem, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), null, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), condition, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), condition, values, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool returnOldItem)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), null, null, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, bool returnOldItem)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), condition, null, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values, bool returnOldItem)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), condition, values, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), null, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), condition, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), condition, values, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool returnOldItem, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), null, null, returnOldItem, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, bool returnOldItem, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), condition, null, returnOldItem, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values, bool returnOldItem, CancellationToken cancellationToken)
		{
			return DeleteAsync(new ItemKey(hashKey, rangeKey), condition, values, returnOldItem, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key)
		{
			return DeleteAsync(key, null, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition)
		{
			return DeleteAsync(key, condition, null, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, Values values)
		{
			return DeleteAsync(key, condition, values, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, bool returnOldItem)
		{
			return DeleteAsync(key, null, null, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, bool returnOldItem)
		{
			return DeleteAsync(key, condition, null, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, Values values, bool returnOldItem)
		{
			return DeleteAsync(key, condition, values, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, CancellationToken cancellationToken)
		{
			return DeleteAsync(key, null, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, CancellationToken cancellationToken)
		{
			return DeleteAsync(key, condition, null, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return DeleteAsync(key, condition, values, false, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, bool returnOldItem, CancellationToken cancellationToken)
		{
			return DeleteAsync(key, null, null, returnOldItem, cancellationToken);
		}
		public Task<DynamoDBMap> DeleteAsync(ItemKey key, PredicateExpression condition, bool returnOldItem, CancellationToken cancellationToken)
		{
			return DeleteAsync(key, condition, null, returnOldItem, cancellationToken);
		}
		#endregion

		#region Delete
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey)
		{
			return Delete(new ItemKey(hashKey), null, null, false);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, PredicateExpression condition)
		{
			return Delete(new ItemKey(hashKey), condition, null, false);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values)
		{
			return Delete(new ItemKey(hashKey), condition, values, false);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, bool returnOldItem)
		{
			return Delete(new ItemKey(hashKey), null, null, returnOldItem);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, PredicateExpression condition, bool returnOldItem)
		{
			return Delete(new ItemKey(hashKey), condition, null, returnOldItem);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, PredicateExpression condition, Values values, bool returnOldItem)
		{
			return Delete(new ItemKey(hashKey), condition, values, returnOldItem);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return Delete(new ItemKey(hashKey, rangeKey), null, null, false);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition)
		{
			return Delete(new ItemKey(hashKey, rangeKey), condition, null, false);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values)
		{
			return Delete(new ItemKey(hashKey, rangeKey), condition, values, false);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool returnOldItem)
		{
			return Delete(new ItemKey(hashKey, rangeKey), null, null, returnOldItem);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, bool returnOldItem)
		{
			return Delete(new ItemKey(hashKey, rangeKey), condition, null, returnOldItem);
		}
		public DynamoDBMap Delete(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, PredicateExpression condition, Values values, bool returnOldItem)
		{
			return Delete(new ItemKey(hashKey, rangeKey), condition, values, returnOldItem);
		}
		public DynamoDBMap Delete(ItemKey key)
		{
			return Delete(key, null, null, false);
		}
		public DynamoDBMap Delete(ItemKey key, PredicateExpression condition)
		{
			return Delete(key, condition, null, false);
		}
		public DynamoDBMap Delete(ItemKey key, PredicateExpression condition, Values values)
		{
			return Delete(key, condition, values, false);
		}
		public DynamoDBMap Delete(ItemKey key, bool returnOldItem)
		{
			return Delete(key, null, null, returnOldItem);
		}
		public DynamoDBMap Delete(ItemKey key, PredicateExpression condition, bool returnOldItem)
		{
			return Delete(key, condition, null, returnOldItem);
		}
		#endregion

		#region Query
		public IQueryContext Query(DynamoDBKeyValue hashKey)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, null, null, false);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, null, null, false);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, null, false);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, null, false);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, values, false);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, values, false);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, null, null, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, null, null, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, null, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, null, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, null, filter, values, consistent);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent)
		{
			return new QueryContext(Region, Name, null, Schema.Key, hashKey, projection, filter, values, consistent);
		}
		#endregion
	}
}
