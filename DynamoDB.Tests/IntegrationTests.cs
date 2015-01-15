using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Schema;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture, Explicit]
	public class IntegrationTests
	{
		[Test]
		public void Async()
		{
			var task = AsyncInternal();
			task.Wait();
		}

		private static async Task AsyncInternal()
		{
			var region = DynamoDBRegion.Connect();
			var tableName = "IntegrationTestAsync-" + Guid.NewGuid();
			var schema = TableSchema();
			await region.CreateTableAsync(tableName, schema); // TODO specify provisioned throughput
			// TODO wait for creation to complete
			await region.DeleteTableAsync(tableName);
			// TODO wait for delete to complete
		}

		[Test]
		public void Sync()
		{
			var region = DynamoDBRegion.Connect();
			var tableName = "IntegrationTestSync-" + Guid.NewGuid();
			var schema = TableSchema();
			region.CreateTable(tableName, schema); // TODO specify provisioned throughput
			// TODO wait for creation to complete
			region.DeleteTable(tableName);
			// TODO wait for delete to complete
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
				{ "global", new IndexSchema(false, globalIndexKey) },
			});
		}
	}
}
