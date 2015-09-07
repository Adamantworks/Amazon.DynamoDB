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
using Adamantworks.Amazon.DynamoDB.Syntax.Query;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class QueryContext : IReverseSyntax, IPagedQueryRangeSyntax
	{
		private readonly Table table;
		private readonly Index index;
		private readonly KeySchema keySchema;
		private readonly ProjectionExpression projection;
		private readonly bool consistent;
		private readonly DynamoDBKeyValue hashKey;
		private readonly PredicateExpression filter;
		private readonly Values values;

		private bool scanIndexForward = true;
		private bool limitSet;
		private int? limit;
		private LastKey? exclusiveStartKey;

		public QueryContext(
			Table table,
			Index index,
			ProjectionExpression projection,
			bool consistent,
			DynamoDBKeyValue hashKey,
			PredicateExpression filter,
			Values values)
		{
			this.table = table;
			this.index = index;
			keySchema = index != null ? index.Schema.Key : table.Schema.Key;
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

		public IQueryRangeSyntax ExclusiveStartKey(LastKey key)
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

		public IPagedQueryRangeSyntax Paged(int pageSize, LastKey? exclusiveStartKey)
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
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBeginsWith(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "BEGINS_WITH");
			return QueryPaged(keyConditions);
		}
		#endregion

		#region RangeKeyEquals
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyEquals(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyEquals(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "EQ");
			return QueryPaged(keyConditions);
		}
		#endregion

		#region RangeKeyLessThan(OrEqualTo)
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LT");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LT");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyLessThan(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThan(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryPaged(keyConditions);
		}

		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "LE");
			return QueryPaged(keyConditions);
		}
		#endregion

		#region RangeKeyGreaterThan(OrEqualTo)
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThan(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GT");
			return QueryPaged(keyConditions);
		}

		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryAsync(keyConditions, readAhead);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return QueryPagedAsync(keyConditions, cancellationToken);
		}

		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
			var keyConditions = hashKey.ToAws(keySchema.HashKey);
			rangeKey.ToAws(keySchema.RangeKey, keyConditions, "GE");
			return Query(keyConditions);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey)
		{
			CheckHasRangeKey();
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
				return AsyncEnumerableEx.GenerateChunked(new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null),
					async (lastResponse, cancellationToken) =>
					{
						request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
						if(limit != null)
							request.Limit = lastResponse.CurrentLimit;
						return new QueryResponse(lastResponse, await table.Region.DB.QueryAsync(request, cancellationToken).ConfigureAwait(false));
					},
					lastResponse => lastResponse.Items.Select(item => item.ToMap()),
					lastResponse => lastResponse.IsComplete(),
					readAhead);
			});
		}
		private async Task<ItemPage> QueryPagedAsync(Dictionary<string, Aws.Condition> keyConditions, CancellationToken cancellationToken)
		{
			var request = BuildQueryRequest(keyConditions);
			var items = new List<DynamoDBMap>();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, await table.Region.DB.QueryAsync(request, cancellationToken).ConfigureAwait(false));
				items.AddRange(lastResponse.Items.Select(item => item.ToMap()));
			} while(!lastResponse.IsComplete());
			return new ItemPage(items, lastResponse.GetLastEvaluatedKey(table.Schema.Key, index != null ? index.Schema.Key : null));
		}
		private IEnumerable<DynamoDBMap> Query(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = BuildQueryRequest(keyConditions);
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Query(request));
				foreach(var item in lastResponse.Items)
					yield return item.ToMap();
			} while(!lastResponse.IsComplete());
		}
		private ItemPage QueryPaged(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = BuildQueryRequest(keyConditions);
			var items = new List<DynamoDBMap>();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Query(request));
				items.AddRange(lastResponse.Items.Select(item => item.ToMap()));
			} while(!lastResponse.IsComplete());
			return new ItemPage(items, lastResponse.GetLastEvaluatedKey(table.Schema.Key, index != null ? index.Schema.Key : null));
		}
		private Aws.QueryRequest BuildQueryRequest(Dictionary<string, Aws.Condition> keyConditions)
		{
			var request = new Aws.QueryRequest()
			{
				TableName = table.Name,
				IndexName = index != null ? index.Name : null,
				KeyConditions = keyConditions,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(projection, filter),
				ConsistentRead = consistent,
				ScanIndexForward = scanIndexForward,
			};
			// No need to set Limit or ExclusiveStartKey, that will be handled by the first QueryResponse
			if(projection != null)
				request.ProjectionExpression = projection.Expression;
			if(filter != null)
				request.FilterExpression = filter.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(filter, values);
			return request;
		}
	}
}
