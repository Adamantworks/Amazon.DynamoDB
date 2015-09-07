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