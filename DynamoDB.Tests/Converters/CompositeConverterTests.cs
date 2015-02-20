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
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests.Converters
{
	[TestFixture]
	public class CompositeConverterTests
	{
		[Test]
		public void MultipleConvertersAtSamePriorityOneMatches()
		{
			var converter = new CompositeConverter();
			converter.Add(FakeConverter.Instance);
			converter.Add(NullConverter.Instance);

			Assert.AreEqual(FakeConverter.Value, DynamoDBValue.Convert(1, converter));

			// Now try opposite order
			converter = new CompositeConverter();
			converter.Add(NullConverter.Instance);
			converter.Add(FakeConverter.Instance);

			Assert.AreEqual(FakeConverter.Value, DynamoDBValue.Convert(1, converter));
		}

		[Test]
		public void MultipleConvertersAtSamePriorityMatch()
		{
			var converter = new CompositeConverter();
			converter.Add(FakeConverter.Instance);
			converter.Add(FakeConverter.Instance);

			Assert.Throws<Exception>(() => DynamoDBValue.Convert(1, converter));
		}

		[Test]
		public void MultipleConvertersMatchAtDifferentPriorities()
		{
			DynamoDBValue value = "2";

			var converter = new CompositeConverter();
			converter.Add(new FakeConverter("1"), 1);
			converter.Add(new FakeConverter(value), 2);

			Assert.AreEqual(value, DynamoDBValue.Convert("a", converter));

			converter = new CompositeConverter();
			converter.Add(new FakeConverter(value), 2);
			converter.Add(new FakeConverter("1"), 1);

			Assert.AreEqual(value, DynamoDBValue.Convert("a", converter));
		}
	}
}
