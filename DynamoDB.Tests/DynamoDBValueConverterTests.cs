using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class DynamoDBValueConverterTests
	{
		[Test]
		public void NullToString()
		{
			DynamoDBValue value = null;
			Assert.AreEqual(null, value.To<string>());
		}

		[Test]
		[TestCase(true)]
		[TestCase(false)]
		public void ToBool(bool value)
		{
			DynamoDBValue dynamoDBValue = (DynamoDBBoolean)value;
			Assert.AreEqual(value, dynamoDBValue.To<bool>());
		}
	}
}
