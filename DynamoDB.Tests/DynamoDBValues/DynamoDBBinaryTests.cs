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
			Assert.AreEqual(null, (byte[])nullBinary, "null binary -> byte[]");
			Assert.IsTrue(((ImmutableArray<byte>)nullBinary).IsDefault, "null binary -> immutable array");

			byte[] nullByteArray = null;
			Assert.AreEqual(null, (DynamoDBBinary)nullByteArray, "null byte[] -> binary");

			byte[] emptyByteArray = null;
			Assert.AreEqual(null, (DynamoDBBinary)emptyByteArray, "empty byte[] -> binary");

			var nullImmutableArray = default(ImmutableArray<byte>);
			Assert.AreEqual(null, (DynamoDBBinary)nullImmutableArray, "null immutable array -> binary");

			var emptyImmutableArray = ImmutableArray<byte>.Empty;
			Assert.AreEqual(null, (DynamoDBBinary)emptyImmutableArray,"empty immutable array -> binary");
		}
	}
}
