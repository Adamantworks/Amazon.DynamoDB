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

using System.Linq;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Syntax;
using Adamantworks.Amazon.DynamoDB.Syntax.Scan;
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	// See Overloads.tt and Overloads.cs for more methods of this class
	internal partial class ScanCountContext : IScanCountOptionsSyntax
	{
		private readonly Table table;
		private readonly Index index;
		private readonly bool consistent;
		private readonly PredicateExpression filter;
		private readonly Values values;

		public ScanCountContext(
			Table table,
			Index index,
			bool consistent,
			PredicateExpression filter,
			Values values)
		{
			this.table = table;
			this.index = index;
			this.consistent = consistent;
			this.filter = filter;
			this.values = values;
		}

		#region All
		public async Task<long> AllAsync()
		{
			var request = BuildScanCountRequest();
			var lastResponse = new QueryResponse(null, null, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if((int?)null != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, await table.Region.DB.ScanAsync(request).ConfigureAwait(false));
			} while(!lastResponse.IsComplete());
			return lastResponse.Count;
		}

		public long All()
		{
			var request = BuildScanCountRequest();
			var lastResponse = new QueryResponse(null, null, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if((int?)null != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Scan(request));
			} while(!lastResponse.IsComplete());
			return lastResponse.Count;
		}
		#endregion

		#region Segment
		public async Task<long> SegmentAsync(int segment, int totalSegments)
		{
			var request = BuildScanCountRequest(segment, totalSegments);
			var lastResponse = new QueryResponse(null, null, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if((int?)null != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, await table.Region.DB.ScanAsync(request).ConfigureAwait(false));
			} while(!lastResponse.IsComplete());
			return lastResponse.Count;
		}

		public long Segment(int segment, int totalSegments)
		{
			var request = BuildScanCountRequest(segment, totalSegments);
			var lastResponse = new QueryResponse(null, null, table.Schema.Key, index != null ? index.Schema.Key : null);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if((int?)null != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, table.Region.DB.Scan(request));
			} while(!lastResponse.IsComplete());
			return lastResponse.Count;
		}
		#endregion

		#region InParallel
		public async Task<long> InParallelAsync(int totalSegments)
		{
			if(totalSegments == 1) return await AllAsync();

			var segmentCountTasks = Enumerable.Range(0, totalSegments)
				.Select(segment => SegmentAsync(segment, totalSegments));
			var segmentCounts = await Task.WhenAll(segmentCountTasks).ConfigureAwait(false);
			return segmentCounts.Sum();
		}
		public long InParallel(int totalSegments)
		{
			if(totalSegments == 1) return All();

			var segmentCountTasks = Enumerable.Range(0, totalSegments)
				.Select(segment => SegmentAsync(segment, totalSegments));
			var segmentCounts = Task.WhenAll(segmentCountTasks).WaitAndUnwrapException();
			return segmentCounts.Sum();
		}

		public Task<long> InParallelAsync()
		{
			return InParallelAsync(EstimateScanSegments());
		}
		public long InParallel()
		{
			return InParallel(EstimateScanSegments());
		}
		#endregion

		private Aws.ScanRequest BuildScanCountRequest()
		{
			var request = new Aws.ScanRequest()
			{
				TableName = table.Name,
				IndexName = index != null ? index.Name : null,
				ExpressionAttributeNames = filter != null ? AwsAttributeNames.Get(filter) : null,
				ConsistentRead = consistent,
				Select = AwsEnums.Select.COUNT,
			};
			// No need to set Limit or ExclusiveStartKey, that will be handled by the first QueryResponse
			if(filter != null)
				request.FilterExpression = filter.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(filter, values);
			return request;
		}
		private Aws.ScanRequest BuildScanCountRequest(int segment, int totalSegments)
		{
			var request = BuildScanCountRequest();
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
