using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Schema;
using NUnit.Framework;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture, Explicit]
	public class IntegrationTests
	{
		private IDynamoDBRegion region;

		[TestFixtureSetUp]
		public void SetUp()
		{
			region = DynamoDBRegion.Connect();
		}

		[Test]
		public void Async()
		{
			var task = AsyncInternal();
			task.Wait();
		}

		private async Task AsyncInternal()
		{
			var tableName = "IntegrationTestAsync-" + Guid.NewGuid();
			var schema = TableSchema();
			var table = await region.CreateTableAsync(tableName, schema); // TODO specify provisioned throughput
			await table.WaitUntilNotAsync(TableStatus.Creating);
			try
			{

			}
			finally
			{
				region.DeleteTable(tableName);
				table.WaitUntilNot(TableStatus.Deleting);
			}
		}

		[Test]
		public void Sync()
		{
			var tableName = "IntegrationTestSync-" + Guid.NewGuid();
			var schema = TableSchema();
			var table = region.CreateTable(tableName, schema); // TODO specify provisioned throughput
			table.WaitUntilNot(TableStatus.Creating);

			try
			{

			}
			finally
			{
				region.DeleteTable(tableName);
				table.WaitUntilNot(TableStatus.Deleting);
			}
		}

		private static TableSchema TableSchema()
		{
			var hashKey = new AttributeSchema("ID", DynamoDBValueType.String);
			var rangeKey = new AttributeSchema("Order", DynamoDBValueType.Number);
			var key = new KeySchema(hashKey, rangeKey);
			var localIndexKey = new KeySchema(hashKey, "CreatedOn", DynamoDBValueType.String);
			var globalIndexKey = new KeySchema(rangeKey, hashKey);
			return new TableSchema(key, new Dictionary<string, IndexSchema>()
			{
				{ "local", new IndexSchema(false, localIndexKey) },
				{ "global", new IndexSchema(true, globalIndexKey) },
			});
		}

		[Test]
		public void LoadTableThatDoesNotExist()
		{
			var tableName = "DoesNotExist-" + Guid.NewGuid();
			Assert.Throws<Aws.ResourceNotFoundException>(() => region.LoadTable(tableName));
		}
	}
}
