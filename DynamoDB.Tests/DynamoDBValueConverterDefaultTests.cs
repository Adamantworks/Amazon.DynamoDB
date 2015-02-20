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
	public class DynamoDBValueConverterDefaultTests
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

		[Test]
		public void HashSetOfGuidToSetOfString()
		{
			var values = new HashSet<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
			var value = DynamoDBValue.Convert(values);
			Assert.IsInstanceOf<DynamoDBSet<DynamoDBString>>(value);
			Assert.AreEqual(values.Count, ((DynamoDBSet<DynamoDBString>)value).Count);
		}


		[Test]
		public void ListOfNumberToListOfInt()
		{
			IList<int> expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
			var values = new DynamoDBList(expected.Select(v => (DynamoDBNumber)v));

			CollectionAssert.AreEqual(expected, values.To<IList<int>>());
		}

		[Test]
		public void NullToListOfInt()
		{
			DynamoDBValue value = null;
			Assert.IsNull(value.To<IList<int>>());
		}

		[Test]
		public void ListOfStringToListOfGuid()
		{
			IList<Guid> expected = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
			var values = new DynamoDBList(expected.Select(v => (DynamoDBString)v.ToString()));

			CollectionAssert.AreEqual(expected, values.To<IList<Guid>>());
		}

		[Test]
		public void ListOfGuidToListOfString()
		{
			var values = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
			var value = DynamoDBValue.Convert(values);
			Assert.IsInstanceOf<DynamoDBList>(value);
			Assert.AreEqual(values.Count, ((DynamoDBList)value).Count);
		}
	}
}
