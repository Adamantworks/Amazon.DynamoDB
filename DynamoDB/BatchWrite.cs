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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public interface IBatchWrite
	{
		void Complete();
	}

	internal class BatchWrite : IBatchWrite, IBatchWriteOperations
	{
		private readonly Region region;
		private readonly Queue<BatchWriteRequest> requests = new Queue<BatchWriteRequest>();
		private bool complete;

		public BatchWrite(Region region)
		{
			this.region = region;
		}

		public void Put(ITable table, DynamoDBMap item)
		{
			if(complete) throw new InvalidOperationException(ExceptionMessages.BatchComplete);
			requests.Enqueue(BatchWriteRequest.Put(table, item));
			if(requests.Count >= Limits.BatchWriteSize)
				DoBatchWrite();
		}

		public void Delete(ITable table, ItemKey key)
		{
			if(complete) throw new InvalidOperationException(ExceptionMessages.BatchComplete);
			requests.Enqueue(BatchWriteRequest.Delete(table, key));
			if(requests.Count >= Limits.BatchWriteSize)
				DoBatchWrite();
		}

		private void DoBatchWrite(bool allowPartial = false)
		{
			var batchRequests = DequeueBatch(allowPartial);
			if(batchRequests == null) return; // Someone must have beat us to it
			var request = new Aws.BatchWriteItemRequest(batchRequests.GroupBy(r => r.TableName).ToDictionary(g => g.Key, g => g.Select(r => r.Request).ToList()));
			var response = region.DB.BatchWriteItem(request);
			foreach(var unprocessedTable in response.UnprocessedItems)
				foreach(var unprocessedItem in unprocessedTable.Value)
					requests.Enqueue(new BatchWriteRequest(unprocessedTable.Key, unprocessedItem));
		}

		private IList<BatchWriteRequest> DequeueBatch(bool allowPartial)
		{
			var currentRequests = new List<BatchWriteRequest>(Limits.BatchWriteSize);

			if(!allowPartial && requests.Count < Limits.BatchWriteSize) return null;

			for(var i = 0; i < Limits.BatchWriteSize && requests.Count > 0; i++)
				currentRequests.Add(requests.Dequeue());

			return currentRequests;
		}

		public void Complete()
		{
			if(complete) throw new InvalidOperationException(ExceptionMessages.BatchComplete);
			complete = true; // really there is a risk some other method is still completing, but the caller should prevent that

			while(requests.Count > 0) // because of unprocessed items we might have to do this more than once
				DoBatchWrite(true);
		}
	}
}
