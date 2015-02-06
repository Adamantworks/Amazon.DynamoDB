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
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class DynamoDBValueConverterCompositeValueDefaultTests
	{
		[Test]
		public void StringToBool()
		{
			var value = Composite.Split<bool, bool>(new DynamoDBString("True;False"));
			Assert.IsTrue(value.Item1);
			Assert.IsFalse(value.Item2);
		}

		[Test]
		public void StringToNumber()
		{
			var value = Composite.Split<int, int, int, decimal>(new DynamoDBString("-1;0;1;3.14"));
			Assert.AreEqual(-1, value.Item1);
			Assert.AreEqual(0, value.Item2);
			Assert.AreEqual(1, value.Item3);
			Assert.AreEqual(3.14m, value.Item4);
		}
	}
}
