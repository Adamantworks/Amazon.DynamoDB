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
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal class QueryContext : IReverseSyntax, IPagedQueryRangeSyntax
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
		private ItemKey? exclusiveStartKey;

		public QueryContext(
			Region region,
			string tableName,
			string indexName,
			KeySchema keySchema,
			ProjectionExpression projection,
			bool consistent,
			DynamoDBKeyValue hashKey,
			PredicateExpression filter,
			Values values)
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

		public IQueryLimitToOrPagedSyntax Reverse()
		{
			scanIndexForward = false;
			return this;
		}

		public IQueryExclusiveStartKeySyntax LimitTo(int? limit)
		{
			if(limitSet)
				throw new Exception("Limit of Query operation already set");

			limitSet = true;
			this.limit = limit;
			return this;
		}

		public IQueryRangeSyntax ExclusiveStartKey(ItemKey key)
		{
			exclusiveStartKey = key;
			return this;
		}

		public IPagedQueryRangeSyntax Paged(int pageSize)
		{
			if(limitSet)
				throw new Exception("Limit of Query operation already set");
			limit = pageSize;
			return this;
		}

		public IPagedQueryRangeSyntax Paged(int pageSize, ItemKey? exclusiveStartKey)
		{
			if(limitSet)
				throw new Exception("Limit of Query operation already set");
			limit = pageSize;
			this.exclusiveStartKey = exclusiveStartKey;
			return this;
		}

		#region AllKeys
		public IAsyncEnumerable<DynamoDBMap> AllKeysAsync(ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.AllKeysAsync(CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> AllKeys()
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.AllKeys()
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			return QueryPaged(keyConditions);
		}
		#endregion

		#region RangeKeyBeginsWith
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBeginsWith(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryPaged(keyConditions);
		}
		#endregion

		#region RangeKeyEquals
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyEquals(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyEquals(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryPaged(keyConditions);
		}
		#endregion

		#region RangeKeyLessThan(OrEqualTo)
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LT");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LT");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyLessThan(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThan(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryPaged(keyConditions);
		}

		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryPaged(keyConditions);
		}
		#endregion

		#region RangeKeyGreaterThan(OrEqualTo)
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThan(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryPaged(keyConditions);
		}

		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryPaged(keyConditions);
		}
		#endregion

		#region RangeKeyBetween
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, ReadAhead readAhead)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			keySchema.RangeKey.Between(startInclusive, endExclusive, keyConditions);
			return QueryPaged(keyConditions);
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
		private async Task<ItemPage> QueryPagedAsync(Dictionary<string, Aws.Condition> keyConditions, CancellationToken cancellationToken)
		{
			var request = BuildQueryRequest(keyConditions);
			var items = new List<DynamoDBMap>();
			Aws.QueryResponse lastResponse = null;
			do
			{
				if(lastResponse != null)
					request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				lastResponse = await region.DB.QueryAsync(request, cancellationToken).ConfigureAwait(false);
				items.AddRange(lastResponse.Items.Select(item => item.ToValue()));
			} while(!IsComplete(lastResponse));
			var lastEvaluatedKey = lastResponse.LastEvaluatedKey;
			return new ItemPage(items, lastEvaluatedKey != null ? lastEvaluatedKey.ToItemKey(keySchema) : default(ItemKey?));
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
		private ItemPage QueryPaged(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = BuildQueryRequest(keyConditions);
			var items = new List<DynamoDBMap>();
			Aws.QueryResponse lastResponse = null;
			do
			{
				if(lastResponse != null)
					request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				lastResponse = region.DB.Query(request);
				items.AddRange(lastResponse.Items.Select(item => item.ToValue()));
			} while(!IsComplete(lastResponse));
			var lastEvaluatedKey = lastResponse.LastEvaluatedKey;
			return new ItemPage(items, lastEvaluatedKey != null ? lastEvaluatedKey.ToItemKey(keySchema) : default(ItemKey?));
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
			if(exclusiveStartKey != null)
				request.ExclusiveStartKey = exclusiveStartKey.Value.ToAws(keySchema);
			return request;
		}
		private static bool IsComplete(Aws.QueryResponse lastResponse)
		{
			return lastResponse.LastEvaluatedKey == null || lastResponse.LastEvaluatedKey.Count == 0;
		}
	}
}
