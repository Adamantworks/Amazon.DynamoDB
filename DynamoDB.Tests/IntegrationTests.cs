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
using System.Linq;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Schema;
using NUnit.Framework;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture, Explicit("Integration Tests hit DynamoDB")]
	public class IntegrationTests
	{
		private static readonly ProvisionedThroughput MinProvisionedThroughput = new ProvisionedThroughput(1, 1);
		private static readonly ProvisionedThroughput LargeProvisionedThroughput = new ProvisionedThroughput(10, 5);
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
			var tableName = "UnitTest-IntegrationTestAsync-" + Guid.NewGuid();
			var schema = TableSchema();
			var table = await region.CreateTableAsync(tableName, schema);
			await table.WaitUntilNotAsync(CollectionStatus.Creating);

			try
			{
				await table.UpdateTableAsync(LargeProvisionedThroughput, new Dictionary<string, ProvisionedThroughput>()
				{
					{"global", LargeProvisionedThroughput}
				});
				await table.WaitUntilNotAsync(CollectionStatus.Updating);

				var writeBatch = region.BeginBatchWriteAsync();
				for(var i = 0; i < 113; i++)
				{
					var item = new DynamoDBMap()
					{
						{"ID", Guid.NewGuid()},
						{"Order", 0},
						{"CreatedOn", DateTime.UtcNow.ToString("u")},
					};
					table.PutAsync(writeBatch, item);
				}
				await writeBatch.Complete().ConfigureAwait(false);

				// TODO test query limits
			}
			finally
			{
				region.DeleteTable(tableName);
				table.WaitUntilNot(CollectionStatus.Deleting);
			}
		}

		[Test]
		public void Sync()
		{
			var tableName = "UnitTest-IntegrationTestSync-" + Guid.NewGuid();
			var schema = TableSchema();
			var table = region.CreateTable(tableName, schema);
			table.WaitUntilNot(CollectionStatus.Creating);

			try
			{
				table.UpdateTable(LargeProvisionedThroughput, new Dictionary<string, ProvisionedThroughput>()
				{
					{"global", LargeProvisionedThroughput}
				});
				table.WaitUntilNot(CollectionStatus.Updating);

				var writeBatch = region.BeginBatchWrite();
				for(var i = 0; i < 113; i++)
				{
					var item = new DynamoDBMap()
					{
						{"ID", Guid.NewGuid()},
						{"Order", 0},
						{"CreatedOn", DateTime.UtcNow.ToString("u")},
					};
					table.Put(writeBatch, item);
				}
				writeBatch.Complete();
				// TODO test query limits
			}
			finally
			{
				region.DeleteTable(tableName);
				table.WaitUntilNot(CollectionStatus.Deleting);
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
			var tableName = "UnitTest-DoesNotExist-" + Guid.NewGuid();
			Assert.Throws<Aws.ResourceNotFoundException>(() => region.LoadTable(tableName));
		}

		[Test]
		public void UpdateIndexOnly()
		{
			var tableName = "UnitTest-UpdateIndexOnly-" + Guid.NewGuid();
			var schema = TableSchema();
			var table = region.CreateTable(tableName, schema); // TODO specify provisioned throughput
			table.WaitUntilNot(CollectionStatus.Creating);

			try
			{
				// Increate Both
				table.UpdateTable(LargeProvisionedThroughput, new Dictionary<string, ProvisionedThroughput>()
				{
					{"global", LargeProvisionedThroughput}
				});
				table.WaitUntilNot(CollectionStatus.Updating);
				// Decrease Index
				table.UpdateTable(new Dictionary<string, ProvisionedThroughput>()
				{
					{"global", MinProvisionedThroughput}
				});
				table.WaitUntilNot(CollectionStatus.Updating);
				Assert.AreEqual(0, table.ProvisionedThroughput.NumberOfDecreasesToday);
				Assert.AreEqual(1, table.Indexes["global"].ProvisionedThroughput.NumberOfDecreasesToday);
				// Decrease Table
				table.UpdateTable(MinProvisionedThroughput);
				table.WaitUntilNot(CollectionStatus.Updating);
				Assert.AreEqual(1, table.ProvisionedThroughput.NumberOfDecreasesToday);
				Assert.AreEqual(1, table.Indexes["global"].ProvisionedThroughput.NumberOfDecreasesToday);
			}
			finally
			{
				region.DeleteTable(tableName);
				table.WaitUntilNot(CollectionStatus.Deleting);
			}
		}
	}
}
