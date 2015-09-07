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
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Syntax.Delete;
using Adamantworks.Amazon.DynamoDB.Syntax.Update;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class WriteContext
	{
		#region PutAsync
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item)
		{
			return PutAsync(item, false, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem)
		{
			return PutAsync(item, returnOldItem, CancellationToken.None);
		}
		public Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			return PutAsync(item, false, cancellationToken);
		}
		#endregion

		#region Put
		public DynamoDBMap Put(DynamoDBMap item)
		{
			return Put(item, false);
		}
		#endregion

		#region UpdateAsync
		public IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update)
		{
			return UpdateAsync(update, null, UpdateReturnValue.None, CancellationToken.None);
		}
		public IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, Values values)
		{
			return UpdateAsync(update, values, UpdateReturnValue.None, CancellationToken.None);
		}
		public IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, UpdateReturnValue returnValue)
		{
			return UpdateAsync(update, null, returnValue, CancellationToken.None);
		}
		public IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, Values values, UpdateReturnValue returnValue)
		{
			return UpdateAsync(update, values, returnValue, CancellationToken.None);
		}
		public IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, CancellationToken cancellationToken)
		{
			return UpdateAsync(update, null, UpdateReturnValue.None, cancellationToken);
		}
		public IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return UpdateAsync(update, values, UpdateReturnValue.None, cancellationToken);
		}
		public IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken)
		{
			return UpdateAsync(update, null, returnValue, cancellationToken);
		}
		#endregion

		#region IUpdateOnItemAsyncSyntax.OnItem
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, converter));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(object hashKey)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(object hashKey, IValueConverter converter)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, converter));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(object hashKey, object rangeKey)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		Task<DynamoDBMap> IUpdateOnItemAsyncSyntax.OnItem(object hashKey, object rangeKey, IValueConverter converter)
		{
			return ((IUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion

		#region Update
		public IUpdateOnItemSyntax Update(UpdateExpression update)
		{
			return Update(update, null, UpdateReturnValue.None);
		}
		public IUpdateOnItemSyntax Update(UpdateExpression update, Values values)
		{
			return Update(update, values, UpdateReturnValue.None);
		}
		public IUpdateOnItemSyntax Update(UpdateExpression update, UpdateReturnValue returnValue)
		{
			return Update(update, null, returnValue);
		}
		#endregion

		#region IUpdateOnItemSyntax.OnItem
		DynamoDBMap IUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, converter));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(object hashKey)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(object hashKey, IValueConverter converter)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, converter));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(object hashKey, object rangeKey)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		DynamoDBMap IUpdateOnItemSyntax.OnItem(object hashKey, object rangeKey, IValueConverter converter)
		{
			return ((IUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion

		#region TryUpdateAsync
		public ITryUpdateOnItemAsyncSyntax TryUpdateAsync(UpdateExpression update)
		{
			return TryUpdateAsync(update, null, CancellationToken.None);
		}
		public ITryUpdateOnItemAsyncSyntax TryUpdateAsync(UpdateExpression update, Values values)
		{
			return TryUpdateAsync(update, values, CancellationToken.None);
		}
		public ITryUpdateOnItemAsyncSyntax TryUpdateAsync(UpdateExpression update, CancellationToken cancellationToken)
		{
			return TryUpdateAsync(update, null, cancellationToken);
		}
		#endregion

		#region ITryUpdateOnItemAsyncSyntax.OnItem
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, converter));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(object hashKey)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(object hashKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, converter));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(object hashKey, object rangeKey)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		Task<bool> ITryUpdateOnItemAsyncSyntax.OnItem(object hashKey, object rangeKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemAsyncSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion

		#region TryUpdate
		public ITryUpdateOnItemSyntax TryUpdate(UpdateExpression update)
		{
			return TryUpdate(update, null);
		}
		#endregion

		#region ITryUpdateOnItemSyntax.OnItem
		bool ITryUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey));
		}
		bool ITryUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, converter));
		}
		bool ITryUpdateOnItemSyntax.OnItem(object hashKey)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey));
		}
		bool ITryUpdateOnItemSyntax.OnItem(object hashKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, converter));
		}
		bool ITryUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		bool ITryUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		bool ITryUpdateOnItemSyntax.OnItem(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		bool ITryUpdateOnItemSyntax.OnItem(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		bool ITryUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		bool ITryUpdateOnItemSyntax.OnItem(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		bool ITryUpdateOnItemSyntax.OnItem(object hashKey, object rangeKey)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey));
		}
		bool ITryUpdateOnItemSyntax.OnItem(object hashKey, object rangeKey, IValueConverter converter)
		{
			return ((ITryUpdateOnItemSyntax)this).OnItem(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion

		#region DeleteAsync
		public IDeleteItemAsyncSyntax DeleteAsync()
		{
			return DeleteAsync(false, CancellationToken.None);
		}
		public IDeleteItemAsyncSyntax DeleteAsync(bool returnOldItem)
		{
			return DeleteAsync(returnOldItem, CancellationToken.None);
		}
		public IDeleteItemAsyncSyntax DeleteAsync(CancellationToken cancellationToken)
		{
			return DeleteAsync(false, cancellationToken);
		}
		#endregion

		#region IDeleteItemAsyncSyntax.Item
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(object hashKey)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(object hashKey, IValueConverter converter)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(object hashKey, object rangeKey)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		Task<DynamoDBMap> IDeleteItemAsyncSyntax.Item(object hashKey, object rangeKey, IValueConverter converter)
		{
			return ((IDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion

		#region Delete
		public IDeleteItemSyntax Delete()
		{
			return Delete(false);
		}
		#endregion

		#region IDeleteItemSyntax.Item
		DynamoDBMap IDeleteItemSyntax.Item(DynamoDBKeyValue hashKey)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey));
		}
		DynamoDBMap IDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		DynamoDBMap IDeleteItemSyntax.Item(object hashKey)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey));
		}
		DynamoDBMap IDeleteItemSyntax.Item(object hashKey, IValueConverter converter)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		DynamoDBMap IDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		DynamoDBMap IDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		DynamoDBMap IDeleteItemSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		DynamoDBMap IDeleteItemSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		DynamoDBMap IDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		DynamoDBMap IDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		DynamoDBMap IDeleteItemSyntax.Item(object hashKey, object rangeKey)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		DynamoDBMap IDeleteItemSyntax.Item(object hashKey, object rangeKey, IValueConverter converter)
		{
			return ((IDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion

		#region TryDeleteAsync
		public ITryDeleteItemAsyncSyntax TryDeleteAsync()
		{
			return TryDeleteAsync(CancellationToken.None);
		}
		#endregion

		#region ITryDeleteItemAsyncSyntax.Item
		Task<bool> ITryDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(object hashKey)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(object hashKey, IValueConverter converter)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(object hashKey, object rangeKey)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		Task<bool> ITryDeleteItemAsyncSyntax.Item(object hashKey, object rangeKey, IValueConverter converter)
		{
			return ((ITryDeleteItemAsyncSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion

		#region TryDelete
		#endregion

		#region ITryDeleteItemSyntax.Item
		bool ITryDeleteItemSyntax.Item(DynamoDBKeyValue hashKey)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey));
		}
		bool ITryDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		bool ITryDeleteItemSyntax.Item(object hashKey)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey));
		}
		bool ITryDeleteItemSyntax.Item(object hashKey, IValueConverter converter)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		bool ITryDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		bool ITryDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		bool ITryDeleteItemSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		bool ITryDeleteItemSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		bool ITryDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		bool ITryDeleteItemSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		bool ITryDeleteItemSyntax.Item(object hashKey, object rangeKey)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		bool ITryDeleteItemSyntax.Item(object hashKey, object rangeKey, IValueConverter converter)
		{
			return ((ITryDeleteItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion
	}
}