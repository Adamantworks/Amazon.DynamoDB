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
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	internal class QueryContext : IReversibleQueryContext
	{
		private readonly Region region;
		private readonly string tableName;
		private readonly string indexName;
		private readonly KeySchema keySchema;
		private readonly DynamoDBKeyValue hashKey;
		private readonly ProjectionExpression projection;
		private readonly PredicateExpression filter;
		private readonly Values values;
		private readonly bool consistent;
		private bool scanIndexForward = true;
		private bool limitSet;
		private int? limit;

		public QueryContext(
			Region region,
			string tableName,
			string indexName,
			KeySchema keySchema,
			DynamoDBKeyValue hashKey,
			ProjectionExpression projection,
			PredicateExpression filter,
			Values values,
			bool consistent)
		{
			this.region = region;
			this.tableName = tableName;
			this.indexName = indexName;
			this.keySchema = keySchema;
			this.hashKey = hashKey;
			this.projection = projection;
			this.filter = filter;
			this.values = values;
			this.consistent = consistent;
		}

		public ILimitableQueryContext Reverse()
		{
			scanIndexForward = false;
			return this;
		}

		public IQueryContext LimitTo(int? limit)
		{
			if(limitSet)
				throw new Exception("Limit of Scan operation already set");

			limitSet = true;
			this.limit = limit;
			return this;
		}

		#region AllKeys
		public IAsyncEnumerable<DynamoDBMap> AllKeysAsync(ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return QueryAsync(keyConditions, readAhead);
		}

		public IEnumerable<DynamoDBMap> AllKeys()
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyBeginsWith
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryAsync(keyConditions, readAhead);
		}

		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyEquals
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryAsync(keyConditions, readAhead);
		}

		public IEnumerable<DynamoDBMap> RangeKeyEquals(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyLessThan(OrEqualTo)
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LT");
			return QueryAsync(keyConditions, readAhead);
		}

		public IEnumerable<DynamoDBMap> RangeKeyLessThan(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return Query(keyConditions);
		}

		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryAsync(keyConditions, readAhead);
		}

		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return Query(keyConditions);
		}
		#endregion

		#region RangeKeyGreaterThan(OrEqualTo)
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryAsync(keyConditions, readAhead);
		}

		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return Query(keyConditions);
		}

		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryAsync(keyConditions, readAhead);
		}

		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return Query(keyConditions);
		}
		#endregion

		#region
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, ReadAhead readAhead)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return QueryAsync(keyConditions, readAhead);
		}

		public IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
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
		private IAsyncEnumerable<DynamoDBMap> QueryAsync(Dictionary<string, Aws.Condition> keyConditions, ReadAhead readAhead)
		{
			return AsyncEnumerable.Defer(() =>
			{
				// This must be in here so it is deferred until GetEnumerator() is called on us (need one per enumerator)
				var request = BuildQueryRequest(keyConditions);
				return AsyncEnumerableEx.GenerateChunked<Aws.QueryResponse, DynamoDBMap>(null,
					(lastResponse, cancellationToken) =>
					{
						if(lastResponse != null)
							request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
						return region.DB.QueryAsync(request, cancellationToken);
					},
					lastResponse => lastResponse.Items.Select(item => item.ToValue()),
					IsComplete,
					readAhead);
			});
		}
		private IEnumerable<DynamoDBMap> Query(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = BuildQueryRequest(keyConditions);
			Aws.QueryResponse lastResponse = null;
			do
			{
				if(lastResponse != null)
					request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				lastResponse = region.DB.Query(request);
				foreach(var item in lastResponse.Items)
					yield return item.ToValue();
			} while(!IsComplete(lastResponse));
		}
		private Aws.QueryRequest BuildQueryRequest(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = new Aws.QueryRequest()
			{
				TableName = tableName,
				IndexName = indexName,
				KeyConditions = keyConditions,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(projection, filter),
				ConsistentRead = consistent,
				ScanIndexForward = scanIndexForward,
			};
			if(limit != null)
				request.Limit = limit.Value;
			if(projection != null)
				request.ProjectionExpression = projection.Expression;
			if(filter != null)
				request.FilterExpression = filter.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(filter, values);
			return request;
		}
		private static bool IsComplete(Aws.QueryResponse lastResponse)
		{
			return lastResponse.LastEvaluatedKey == null || lastResponse.LastEvaluatedKey.Count == 0;
		}
	}
}
