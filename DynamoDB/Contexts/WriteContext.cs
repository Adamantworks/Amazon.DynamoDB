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
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Syntax;
using Adamantworks.Amazon.DynamoDB.Syntax.Delete;
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class WriteContext : IWriteConditionallySyntax, IDeleteItemAsyncSyntax, IDeleteItemSyntax, ITryDeleteItemAsyncSyntax, ITryDeleteItemSyntax
	{
		private readonly Table table;
		private readonly PredicateExpression condition;
		private readonly Values conditionValues;
		private AwsEnums.ReturnValue returnValue;
		private CancellationToken? cancellationToken;

		public WriteContext(
			Table table,
			PredicateExpression condition,
			Values conditionValues,
			bool? returnOldItem = null,
			CancellationToken? cancellationToken = null)
		{
			this.table = table;

			this.condition = condition;
			this.conditionValues = conditionValues;
			if(returnOldItem != null)
				returnValue = returnOldItem.Value ? AwsEnums.ReturnValue.ALL_OLD : AwsEnums.ReturnValue.NONE;
			if(cancellationToken != null)
				this.cancellationToken = cancellationToken;
		}

		private void SetReturnValue(bool returnOldItem)
		{
			SetReturnValue(returnOldItem ? AwsEnums.ReturnValue.ALL_OLD : AwsEnums.ReturnValue.NONE);
		}
		private void SetReturnValue(AwsEnums.ReturnValue returnValue)
		{
			if(this.returnValue != null)
				throw new NotSupportedException("Can't set return value twice");

			this.returnValue = returnValue;
		}
		private void SetCancellationToken(CancellationToken cancellationToken)
		{
			if(this.cancellationToken != null)
				throw new NotSupportedException("Can't set cancellation token twice");

			this.cancellationToken = cancellationToken;
		}

		#region Put
		public async Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem, CancellationToken cancellationToken)
		{
			var request = BuildPutItemRequest(item, returnOldItem);
			var response = await table.Region.DB.PutItemAsync(request, cancellationToken).ConfigureAwait(false);
			if(!returnOldItem) return null;
			return response.Attributes.ToMap();
		}

		public DynamoDBMap Put(DynamoDBMap item, bool returnOldItem)
		{
			var request = BuildPutItemRequest(item, returnOldItem);
			var response = table.Region.DB.PutItem(request);
			if(!returnOldItem) return null;
			return response.Attributes.ToMap();
		}

		private Aws.PutItemRequest BuildPutItemRequest(DynamoDBMap item, bool returnOldItem)
		{
			var request = new Aws.PutItemRequest()
			{
				TableName = table.Name,
				Item = item.ToAwsDictionary(),
				ReturnValues = returnOldItem ? AwsEnums.ReturnValue.ALL_OLD : AwsEnums.ReturnValue.NONE,
			};
			if(condition != null)
			{
				request.ConditionExpression = condition.Expression;
				request.ExpressionAttributeNames = AwsAttributeNames.GetCombined(condition);
			}
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(condition, conditionValues);
			return request;
		}
		#endregion

		#region TryPut
		public async Task<bool> TryPutAsync(DynamoDBMap item)
		{
			try
			{
				await PutAsync(item, false, CancellationToken.None).ConfigureAwait(false);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}
		public async Task<bool> TryPutAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			try
			{
				await PutAsync(item, false, cancellationToken).ConfigureAwait(false);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}

		public bool TryPut(DynamoDBMap item)
		{
			try
			{
				Put(item, false);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}
		#endregion

		#region Delete
		public IDeleteItemAsyncSyntax DeleteAsync(bool returnOldItem, CancellationToken cancellationToken)
		{
			SetReturnValue(returnOldItem);
			SetCancellationToken(cancellationToken);
			return this;
		}
		async Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(ItemKey key)
		{
			var request = BuildDeleteRequest(key);
			var response = await table.Region.DB.DeleteItemAsync(request, cancellationToken.Value).ConfigureAwait(false);
			if(returnValue == AwsEnums.ReturnValue.NONE) return null;
			return response.Attributes.ToMap();
		}

		public IDeleteItemSyntax Delete(bool returnOldItem)
		{
			SetReturnValue(returnOldItem);
			SetCancellationToken(CancellationToken.None);
			return this;
		}
		DynamoDBMap IDeleteItemSyntax.Item(ItemKey key)
		{
			var request = BuildDeleteRequest(key);
			var response = table.Region.DB.DeleteItem(request);
			if(returnValue == AwsEnums.ReturnValue.NONE) return null;
			return response.Attributes.ToMap();
		}

		private Aws.DeleteItemRequest BuildDeleteRequest(ItemKey key)
		{
			var request = new Aws.DeleteItemRequest()
			{
				TableName = table.Name,
				Key = key.ToAws(table.Schema.Key),
				ReturnValues = returnValue,
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(condition, conditionValues);
			return request;
		}
		#endregion

		#region TryDelete
		public ITryDeleteItemAsyncSyntax TryDeleteAsync(CancellationToken cancellationToken)
		{
			SetReturnValue(false);
			SetCancellationToken(cancellationToken);
			return this;
		}
		async Task<bool> ITryDeleteItemAsyncSyntax.Item(ItemKey key)
		{
			try
			{
				await ((IDeleteItemAsyncSyntax)this).Item(key).ConfigureAwait(false);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}

		public ITryDeleteItemSyntax TryDelete()
		{
			SetReturnValue(false);
			SetCancellationToken(CancellationToken.None);
			return this;
		}
		bool ITryDeleteItemSyntax.Item(ItemKey key)
		{
			try
			{
				((IDeleteItemSyntax)this).Item(key);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}


		//public async Task<bool> TryDeleteAsync(ItemKey key, CancellationToken cancellationToken)
		//{
		//	try
		//	{
		//		await DeleteAsync(key, false, cancellationToken).ConfigureAwait(false);
		//		return true;
		//	}
		//	catch(Aws.ConditionalCheckFailedException)
		//	{
		//		return false;
		//	}
		//}

		//public bool TryDelete(ItemKey key)
		//{

		//}
		#endregion
	}
}
