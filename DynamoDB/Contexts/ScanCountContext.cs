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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	// See Overloads.tt and Overloads.cs for more methods of this class
	internal partial class ScanCountContext
	{
		private readonly Table table;
		private readonly Index index;
		private readonly PredicateExpression filter;
		private readonly Values values;
		private bool limitSet;
		private int? limit;
		private LastKey? exclusiveStartKey;

		public ScanCountContext(
			Table table,
			Index index,
			PredicateExpression filter,
			Values values)
		{
			this.table = table;
			this.index = index;
			this.filter = filter;
			this.values = values;
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
		#endregion

		#region Segment
		public IAsyncEnumerable<DynamoDBMap> SegmentAsync(int segment, int totalSegments, ReadAhead readAhead)
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

		public IEnumerable<DynamoDBMap> Segment(int segment, int totalSegments)
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
		#endregion

		#region InParallel
		public IAsyncEnumerable<DynamoDBMap> InParallelAsync(int totalSegments)
		{
			if(totalSegments == 1) return AllAsync(ReadAhead.All);

			return AsyncEnumerable.Defer(() =>
			{
				var segments = Enumerable.Range(0, totalSegments).Select(segment => SegmentAsync(segment, totalSegments, ReadAhead.All).GetEnumerator()).ToList();

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
		public IAsyncEnumerable<DynamoDBMap> InParallelAsync()
		{
			return InParallelAsync(EstimateScanSegments());
		}

		public IEnumerable<DynamoDBMap> InParallel(int totalSegments)
		{
			return InParallelAsync(totalSegments).ToEnumerable();
		}
		public IEnumerable<DynamoDBMap> InParallel()
		{
			return InParallelAsync().ToEnumerable();
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

		#region CountSegment
		public async Task<long> CountSegmentAsync(int segment, int totalSegments)
		{
			var request = BuildCountRequest(segment, totalSegments);
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

		public long CountSegment(int segment, int totalSegments)
		{
			var request = BuildCountRequest(segment, totalSegments);
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

		#region CountInParallel
		public async Task<long> CountInParallelAsync(int totalSegments)
		{
			if(totalSegments == 1) return await CountAllAsync();

			var segmentCountTasks = Enumerable.Range(0, totalSegments)
				.Select(segment => CountSegmentAsync(segment, totalSegments));
			var segmentCounts = await Task.WhenAll(segmentCountTasks).ConfigureAwait(false);
			return segmentCounts.Sum();
		}
		public long CountInParallel(int totalSegments)
		{
			if(totalSegments == 1) return CountAll();

			var segmentCountTasks = Enumerable.Range(0, totalSegments)
				.Select(segment => CountSegmentAsync(segment, totalSegments));
			var segmentCounts = Task.WhenAll(segmentCountTasks).WaitAndUnwrapException();
			return segmentCounts.Sum();
		}

		public Task<long> CountInParallelAsync()
		{
			return CountInParallelAsync(EstimateScanSegments());
		}
		public long CountInParallel()
		{
			return CountInParallel(EstimateScanSegments());
		}
		#endregion

		private Aws.ScanRequest BuildScanRequest()
		{
			var request = new Aws.ScanRequest()
			{
				TableName = table.Name,
				IndexName = index != null ? index.Name : null,
				ExpressionAttributeNames = AwsAttributeNames.Get(filter),
			};
			// No need to set Limit or ExclusiveStartKey, that will be handled by the first QueryResponse
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
		private Aws.ScanRequest BuildCountRequest(int segment, int totalSegments)
		{
			var request = BuildCountRequest();
			request.Segment = segment;
			request.TotalSegments = totalSegments;
			return request;
		}

		private int EstimateScanSegments()
		{
			var sizeInBytes = index != null ? index.SizeInBytes : table.SizeInBytes;
			return DynamoDBRegion.EstimateScanSegments(sizeInBytes);
		}
	}
}
