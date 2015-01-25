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
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Amazon.DynamoDBv2;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	// See Overloads.tt and Overloads.cs for more method overloads of this interface
	public partial interface IDynamoDBRegion
	{
		TimeSpan WaitStatusPollingInterval { get; set; }

		IBatchGetAsync BeginBatchGetAsync();
		IBatchGet BeginBatchGet();

		IBatchWriteAsync BeginBatchWriteAsync();
		IBatchWrite BeginBatchWrite();

		IAsyncEnumerable<string> ListTablesAsync(ReadAhead readAhead);
		IEnumerable<string> ListTables();

		// TODO ListTableNamesBeginningWith // Could use exclusive start key to do this

		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, CancellationToken cancellationToken = default(CancellationToken));
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken = default(CancellationToken));
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken = default(CancellationToken));
		ITable CreateTable(string tableName, TableSchema schema);
		ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput);
		ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);

		Task<ITable> LoadTableAsync(string tableName, CancellationToken cancellationToken = default(CancellationToken));
		ITable LoadTable(string tableName);

		Task DeleteTableAsync(string tableName, CancellationToken cancellationToken = default(CancellationToken));
		void DeleteTable(string tableName);
	}

	// See Overloads.tt and Overloads.cs for more method overloads of this class
	internal partial class Region : IDynamoDBRegion
	{
		internal readonly IAmazonDynamoDB DB;

		internal Region(IAmazonDynamoDB db)
		{
			DB = db;
			WaitStatusPollingInterval = TimeSpan.FromSeconds(5);
		}

		public TimeSpan WaitStatusPollingInterval { get; set; }

		#region BeginBatchGet
		public IBatchGetAsync BeginBatchGetAsync()
		{
			return new BatchGetAsync(this);
		}

		public IBatchGet BeginBatchGet()
		{
			return new BatchGet(this);
		}
		#endregion

		#region BeginBatchWrite
		public IBatchWriteAsync BeginBatchWriteAsync()
		{
			return new BatchWriteAsync(this);
		}

		public IBatchWrite BeginBatchWrite()
		{
			return new BatchWrite(this);
		}
		#endregion

		#region ListTables
		public IAsyncEnumerable<string> ListTablesAsync(ReadAhead readAhead)
		{
			return AsyncEnumerable.Defer(() =>
			{
				// This must be in here so it is deferred until GetEnumerator() is called on us (need one per enumerator)
				var request = new Aws.ListTablesRequest();
				return AsyncEnumerableEx.GenerateChunked<Aws.ListTablesResponse, string>(null,
					(lastResponse, cancellationToken) =>
					{
						if(lastResponse != null)
							request.ExclusiveStartTableName = lastResponse.LastEvaluatedTableName;
						return DB.ListTablesAsync(request, cancellationToken);
					},
					lastResponse => lastResponse.TableNames,
					IsComplete,
					readAhead);
			});
		}

		public IEnumerable<string> ListTables()
		{
			var request = new Aws.ListTablesRequest();
			Aws.ListTablesResponse lastResponse = null;
			do
			{
				if(lastResponse != null)
					request.ExclusiveStartTableName = lastResponse.LastEvaluatedTableName;
				lastResponse = DB.ListTables(request);
				foreach(var tableName in lastResponse.TableNames)
					yield return tableName;
			} while(IsComplete(lastResponse));
		}

		private static bool IsComplete(Aws.ListTablesResponse lastResponse)
		{
			return lastResponse.LastEvaluatedTableName == null;
		}
		#endregion

		#region CreateTable
		public async Task<ITable> CreateTableAsync(string tableName, TableSchema schema, CancellationToken cancellationToken)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			var response = await DB.CreateTableAsync(request, cancellationToken).ConfigureAwait(false);
			return new Table(this, response.TableDescription);
		}
		public async Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			RequestProvisionedThroughput(request, provisionedThroughput, null);
			var response = await DB.CreateTableAsync(request, cancellationToken).ConfigureAwait(false);
			return new Table(this, response.TableDescription);
		}
		public async Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			RequestProvisionedThroughput(request, provisionedThroughput, indexProvisionedThroughputs);
			var response = await DB.CreateTableAsync(request, cancellationToken).ConfigureAwait(false);
			return new Table(this, response.TableDescription);
		}

		public ITable CreateTable(string tableName, TableSchema schema)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			var response = DB.CreateTable(request);
			return new Table(this, response.TableDescription);
		}
		public ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			RequestProvisionedThroughput(request, provisionedThroughput, null);
			var response = DB.CreateTable(request);
			return new Table(this, response.TableDescription);
		}
		public ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			RequestProvisionedThroughput(request, provisionedThroughput, indexProvisionedThroughputs);
			var response = DB.CreateTable(request);
			return new Table(this, response.TableDescription);
		}

		private static Aws.CreateTableRequest BuildCreateTableRequest(string tableName, TableSchema schema)
		{
			return new Aws.CreateTableRequest()
			{
				TableName = tableName,
				KeySchema = schema.Key.ToAws(),
				LocalSecondaryIndexes = schema.Indexes.Where(i => !i.Value.IsGlobal).Select(i => new Aws.LocalSecondaryIndex()
				{
					IndexName = i.Key,
					KeySchema = i.Value.Key.ToAws(),
					Projection = i.Value.ToAwsProjection(),
				}).ToList(),
				GlobalSecondaryIndexes = schema.Indexes.Where(i => i.Value.IsGlobal).Select(i => new Aws.GlobalSecondaryIndex()
				{
					IndexName = i.Key,
					KeySchema = schema.Key.ToAws(),
					Projection = i.Value.ToAwsProjection(),
					ProvisionedThroughput = new Aws.ProvisionedThroughput(1, 1),
				}).ToList(),
				AttributeDefinitions = schema.ToAwsAttributeDefinitions(),
				ProvisionedThroughput = new Aws.ProvisionedThroughput(1, 1),
			};
		}
		private static void RequestProvisionedThroughput(Aws.CreateTableRequest request, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			request.ProvisionedThroughput = provisionedThroughput.ToAws();
			if(indexProvisionedThroughputs != null && indexProvisionedThroughputs.Count != 0)
				foreach(var index in request.GlobalSecondaryIndexes)
					if(indexProvisionedThroughputs.TryGetValue(index.IndexName, out provisionedThroughput))
						index.ProvisionedThroughput = provisionedThroughput.ToAws();
		}
		#endregion

		#region LoadTable
		public async Task<ITable> LoadTableAsync(string tableName, CancellationToken cancellationToken)
		{
			var response = await DB.DescribeTableAsync(new Aws.DescribeTableRequest(tableName), cancellationToken).ConfigureAwait(false);
			return new Table(this, response.Table);
		}
		public ITable LoadTable(string tableName)
		{
			var response = DB.DescribeTable(tableName);
			return new Table(this, response.Table);
		}
		#endregion

		#region DeleteTable
		public Task DeleteTableAsync(string tableName, CancellationToken cancellationToken)
		{
			var request = new Aws.DeleteTableRequest(tableName);
			return DB.DeleteTableAsync(request, cancellationToken);
		}
		public void DeleteTable(string tableName)
		{
			DB.DeleteTable(tableName);
		}
		#endregion
	}
}
