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

namespace Adamantworks.Amazon.DynamoDB.Tests.DynamoDBValues
{
	[TestFixture]
	public class DynamoDBNumberTests
	{
		[Test]
		public void RoundTrip()
		{
			AssertRoundTrip("42", 42);
			AssertRoundTrip("3.33", 3.33);
			// The next value has problems in .NET for x64 machines
			// see http://stackoverflow.com/questions/24299692
			AssertRoundTrip("0.84551240822557006", 0.84551240822557006);
			AssertRoundTrip("3.14", 3.14m);
		}

		private static void AssertRoundTrip(string expected, int value)
		{
			var number = (DynamoDBNumber)value;
			var actual = number.ToString();
			Assert.AreEqual(expected, actual);
			Assert.AreEqual(value, (int)number);
		}
		private static void AssertRoundTrip(string expected, double value)
		{
			var number = (DynamoDBNumber)value;
			var actual = number.ToString();
			Assert.AreEqual(expected, actual);
			Assert.AreEqual(value, (double)number);
		}
		private static void AssertRoundTrip(string expected, decimal value)
		{
			var number = (DynamoDBNumber)value;
			var actual = number.ToString();
			Assert.AreEqual(expected, actual);
			Assert.AreEqual(value, (decimal)number);
		}
	}
}
