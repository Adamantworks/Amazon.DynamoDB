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

using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly IDictionary<ITable, IList<DynamoDBMap>> puts = new Dictionary<ITable, IList<DynamoDBMap>>();
		private readonly IDictionary<ITable, IList<ItemKey>> deletes = new Dictionary<ITable, IList<ItemKey>>();

		public BatchWrite(Region region)
		{
			this.region = region;
		}

		public void Put(ITable table, DynamoDBMap item)
		{
			IList<DynamoDBMap> items;
			if(!puts.TryGetValue(table, out items))
				puts.Add(table, items = new List<DynamoDBMap>());

			items.Add(item);
			Flush(false);
		}

		public void Complete()
		{
			throw new NotImplementedException();
		}

		private void Flush(bool force)
		{
			if(!force && (puts.Count + deletes.Count) < Limits.BatchWriteSize) return;
			var requestItems = new Dictionary<string, List<Aws.WriteRequest>>();
			foreach(var putGroup in puts)
			{
				List<Aws.WriteRequest> writeRequests;
				if(!requestItems.TryGetValue(putGroup.Key.Name, out writeRequests))
					requestItems.Add(putGroup.Key.Name, writeRequests = new List<Aws.WriteRequest>());

				writeRequests.AddRange(putGroup.Value.Select(put => new Aws.WriteRequest(new Aws.PutRequest(put.ToAwsDictionary()))));
			}
			puts.Clear();
			foreach(var deleteGroup in deletes)
			{
				List<Aws.WriteRequest> writeRequests;
				if(!requestItems.TryGetValue(deleteGroup.Key.Name, out writeRequests))
					requestItems.Add(deleteGroup.Key.Name, writeRequests = new List<Aws.WriteRequest>());

				var keySchema = deleteGroup.Key.Schema.Key;
				writeRequests.AddRange(deleteGroup.Value.Select(delete => new Aws.WriteRequest(new Aws.DeleteRequest(delete.ToAws(keySchema)))));
			}
			deletes.Clear();

			var request = new Aws.BatchWriteItemRequest(requestItems);
			var response = region.DB.BatchWriteItem(request);
			// TODO deal with unprocessed items
			throw new NotImplementedException();
		}
	}
}
