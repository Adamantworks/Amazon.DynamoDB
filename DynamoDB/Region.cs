﻿// Copyright 2015 Adamantworks.  All Rights Reserved.
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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using AwsRoot = Amazon.DynamoDBv2;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public partial interface IDynamoDBRegion
	{
		TimeSpan WaitStatusPollingInterval { get; set; }

		IBatchGetAsync BeginBatchGetAsync();
		IBatchGet BeginBatchGet();

		IBatchWriteAsync BeginBatchWriteAsync();
		IBatchWriteAsync BeginBatchWriteAsync(CancellationToken cancellationToken);
		IBatchWrite BeginBatchWrite();

		IAsyncEnumerable<string> ListTablesAsync(ReadAhead readAhead);
		IEnumerable<string> ListTables();

		IEnumerable<string> ListTablesWithPrefix(string tableNamePrefix);

		// The overloads of these methods are in RegionOverloads.tt and call the private implementations
		// Task<ITable> CreateTableAsync(...);
		// ITable CreateTable();

		// TODO replace with overload
		Task<ITable> LoadTableAsync(string tableName, CancellationToken cancellationToken = default(CancellationToken));
		ITable LoadTable(string tableName);

		Task<ITable> TryLoadTableAsync(string tableName, CancellationToken cancellationToken = default(CancellationToken));
		ITable TryLoadTable(string tableName);

		Task DeleteTableAsync(string tableName, CancellationToken cancellationToken = default(CancellationToken));
		void DeleteTable(string tableName);
	}

	internal partial class Region : IDynamoDBRegion
	{
		private static readonly Regex TableNamePrefix = new Regex("^[-A-Za-z._0-9]{1,254}$");

		internal readonly AwsRoot.IAmazonDynamoDB DB;

		internal Region(AwsRoot.IAmazonDynamoDB db)
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
			return new BatchWriteAsync(this, CancellationToken.None);
		}
		public IBatchWriteAsync BeginBatchWriteAsync(CancellationToken cancellationToken)
		{
			return new BatchWriteAsync(this, cancellationToken);
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
			} while(!IsComplete(lastResponse));
		}

		private static bool IsComplete(Aws.ListTablesResponse lastResponse)
		{
			return lastResponse.LastEvaluatedTableName == null;
		}
		#endregion

		#region ListTablesWithPrefix
		public IAsyncEnumerable<string> ListTablesWithPrefixAsync(string tableNamePrefix, ReadAhead readAhead)
		{
			CheckTablePrefix(tableNamePrefix);
			return AsyncEnumerable.Defer(() =>
			{
				// This must be in here so it is deferred until GetEnumerator() is called on us (need one per enumerator)
				var request = new Aws.ListTablesRequest();
				return AsyncEnumerableEx.GenerateChunked<Aws.ListTablesResponse, string>(null,
					(lastResponse, cancellationToken) =>
					{
						request.ExclusiveStartTableName = lastResponse != null ? lastResponse.LastEvaluatedTableName : TableNameBefore(tableNamePrefix);
						return DB.ListTablesAsync(request, cancellationToken);
					},
					lastResponse => lastResponse.TableNames.TakeWhile(tableName => tableName.StartsWith(tableNamePrefix)),
					lastResponse => IsComplete(lastResponse) || !lastResponse.TableNames.Last().StartsWith(tableNamePrefix),
					readAhead);
			});
		}

		public IEnumerable<string> ListTablesWithPrefix(string tableNamePrefix)
		{
			CheckTablePrefix(tableNamePrefix);
			var request = new Aws.ListTablesRequest();
			Aws.ListTablesResponse lastResponse = null;
			do
			{
				request.ExclusiveStartTableName = lastResponse != null ? lastResponse.LastEvaluatedTableName : TableNameBefore(tableNamePrefix);
				lastResponse = DB.ListTables(request);
				foreach(var tableName in lastResponse.TableNames)
					if(tableName.StartsWith(tableNamePrefix))
						yield return tableName;
					else
						yield break;
			} while(!IsComplete(lastResponse));
		}

		private static void CheckTablePrefix(string tableNamePrefix)
		{
			if(tableNamePrefix == null)
				throw new ArgumentNullException("tableNamePrefix");
			if(!TableNamePrefix.IsMatch(tableNamePrefix))
				throw new ArgumentException("Length must be between 1 and 254 (inclusive) and all characters allowed in table names", "tableNamePrefix");
		}
		internal static string TableNameBefore(string name)
		{
			var nameBefore = new StringBuilder(name);
			var end = name.Length - 1;
			switch(name[end])
			{
				case '-':
					if(name.Length == 1)
						return null; // Nothing comes before "-"
					nameBefore.Length -= 1; // Remove the -
					nameBefore[end - 1] = 'z'; // change the char before to z
					break;
				case '.':
					nameBefore[end] = '-';
					break;
				case '0':
					nameBefore[end] = '.';
					break;
				case 'A':
					nameBefore[end] = '9';
					break;
				case '_':
					nameBefore[end] = 'Z';
					break;
				case 'a':
					nameBefore[end] = '_';
					break;
				default:
					nameBefore[end] = (char)(name[end] - 1);
					break;
			}
			return nameBefore.ToString().PadRight(255, 'z');
		}
		#endregion

		#region CreateTable
		private async Task<ITable> CreateTableAsync(string tableName, TableSchema schema, ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			var request = BuildCreateTableRequest(tableName, schema, provisionedThroughput, indexProvisionedThroughputs);
			var response = await DB.CreateTableAsync(request, cancellationToken).ConfigureAwait(false);
			return new Table(this, response.TableDescription);
		}

		private ITable CreateTable(string tableName, TableSchema schema, ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = BuildCreateTableRequest(tableName, schema, provisionedThroughput, indexProvisionedThroughputs);
			var response = DB.CreateTable(request);
			return new Table(this, response.TableDescription);
		}

		private static Aws.CreateTableRequest BuildCreateTableRequest(string tableName, TableSchema schema, ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = new Aws.CreateTableRequest()
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
					KeySchema = i.Value.Key.ToAws(),
					Projection = i.Value.ToAwsProjection(),
					ProvisionedThroughput = new Aws.ProvisionedThroughput(1, 1),
				}).ToList(),
				AttributeDefinitions = schema.ToAwsAttributeDefinitions(),
				ProvisionedThroughput = new Aws.ProvisionedThroughput(1, 1),
			};
			if(provisionedThroughput != null)
				request.ProvisionedThroughput = provisionedThroughput.Value.ToAws();
			if(indexProvisionedThroughputs != null && indexProvisionedThroughputs.Count != 0)
			{
				ProvisionedThroughput indexProvisionedThroughput;
				foreach(var index in request.GlobalSecondaryIndexes)
					if(indexProvisionedThroughputs.TryGetValue(index.IndexName, out indexProvisionedThroughput))
						index.ProvisionedThroughput = indexProvisionedThroughput.ToAws();
			}
			return request;
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

		#region TryLoadTable
		public async Task<ITable> TryLoadTableAsync(string tableName, CancellationToken cancellationToken)
		{
			try
			{
				return await LoadTableAsync(tableName, cancellationToken).ConfigureAwait(false);
			}
			catch(Aws.ResourceNotFoundException)
			{
				return null;
			}
		}

		public ITable TryLoadTable(string tableName)
		{
			try
			{
				return LoadTable(tableName);
			}
			catch(Aws.ResourceNotFoundException)
			{
				return null;
			}
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
