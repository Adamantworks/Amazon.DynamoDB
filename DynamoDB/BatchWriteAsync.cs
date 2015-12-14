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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public interface IBatchWriteAsync
	{
		Task Complete();
	}

	internal class BatchWriteAsync : IBatchWriteAsync, IBatchWriteOperations
	{
		private readonly Region region;
		private readonly CancellationTokenSource cts;
		private readonly CancellationToken cancellationToken;
		private readonly ConcurrentQueue<BatchWriteRequest> requests = new ConcurrentQueue<BatchWriteRequest>();
		private readonly ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
		private readonly ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
		private readonly object syncRoot = new object();
		private bool complete;

		public BatchWriteAsync(Region region, CancellationToken cancellationToken)
		{
			this.region = region;
			cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			this.cancellationToken = cts.Token;
		}

		public void Put(ITable table, DynamoDBMap item)
		{
			ReportExceptions();
			if(complete) throw new InvalidOperationException(ExceptionMessages.BatchComplete);
			requests.Enqueue(BatchWriteRequest.Put(table, item));
			if(requests.Count >= Limits.BatchWriteSize)
				DoBatchWrite();
		}

		public void Delete(ITable table, ItemKey key)
		{
			ReportExceptions();
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
			var response = region.DB.BatchWriteItemAsync(request, cancellationToken);
			tasks.Enqueue(HandleResponse(response));
		}

		private IList<BatchWriteRequest> DequeueBatch(bool allowPartial)
		{
			// Pull out items for a batch (must be in lock so two people don't try to grab the same set
			var currentRequests = new List<BatchWriteRequest>(Limits.BatchWriteSize);
			lock(syncRoot)
			{
				if(!allowPartial && requests.Count < Limits.BatchWriteSize) return null;
				BatchWriteRequest request;
				for(var i = 0; i < Limits.BatchWriteSize; i++)
					if(requests.TryDequeue(out request))
						currentRequests.Add(request);
					else
						break; // couldn't get a full batch
			}

			if(allowPartial || currentRequests.Count == Limits.BatchWriteSize) return currentRequests;

			// Something went really wrong, we'll just put our items back and bail
			foreach(var request in currentRequests)
				requests.Enqueue(request);
			return null;
		}

		private async Task HandleResponse(Task<Aws.BatchWriteItemResponse> responseTask)
		{
			try
			{
				var response = await responseTask.ConfigureAwait(false);
				foreach(var unprocessedTable in response.UnprocessedItems)
					foreach(var unprocessedItem in unprocessedTable.Value)
						requests.Enqueue(new BatchWriteRequest(unprocessedTable.Key, unprocessedItem));
			}
			catch(Exception ex)
			{
				cts.Cancel();
				exceptions.Enqueue(ex);
				lock(syncRoot)
				{
					complete = true;
				}
			}
		}

		private void ReportExceptions()
		{
			if(exceptions.IsEmpty) return;
			// Wait for all to complete, so we get all exceptions and clear task blockage.
			// Since we cancelled, hopefully won't take too long.
			Task.WaitAll(tasks.ToArray());
			var allExceptions = exceptions.ToArray();
			var triggeringExceptions = allExceptions.Where(ex => !(ex is OperationCanceledException)).ToList();
			if(triggeringExceptions.Count > 0)
				ThrowException(triggeringExceptions);
			else
				ThrowException(allExceptions);
		}

		private static void ThrowException(ICollection<Exception> allExceptions)
		{
			if(allExceptions.Count == 1)
			{
				var ex = allExceptions.Single();
				throw new Exception("Batch Write Failed: " + ex.Message, ex);
			}

			throw new AggregateException("Batch Write Failed", allExceptions);
		}

		public async Task Complete()
		{
			// only one thread should complete the batch
			lock(syncRoot)
			{
				if(complete) throw new InvalidOperationException(ExceptionMessages.BatchComplete);
				complete = true; // really there is a risk some other method is still completing, but the caller should prevent that
			}

			cancellationToken.ThrowIfCancellationRequested();

			// queue up any items not done because we don't have a full batch (most likely won't get more items)
			if(!requests.IsEmpty)
				DoBatchWrite(true);

			// work through the tasks, this will cause any items to be retryed as needed
			do
			{
				ReportExceptions();

				// if we have enough requests for a batch or are out of responses
				if(requests.Count >= Limits.BatchWriteSize
					|| (tasks.IsEmpty && !requests.IsEmpty))
					DoBatchWrite(true);

				Task responseTask;
				if(tasks.TryDequeue(out responseTask))
					await responseTask.ConfigureAwait(false);
			} while(!cancellationToken.IsCancellationRequested && (!tasks.IsEmpty || !requests.IsEmpty));

			// If we exited the loop because of the cancellation token there could be outstanding exceptions we need to report
			ReportExceptions();
		}
	}
}
