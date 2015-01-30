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

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public struct CompositeHashKeySyntax
	{
		private readonly DynamoDBKeyValue hashKey;

		internal CompositeHashKeySyntax(DynamoDBKeyValue hashKey)
		{
			this.hashKey = hashKey;
		}

		public  ItemKey RangeKeyStrict(DynamoDBKeyValue key1)
		{
			return ItemKey.CreateStrict(hashKey, key1);
		}
		public  ItemKey RangeKeyStrict(DynamoDBKeyValue key1, DynamoDBKeyValue key2)
		{
			return ItemKey.CreateStrict(hashKey, Composite.ValueStrict(key1, key2));
		}
		public  ItemKey RangeKeyStrict(DynamoDBKeyValue key1, DynamoDBKeyValue key2, DynamoDBKeyValue key3)
		{
			return ItemKey.CreateStrict(hashKey, Composite.ValueStrict(key1, key2, key3));
		}
		public  ItemKey RangeKeyStrict(DynamoDBKeyValue key1, DynamoDBKeyValue key2, DynamoDBKeyValue key3, DynamoDBKeyValue key4)
		{
			return ItemKey.CreateStrict(hashKey, Composite.ValueStrict(key1, key2, key3, key4));
		}

		public  ItemKey RangeKey<TKey1>(TKey1 key1)
		{
			return ItemKey.CreateStrict(hashKey, DynamoDBKeyValue.Convert(key1, DynamoDBValueConverter.DefaultComposite));
		}
		public  ItemKey RangeKey<TKey1>(TKey1 key1, IDynamoDBValueConverter converter)
		{
			return ItemKey.CreateStrict(hashKey, DynamoDBKeyValue.Convert(key1, converter));
		}
		public  ItemKey RangeKey<TKey1, TKey2>(TKey1 key1, TKey2 key2)
		{
			return ItemKey.CreateStrict(hashKey, Composite.Value(key1, key2));
		}
		public  ItemKey RangeKey<TKey1, TKey2>(TKey1 key1, TKey2 key2, IDynamoDBValueConverter converter)
		{
			return ItemKey.CreateStrict(hashKey, Composite.Value(key1, key2, converter));
		}
		public  ItemKey RangeKey<TKey1, TKey2, TKey3>(TKey1 key1, TKey2 key2, TKey3 key3)
		{
			return ItemKey.CreateStrict(hashKey, Composite.Value(key1, key2, key3));
		}
		public  ItemKey RangeKey<TKey1, TKey2, TKey3>(TKey1 key1, TKey2 key2, TKey3 key3, IDynamoDBValueConverter converter)
		{
			return ItemKey.CreateStrict(hashKey, Composite.Value(key1, key2, key3, converter));
		}
		public  ItemKey RangeKey<TKey1, TKey2, TKey3, TKey4>(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
		{
			return ItemKey.CreateStrict(hashKey, Composite.Value(key1, key2, key3, key4));
		}
		public  ItemKey RangeKey<TKey1, TKey2, TKey3, TKey4>(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, IDynamoDBValueConverter converter)
		{
			return ItemKey.CreateStrict(hashKey, Composite.Value(key1, key2, key3, key4, converter));
		}
	}
}
