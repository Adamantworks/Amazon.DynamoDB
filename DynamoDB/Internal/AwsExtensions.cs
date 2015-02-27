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
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	internal static class AwsExtensions
	{
		public static DynamoDBMap ToMap(this Dictionary<string, Aws.AttributeValue> values)
		{
			return new DynamoDBMap(values);
		}

		/// <summary>
		/// Needed so we can check IsItemSet
		/// </summary>
		public static DynamoDBMap GetItem(this Aws.GetItemResponse response)
		{
			return response.IsItemSet ? response.Item.ToMap() : null;
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

			if(value.IsBOOLSet)
				return (DynamoDBBoolean)value.BOOL;
			if(value.IsLSet)
				return new DynamoDBList(value.L.Select(ToValue));
			if(value.IsMSet)
				return new DynamoDBMap(value.M);

			// At this point we have exhausted all official means of determining the value of an AttributeValue.
			// We shouldn't get to this point.  However, if the AttributeValue was not correctly constructed by
			// setting IsLSet or IsMSet we could.  The value could be one of:
			//     new Aws.AttributeValue() { L = new List<Aws.AttributeValue>() };
			//     new Aws.AttributeValue() { M = new Dictionary<string, Aws.AttributeValue>() };
			// Because AWS SDK always initializes collections, we don't know.  But really, those are incorrect
			// uses of AttributeValue. So rather than accepting those values, are will consider this AttributeValue
			// invalid.

			throw new NotSupportedException("AttributeValue type is not supported");
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

		public static CollectionStatus ToCollectionStatus(this AwsEnums.TableStatus tableStatus)
		{
			if(tableStatus == AwsEnums.TableStatus.ACTIVE)
				return CollectionStatus.Active;
			if(tableStatus == AwsEnums.TableStatus.UPDATING)
				return CollectionStatus.Updating;
			if(tableStatus == AwsEnums.TableStatus.CREATING)
				return CollectionStatus.Creating;
			if(tableStatus == AwsEnums.TableStatus.DELETING)
				return CollectionStatus.Deleting;

			throw new NotSupportedException(string.Format("TableStatus '{0}' not supported", tableStatus.Value));
		}

		public static CollectionStatus ToCollectionStatus(this AwsEnums.IndexStatus indexStatus)
		{
			if(indexStatus == AwsEnums.IndexStatus.ACTIVE)
				return CollectionStatus.Active;
			if(indexStatus == AwsEnums.IndexStatus.UPDATING)
				return CollectionStatus.Updating;
			if(indexStatus == AwsEnums.IndexStatus.CREATING)
				return CollectionStatus.Creating;
			if(indexStatus == AwsEnums.IndexStatus.DELETING)
				return CollectionStatus.Deleting;

			throw new NotSupportedException(string.Format("IndexStatus '{0}' not supported", indexStatus.Value));
		}

		public static ProvisionedThroughputInfo ToInfo(this Aws.ProvisionedThroughputDescription p)
		{
			// TODO make sure DateTimes are UTC
			return new ProvisionedThroughputInfo(p.LastDecreaseDateTime, p.LastIncreaseDateTime, p.NumberOfDecreasesToday, p.ReadCapacityUnits, p.WriteCapacityUnits);
		}

		public static ItemKey ToItemKey(this Dictionary<string, Aws.AttributeValue> values, KeySchema schema)
		{
			var hashKeyValue = values.ToKeyValue(schema.HashKey, "Hash");
			var rangeKeyValue = values.ToKeyValue(schema.RangeKey, "Range");
			return ItemKey.Create(hashKeyValue, rangeKeyValue);
		}

		public static LastKey ToLastKey(this Dictionary<string, Aws.AttributeValue> values, KeySchema tableKeySchema, KeySchema indexKeySchema)
		{
			var tableHashKeyValue = values.ToKeyValue(tableKeySchema.HashKey, "Table hash");
			var tableRangeKeyValue = values.ToKeyValue(tableKeySchema.RangeKey, "Table range");
			var indexHashKeyValue = values.ToKeyValue(indexKeySchema != null ? indexKeySchema.HashKey : null, "Index hash", tableKeySchema);
			var indexRangeKeyValue = values.ToKeyValue(indexKeySchema != null ? indexKeySchema.RangeKey : null, "Index range", tableKeySchema);
			return new LastKey(tableHashKeyValue, tableRangeKeyValue, indexHashKeyValue, indexRangeKeyValue);
		}

		private static DynamoDBKeyValue ToKeyValue(this Dictionary<string, Aws.AttributeValue> values, AttributeSchema schema, string keyType, KeySchema primaryKeySchema = null)
		{
			if(schema == null
				|| (primaryKeySchema != null && (schema.Name == primaryKeySchema.HashKey.Name || (primaryKeySchema.RangeKey != null && schema.Name == primaryKeySchema.RangeKey.Name))))
				return null;
			var value = values[schema.Name].ToValue();
			if(value.Type != schema.Type)
				throw new InvalidCastException(keyType + " key type does not match schema");
			return (DynamoDBKeyValue)value;
		}

		public static void AddKey(this Dictionary<string, Aws.AttributeValue> key, AttributeSchema schema, DynamoDBKeyValue value, KeySchema primaryKeySchema = null)
		{
			if(schema != null
			   && (primaryKeySchema != null && (schema.Name == primaryKeySchema.HashKey.Name || (primaryKeySchema.RangeKey != null && schema.Name == primaryKeySchema.RangeKey.Name))))
			{
				// Shared key attribute
				if(value != null)
					throw new InvalidOperationException(string.Format("Do not provide value for index key attribute '{0}' that is also a table key", schema.Name));
				return; // Don't try to add again
			}
			if(value != null)
			{
				if(schema == null)
					throw new InvalidOperationException("Can't specify key value for table/index without the given key");
				if(value.Type != schema.Type)
					throw new InvalidCastException("Key value type does not match schema");
				key.Add(schema.Name, value.ToAws());
			}
			else if(schema != null)
				throw new InvalidOperationException(string.Format("Must specify value for key '{0}'", schema.Name));
		}
	}
}
