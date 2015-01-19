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
using System.Collections.Generic;
using System.Linq;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public struct ItemKey : IEquatable<ItemKey>
	{
		public readonly DynamoDBKeyValue HashKey;
		public readonly DynamoDBKeyValue RangeKey;

		public ItemKey(DynamoDBKeyValue hashKey)
		{
			if(hashKey == null)
				throw new ArgumentNullException("hashKey");

			HashKey = hashKey;
			RangeKey = null;
		}

		public ItemKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
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

		#region Composite

		public static char CompositeValueSeparator
		{
			get { return compositeValueSeparator; }
			set
			{
				compositeValueSeparator = value;
				separatorString = new string(value, 1);
				separatorArray = new[] { value };
			}
		}
		private static char compositeValueSeparator;
		private static string separatorString;
		private static char[] separatorArray;

		static ItemKey()
		{
			CompositeValueSeparator = ';';
		}

		public static CompositeHashKey CompositeHashKey(params DynamoDBKeyValue[] hashKey)
		{
			if(hashKey == null || hashKey.Length == 0)
				throw new ArgumentException("Must provide a key value", "hashKey");

			return new CompositeHashKey(CompositeValue(hashKey));
		}

		internal static string CompositeValue(DynamoDBKeyValue[] values)
		{
			return string.Join(separatorString, values.Select(v => v != null ? v.ToString() : null));
		}

		public static string CompositeName(params string[] names)
		{
			return string.Join(separatorString, names);
		}
		#endregion

		public static string[] Split(DynamoDBString value)
		{
			return value.ToString().Split(separatorArray);
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
	}
}
