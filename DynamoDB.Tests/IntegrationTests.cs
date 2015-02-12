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
using Nito.AsyncEx.Synchronous;
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

		private static string TableNamePrefix(string test)
		{
			return "UnitTest-" + test;
		}

		private static string UniqueTableName(string test)
		{
			return TableNamePrefix(test) + "-" + Guid.NewGuid().ToString("N");
		}

		private static TableSchema TableSchema()
		{
			var id = new AttributeSchema("ID", DynamoDBValueType.String);
			var order = new AttributeSchema("Order", DynamoDBValueType.Number);
			var key = new KeySchema(id, order);
			var group = new AttributeSchema("Group", DynamoDBValueType.Number);
			var localIndexKey = new KeySchema(id, group);
			var globalIndexKey = new KeySchema(group, order);
			return new TableSchema(key, new Dictionary<string, IndexSchema>()
			{
				{"local", new IndexSchema(false, localIndexKey)},
				{"global", new IndexSchema(true, globalIndexKey)},
			});
		}

		private async Task WithTableAsync(string test, Func<ITable, Task> actions)
		{
			var tableName = UniqueTableName(test);
			;
			var schema = TableSchema();
			var table = await region.CreateTableAsync(tableName, schema);
			await table.WaitUntilNotAsync(CollectionStatus.Creating);
			try
			{
				await actions(table).ConfigureAwait(false);
			}
			finally
			{
				region.DeleteTable(tableName);
				table.WaitUntilNot(CollectionStatus.Deleting);
			}
		}

		[Test]
		public void Async()
		{
			var task = WithTableAsync("IntegrationTest", async table =>
			{
				await table.UpdateTableAsync(LargeProvisionedThroughput, new Dictionary<string, ProvisionedThroughput>()
				{
					{"global", LargeProvisionedThroughput}
				});
				await table.WaitUntilNotAsync(CollectionStatus.Updating);

				var writeBatch = region.BeginBatchWriteAsync();
				for(var i = 0; i < 513; i++)
				{
					var item = new DynamoDBMap()
					{
						{"ID", Guid.NewGuid()},
						{"Order", i + 1},
						{"Group", i/100},
					};
					table.PutAsync(writeBatch, item);
				}
				await writeBatch.Complete().ConfigureAwait(false);

				var items = await table.Scan().LimitTo(2).AllAsync().ToList().ConfigureAwait(false);
				Assert.AreEqual(2, items.Count);

				items = await table.Scan().LimitTo(112).AllAsync().ToList().ConfigureAwait(false);
				Assert.AreEqual(112, items.Count);

				// TODO paged scan

				var globalIndex = table.Indexes["global"];
				items = await globalIndex.Query(2).LimitTo(3).AllKeysAsync().ToList().ConfigureAwait(false);
				Assert.AreEqual(3, items.Count);

				items = await globalIndex.Query(2).LimitTo(98).AllKeysAsync().ToList().ConfigureAwait(false);
				Assert.AreEqual(98, items.Count);

				await PagedQueryAsync(globalIndex, 3).ConfigureAwait(false);
				await PagedQueryAsync(globalIndex, 52).ConfigureAwait(false);
			});

			// Wait on the async to complete
			task.WaitAndUnwrapException();
		}

		private static async Task PagedQueryAsync(IIndex globalIndex, int pageSize)
		{
			LastKey? lastKey = null;

			do
			{
				var page = await globalIndex.Query(2).Paged(pageSize, lastKey).AllKeysAsync().ConfigureAwait(false);
				lastKey = page.LastEvaluatedKey;
				if(lastKey != null)
					Assert.AreEqual(pageSize, page.Items.Count);
			} while(lastKey != null);
		}

		private void WithTable(string test, Action<ITable> actions)
		{
			var tableName = UniqueTableName(test);
			var schema = TableSchema();
			var table = region.CreateTable(tableName, schema); // TODO specify provisioned throughput
			table.WaitUntilNot(CollectionStatus.Creating);
			try
			{
				actions(table);
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
			WithTable("IntegrationTest", table =>
			{
				table.UpdateTable(LargeProvisionedThroughput, new Dictionary<string, ProvisionedThroughput>()
				{
					{"global", LargeProvisionedThroughput}
				});
				table.WaitUntilNot(CollectionStatus.Updating);

				var writeBatch = region.BeginBatchWrite();
				for(var i = 0; i < 513; i++)
				{
					var item = new DynamoDBMap()
					{
						{"ID", Guid.NewGuid()},
						{"Order", i + 1},
						{"Group", i/100},
					};
					table.Put(writeBatch, item);
				}
				writeBatch.Complete();

				var items = table.Scan().LimitTo(2).All().ToList();
				Assert.AreEqual(2, items.Count);

				items = table.Scan().LimitTo(112).All().ToList();
				Assert.AreEqual(112, items.Count);

				// TODO paged scan

				var globalIndex = table.Indexes["global"];
				items = globalIndex.Query(2).LimitTo(3).AllKeys().ToList();
				Assert.AreEqual(3, items.Count);

				items = globalIndex.Query(2).LimitTo(98).AllKeys().ToList();
				Assert.AreEqual(98, items.Count);

				PagedQuery(globalIndex, 3);
				PagedQuery(globalIndex, 52);
			});
		}

		private static void PagedQuery(IIndex globalIndex, int pageSize)
		{
			LastKey? lastKey = null;

			do
			{
				var page = globalIndex.Query(2).Paged(pageSize, lastKey).AllKeys();
				lastKey = page.LastEvaluatedKey;
				if(lastKey != null)
					Assert.AreEqual(pageSize, page.Items.Count);
			} while(lastKey != null);
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
			WithTable("UpdateIndexOnly", table =>
			{
				// Increase Both
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
			});
		}

		[Test]
		public void ListTablesWithPrefix()
		{
			var begins = TableNamePrefix("ListTablesWithPrefix");
			var schema = TableSchema();
			var basicTable = region.CreateTable(begins, schema);
			var table1 = region.CreateTable(UniqueTableName("ListTablesWithPrefix"), schema);
			var table2 = region.CreateTable(UniqueTableName("ListTablesWithPrefix"), schema);
			var table3 = region.CreateTable(UniqueTableName("ListTablesXWithPrefix"), schema);
			try
			{
				var tables = region.ListTablesWithPrefix(begins).ToList();
				Assert.AreEqual(3, tables.Count, "Count");
				CollectionAssert.Contains(tables, begins);
				Assert.IsTrue(tables.All(t => t.StartsWith(begins)));

				// We are checking to make sure this doesn't throw an exception, because before "-" is ""
				tables = region.ListTablesWithPrefix("-").ToList();
			}
			finally
			{
				basicTable.WaitUntilNot(CollectionStatus.Creating);
				table1.WaitUntilNot(CollectionStatus.Creating);
				table2.WaitUntilNot(CollectionStatus.Creating);
				table3.WaitUntilNot(CollectionStatus.Creating);
				region.DeleteTable(basicTable.Name);
				region.DeleteTable(table1.Name);
				region.DeleteTable(table2.Name);
				region.DeleteTable(table3.Name);
				basicTable.WaitUntilNot(CollectionStatus.Deleting);
				table1.WaitUntilNot(CollectionStatus.Deleting);
				table2.WaitUntilNot(CollectionStatus.Deleting);
				table3.WaitUntilNot(CollectionStatus.Deleting);
			}
		}
	}
}
