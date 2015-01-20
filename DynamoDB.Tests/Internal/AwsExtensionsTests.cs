using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using NUnit.Framework;
using System.Collections.Generic;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Tests.Internal
{
	[TestFixture]
	public class AwsExtensionsTests
	{
		[Test]
		[TestCase(true)]
		[TestCase(false)]
		public void ToValueBool(bool boolValue)
		{
			var value = new Aws.AttributeValue() { BOOL = boolValue };
			var expected = (DynamoDBBoolean)boolValue;
			Assert.AreEqual(expected, value.ToValue());
		}

		[Test]
		public void ToValueString()
		{
			var value = new Aws.AttributeValue() { S = "hello" };
			var expected = (DynamoDBString)"hello";
			Assert.AreEqual(expected, value.ToValue());
		}

		[Test]
		public void ToValueEquivalenceOfEmpty()
		{
			var boolValue = new Aws.AttributeValue() { BOOL = false };
			var listValue = new Aws.AttributeValue() { L = new List<Aws.AttributeValue>() };
			var mapValue = new Aws.AttributeValue() { M = new Dictionary<string, Aws.AttributeValue>() };
			Assert.IsInstanceOf<DynamoDBBoolean>(boolValue.ToValue());
			Assert.IsInstanceOf<DynamoDBList>(listValue.ToValue());
			Assert.IsInstanceOf<DynamoDBMap>(mapValue.ToValue());
		}
	}
}
