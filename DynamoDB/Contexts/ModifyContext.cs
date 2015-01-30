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
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class ModifyContext : IIfSyntax
	{
		private readonly Table table;
		private readonly ItemKey key;
		private PredicateExpression condition;
		private Values conditionValues;

		public ModifyContext(Table table, ItemKey key)
		{
			this.table = table;
			this.key = key;
		}

		public IModifySyntax If(PredicateExpression condition)
		{
			this.condition = condition;
			return this;
		}
		public IModifySyntax If(PredicateExpression condition, Values values)
		{
			this.condition = condition;
			conditionValues = values;
			return this;
		}

		#region Update
		public async Task<DynamoDBMap> UpdateAsync(UpdateExpression update, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			var request = BuildUpdateRequest(update, values, returnValue);
			var response = await table.Region.DB.UpdateItemAsync(request, cancellationToken).ConfigureAwait(false);
			if(returnValue == UpdateReturnValue.None) return null;
			return response.Attributes.ToGetValue();
		}

		public DynamoDBMap Update(UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			var request = BuildUpdateRequest(update, values, returnValue);
			var response = table.Region.DB.UpdateItem(request);
			if(returnValue == UpdateReturnValue.None) return null;
			return response.Attributes.ToGetValue();
		}

		private global::Amazon.DynamoDBv2.Model.UpdateItemRequest BuildUpdateRequest(UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			var request = new global::Amazon.DynamoDBv2.Model.UpdateItemRequest()
			{
				TableName = table.Name,
				Key = key.ToAws(table.Schema.Key),
				UpdateExpression = update.Expression,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(update, condition),
				ReturnValues = returnValue.ToAws(),
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(update, condition, conditionValues, values);

			return request;
		}
		#endregion

		#region TryUpdate
		public async Task<bool> TryUpdateAsync(UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			try
			{
				await UpdateAsync(update, values, UpdateReturnValue.None, cancellationToken).ConfigureAwait(false);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}

		public bool TryUpdate(UpdateExpression update, Values values)
		{
			try
			{
				Update(update, values, UpdateReturnValue.None);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}
		#endregion

		#region Delete
		public async Task<DynamoDBMap> DeleteAsync(bool returnOldItem, CancellationToken cancellationToken)
		{
			var request = BuildDeleteRequest(returnOldItem);
			var response = await table.Region.DB.DeleteItemAsync(request, cancellationToken).ConfigureAwait(false);
			if(!returnOldItem) return null;
			return response.Attributes.ToGetValue();
		}

		public DynamoDBMap Delete(bool returnOldItem)
		{
			var request = BuildDeleteRequest(returnOldItem);
			var response = table.Region.DB.DeleteItem(request);
			if(!returnOldItem) return null;
			return response.Attributes.ToGetValue();
		}

		private Aws.DeleteItemRequest BuildDeleteRequest(bool returnOldItem)
		{
			var request = new Aws.DeleteItemRequest()
			{
				TableName = table.Name,
				Key = key.ToAws(table.Schema.Key),
				ReturnValues = returnOldItem ? AwsEnums.ReturnValue.ALL_OLD : AwsEnums.ReturnValue.NONE,
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(condition, conditionValues);
			return request;
		}
		#endregion
	}
}
