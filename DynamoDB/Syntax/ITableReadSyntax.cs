﻿// Copyright 2015 Adamantworks.  All Rights Reserved.
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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public partial interface ITableReadSyntax : IIndexReadSyntax
	{
		Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken);
		DynamoDBMap Get(ItemKey key);

		IAsyncEnumerable<DynamoDBMap> BatchGetAsync(IAsyncEnumerable<ItemKey> keys, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> BatchGet(IEnumerable<ItemKey> keys);

		IAsyncEnumerable<TResult> BatchGetJoinAsync<T, TResult>(IAsyncEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector, ReadAhead readAhead);
		IEnumerable<TResult> BatchGetJoin<T, TResult>(IEnumerable<T> outerItems, Func<T, ItemKey> keySelector, Func<T, DynamoDBMap, TResult> resultSelector);
	}
}
