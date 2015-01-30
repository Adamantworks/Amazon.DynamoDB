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

		/// <summary>
		/// This comes up in cases like ItemKey and DynamoDBMap where we want to have generic overloads
		/// for conversion, but there are also implicit conversions available. This is the reason why
		/// there are "Strict" versions of methods.
		/// </summary>
		[Test]
		public void PrefersGenericMethodToImplicitConversion()
		{
			Assert.AreEqual("Generic", Method("string value"));
		}

		/// <summary>
		/// This is the reason it isn't even worth having DynamoDBValue overloads of non-"Strict" methods.
		/// Even then sub classes like DynamoDBKeyValue wouldn't work.
		/// </summary>
		[Test]
		public void PrefersGenericMethodToBaseTypes()
		{
			Assert.AreEqual("Base", Method(new TestValue()));
		}

		public string Method<T>(T value)
		{
			return "Generic";
		}
		public string Method(TestValue value)
		{
			return "Non-Generic";
		}
		public string Method(TestBase value)
		{
			return "Base";
		}

		public class TestBase
		{
		}

		public class TestValue : TestBase
		{
			public static implicit operator TestValue(string value)
			{
				return null;
			}
		}
	}
}
