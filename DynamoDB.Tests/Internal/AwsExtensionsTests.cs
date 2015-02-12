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
using System.Globalization;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using NUnit.Framework;
using System.Collections.Generic;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Tests.Internal
{
	[TestFixture]
	public class AwsExtensionsTests
	{
		[Test]
		[TestCase(true)]
		[TestCase(false)]
		public void ToValue_Bool(bool boolValue)
		{
			var value = new Aws.AttributeValue() { BOOL = boolValue };
			var expected = (DynamoDBBoolean)boolValue;
			Assert.AreEqual(expected, value.ToValue());
		}

		[Test]
		public void ToValue_String()
		{
			var value = new Aws.AttributeValue() { S = "hello" };
			var expected = (DynamoDBString)"hello";
			Assert.AreEqual(expected, value.ToValue());
		}

		[Test]
		[Ignore("This is failing due to a bug in AWS SDK")]
		public void ToValue_EquivalenceOfEmpty()
		{
			var boolValue = new Aws.AttributeValue() { BOOL = false };
			var listValue = new Aws.AttributeValue() { L = new List<Aws.AttributeValue>() };
			var mapValue = new Aws.AttributeValue() { M = new Dictionary<string, Aws.AttributeValue>() };
			Assert.IsInstanceOf<DynamoDBBoolean>(boolValue.ToValue());
			Assert.IsInstanceOf<DynamoDBList>(listValue.ToValue());
			Assert.IsInstanceOf<DynamoDBMap>(mapValue.ToValue());
		}

		[Test]
		public void ToItemKey_Types()
		{
			const string stringValue = "StringValue";
			const int numberValue = 42;

			var key = new Dictionary<string, Aws.AttributeValue>
			{
				{"Hash", new Aws.AttributeValue(stringValue)},
				{"Range", new Aws.AttributeValue() {N = numberValue.ToString(CultureInfo.InvariantCulture)}},
			};
			var expected = ItemKey.Create(stringValue, numberValue);
			Assert.AreEqual(expected, key.ToItemKey(new KeySchema("Hash", DynamoDBValueType.String, "Range", DynamoDBValueType.Number)));

			var binaryValue = (DynamoDBBinary)new byte[] { 1, 2, 3, 4 };
			key = new Dictionary<string, Aws.AttributeValue>()
			{
				{"Hash", new Aws.AttributeValue() {B = binaryValue.ToMemoryStream()}}
			};
			expected = ItemKey.Create(binaryValue);
			Assert.AreEqual(expected, key.ToItemKey(new KeySchema("Hash", DynamoDBValueType.Binary)));
		}

		[Test]
		public void ToItemKey_TypeMismatch()
		{
			var key = new Dictionary<string, Aws.AttributeValue>
			{
				{"ID", new Aws.AttributeValue("StringValue")},
			};
			var schema = new KeySchema("ID", DynamoDBValueType.Number);
			Assert.Throws<InvalidCastException>(() => key.ToItemKey(schema));
		}

		[Test]
		public void ToItemKey()
		{
			var schema = new KeySchema("H", DynamoDBValueType.String, "R", DynamoDBValueType.Number);
			var awsKey = new Dictionary<string, Aws.AttributeValue>()
			{
				{"H", new Aws.AttributeValue("hash")},
				{"R", new Aws.AttributeValue() {N = "42"}}
			};

			Assert.AreEqual(ItemKey.Create("hash", 42), awsKey.ToItemKey(schema));
		}
		[Test]
		public void ToItemKey_NoRangeKey()
		{
			var schema = new KeySchema("H", DynamoDBValueType.String);
			var awsKey = new Dictionary<string, Aws.AttributeValue>()
			{
				{"H", new Aws.AttributeValue("hash")},
			};

			Assert.AreEqual(ItemKey.Create("hash"), awsKey.ToItemKey(schema));
		}

		[Test]
		public void ToLastKey_Table()
		{
			var schema = new KeySchema("H", DynamoDBValueType.String, "R", DynamoDBValueType.Number);
			var awsKey = new Dictionary<string, Aws.AttributeValue>()
			{
				{"H", new Aws.AttributeValue("hash")},
				{"R", new Aws.AttributeValue() {N = "42"}}
			};

			Assert.AreEqual(new LastKey("hash", 42), awsKey.ToLastKey(schema, null));
		}
		[Test]
		public void ToLastKey_TableNoRangeKey()
		{
			var schema = new KeySchema("H", DynamoDBValueType.String);
			var awsKey = new Dictionary<string, Aws.AttributeValue>()
			{
				{"H", new Aws.AttributeValue("hash")},
			};

			Assert.AreEqual(new LastKey("hash"), awsKey.ToLastKey(schema, null));
		}
		[Test]
		public void ToLastKey_Index()
		{
			var tableSchema = new KeySchema("H", DynamoDBValueType.String, "R", DynamoDBValueType.Number);
			var indexSchema = new KeySchema("IH", DynamoDBValueType.String, "IR", DynamoDBValueType.Number);
			var awsKey = new Dictionary<string, Aws.AttributeValue>()
			{
				{"H", new Aws.AttributeValue("hash")},
				{"R", new Aws.AttributeValue() {N = "42"}},
				{"IH", new Aws.AttributeValue("i-hash")},
				{"IR", new Aws.AttributeValue() {N = "52"}},
			};

			Assert.AreEqual(new LastKey("hash", 42, "i-hash", 52), awsKey.ToLastKey(tableSchema, indexSchema));
		}
		[Test]
		public void ToLastKey_LocalIndex()
		{
			var tableSchema = new KeySchema("H", DynamoDBValueType.String, "R", DynamoDBValueType.Number);
			var indexSchema = new KeySchema("H", DynamoDBValueType.String, "IR", DynamoDBValueType.Number);
			var awsKey = new Dictionary<string, Aws.AttributeValue>()
			{
				{"H", new Aws.AttributeValue("hash")},
				{"R", new Aws.AttributeValue() {N = "42"}},
				{"IR", new Aws.AttributeValue() {N = "52"}},
			};

			Assert.AreEqual(new LastKey("hash", 42, null, 52), awsKey.ToLastKey(tableSchema, indexSchema));
		}
		[Test]
		public void ToLastKey_SharedIndex()
		{
			var tableSchema = new KeySchema("H", DynamoDBValueType.String, "R", DynamoDBValueType.Number);
			var indexSchema = new KeySchema("R", DynamoDBValueType.Number, "H", DynamoDBValueType.String);
			var awsKey = new Dictionary<string, Aws.AttributeValue>()
			{
				{"H", new Aws.AttributeValue("hash")},
				{"R", new Aws.AttributeValue() {N = "42"}},
			};

			Assert.AreEqual(new LastKey("hash", 42, null, null), awsKey.ToLastKey(tableSchema, indexSchema));
		}
		[Test]
		public void ToLastKey_IndexAndTableNoRangeKey()
		{
			var tableSchema = new KeySchema("H", DynamoDBValueType.String);
			var indexSchema = new KeySchema("IH", DynamoDBValueType.String, "IR", DynamoDBValueType.Number);
			var awsKey = new Dictionary<string, Aws.AttributeValue>()
			{
				{"H", new Aws.AttributeValue("hash")},
				{"R", new Aws.AttributeValue() {N = "42"}},
				{"IH", new Aws.AttributeValue("i-hash")},
				{"IR", new Aws.AttributeValue() {N = "52"}},
			};

			Assert.AreEqual(new LastKey("hash", null, "i-hash", 52), awsKey.ToLastKey(tableSchema, indexSchema));
		}
	}
}
