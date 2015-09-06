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
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public partial interface ITableReadSyntax
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
}