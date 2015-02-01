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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Schema
{
	public class KeySchema
	{
		public readonly AttributeSchema HashKey;
		public readonly AttributeSchema RangeKey;

		public KeySchema(AttributeSchema hashKey)
		{
			if(hashKey == null)
				throw new ArgumentNullException("hashKey");
			if(!hashKey.Type.IsKeyValue())
				throw new ArgumentException("Must by a KeyValue type", "hashKey");

			HashKey = hashKey;
		}

		public KeySchema(string hashKeyName, DynamoDBValueType hashKeyType)
			: this(new AttributeSchema(hashKeyName, hashKeyType))
		{
		}

		public KeySchema(AttributeSchema hashKey, AttributeSchema rangeKey)
			: this(hashKey)
		{
			if(rangeKey != null && !hashKey.Type.IsKeyValue())
				throw new ArgumentException("Must by a KeyValue type", "rangeKey");

			RangeKey = rangeKey;
		}

		public KeySchema(AttributeSchema hashKey, string rangeKeyName, DynamoDBValueType rangeKeyType)
			: this(hashKey, new AttributeSchema(rangeKeyName, rangeKeyType))
		{
		}

		public KeySchema(string hashKeyName, DynamoDBValueType hashKeyType, AttributeSchema rangeKey)
			: this(new AttributeSchema(hashKeyName, hashKeyType), rangeKey)
		{
		}

		public KeySchema(string hashKeyName, DynamoDBValueType hashKeyType, string rangeKeyName, DynamoDBValueType rangeKeyType)
			: this(new AttributeSchema(hashKeyName, hashKeyType), new AttributeSchema(rangeKeyName, rangeKeyType))
		{
		}

		internal List<Aws.KeySchemaElement> ToAws()
		{
			var keys = new List<Aws.KeySchemaElement>()
			{
				new Aws.KeySchemaElement(HashKey.Name, AwsEnums.KeyType.HASH)
			};
			if(RangeKey != null)
				keys.Add(new Aws.KeySchemaElement(RangeKey.Name, AwsEnums.KeyType.RANGE));
			return keys;
		}

		public ItemKey GetKey(DynamoDBMap item)
		{
			var hashKeyValue = (DynamoDBKeyValue)item[HashKey.Name];
			var rangeKeyValue = (DynamoDBKeyValue)(RangeKey != null ? item[RangeKey.Name] : null);
			return ItemKey.Create(hashKeyValue, rangeKeyValue);
		}
	}
}
