using System.Collections.Generic;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests.Converters
{
	[TestFixture]
	public class ListConverterTests
	{
		[Test]
		public void StringListOfNullToDynamoDBValue()
		{
			var list = new List<string>() { null };

			var result = DynamoDBValue.Convert(list);
			Assert.IsInstanceOf<DynamoDBList>(result);
			var listResult = (DynamoDBList)result;
			Assert.AreEqual(1, listResult.Count, "length");
			Assert.IsNull(listResult[0], "list item");
		}
	}
}
