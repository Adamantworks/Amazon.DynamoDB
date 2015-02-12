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

using System.Collections.Generic;
using Adamantworks.Amazon.DynamoDB.Converters;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests.Converters.Basic
{
	[TestFixture]
	public class CastConverterTests
	{
		private readonly IValueConverter converter = BasicConverters.Cast;

		[Test]
		public void CanConvert()
		{
			// Upcast
			Assert.IsTrue(converter.CanConvert<List<int>, IList<int>>(), "upcast");

			// Downcast
			Assert.IsTrue(converter.CanConvert<IList<int>, List<int>>(), "downcast");

			// Can't cast
			Assert.IsFalse(converter.CanConvert<int, string>(), "can't cast");
		}

		[Test]
		public void TryConvert()
		{
			// Upcast
			var concreteValue = new List<int>();
			IList<int> interfaceValue;
			Assert.IsTrue(converter.TryConvert(concreteValue, out interfaceValue), "upcast");
			Assert.AreSame(concreteValue, interfaceValue);

			// Downcast
			Assert.IsTrue(converter.TryConvert(interfaceValue, out concreteValue, converter), "downcast");
			Assert.AreSame(interfaceValue, concreteValue);

			// Can't cast
			Assert.IsFalse(converter.TryConvert("hello", out interfaceValue, converter), "can't cast");
			Assert.IsNull(interfaceValue);
		}

		[Test]
		public void TryConvertNull()
		{
			// Reference type
			IList<int> referenceValue;
			Assert.IsTrue(converter.TryConvert(null, out referenceValue), "reference");
			Assert.IsNull(referenceValue);

			// Nullable type
			int? nullableValue;
			Assert.IsTrue(converter.TryConvert(null, out nullableValue), "nullable");
			Assert.IsNull(nullableValue);

			// Value type
			int value;
			Assert.IsFalse(converter.TryConvert(null, out value), "value");
			Assert.AreEqual(0, value);
		}
	}
}
