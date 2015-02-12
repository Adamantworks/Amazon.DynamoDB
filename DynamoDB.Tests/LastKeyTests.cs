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
using Adamantworks.Amazon.DynamoDB.Schema;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class LastKeyTests
	{
		[Test]
		public void ToAws_Table()
		{
			var schema = new KeySchema("ID", DynamoDBValueType.String, "Range", DynamoDBValueType.Number);
			var key = new LastKey("Hello", 42);
			var value = key.ToAws(schema, null);
			Assert.AreEqual(2, value.Count, "Count");
			Assert.AreEqual("Hello", value["ID"].S);
			Assert.AreEqual("42", value["Range"].N);
		}
		[Test]
		public void ToAws_TableNoRangeKey()
		{
			var schema = new KeySchema("ID", DynamoDBValueType.String);
			var key = new LastKey("Hello");
			var value = key.ToAws(schema, null);
			Assert.AreEqual(1, value.Count, "Count");
			Assert.IsTrue(value.ContainsKey("ID"));
			Assert.AreEqual("Hello", value["ID"].S);
		}
		[Test]
		public void ToAws_Index()
		{
			var tableSchema = new KeySchema("H", DynamoDBValueType.String, "R", DynamoDBValueType.Number);
			var indexSchema = new KeySchema("IH", DynamoDBValueType.String, "IR", DynamoDBValueType.Number);
			var key = new LastKey("Hello", 42, "Something", 52);
			var value = key.ToAws(tableSchema, indexSchema);
			Assert.AreEqual(4, value.Count, "Count");
			Assert.AreEqual("Hello", value["H"].S);
			Assert.AreEqual("42", value["R"].N);
			Assert.AreEqual("Something", value["IH"].S);
			Assert.AreEqual("52", value["IR"].N);
		}
		[Test]
		public void ToAws_LocalIndex()
		{
			var tableSchema = new KeySchema("H", DynamoDBValueType.String, "R", DynamoDBValueType.Number);
			var indexSchema = new KeySchema("H", DynamoDBValueType.String, "IR", DynamoDBValueType.Number);
			var key = new LastKey("Hello", 42, null, 52);
			var value = key.ToAws(tableSchema, indexSchema);
			Assert.AreEqual(3, value.Count, "Count");
			Assert.AreEqual("Hello", value["H"].S);
			Assert.AreEqual("42", value["R"].N);
			Assert.AreEqual("52", value["IR"].N);
		}
		[Test]
		public void ToAws_SharedIndex()
		{
			var tableSchema = new KeySchema("H", DynamoDBValueType.String, "R", DynamoDBValueType.Number);
			var indexSchema = new KeySchema("R", DynamoDBValueType.Number, "H", DynamoDBValueType.String);
			var key = new LastKey("Hello", 42);
			var value = key.ToAws(tableSchema, indexSchema);
			Assert.AreEqual(2, value.Count, "Count");
			Assert.AreEqual("Hello", value["H"].S);
			Assert.AreEqual("42", value["R"].N);
		}
		[Test]
		public void ToAws_IndexAndTableNoRangeKey()
		{
			var tableSchema = new KeySchema("H", DynamoDBValueType.String);
			var indexSchema = new KeySchema("IH", DynamoDBValueType.String, "IR", DynamoDBValueType.Number);
			var key = new LastKey("Hello", null, "i-hash", 52);
			var value = key.ToAws(tableSchema, indexSchema);
			Assert.AreEqual(3, value.Count, "Count");
			Assert.AreEqual("Hello", value["H"].S);
			Assert.AreEqual("i-hash", value["IH"].S);
			Assert.AreEqual("52", value["IR"].N);
		}
	}
}
