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
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Schema;
using Amazon.DynamoDBv2;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public interface IDynamoDBRegion
	{
		IBatchGetAsync BeginBatchGetAsync();
		IBatchWriteAsync BeginBatchWriteAsync();
		IAsyncEnumerable<string> ListTablesAsync(CancellationToken cancellationToken = default(CancellationToken));

		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, CancellationToken cancellationToken = default(CancellationToken));
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughput = null, CancellationToken cancellationToken = default(CancellationToken));
		ITable CreateTable(string tableName, TableSchema schema);
		ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughput = null);

		Task<ITable> LoadTableAsync(string tableName, CancellationToken cancellationToken = default(CancellationToken));

		Task DeleteTableAsync(string tableName, CancellationToken cancellationToken = default(CancellationToken));
		void DeleteTable(string tableName);
	}

	internal class Region : IDynamoDBRegion
	{
		internal readonly IAmazonDynamoDB DB;

		internal Region(IAmazonDynamoDB db)
		{
			DB = db;
		}

		public IBatchGetAsync BeginBatchGetAsync()
		{
			throw new System.NotImplementedException();
		}

		public IBatchWriteAsync BeginBatchWriteAsync()
		{
			throw new System.NotImplementedException();
		}

		public IAsyncEnumerable<string> ListTablesAsync(CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		#region CreateTable
		public async Task<ITable> CreateTableAsync(string tableName, TableSchema schema, CancellationToken cancellationToken)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			var response = await DB.CreateTableAsync(request, cancellationToken).ConfigureAwait(false);
			return new Table(this, response.TableDescription);
		}
		public async Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughput, CancellationToken cancellationToken)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			RequestProvisionedThroughput(request, provisionedThroughput, indexProvisionedThroughput);
			var response = await DB.CreateTableAsync(request, cancellationToken).ConfigureAwait(false);
			return new Table(this, response.TableDescription);
		}

		public ITable CreateTable(string tableName, TableSchema schema)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			var response = DB.CreateTable(request);
			return new Table(this, response.TableDescription);
		}

		public ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughput)
		{
			var request = BuildCreateTableRequest(tableName, schema);
			RequestProvisionedThroughput(request, provisionedThroughput, indexProvisionedThroughput);
			var response = DB.CreateTable(request);
			return new Table(this, response.TableDescription);
		}

		private static Aws.CreateTableRequest BuildCreateTableRequest(string tableName, TableSchema schema)
		{
			var request = new Aws.CreateTableRequest()
			{
				TableName = tableName,
			};
			throw new NotImplementedException();
			return request;
		}
		private static Aws.CreateTableRequest RequestProvisionedThroughput(Aws.CreateTableRequest request, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughput)
		{
			throw new NotImplementedException();
		}
		#endregion

		public async Task<ITable> LoadTableAsync(string tableName, CancellationToken cancellationToken)
		{
			var response = await DB.DescribeTableAsync(new Aws.DescribeTableRequest(tableName), cancellationToken).ConfigureAwait(false);
			return new Table(this, response.Table);
		}

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
