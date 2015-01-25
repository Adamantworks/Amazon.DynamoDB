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

using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;

namespace Adamantworks.Amazon.DynamoDB
{
	public interface IBatchWriteAsync
	{
		Task Complete();
	}

	internal class BatchWriteAsync : IBatchWriteAsync, IBatchWriteOperations
	{
		private readonly Region region;

		public BatchWriteAsync(Region region)
		{
			this.region = region;
		}

		public void Put(ITable table, DynamoDBMap item)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(ITable table, ItemKey key)
		{
			throw new System.NotImplementedException();
		}

		public Task Complete()
		{
			throw new System.NotImplementedException();
		}
	}
}
