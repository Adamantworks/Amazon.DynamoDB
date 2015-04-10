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
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	// See Overloads.tt and Overloads.cs for more methods of this class
	internal partial class ScanContext : IScanLimitToOrPagedSyntax, IPagedScanOptionsSyntax
	{
		private readonly Table table;
		private readonly Index index;
		private readonly ProjectionExpression projection;
		private readonly PredicateExpression filter;
		private readonly Values values;
		private bool limitSet;
		private int? limit;
		private LastKey? exclusiveStartKey;

		public ScanContext(
			Table table,
			Index index,
			ProjectionExpression projection,
			PredicateExpression filter,
			Values values)
		{
			this.table = table;
			this.index = index;
			this.projection = projection;
			this.filter = filter;
			this.values = values;
		}

		public IScanExclusiveStartKeySyntax LimitTo(int? limit)
		{
			if(limitSet)
				throw new Exception("Limit of Scan operation already set");

			limitSet = true;
			this.limit = limit;
			return this;
		}

		public IScanOptionsSyntax ExclusiveStartKey(LastKey key)
		{
			exclusiveStartKey = key;
			return this;
		}

		public IPagedScanOptionsSyntax Paged(int pageSize)
		{
			if(limitSet)
				throw new Exception("Limit of Scan operation already set");
			limit = pageSize;
			return this;
		}

		public IPagedScanOptionsSyntax Paged(int pageSize, LastKey? exclusiveStartKey)
		{
			if(limitSet)
				throw new Exception("Limit of Scan operation already set");
			limit = pageSize;
			this.exclusiveStartKey = exclusiveStartKey;
			return this;
		}

		#region All
		public IAsyncEnumerable<DynamoDBMap> AllAsync(ReadAhead readAhead)
		{
			return AsyncEnumerable.Defer(() =>
			{
				// This must be in here so it is deferred until GetEnumerator() is called on us (need one per enumerator)
				var request = BuildScanRequest();
				return AsyncEnumerableEx.GenerateChunked(new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null),
					async (lastResponse, cancellationToken) =>
					{
						request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
						if(limit != null)
							request.Limit = lastResponse.CurrentLimit;
						return new QueryResponse(lastResponse, await table.Region.DB.ScanAsync(request, cancellationToken).ConfigureAwait(false));
					},
					lastResponse => lastResponse.Items.Select(item => item.ToMap()),
					lastResponse => lastResponse.IsComplete(),
					readAhead);
			});
		}
		async Task<ItemPage> IPagedScanOptionsSyntax.AllAsync(CancellationToken cancellationToken)
		{
			var request = BuildScanRequest();
			var items = new List<DynamoDBMap>();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, await table.Region.DB.ScanAsync(request, cancellationToken).ConfigureAwait(false));
				items.AddRange(lastResponse.Items.Select(item => item.ToMap()));
			} while(!lastResponse.IsComplete());
			return new ItemPage(items, lastResponse.GetLastEvaluatedKey(table.Schema.Key, index != null ? index.Schema.Key : null));
		}

		public IEnumerable<DynamoDBMap> All()
		{
			var request = BuildScanRequest();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Scan(request));
				foreach(var item in lastResponse.Items)
					yield return item.ToMap();
			} while(!lastResponse.IsComplete());
		}
		ItemPage IPagedScanOptionsSyntax.All()
		{
			var request = BuildScanRequest();
			var items = new List<DynamoDBMap>();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Scan(request));
				items.AddRange(lastResponse.Items.Select(item => item.ToMap()));
			} while(!lastResponse.IsComplete());
			return new ItemPage(items, lastResponse.GetLastEvaluatedKey(table.Schema.Key, index != null ? index.Schema.Key : null));
		}
		#endregion

		#region Parallel
		public IAsyncEnumerable<DynamoDBMap> ParallelAsync(int segment, int totalSegments, ReadAhead readAhead)
		{
			return AsyncEnumerable.Defer(() =>
			{
				// This must be in here so it is deferred until GetEnumerator() is called on us (need one per enumerator)
				var request = BuildScanRequest(segment, totalSegments);
				return AsyncEnumerableEx.GenerateChunked(new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null),
					async (lastResponse, cancellationToken) =>
					{
						request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
						if(limit != null)
							request.Limit = lastResponse.CurrentLimit;
						return new QueryResponse(lastResponse, await table.Region.DB.ScanAsync(request, cancellationToken).ConfigureAwait(false));
					},
					lastResponse => lastResponse.Items.Select(item => item.ToMap()),
					lastResponse => lastResponse.IsComplete(),
					readAhead);
			});
		}
		async Task<ItemPage> IPagedScanOptionsSyntax.ParallelAsync(int segment, int totalSegments, CancellationToken cancellationToken)
		{
			var request = BuildScanRequest(segment, totalSegments);
			var items = new List<DynamoDBMap>();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, await table.Region.DB.ScanAsync(request, cancellationToken).ConfigureAwait(false));
				items.AddRange(lastResponse.Items.Select(item => item.ToMap()));
			} while(!lastResponse.IsComplete());
			return new ItemPage(items, lastResponse.GetLastEvaluatedKey(table.Schema.Key, index != null ? index.Schema.Key : null));
		}

		public IEnumerable<DynamoDBMap> Parallel(int segment, int totalSegments)
		{
			var request = BuildScanRequest(segment, totalSegments);
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Scan(request));
				foreach(var item in lastResponse.Items)
					yield return item.ToMap();
			} while(!lastResponse.IsComplete());
		}
		ItemPage IPagedScanOptionsSyntax.Parallel(int segment, int totalSegments)
		{
			var request = BuildScanRequest(segment, totalSegments);
			var items = new List<DynamoDBMap>();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Scan(request));
				items.AddRange(lastResponse.Items.Select(item => item.ToMap()));
			} while(!lastResponse.IsComplete());
			return new ItemPage(items, lastResponse.GetLastEvaluatedKey(table.Schema.Key, index != null ? index.Schema.Key : null));
		}
		#endregion

		#region ParallelTasks
		public IAsyncEnumerable<DynamoDBMap> ParallelTasksAsync(int totalSegments)
		{
			if(totalSegments == 1) return AllAsync(ReadAhead.All);

			return AsyncEnumerable.Defer(() =>
			{
				var segments = Enumerable.Range(0, totalSegments).Select(segment => ParallelAsync(segment, totalSegments, ReadAhead.All).GetEnumerator()).ToList();

				return AsyncEnumerableEx.GenerateChunked(new List<DynamoDBMap>(),
					async (lastResult, token) =>
					{
						var moveNext = await Task.WhenAll(segments.Select(e => e.MoveNext(token))).ConfigureAwait(false);
						var result = new List<DynamoDBMap>(segments.Count);
						for(var i = segments.Count - 1; i >= 0; i--)
							if(moveNext[i])
								result.Add(segments[i].Current);
							else
								segments.RemoveAt(i);
						return result;
					},
					state => state,
					state => state.Count == 0,
					ReadAhead.All);
			});
		}

		public IAsyncEnumerable<DynamoDBMap> ParallelTasksAsync()
		{
			var sizeInBytes = index != null ? index.SizeInBytes : table.SizeInBytes;
			return ParallelTasksAsync(DynamoDBRegion.EstimateScanSegments(sizeInBytes));
		}
		#endregion

		#region CountAll
		public async Task<long> CountAllAsync()
		{
			var request = BuildCountRequest();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, await table.Region.DB.ScanAsync(request).ConfigureAwait(false));
			} while(!lastResponse.IsComplete());
			return lastResponse.Count;
		}

		public long CountAll()
		{
			var request = BuildCountRequest();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Scan(request));
			} while(!lastResponse.IsComplete());
			return lastResponse.Count;
		}
		#endregion

		private Aws.ScanRequest BuildScanRequest()
		{
			var request = new Aws.ScanRequest()
			{
				TableName = table.Name,
				IndexName = index != null ? index.Name : null,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(projection, filter),
			};
			// No need to set Limit or ExclusiveStartKey, that will be handled by the first QueryResponse
			if(projection != null)
				request.ProjectionExpression = projection.Expression;
			if(filter != null)
				request.FilterExpression = filter.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(filter, values);
			return request;
		}
		private Aws.ScanRequest BuildScanRequest(int segment, int totalSegments)
		{
			var request = BuildScanRequest();
			request.Segment = segment;
			request.TotalSegments = totalSegments;
			return request;
		}
		private Aws.ScanRequest BuildCountRequest()
		{
			var request = BuildScanRequest();
			request.Select = AwsEnums.Select.COUNT;
			return request;
		}
	}
}
