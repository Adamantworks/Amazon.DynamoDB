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

using System.Collections.Generic;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using NUnit.Framework;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class ItemKeyTests
	{
		[Test]
		public void ToAws_NoRangeKey()
		{
			var schema = new KeySchema("ID", DynamoDBValueType.String);
			var key = ItemKey.Create("Hello");
			var value = key.ToAws(schema);
			Assert.AreEqual(1, value.Count, "Count");
			Assert.IsTrue(value.ContainsKey("ID"));
			Assert.AreEqual("Hello", value["ID"].S);
		}
	}
}
