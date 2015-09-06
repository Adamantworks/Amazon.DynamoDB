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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using Amazon.DynamoDBv2;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class QueryCountContext : IQueryCountRangeSyntax
	{
		private readonly Table table;
		private readonly Index index;
		private readonly KeySchema keySchema;
		private readonly bool consistent;
		private readonly DynamoDBKeyValue hashKey;
		private readonly PredicateExpression filter;
		private readonly Values values;

		public QueryCountContext(
			Table table,
			Index index,
			bool consistent,
			DynamoDBKeyValue hashKey,
			PredicateExpression filter,
			Values values)
		{
			this.table = table;
			this.index = index;
			keySchema = index != null ? index.Schema.Key : table.Schema.Key;
			this.consistent = consistent;
			this.hashKey = hashKey;
			this.filter = filter;
			this.values = values;
		}

		#region AllKeys
		public Task<long> AllKeysAsync(CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return QueryCountAsync(keyConditions, cancellationToken);
		}

		public long AllKeys()
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return QueryCount(keyConditions);
		}
		#endregion

		#region RangeKeyBeginsWith
		public Task<long> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryCountAsync(keyConditions, cancellationToken);
		}

		public long RangeKeyBeginsWith(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryCount(keyConditions);
		}
		#endregion

		#region RangeKeyEquals
		public Task<long> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryCountAsync(keyConditions, cancellationToken);
		}

		public long RangeKeyEquals(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryCount(keyConditions);
		}
		#endregion

		#region RangeKeyLessThan(OrEqualTo)
		public Task<long> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LT");
			return QueryCountAsync(keyConditions, cancellationToken);
		}

		public long RangeKeyLessThan(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryCount(keyConditions);
		}

		public Task<long> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryCountAsync(keyConditions, cancellationToken);
		}

		public long RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryCount(keyConditions);
		}
		#endregion

		#region RangeKeyGreaterThan(OrEqualTo)
		public Task<long> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryCountAsync(keyConditions, cancellationToken);
		}

		public long RangeKeyGreaterThan(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryCount(keyConditions);
		}

		public Task<long> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryCountAsync(keyConditions, cancellationToken);
		}

		public long RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryCount(keyConditions);
		}
		#endregion

		#region RangeKeyBetween
		public Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return QueryCountAsync(keyConditions, cancellationToken);
		}

		public long RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return QueryCount(keyConditions);
		}
		#endregion

		private void CheckHasRangeKey()
		{
			if(keySchema.RangeKey == null)
				throw new NotSupportedException("Can't specify range key condition for table or index without a range key");
		}
		private async Task<long> QueryCountAsync(Dictionary<string, Aws.Condition> keyConditions, CancellationToken cancellationToken)
		{
			var request = BuildQueryCountRequest(keyConditions);
			var lastResponse = new QueryResponse(null, null, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				lastResponse = new QueryResponse(lastResponse, await table.Region.DB.QueryAsync(request, cancellationToken).ConfigureAwait(false));
			} while(!lastResponse.IsComplete());
			return lastResponse.Count;
		}
		private long QueryCount(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = BuildQueryCountRequest(keyConditions);
			var lastResponse = new QueryResponse(null, null, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Query(request));
			} while(!lastResponse.IsComplete());
			return lastResponse.Count;
		}
		private Aws.QueryRequest BuildQueryCountRequest(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = new Aws.QueryRequest()
			{
				TableName = table.Name,
				IndexName = index != null ? index.Name : null,
				KeyConditions = keyConditions,
				ExpressionAttributeNames = filter != null ? AwsAttributeNames.Get(filter) : null,
				ConsistentRead = consistent,
				Select = Select.COUNT,
			};
			if(filter != null)
				request.FilterExpression = filter.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(filter, values);
			return request;
		}
	}
}
