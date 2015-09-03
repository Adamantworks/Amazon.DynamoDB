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
	// See Overloads.tt and Overloads.cs for more methods of this class
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
		public Task<int> AllKeysAsync(CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return QueryAsync(keyConditions, cancellationToken);
		}

		public int AllKeys()
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyBeginsWith
		public Task<int> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryAsync(keyConditions, cancellationToken);
		}

		public int RangeKeyBeginsWith(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyEquals
		public Task<int> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryAsync(keyConditions, cancellationToken);
		}

		public int RangeKeyEquals(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyLessThan(OrEqualTo)
		public Task<int> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LT");
			return QueryAsync(keyConditions, cancellationToken);
		}

		public int RangeKeyLessThan(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return Query(keyConditions);
		}

		public Task<int> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryAsync(keyConditions, cancellationToken);
		}

		public int RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyGreaterThan(OrEqualTo)
		public Task<int> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryAsync(keyConditions, cancellationToken);
		}

		public int RangeKeyGreaterThan(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return Query(keyConditions);
		}

		public Task<int> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryAsync(keyConditions, cancellationToken);
		}

		public int RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyBetween
		public Task<int> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return QueryAsync(keyConditions, cancellationToken);
		}

		public int RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return Query(keyConditions);
		}
		#endregion

		private void CheckHasRangeKey()
		{
			if(keySchema.RangeKey == null)
				throw new NotSupportedException("Can't specify range key condition for table or index without a range key");
		}
		private async Task<int> QueryAsync(Dictionary<string, Aws.Condition> keyConditions, CancellationToken cancellationToken)
		{
			var request = BuildQueryRequest(keyConditions);
			var response = await table.Region.DB.QueryAsync(request, cancellationToken);
			return response.Count;
		}
		private int Query(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = BuildQueryRequest(keyConditions);
			var response = table.Region.DB.Query(request);
			return response.Count;
		}
		private Aws.QueryRequest BuildQueryRequest(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = new Aws.QueryRequest()
			{
				TableName = table.Name,
				IndexName = index != null ? index.Name : null,
				KeyConditions = keyConditions,
				ExpressionAttributeNames = AwsAttributeNames.Get(filter),
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
