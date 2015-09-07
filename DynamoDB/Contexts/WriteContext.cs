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
using Adamantworks.Amazon.DynamoDB.Syntax.Update;
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class WriteContext
		: IWriteConditionallySyntax,
		IUpdateOnItemAsyncSyntax, IUpdateOnItemSyntax, ITryUpdateOnItemAsyncSyntax, ITryUpdateOnItemSyntax,
		IDeleteItemAsyncSyntax, IDeleteItemSyntax, ITryDeleteItemAsyncSyntax, ITryDeleteItemSyntax
	{
		private readonly Table table;
		private readonly PredicateExpression condition;
		private readonly Values conditionValues;
		private UpdateExpression update;
		private Values values;
		private AwsEnums.ReturnValue returnValue;
		private CancellationToken? cancellationToken;

		// Called from If() and for Insert()
		public WriteContext(
			Table table,
			PredicateExpression condition,
			Values conditionValues)
		{
			this.table = table;

			this.condition = condition;
			this.conditionValues = conditionValues;
		}

		// Called from Put() and Delete()
		public WriteContext(
			Table table,
			bool returnOldItem,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			this.table = table;
			returnValue = returnOldItem ? AwsEnums.ReturnValue.ALL_OLD : AwsEnums.ReturnValue.NONE;
			this.cancellationToken = cancellationToken;
		}

		// Called from Update()
		public WriteContext(
			Table table,
			UpdateExpression update,
			Values values,
			UpdateReturnValue returnValue,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			this.table = table;
			this.update = update;
			this.values = values;
			this.returnValue = returnValue.ToAws();
			this.cancellationToken = cancellationToken;
		}

		private void SafeSet(UpdateExpression update)
		{
			if(this.update != null)
				throw new NotSupportedException("Can't set update expression twice");

			this.update = update;
		}
		private void SafeSet(Values values)
		{
			if(this.values != null)
				throw new NotSupportedException("Can't set update values twice");

			this.values = values;
		}
		private void SafeSet(UpdateReturnValue returnValue)
		{
			if(this.returnValue != null)
				throw new NotSupportedException("Can't set return value twice");

			this.returnValue = returnValue.ToAws();
		}
		private void SafeSet(bool returnOldItem)
		{
			SafeSet(returnOldItem ? AwsEnums.ReturnValue.ALL_OLD : AwsEnums.ReturnValue.NONE);
		}
		private void SafeSet(AwsEnums.ReturnValue returnValue)
		{
			if(this.returnValue != null)
				throw new NotSupportedException("Can't set return value twice");

			this.returnValue = returnValue;
		}
		private void SafeSet(CancellationToken cancellationToken)
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

		#region Update
		public IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, Values values, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			SafeSet(update);
			SafeSet(values);
			SafeSet(returnValue);
			SafeSet(cancellationToken);
			return this;
		}
		async Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(ItemKey key)
		{
			var request = BuildUpdateRequest(key);
			var response = await table.Region.DB.UpdateItemAsync(request, cancellationToken.Value).ConfigureAwait(false);
			if(returnValue == AwsEnums.ReturnValue.NONE) return null;
			return response.Attributes.ToMap();
		}

		public IUpdateOnItemSyntax Update(UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			SafeSet(update);
			SafeSet(values);
			SafeSet(returnValue);
			SafeSet(CancellationToken.None);
			return this;
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(ItemKey key)
		{
			var request = BuildUpdateRequest(key);
			var response = table.Region.DB.UpdateItem(request);
			if(returnValue == AwsEnums.ReturnValue.NONE) return null;
			return response.Attributes.ToMap();
		}

		private Aws.UpdateItemRequest BuildUpdateRequest(ItemKey key)
		{
			var request = new Aws.UpdateItemRequest()
			{
				TableName = table.Name,
				Key = key.ToAws(table.Schema.Key),
				UpdateExpression = update.Expression,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(update, condition),
				ReturnValues = returnValue,
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(update, condition, conditionValues, values);

			return request;
		}
		#endregion

		#region TryUpdate
		public ITryUpdateOnItemAsyncSyntax TryUpdateAsync(UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			SafeSet(update);
			SafeSet(values);
			SafeSet(UpdateReturnValue.None);
			SafeSet(cancellationToken);
			return this;
		}
		async Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(ItemKey key)
		{
			try
			{
				await ((IUpdateOnItemAsyncSyntax)this).OnItem(key).ConfigureAwait(false);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}

		public ITryUpdateOnItemSyntax TryUpdate(UpdateExpression update, Values values)
		{
			SafeSet(update);
			SafeSet(values);
			SafeSet(UpdateReturnValue.None);
			SafeSet(CancellationToken.None);
			return this;
		}
		bool ITryUpdateOnItemSyntax.OnItem(ItemKey key)
		{
			try
			{
				((IUpdateOnItemSyntax)this).OnItem(key);
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
			SafeSet(returnOldItem);
			SafeSet(cancellationToken);
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
			SafeSet(returnOldItem);
			SafeSet(CancellationToken.None);
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
			SafeSet(false);
			SafeSet(cancellationToken);
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
			SafeSet(false);
			SafeSet(CancellationToken.None);
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
