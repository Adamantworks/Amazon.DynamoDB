﻿// Copyright 2015 Adamantworks.  All Rights Reserved.
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

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class CompositeTests
	{
		private const string Cast = "cast";
		private static readonly Guid Convert = Guid.Empty;
		private static readonly DynamoDBString Upcast = new DynamoDBString("upcast");

		[Test]
		public void ValueSelectsCorrectOverload()
		{
			var fakeConverter = FakeConverter.Instance;

			Assert.AreEqual("cast;cast", Composite.Value(Cast, Cast, fakeConverter).ToString());
			Assert.AreEqual("cast;converted", Composite.Value(Cast, Convert, fakeConverter).ToString());
			Assert.AreEqual("converted;cast", Composite.Value(Convert, Cast, fakeConverter).ToString());
			Assert.AreEqual("converted;converted", Composite.Value(Convert, Convert, fakeConverter).ToString());
			Assert.AreEqual("upcast;converted", Composite.Value(Upcast, Convert, fakeConverter).ToString());
		}
	}
}
