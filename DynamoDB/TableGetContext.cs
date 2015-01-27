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
using System.Threading;
using System.Threading.Tasks;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	internal partial class TableGetContext : ITableConsistentSyntax
	{
		private readonly Table table;
		private readonly ProjectionExpression projection;
		private bool? consistentRead;

		public TableGetContext(Table table, ProjectionExpression projection)
		{
			this.table = table;
			this.projection = projection;
		}
		public TableGetContext(Table table, ProjectionExpression projection, bool consistentRead)
		{
			this.table = table;
			this.projection = projection;
			this.consistentRead = consistentRead;
		}

		public ITableConsistentSyntax Consistent
		{
			get
			{
				if(consistentRead != null)
					throw new InvalidOperationException("Can't set Consistent twice");
				consistentRead = true;
				return this;
			}
		}

		#region Get
		public async Task<DynamoDBMap> GetAsync(ItemKey key, CancellationToken cancellationToken)
		{
			var request = BuildGetRequest(key);
			var result = await table.Region.DB.GetItemAsync(request, cancellationToken).ConfigureAwait(false);
			return result.Item.ToGetValue();
		}

		public DynamoDBMap Get(ItemKey key)
		{
			var request = BuildGetRequest(key);
			var result = table.Region.DB.GetItem(request);
			return result.Item.ToGetValue();
		}

		private Aws.GetItemRequest BuildGetRequest(ItemKey key)
		{
			var request = new Aws.GetItemRequest()
			{
				TableName = table.Name,
				Key = key.ToAws(table.Schema.Key),
				ConsistentRead = consistentRead ?? false,
			};
			if(projection != null)
			{
				request.ProjectionExpression = projection.Expression;
				request.ExpressionAttributeNames = AwsAttributeNames.Get(projection);
			}
			return request;
		}
		#endregion
	}
}
