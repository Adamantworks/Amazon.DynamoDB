using System;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Tests.Converters;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class DynamoDBValueExtensionsTests
	{
		[Test]
		public void ConvertExceptionMessage()
		{
			var value = new DynamoDBString("42");
			var ex = Assert.Throws<InvalidCastException>(() => value.To<int>(NullConverter.Instance));
			Assert.AreEqual("Unable to convert object of type 'DynamoDBString' to type 'System.Int32'.", ex.Message);

			value = null;
			ex = Assert.Throws<InvalidCastException>(() => value.To<int>(NullConverter.Instance));
			Assert.AreEqual("Unable to convert value null to type 'System.Int32'.", ex.Message);
		}
	}
}
