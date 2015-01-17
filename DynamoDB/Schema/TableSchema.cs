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
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Schema
{
	public class TableSchema
	{
		public readonly KeySchema Key;
		public readonly IReadOnlyDictionary<string, IndexSchema> Indexes;

		public TableSchema(KeySchema key, IDictionary<string, IndexSchema> indexes = null)
		{
			if(key == null)
				throw new ArgumentNullException("key");

			Key = key;
			Indexes = indexes != null ? new Dictionary<string, IndexSchema>(indexes) : new Dictionary<string, IndexSchema>();
		}

		internal List<Aws.AttributeDefinition> ToAwsAttributeDefinitions()
		{
			var attributeDefinitions = new Dictionary<string, DynamoDBValueType>();
			Add(attributeDefinitions, Key);
			foreach(var index in Indexes.Values)
				Add(attributeDefinitions, index.Key);

			return attributeDefinitions.Select(a => new Aws.AttributeDefinition(a.Key, a.Value.ToAws())).ToList();
		}

		private static void Add(IDictionary<string, DynamoDBValueType> attributeDefinitions, KeySchema key)
		{
			Add(attributeDefinitions, key.HashKey);
			if(key.RangeKey != null)
				Add(attributeDefinitions, key.RangeKey);
		}

		private static void Add(IDictionary<string, DynamoDBValueType> attributeDefinitions, AttributeSchema attribute)
		{
			DynamoDBValueType type;
			if(!attributeDefinitions.TryGetValue(attribute.Name, out type))
				attributeDefinitions.Add(attribute.Name, attribute.Type);
			else if(type != attribute.Type)
				throw new Exception(string.Format("Multiple types defined for attribute '{0}' in schema", attribute.Name));
		}
	}
}
