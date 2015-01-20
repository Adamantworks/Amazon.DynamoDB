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
using System.Reflection;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Schema;
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	internal static class AwsExtensions
	{
		private static readonly object[] NoArgs = new object[0];

		public static DynamoDBMap ToValue(this Dictionary<string, Aws.AttributeValue> values)
		{
			return new DynamoDBMap(values);
		}

		public static DynamoDBMap ToGetValue(this Dictionary<string, Aws.AttributeValue> values)
		{
			if(values == null || values.Count == 0) return null;
			return new DynamoDBMap(values);
		}

		public static DynamoDBValue ToValue(this Aws.AttributeValue value)
		{
			if(value.NULL)
				return null;
			if(value.S != null)
				return new DynamoDBString(value.S);
			if(value.N != null)
				return new DynamoDBNumber(value.N);
			if(value.B != null)
				return new DynamoDBBinary(value.B.ToArray());
			if(value.SS != null && value.SS.Count != 0)
				return new DynamoDBSet<DynamoDBString>(value.SS.Select(v => new DynamoDBString(v)));
			if(value.NS != null && value.NS.Count != 0)
				return new DynamoDBSet<DynamoDBNumber>(value.NS.Select(v => new DynamoDBNumber(v)));
			if(value.BS != null && value.BS.Count != 0)
				return new DynamoDBSet<DynamoDBBinary>(value.BS.Select(v => new DynamoDBBinary(v.ToArray())));

			// For performance reasons, we check for these cases before going to reflection
			if(value.L != null && value.L.Count != 0)
				return new DynamoDBList(value.L.Select(ToValue));
			if(value.M != null && value.M.Count != 0)
				return new DynamoDBMap(value.M);
			if(value.BOOL)
				return (DynamoDBBoolean)value.BOOL;

			// Now we have to start using reflection because the rest are indistinguishable through the public API
			if(IsBool(value))
				return (DynamoDBBoolean)value.BOOL;
			if(value.L != null)
				return new DynamoDBList(value.L.Select(ToValue));
			if(value.M != null)
				return new DynamoDBMap(value.M);

			throw new NotSupportedException("AttributeValue type is not supported");
		}

		private static bool IsBool(Aws.AttributeValue value)
		{
			return (bool)typeof(Aws.AttributeValue).InvokeMember("IsSetBOOL", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, value, NoArgs);
		}

		public static TableSchema ToSchema(this Aws.TableDescription tableDescription)
		{
			var globalIndexes = tableDescription.GlobalSecondaryIndexes.Select(i => new KeyValuePair<string, IndexSchema>(i.IndexName, i.ToSchema(tableDescription)));
			var localIndexes = tableDescription.LocalSecondaryIndexes.Select(i => new KeyValuePair<string, IndexSchema>(i.IndexName, i.ToSchema(tableDescription)));

			return new TableSchema(tableDescription.KeySchema.ToSchema(tableDescription.AttributeDefinitions),
				globalIndexes.Concat(localIndexes).ToDictionary(e => e.Key, e => e.Value));
		}

		public static IndexSchema ToSchema(this Aws.GlobalSecondaryIndexDescription indexDescription, Aws.TableDescription tableDescription)
		{
			return new IndexSchema(true, indexDescription.KeySchema.ToSchema(tableDescription.AttributeDefinitions));
		}
		public static IndexSchema ToSchema(this Aws.LocalSecondaryIndexDescription indexDescription, Aws.TableDescription tableDescription)
		{
			return new IndexSchema(false, indexDescription.KeySchema.ToSchema(tableDescription.AttributeDefinitions));
		}

		public static KeySchema ToSchema(this List<Aws.KeySchemaElement> keySchema, List<Aws.AttributeDefinition> attributeDefinitions)
		{
			var hashKeyName = keySchema.Where(k => k.KeyType == AwsEnums.KeyType.HASH).Select(k => k.AttributeName).Single();
			var rangeKeyName = keySchema.Where(k => k.KeyType == AwsEnums.KeyType.RANGE).Select(k => k.AttributeName).SingleOrDefault();
			var hashKey = attributeDefinitions.Single(a => a.AttributeName == hashKeyName).ToSchema();
			var rangeKey = rangeKeyName == null ? null : attributeDefinitions.Single(a => a.AttributeName == rangeKeyName).ToSchema();
			return new KeySchema(hashKey, rangeKey);
		}

		public static AttributeSchema ToSchema(this Aws.AttributeDefinition attributeDefinition)
		{
			return new AttributeSchema(attributeDefinition.AttributeName, attributeDefinition.AttributeType.ToDynamoDBValueType());
		}

		public static DynamoDBValueType ToDynamoDBValueType(this AwsEnums.ScalarAttributeType attributeType)
		{
			if(attributeType == AwsEnums.ScalarAttributeType.S)
				return DynamoDBValueType.String;
			if(attributeType == AwsEnums.ScalarAttributeType.N)
				return DynamoDBValueType.Number;
			if(attributeType == AwsEnums.ScalarAttributeType.B)
				return DynamoDBValueType.Binary;

			throw new NotSupportedException(string.Format("ScalarAttributeType '{0}' not supported", attributeType.Value));
		}

		public static TableStatus ToTableStatus(this AwsEnums.TableStatus tableStatus)
		{
			if(tableStatus == AwsEnums.TableStatus.ACTIVE)
				return TableStatus.Active;
			if(tableStatus == AwsEnums.TableStatus.UPDATING)
				return TableStatus.Updating;
			if(tableStatus == AwsEnums.TableStatus.CREATING)
				return TableStatus.Creating;
			if(tableStatus == AwsEnums.TableStatus.DELETING)
				return TableStatus.Deleting;

			throw new NotSupportedException(string.Format("TableStatus '{0}' not supported", tableStatus.Value));
		}

		public static TableStatus ToTableStatus(this AwsEnums.IndexStatus indexStatus)
		{
			if(indexStatus == AwsEnums.IndexStatus.ACTIVE)
				return TableStatus.Active;
			if(indexStatus == AwsEnums.IndexStatus.UPDATING)
				return TableStatus.Updating;
			if(indexStatus == AwsEnums.IndexStatus.CREATING)
				return TableStatus.Creating;
			if(indexStatus == AwsEnums.IndexStatus.DELETING)
				return TableStatus.Deleting;

			throw new NotSupportedException(string.Format("IndexStatus '{0}' not supported", indexStatus.Value));
		}

		public static ProvisionedThroughputInfo ToInfo(this Aws.ProvisionedThroughputDescription p)
		{
			// TODO make sure DateTimes are UTC
			return new ProvisionedThroughputInfo(p.LastDecreaseDateTime, p.LastIncreaseDateTime, p.NumberOfDecreasesToday, p.ReadCapacityUnits, p.WriteCapacityUnits);
		}

		public static ItemKey ToItemKey(this Dictionary<string, Aws.AttributeValue> values, KeySchema key)
		{
			var hashKeyValue = values[key.HashKey.Name];
			var rangeKeyValue = key.RangeKey != null ? values[key.RangeKey.Name] : null;
			return new ItemKey(hashKeyValue.ToDynamoDBKeyValue(), rangeKeyValue.ToDynamoDBKeyValue());
		}
	}
}
