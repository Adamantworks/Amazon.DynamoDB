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

using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Schema;
using System;
using System.Collections.Generic;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public struct ItemKey : IEquatable<ItemKey>
	{
		public readonly DynamoDBKeyValue HashKey;
		public readonly DynamoDBKeyValue RangeKey;

		private ItemKey(DynamoDBKeyValue hashKey)
		{
			if(hashKey == null)
				throw new ArgumentNullException("hashKey");

			HashKey = hashKey;
			RangeKey = null;
		}
		private ItemKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			if(hashKey == null)
				throw new ArgumentNullException("hashKey");

			HashKey = hashKey;
			RangeKey = rangeKey;
		}

		internal Dictionary<string, Aws.AttributeValue> ToAws(KeySchema keySchema)
		{
			var value = new Dictionary<string, Aws.AttributeValue>() { { keySchema.HashKey.Name, HashKey.ToAws() } };
			if(RangeKey != null)
			{
				if(keySchema.RangeKey == null)
					throw new InvalidOperationException("Can't specify range key for table or index without a range key");
				value.Add(keySchema.RangeKey.Name, RangeKey.ToAws());
			}
			return value;
		}

		public override bool Equals(object obj)
		{
			var itemKey = obj as ItemKey?;
			return itemKey != null && Equals(itemKey.Value);
		}

		public bool Equals(ItemKey other)
		{
			return HashKey.Equals(other.HashKey) && Equals(RangeKey, other.RangeKey);
		}

		public override int GetHashCode()
		{
			var hash = HashKey.GetHashCode();
			if(RangeKey != null)
				hash ^= RangeKey.GetHashCode();
			return hash;
		}

		public static ItemKey CreateStrict(DynamoDBKeyValue hashKey)
		{
			return new ItemKey(hashKey);
		}
		public static ItemKey CreateStrict(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return new ItemKey(hashKey, rangeKey);
		}
		public static ItemKey Create<THash>(THash hashKey)
		{
			return new ItemKey(DynamoDBKeyValue.Convert(hashKey));
		}
		public static ItemKey Create<THash>(THash hashKey, IValueConverter converter)
		{
			return new ItemKey(DynamoDBKeyValue.Convert(hashKey, converter));
		}
		public static ItemKey Create<THash, TRange>(THash hashKey, TRange rangeKey)
		{
			return new ItemKey(DynamoDBKeyValue.Convert(hashKey), DynamoDBKeyValue.Convert(rangeKey));
		}
		public static ItemKey Create<THash, TRange>(THash hashKey, TRange rangeKey, IValueConverter converter)
		{
			return new ItemKey(DynamoDBKeyValue.Convert(hashKey, converter), DynamoDBKeyValue.Convert(rangeKey, converter));
		}
	}
}
