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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Tests.Converters;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests.DynamoDBValues
{
	class DynamoDBStringTests
	{
		[Test]
		public void ConvertFromToNullOrEmpty()
		{
			DynamoDBString nullDynamoString = null;
			Assert.IsNull((string)nullDynamoString, "null dynamo string -> string");

			string nullString = null;
			Assert.IsNull((DynamoDBString)nullString, "null string -> dynamo string");

			var emptyString = ""; // Not constant because we need the cast to be evaluated at run time
			Assert.IsNull((DynamoDBString)emptyString, "empty string -> dynamo string");
		}

		[Test]
		public void ConvertExceptionMessage()
		{
			var ex = Assert.Throws<InvalidCastException>(() => DynamoDBString.Convert(45, NullConverter.Instance));
			Assert.AreEqual("Unable to convert object of type 'System.Int32' to type 'DynamoDBString'.", ex.Message);
		}
	}
}
