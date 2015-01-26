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
using System;
using System.Collections.Generic;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Schema
{
	public class AttributeSchema
	{
		public readonly string Name;
		public readonly DynamoDBValueType Type;

		public AttributeSchema(string name, DynamoDBValueType type)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Invalid name", "name");

			Name = name;
			Type = type;
		}

		internal void Between(DynamoDBKeyValue start, DynamoDBKeyValue end, Dictionary<string, Aws.Condition> keyConditions)
		{
			if(start.Type != Type)
				throw new InvalidOperationException(string.Format("Can't provide {0} value for key {1} of type {2}", start.Type, Name, Type));
			if(end.Type != Type)
				throw new InvalidOperationException(string.Format("Can't provide {0} value for key {1} of type {2}", end.Type, Name, Type));

			keyConditions.Add(Name, new Aws.Condition() { ComparisonOperator = "BETWEEN ", AttributeValueList = new List<Aws.AttributeValue>() { start.ToAws(), end.ToAws() } });
		}
	}
}
