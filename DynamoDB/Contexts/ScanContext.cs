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

using System;
using System.Collections.Generic;
using System.Linq;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal class ScanContext : IScanLimitToSyntax
	{
		private readonly Region region;
		private readonly string tableName;
		private readonly KeySchema keySchema;
		private readonly ProjectionExpression projection;
		private readonly PredicateExpression filter;
		private readonly Values values;
		private bool limitSet;
		private int? limit;
		private LastKey? exclusiveStartKey;

		public ScanContext(
			Region region,
			string tableName,
			KeySchema keySchema,
			ProjectionExpression projection,
			PredicateExpression filter,
			Values values)
		{
			this.region = region;
			this.tableName = tableName;
			this.keySchema = keySchema;
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

		#region All
		public IAsyncEnumerable<DynamoDBMap> AllAsync(ReadAhead readAhead = ReadAhead.Some)
		{
			return AsyncEnumerable.Defer(() =>
			{
				// This must be in here so it is deferred until GetEnumerator() is called on us (need one per enumerator)
				var request = BuildScanRequest();
				return AsyncEnumerableEx.GenerateChunked(new QueryResponse(limit, exclusiveStartKey, keySchema),
					async (lastResponse, cancellationToken) =>
					{
						request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
						if(limit != null)
							request.Limit = lastResponse.CurrentLimit;
						return new QueryResponse(lastResponse, await region.DB.ScanAsync(request, cancellationToken).ConfigureAwait(false));
					},
					lastResponse => lastResponse.Items.Select(item => item.ToMap()),
					lastResponse => lastResponse.IsComplete(),
					readAhead);
			});
		}
		public IEnumerable<DynamoDBMap> All()
		{
			var request = BuildScanRequest();
			var lastResponse = new QueryResponse(limit, exclusiveStartKey, keySchema);
			do
			{
				request.ExclusiveStartKey = lastResponse.LastEvaluatedKey;
				if(limit != null)
					request.Limit = lastResponse.CurrentLimit;
				lastResponse = new QueryResponse(lastResponse, region.DB.Scan(request));
				foreach(var item in lastResponse.Items)
					yield return item.ToMap();
			} while(!lastResponse.IsComplete());
		}
		#endregion

		private Aws.ScanRequest BuildScanRequest()
		{
			var request = new Aws.ScanRequest()
			{
				TableName = tableName,
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
	}
}
