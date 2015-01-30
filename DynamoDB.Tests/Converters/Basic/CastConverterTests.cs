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

using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests.Converters.Basic
{
	[TestFixture]
	public class CastConverterTests
	{
		private readonly IDynamoDBValueConverter converter = BasicConverters.Cast;

		[Test]
		public void CanConvertFrom()
		{
			// To DynamoDBValue
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBValue>(typeof(DynamoDBString), converter), "DynamoDBString -> DynamoDBValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBValue>(typeof(DynamoDBNumber), converter), "DynamoDBNumber -> DynamoDBValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBValue>(typeof(DynamoDBBoolean), converter), "DynamoDBBoolean -> DynamoDBValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBValue>(typeof(DynamoDBSet<DynamoDBString>), converter), "DynamoDBSet<DynamoDBString> -> DynamoDBValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBValue>(typeof(DynamoDBList), converter), "DynamoDBList -> DynamoDBValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBValue>(typeof(DynamoDBKeyValue), converter), "DynamoDBKeyValue -> DynamoDBValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBValue>(typeof(DynamoDBScalar), converter), "DynamoDBScalar -> DynamoDBValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBValue>(typeof(DynamoDBValue), converter), "DynamoDBValue -> DynamoDBValue");

			// To DynamoDBScalar
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBScalar>(typeof(DynamoDBString), converter), "DynamoDBString -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBScalar>(typeof(DynamoDBNumber), converter), "DynamoDBNumber -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBScalar>(typeof(DynamoDBBoolean), converter), "DynamoDBBoolean -> DynamoDBScalar");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBScalar>(typeof(DynamoDBSet<DynamoDBString>), converter), "DynamoDBSet<DynamoDBString> -> DynamoDBScalar");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBScalar>(typeof(DynamoDBList), converter), "DynamoDBList -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBScalar>(typeof(DynamoDBKeyValue), converter), "DynamoDBKeyValue -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBScalar>(typeof(DynamoDBScalar), converter), "DynamoDBScalar -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBScalar>(typeof(DynamoDBValue), converter), "DynamoDBValue -> DynamoDBScalar");

			// To DynamoDBKeyValue
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(DynamoDBString), converter), "DynamoDBString -> DynamoDBKeyValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(DynamoDBNumber), converter), "DynamoDBNumber -> DynamoDBKeyValue");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(DynamoDBBoolean), converter), "DynamoDBBoolean -> DynamoDBKeyValue");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(DynamoDBSet<DynamoDBString>), converter), "DynamoDBSet<DynamoDBString> -> DynamoDBKeyValue");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(DynamoDBList), converter), "DynamoDBList -> DynamoDBKeyValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(DynamoDBKeyValue), converter), "DynamoDBKeyValue -> DynamoDBKeyValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(DynamoDBScalar), converter), "DynamoDBScalar -> DynamoDBKeyValue");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(DynamoDBValue), converter), "DynamoDBValue -> DynamoDBKeyValue");

			// To DynamoDBString
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBString>(typeof(DynamoDBString), converter), "DynamoDBString -> DynamoDBString");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBString>(typeof(DynamoDBNumber), converter), "DynamoDBNumber -> DynamoDBString");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBString>(typeof(DynamoDBBoolean), converter), "DynamoDBBoolean -> DynamoDBString");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBString>(typeof(DynamoDBSet<DynamoDBString>), converter), "DynamoDBSet<DynamoDBString> -> DynamoDBString");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBString>(typeof(DynamoDBList), converter), "DynamoDBList -> DynamoDBString");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBString>(typeof(DynamoDBKeyValue), converter), "DynamoDBKeyValue -> DynamoDBString");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBString>(typeof(DynamoDBScalar), converter), "DynamoDBScalar -> DynamoDBString");
			Assert.IsTrue(converter.CanConvertFrom<DynamoDBString>(typeof(DynamoDBValue), converter), "DynamoDBValue -> DynamoDBString");

			// From object
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBString>(typeof(object), converter), "object -> DynamoDBString");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBNumber>(typeof(object), converter), "object -> DynamoDBNumber");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBBoolean>(typeof(object), converter), "object -> DynamoDBBoolean");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBSet<DynamoDBString>>(typeof(object), converter), "object -> DynamoDBSet<DynamoDBString>");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBList>(typeof(object), converter), "object -> DynamoDBList");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBKeyValue>(typeof(object), converter), "object -> DynamoDBKeyValue");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBScalar>(typeof(object), converter), "object -> DynamoDBScalar");
			Assert.IsFalse(converter.CanConvertFrom<DynamoDBValue>(typeof(object), converter), "object -> DynamoDBValue");
		}

		[Test]
		public void CanConvertTo()
		{
			// From DynamoDBValue
			Assert.IsTrue(converter.CanConvertTo<DynamoDBValue>(typeof(DynamoDBString), converter), "DynamoDBValue -> DynamoDBString");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBValue>(typeof(DynamoDBNumber), converter), "DynamoDBValue -> DynamoDBNumber");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBValue>(typeof(DynamoDBBoolean), converter), "DynamoDBValue -> DynamoDBBoolean");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBValue>(typeof(DynamoDBSet<DynamoDBString>), converter), "DynamoDBValue -> DynamoDBSet<DynamoDBString>");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBValue>(typeof(DynamoDBList), converter), "DynamoDBValue -> DynamoDBList");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBValue>(typeof(DynamoDBKeyValue), converter), "DynamoDBValue -> DynamoDBKeyValue");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBValue>(typeof(DynamoDBScalar), converter), "DynamoDBValue -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBValue>(typeof(DynamoDBValue), converter), "DynamoDBValue -> DynamoDBValue");

			// From DynamoDBScalar
			Assert.IsTrue(converter.CanConvertTo<DynamoDBScalar>(typeof(DynamoDBString), converter), "DynamoDBScalar -> DynamoDBString");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBScalar>(typeof(DynamoDBNumber), converter), "DynamoDBScalar -> DynamoDBNumber");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBScalar>(typeof(DynamoDBBoolean), converter), "DynamoDBScalar -> DynamoDBBoolean");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBScalar>(typeof(DynamoDBSet<DynamoDBString>), converter), "DynamoDBScalar -> DynamoDBSet<DynamoDBString>");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBScalar>(typeof(DynamoDBList), converter), "DynamoDBScalar -> DynamoDBList");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBScalar>(typeof(DynamoDBKeyValue), converter), "DynamoDBScalar -> DynamoDBKeyValue");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBScalar>(typeof(DynamoDBScalar), converter), "DynamoDBScalar -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBScalar>(typeof(DynamoDBValue), converter), "DynamoDBScalar -> DynamoDBValue");

			// From DynamoDBKeyValue
			Assert.IsTrue(converter.CanConvertTo<DynamoDBKeyValue>(typeof(DynamoDBString), converter), "DynamoDBKeyValue -> DynamoDBString");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBKeyValue>(typeof(DynamoDBNumber), converter), "DynamoDBKeyValue -> DynamoDBNumber");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBKeyValue>(typeof(DynamoDBBoolean), converter), "DynamoDBKeyValue -> DynamoDBBoolean");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBKeyValue>(typeof(DynamoDBSet<DynamoDBString>), converter), "DynamoDBKeyValue -> DynamoDBSet<DynamoDBString>");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBKeyValue>(typeof(DynamoDBList), converter), "DynamoDBKeyValue -> DynamoDBList");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBKeyValue>(typeof(DynamoDBKeyValue), converter), "DynamoDBKeyValue -> DynamoDBKeyValue");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBKeyValue>(typeof(DynamoDBScalar), converter), "DynamoDBKeyValue -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBKeyValue>(typeof(DynamoDBValue), converter), "DynamoDBKeyValue -> DynamoDBValue");

			// From DynamoDBString
			Assert.IsTrue(converter.CanConvertTo<DynamoDBString>(typeof(DynamoDBString), converter), "DynamoDBString -> DynamoDBString");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBString>(typeof(DynamoDBNumber), converter), "DynamoDBString -> DynamoDBNumber");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBString>(typeof(DynamoDBBoolean), converter), "DynamoDBString -> DynamoDBBoolean");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBString>(typeof(DynamoDBSet<DynamoDBString>), converter), "DynamoDBString -> DynamoDBSet<DynamoDBString>");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBString>(typeof(DynamoDBList), converter), "DynamoDBString -> DynamoDBList");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBString>(typeof(DynamoDBKeyValue), converter), "DynamoDBString -> DynamoDBKeyValue");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBString>(typeof(DynamoDBScalar), converter), "DynamoDBString -> DynamoDBScalar");
			Assert.IsTrue(converter.CanConvertTo<DynamoDBString>(typeof(DynamoDBValue), converter), "DynamoDBString -> DynamoDBValue");

			// To object
			Assert.IsFalse(converter.CanConvertTo<DynamoDBString>(typeof(object), converter), "DynamoDBString -> object");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBNumber>(typeof(object), converter), "DynamoDBNumber -> object");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBBoolean>(typeof(object), converter), "DynamoDBBoolean -> object");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBSet<DynamoDBString>>(typeof(object), converter), "DynamoDBSet<DynamoDBString> -> object");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBList>(typeof(object), converter), "DynamoDBList -> object");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBKeyValue>(typeof(object), converter), "DynamoDBKeyValue -> object");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBScalar>(typeof(object), converter), "DynamoDBString -> object");
			Assert.IsFalse(converter.CanConvertTo<DynamoDBValue>(typeof(object), converter), "DynamoDBString -> object");
		}

		[Test]
		public void TryConvertFrom()
		{
			// Up cast
			var stringValue = new DynamoDBString("some value");
			DynamoDBValue dynamoDBValue;
			Assert.IsTrue(converter.TryConvertFrom(typeof(DynamoDBString), stringValue, out dynamoDBValue, converter));
			Assert.AreSame(stringValue, dynamoDBValue);

			// Down cast
			Assert.IsTrue(converter.TryConvertFrom(typeof(DynamoDBValue), dynamoDBValue, out stringValue, converter));
			Assert.AreSame(dynamoDBValue, stringValue);

			// Can't cast
			Assert.IsFalse(converter.TryConvertFrom(typeof(object), stringValue, out dynamoDBValue, converter));
			Assert.IsNull(dynamoDBValue);
		}

		[Test]
		public void TryConvertFromNull()
		{
			// Up cast
			DynamoDBValue dynamoDBValue;
			Assert.IsTrue(converter.TryConvertFrom(typeof(DynamoDBString), null, out dynamoDBValue, converter));
			Assert.IsNull(dynamoDBValue);

			// Down cast
			DynamoDBString stringValue;
			Assert.IsTrue(converter.TryConvertFrom(typeof(DynamoDBValue), null, out stringValue, converter));
			Assert.IsNull(stringValue);

			// Can't cast
			Assert.IsFalse(converter.TryConvertFrom(typeof(object), null, out dynamoDBValue, converter));
			Assert.IsNull(dynamoDBValue);
		}

		[Test]
		public void TryConvertTo()
		{
			// Up cast
			var stringValue = new DynamoDBString("some value");
			object objectValue;
			Assert.IsTrue(converter.TryConvertTo(stringValue, typeof(DynamoDBValue), out objectValue, converter));
			Assert.AreSame(stringValue, objectValue);

			// Down cast
			DynamoDBValue dynamoDBValue = new DynamoDBString("some value");
			Assert.IsTrue(converter.TryConvertTo(dynamoDBValue, typeof(DynamoDBString), out objectValue, converter));
			Assert.AreSame(dynamoDBValue, objectValue);

			// Can't cast
			Assert.IsFalse(converter.TryConvertTo(stringValue, typeof(object), out objectValue, converter));
			Assert.IsNull(objectValue);
		}

		[Test]
		public void TryConvertToNull()
		{
			// Up cast
			object objectValue;
			Assert.IsTrue(converter.TryConvertTo<DynamoDBString>(null, typeof(DynamoDBValue), out objectValue, converter));
			Assert.IsNull(objectValue);

			// Down cast
			Assert.IsTrue(converter.TryConvertTo<DynamoDBValue>(null, typeof(DynamoDBString), out objectValue, converter));
			Assert.IsNull(objectValue);

			// Can't cast
			Assert.IsFalse(converter.TryConvertTo<DynamoDBString>(null, typeof(object), out objectValue, converter));
			Assert.IsNull(objectValue);
		}
	}
}
