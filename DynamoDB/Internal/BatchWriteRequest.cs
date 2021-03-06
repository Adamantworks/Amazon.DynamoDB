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

using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	internal class BatchWriteRequest
	{
		public readonly string TableName;
		public readonly Aws.WriteRequest Request;

		public BatchWriteRequest(string tableName, Aws.WriteRequest request)
		{
			TableName = tableName;
			Request = request;
		}

		public static BatchWriteRequest Put(ITable table, DynamoDBMap item)
		{
			return new BatchWriteRequest(table.Name, new Aws.WriteRequest(new Aws.PutRequest(item.ToAwsDictionary())));
		}

		public static BatchWriteRequest Delete(ITable table, ItemKey key)
		{
			return new BatchWriteRequest(table.Name, new Aws.WriteRequest(new Aws.DeleteRequest(key.ToAws(table.Schema.Key))));
		}
	}
}
