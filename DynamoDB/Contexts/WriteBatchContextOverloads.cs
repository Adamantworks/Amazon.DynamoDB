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

using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Syntax.Delete;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class WriteBatchContext
	{
		#region IDeleteBatchItemSyntax.Item
		void IDeleteBatchItemSyntax.Item(DynamoDBKeyValue hashKey)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey));
		}
		void IDeleteBatchItemSyntax.Item(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		void IDeleteBatchItemSyntax.Item(object hashKey)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey));
		}
		void IDeleteBatchItemSyntax.Item(object hashKey, IValueConverter converter)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, converter));
		}
		void IDeleteBatchItemSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		void IDeleteBatchItemSyntax.Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		void IDeleteBatchItemSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		void IDeleteBatchItemSyntax.Item(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		void IDeleteBatchItemSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		void IDeleteBatchItemSyntax.Item(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		void IDeleteBatchItemSyntax.Item(object hashKey, object rangeKey)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey));
		}
		void IDeleteBatchItemSyntax.Item(object hashKey, object rangeKey, IValueConverter converter)
		{
			((IDeleteBatchItemSyntax)this).Item(ItemKey.Create(hashKey, rangeKey, converter));
		}
		#endregion
	}
}