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

using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Syntax;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class PutContext : IPutSyntax
	{
		private readonly Table table;
		private readonly PredicateExpression condition;
		private readonly Values values;

		public PutContext(Table table, PredicateExpression condition, Values values)
		{
			this.table = table;
			this.condition = condition;
			this.values = values;
		}

		#region Put
		public async Task<DynamoDBMap> PutAsync(DynamoDBMap item,  bool returnOldItem, CancellationToken cancellationToken)
		{
			var request = BuildPutItemRequest(item,  returnOldItem);
			var response = await table.Region.DB.PutItemAsync(request, cancellationToken).ConfigureAwait(false);
			if(!returnOldItem) return null;
			return response.Attributes.ToGetValue();
		}

		public DynamoDBMap Put(DynamoDBMap item,  bool returnOldItem)
		{
			var request = BuildPutItemRequest(item, returnOldItem);
			var response = table.Region.DB.PutItem(request);
			if(!returnOldItem) return null;
			return response.Attributes.ToGetValue();
		}

		private global::Amazon.DynamoDBv2.Model.PutItemRequest BuildPutItemRequest(DynamoDBMap item, bool returnOldItem)
		{
			var request = new global::Amazon.DynamoDBv2.Model.PutItemRequest()
			{
				TableName = table.Name,
				Item = item.ToAwsDictionary(),
				ReturnValues = returnOldItem ? global::Amazon.DynamoDBv2.ReturnValue.ALL_OLD : global::Amazon.DynamoDBv2.ReturnValue.NONE,
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(condition, values);
			return request;
		}
		#endregion
	}
}
