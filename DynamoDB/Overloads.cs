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
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB
{
	public partial interface ITable
	{
		Task ReloadAsync();

		Task WaitUntilNotAsync(TableStatus status);
		Task WaitUntilNotAsync(TableStatus status, TimeSpan timeout);

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
	}

	internal partial class Table
	{
		#region ReloadAsync
		public Task ReloadAsync()
		{
			return ReloadAsync(default(CancellationToken));
		}
		#endregion

		#region WaitUntilNotAsync
		public Task WaitUntilNotAsync(TableStatus status)
		{
			return WaitUntilNotAsync(status, default(CancellationToken));
		}
		public Task WaitUntilNotAsync(TableStatus status, TimeSpan timeout)
		{
			return WaitUntilNotAsync(status, timeout, default(CancellationToken));
		}
		#endregion

		#region GetAsync
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey)
		{
			return GetAsync(new ItemKey(hashKey), null, false, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection)
		{
			return GetAsync(new ItemKey(hashKey), projection, false, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, bool consistent)
		{
			return GetAsync(new ItemKey(hashKey), null, consistent, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent)
		{
			return GetAsync(new ItemKey(hashKey), projection, consistent, default(CancellationToken));
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
			return GetAsync(new ItemKey(hashKey, rangeKey), null, false, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), projection, false, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), null, consistent, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent)
		{
			return GetAsync(new ItemKey(hashKey, rangeKey), projection, consistent, default(CancellationToken));
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
			return GetAsync(key, null, false, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection)
		{
			return GetAsync(key, projection, false, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, bool consistent)
		{
			return GetAsync(key, null, consistent, default(CancellationToken));
		}
		public Task<DynamoDBMap> GetAsync(ItemKey key, ProjectionExpression projection, bool consistent)
		{
			return GetAsync(key, projection, consistent, default(CancellationToken));
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
	}
}
