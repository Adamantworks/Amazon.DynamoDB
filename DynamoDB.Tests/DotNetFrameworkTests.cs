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
using System.Globalization;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class DotNetFrameworkTests
	{
		[TestCase("2.1", 2)]
		[TestCase("255.5", 255)]
		[Test]
		public void ParseByteViaDecimal(string value, byte expected)
		{
			Assert.AreEqual((byte)decimal.Parse(value, NumberStyles.Float), expected);
		}

		[Test]
		public void DecimalParseBiggerNumber()
		{
			var value = new string('9', 38);
			Assert.Throws<OverflowException>(() => decimal.Parse(value));
		}

		[Test]
		public void CastNullToNonNullable()
		{
			int? foo = null;
			byte result;
			Assert.Throws<InvalidOperationException>(() => result = (byte)foo);
		}

		[Test]
		public void InstanceOfOpenGeneric()
		{
			Assert.IsFalse(typeof(ISet<>).IsInstanceOfType(new HashSet<Guid>()));
		}

		/// <summary>
		/// This comes up in cases where we might want to have generic overloads
		/// for conversion, but there are also implicit conversions available.
		/// </summary>
		[Test]
		public void PrefersGenericMethodOverImplicitConversion()
		{
			Assert.AreEqual("Generic", MethodWithGenericOverload("string value"));
		}

		public string MethodWithGenericOverload<T>(T value)
		{
			return "Generic";
		}
		public string MethodWithGenericOverload(TestValue value)
		{
			return "Non-Generic";
		}

		[Test]
		public void PrefersImplicitConversionOverObjectParameter()
		{
			Assert.AreEqual("TestValue", MethodWithObjectOverload("string value"));
		}

		public string MethodWithObjectOverload(TestValue value)
		{
			return "TestValue";
		}

		public string MethodWithObjectOverload(object value)
		{
			return "object";
		}

		public class TestValue
		{
			public static implicit operator TestValue(string value)
			{
				return null;
			}
		}
	}
}
