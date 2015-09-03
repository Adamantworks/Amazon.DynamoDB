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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Schema;

namespace Adamantworks.Amazon.DynamoDB
{
	public partial interface IDynamoDBRegion
	{
		IAsyncEnumerable<string> ListTablesAsync();

		IAsyncEnumerable<string> ListTablesWithPrefixAsync(string tableNamePrefix);

		Task<ITable> CreateTableAsync(string tableName, TableSchema schema);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, CancellationToken cancellationToken);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken);
		Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken);
		ITable CreateTable(string tableName, TableSchema schema);
		ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput);
		ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs);
	}

	internal partial class Region
	{
		#region ListTablesAsync
		public IAsyncEnumerable<string> ListTablesAsync()
		{
			return ListTablesAsync(ReadAhead.Some);
		}
		#endregion

		#region ListTablesWithPrefixAsync
		public IAsyncEnumerable<string> ListTablesWithPrefixAsync(string tableNamePrefix)
		{
			return ListTablesWithPrefixAsync(tableNamePrefix, ReadAhead.Some);
		}
		#endregion

		#region CreateTableAsync
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema)
		{
			return CreateTableAsync(tableName, schema, null, null, CancellationToken.None);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput)
		{
			return CreateTableAsync(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, null, CancellationToken.None);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			return CreateTableAsync(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs, CancellationToken.None);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, CancellationToken cancellationToken)
		{
			return CreateTableAsync(tableName, schema, null, null, cancellationToken);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, CancellationToken cancellationToken)
		{
			return CreateTableAsync(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, null, cancellationToken);
		}
		public Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			return CreateTableAsync(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs, cancellationToken);
		}
		#endregion

		#region CreateTable
		public ITable CreateTable(string tableName, TableSchema schema)
		{
			return CreateTable(tableName, schema, null, null);
		}
		public ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput)
		{
			return CreateTable(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, null);
		}
		public ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			return CreateTable(tableName, schema, (ProvisionedThroughput?)provisionedThroughput, indexProvisionedThroughputs);
		}
		#endregion
	}
}