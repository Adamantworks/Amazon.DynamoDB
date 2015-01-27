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
using Adamantworks.Amazon.DynamoDB.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal class ScanContext : IScanContext
	{
		private readonly Region region;
		private readonly string tableName;
		private readonly ProjectionExpression projection;
		private readonly PredicateExpression filter;
		private readonly Values values;
		private bool limitSet;
		private int? limit;

		public ScanContext(
			Region region,
			string tableName,
			ProjectionExpression projection,
			PredicateExpression filter,
			Values values)
		{
			this.region = region;
			this.tableName = tableName;
			this.projection = projection;
			this.filter = filter;
			this.values = values;
		}

		public IScanContext LimitTo(int? limit)
		{
			if(limitSet)
				throw new Exception("Limit of Scan operation already set");

			limitSet = true;
			this.limit = limit;
			return this;
		}

		#region All
		public IAsyncEnumerable<DynamoDBMap> AllAsync(ReadAhead readAhead = ReadAhead.Some)
		{
			return AsyncEnumerable.Defer(() =>
			{
				// This must be in here so it is deferred until GetEnumerator() is called on us (need one per enumerator)
				var request = BuildScanRequest();
				return AsyncEnumerableEx.GenerateChunked<global::Amazon.DynamoDBv2.Model.ScanResponse, DynamoDBMap>(null,
					(lastResponse, cancellationToken) =>
					{
						if(lastResponse != null)
							request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
						return region.DB.ScanAsync(request, cancellationToken);
					},
					lastResponse => lastResponse.Items.Select(item => item.ToValue()),
					IsComplete,
					readAhead);
			});
		}
		public IEnumerable<DynamoDBMap> All()
		{
			var request = BuildScanRequest();
			global::Amazon.DynamoDBv2.Model.ScanResponse lastResponse = null;
			do
			{
				if(lastResponse != null)
					request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				lastResponse = region.DB.Scan(request);
				foreach(var item in lastResponse.Items)
					yield return item.ToValue();
			} while(!IsComplete(lastResponse));
		}
		#endregion

		private global::Amazon.DynamoDBv2.Model.ScanRequest BuildScanRequest()
		{
			var request = new global::Amazon.DynamoDBv2.Model.ScanRequest()
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
			return request;
		}
		private static bool IsComplete(global::Amazon.DynamoDBv2.Model.ScanResponse lastResponse)
		{
			return lastResponse.LastEvaluatedKey == null || lastResponse.LastEvaluatedKey.Count == 0;
		}
	}
}
