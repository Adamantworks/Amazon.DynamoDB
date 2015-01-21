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
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class DynamoDBValueConverterTests
	{
		[Test]
		public void NullToString()
		{
			DynamoDBValue value = null;
			Assert.AreEqual(null, value.To<string>());
		}

		[Test]
		[TestCase(true)]
		[TestCase(false)]
		public void ToBool(bool value)
		{
			DynamoDBValue dynamoDBValue = (DynamoDBBoolean)value;
			Assert.AreEqual(value, dynamoDBValue.To<bool>());
		}

		[Test]
		public void SetOfNumberToSetOfInt()
		{
			ISet<int> expected = new HashSet<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
			var values = new DynamoDBSet<DynamoDBNumber>(expected.Select(v => (DynamoDBNumber)v));

			Assert.IsTrue(expected.SetEquals(values.To<ISet<int>>()));
		}

		[Test]
		public void NullToSetOfInt()
		{
			DynamoDBValue value = null;
			Assert.IsNull(value.To<ISet<int>>());
		}

		[Test]
		public void SetOfStringToSetOfGuid()
		{
			ISet<Guid> expected = new HashSet<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
			var values = new DynamoDBSet<DynamoDBString>(expected.Select(v => (DynamoDBString)v.ToString()));

			Assert.IsTrue(expected.SetEquals(values.To<ISet<Guid>>()));
		}
	}
}
