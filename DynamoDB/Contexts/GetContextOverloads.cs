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
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Syntax;

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

		#region QueryCount
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, hashKey, null, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, hashKey, filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, hashKey, filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, hashKey, null, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, hashKey, filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, hashKey, filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), null, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, PredicateExpression filter)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey), filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), null, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(table, null, consistentRead ?? false, DynamoDBKeyValue.Convert(hashKey, converter), filter, values);
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

		#region ScanCount
		public IScanCountOptionsSyntax ScanCount()
		{
			return new ScanCountContext(table, null, null, null);
		}
		public IScanCountOptionsSyntax ScanCount(PredicateExpression filter)
		{
			return new ScanCountContext(table, null, filter, null);
		}
		public IScanCountOptionsSyntax ScanCount(PredicateExpression filter, Values values)
		{
			return new ScanCountContext(table, null, filter, values);
		}
		#endregion
	}
}