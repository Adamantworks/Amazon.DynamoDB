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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public interface IScanContext
	{
		IScanContext LimitTo(int? limit);
		IAsyncEnumerable<DynamoDBMap> All();
		// TODO: AllSegmented() // do a parallel scan to distribute load (better name?)
		// TODO: BeginsWith() // use last key to create a key begins with query
		// TODO: Between() // use last key to create a key between query
		// TODO: Parallel(totalSegments, currentSegment)
	}

	internal class ScanContext : IScanContext
	{
		private readonly Region region;
		private readonly string tableName;
		private readonly ProjectionExpression projection;
		private readonly PredicateExpression filter;
		private readonly Values values;
		private readonly ReadAhead readAhead;
		private bool limitSet;
		private int? limit;

		public ScanContext(
			Region region,
			string tableName,
			ProjectionExpression projection,
			PredicateExpression filter,
			Values values,
			ReadAhead readAhead)
		{
			this.region = region;
			this.tableName = tableName;
			this.projection = projection;
			this.filter = filter;
			this.values = values;
			this.readAhead = readAhead;
		}

		public IScanContext LimitTo(int? limit)
		{
			if(limitSet)
				throw new Exception("Limit of Scan operation already set");

			limitSet = true;
			this.limit = limit;
			return this;
		}

		public IAsyncEnumerable<DynamoDBMap> All()
		{
			var request = new Aws.ScanRequest()
			{
				TableName = tableName,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(projection, filter),
			};
			if(limit != null)
				request.Limit = limit.Value;
			if(projection != null)
				request.ProjectionExpression = projection.Expression;
			if(filter != null)
				request.FilterExpression = filter.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(filter, values);
			return AsyncEnumerableEx.GenerateChunked<Aws.ScanResponse, DynamoDBMap>(null,
				(lastResponse, cancellationToken) =>
				{
					if(lastResponse != null)
						request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
					return region.DB.ScanAsync(request, cancellationToken);
				},
				lastResponse => lastResponse.Items.Select(item => item.ToValue()),
				lastResponse => lastResponse.LastEvaluatedKey == null || lastResponse.LastEvaluatedKey.Count == 0,
				readAhead);
		}
	}
}
