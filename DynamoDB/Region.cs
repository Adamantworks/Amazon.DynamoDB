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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public interface IDynamoDBRegion
	{
		IBatchGet BeginBatchGet();
		IBatchWrite BeginBatchWrite();
		IAsyncEnumerable<string> ListTables(CancellationToken cancellationToken = default(CancellationToken));
		Task<ITable> CreateTable(CancellationToken cancellationToken = default(CancellationToken));
		Task<ITable> LoadTable(string tableName, CancellationToken cancellationToken = default(CancellationToken));
		Task DeleteTable(CancellationToken cancellationToken = default(CancellationToken));
	}

	internal class Region : IDynamoDBRegion
	{
		internal readonly IAmazonDynamoDB DB;

		internal Region(IAmazonDynamoDB db)
		{
			DB = db;
		}

		IBatchGet IDynamoDBRegion.BeginBatchGet()
		{
			throw new System.NotImplementedException();
		}

		IBatchWrite IDynamoDBRegion.BeginBatchWrite()
		{
			throw new System.NotImplementedException();
		}

		IAsyncEnumerable<string> IDynamoDBRegion.ListTables(CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		Task<ITable> IDynamoDBRegion.CreateTable(CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		async Task<ITable> IDynamoDBRegion.LoadTable(string tableName, CancellationToken cancellationToken)
		{
			var response = await DB.DescribeTableAsync(new Aws.DescribeTableRequest(tableName), cancellationToken).ConfigureAwait(false);
			return new Table(this, response.Table);
		}

		Task IDynamoDBRegion.DeleteTable(CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}
	}
}
