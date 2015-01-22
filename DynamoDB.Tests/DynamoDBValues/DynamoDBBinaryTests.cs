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

using System.Collections.Immutable;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests.DynamoDBValues
{
	[TestFixture]
	public class DynamoDBBinaryTests
	{
		[Test]
		public void ConvertFromToNullOrEmpty()
		{
			DynamoDBBinary nullBinary = null;
			Assert.IsNull((byte[])nullBinary, "null binary -> byte[]");
			Assert.IsTrue(((ImmutableArray<byte>)nullBinary).IsDefault, "null binary -> immutable array");

			byte[] nullByteArray = null;
			Assert.IsNull((DynamoDBBinary)nullByteArray, "null byte[] -> binary");

			var emptyByteArray = new byte[0];
			Assert.IsNull((DynamoDBBinary)emptyByteArray, "empty byte[] -> binary");

			var nullImmutableArray = default(ImmutableArray<byte>);
			Assert.IsNull((DynamoDBBinary)nullImmutableArray, "null immutable array -> binary");

			var emptyImmutableArray = ImmutableArray<byte>.Empty;
			Assert.IsNull((DynamoDBBinary)emptyImmutableArray, "empty immutable array -> binary");
		}

		[Test]
		public void Equals()
		{
			// Test that two independent binary values will be equal based on content
			var value1 = new DynamoDBBinary(new byte[] { 1, 2, 3, 4 });
			var value2 = new DynamoDBBinary(new byte[] { 1, 2, 3, 4 });
			Assert.AreNotSame(value1, value2);
			// Check that the immutable arrays are not equal
			Assert.IsFalse(((ImmutableArray<byte>)value1).Equals(value2)); // relies on the fact that cast reveals the internal array
			Assert.IsTrue(Equals(value1, value2)); // Use object.Equals() to avoid NUnit content comparison
			Assert.AreEqual(value1.GetHashCode(), value2.GetHashCode());
		}

		[Test]
		public new void GetHashCode()
		{
			// Differ in last byte
			var value1 = new DynamoDBBinary(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
			var value2 = new DynamoDBBinary(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 });
			Assert.AreNotEqual(value1.GetHashCode(), value2.GetHashCode());
		}
	}
}
