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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public interface ITable
	{
		string Name { get; }
		TableSchema Schema { get; }
		IReadOnlyDictionary<string, IIndex> Indexes { get; }
		DateTime CreationDateTime { get; }
		long ItemCount { get; }
		long SizeInBytes { get; }
		TableStatus Status { get; }
		IProvisionedThroughput ProvisionedThroughput { get; }

		Task<DynamoDBMap> Get(DynamoDBKeyValue hashKey, bool consistent = false, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent = false, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> Get(ItemKey key, bool consistent = false, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> Get(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent = false, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent = false, CancellationToken cancellationToken = default(CancellationToken));
		Task<DynamoDBMap> Get(ItemKey key, ProjectionExpression projection, bool consistent = false, CancellationToken cancellationToken = default(CancellationToken));

		// TODO:Task<Item> Get(IBatchGet batch);
		// TODO:Task Put(Item item);
		// TODO:Task Put(IBatchWrite batch, Item item);
		// TODO:Task Insert() // does a put with a condition that the row doesn't exist
		Task Update(DynamoDBKeyValue hashKey, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task Update(ItemKey key, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task Update(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));

		Task<bool> TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdate(ItemKey key, UpdateExpression update, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> TryUpdate(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values = null, CancellationToken cancellationToken = default(CancellationToken));

		// TODO:Task Delete();
		// TODO:Task Delete(IBatchWrite batch);

		// TODO:IAsyncEnumerable<Item> Query();
		// TODO: QueryBeginsWtih etc
		// TODO: Could combine queries as Query(hashKey, consistent...).BeginsWith

		IScanContext Scan(ReadAhead readAhead = ReadAhead.Some);
		IScanContext Scan(PredicateExpression filter, ReadAhead readAhead = ReadAhead.Some);
		IScanContext Scan(PredicateExpression filter, Values values, ReadAhead readAhead = ReadAhead.Some);
		IScanContext Scan(ProjectionExpression projection, ReadAhead readAhead = ReadAhead.Some);
		IScanContext Scan(ProjectionExpression projection, PredicateExpression filter, ReadAhead readAhead = ReadAhead.Some);
		IScanContext Scan(ProjectionExpression projection, PredicateExpression filter, Values values, ReadAhead readAhead = ReadAhead.Some);

		// TODO: Count()
		// TODO: Task GetStatus(); // TODO maybe this is part of some larger op
		// TODO: Task UpdateProvisionedThroughput();

		// TODO: bool ReportConsumedCapacity
		// TODO: CapacityConsumedEvent
	}

	internal class Table : ITable
	{
		internal readonly Region Region;

		public Table(Region region, Aws.TableDescription tableDescription)
		{
			Region = region;
			UpdateTableDescription(tableDescription);
		}

		private void UpdateTableDescription(Aws.TableDescription tableDescription)
		{
			Name = tableDescription.TableName;
			Schema = tableDescription.ToSchema();
			CreationDateTime = tableDescription.CreationDateTime;
			ItemCount = tableDescription.ItemCount;
			SizeInBytes = tableDescription.TableSizeBytes;
			Status = tableDescription.TableStatus.ToTableStatus();
			// TODO: ProvisionedThroughput =

			// Indexes
			var existingIndexes = Indexes;
			var newIndexes = new Dictionary<string, IIndex>();
			foreach(var indexDescription in tableDescription.GlobalSecondaryIndexes)
			{
				IIndex existingIndex = null;
				if(existingIndexes != null)
					existingIndexes.TryGetValue(indexDescription.IndexName, out existingIndex);
				var newIndex = (Index)existingIndex ?? new Index(this, indexDescription.IndexName);
				newIndex.UpdateDescription(indexDescription, Schema.Indexes[indexDescription.IndexName]);
				newIndexes.Add(indexDescription.IndexName, newIndex);
			}
			foreach(var indexDescription in tableDescription.LocalSecondaryIndexes)
			{
				IIndex existingIndex = null;
				if(existingIndexes != null)
					existingIndexes.TryGetValue(indexDescription.IndexName, out existingIndex);
				var newIndex = (Index)existingIndex ?? new Index(this, indexDescription.IndexName);
				newIndex.UpdateDescription(indexDescription, Schema.Indexes[indexDescription.IndexName]);
				newIndexes.Add(indexDescription.IndexName, newIndex);
			}
			Indexes = newIndexes;
		}

		public string Name { get; private set; }
		public TableSchema Schema { get; private set; }
		public IReadOnlyDictionary<string, IIndex> Indexes { get; private set; }
		public DateTime CreationDateTime { get; private set; }
		public long ItemCount { get; private set; }
		public long SizeInBytes { get; private set; }
		public TableStatus Status { get; private set; }
		public IProvisionedThroughput ProvisionedThroughput { get; private set; }

		public Task<DynamoDBMap> Get(DynamoDBKeyValue hashKey, bool consistent, CancellationToken cancellationToken)
		{
			return Get(new ItemKey(hashKey), null, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, bool consistent, CancellationToken cancellationToken)
		{
			return Get(new ItemKey(hashKey, rangeKey), null, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> Get(ItemKey key, bool consistent, CancellationToken cancellationToken)
		{
			return Get(key, null, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> Get(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			return Get(new ItemKey(hashKey), projection, consistent, cancellationToken);
		}
		public Task<DynamoDBMap> Get(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			return Get(new ItemKey(hashKey, rangeKey), projection, consistent, cancellationToken);
		}
		public async Task<DynamoDBMap> Get(ItemKey key, ProjectionExpression projection, bool consistent, CancellationToken cancellationToken)
		{
			var request = new Aws.GetItemRequest()
			{
				TableName = Name,
				Key = key.ToAws(Schema.Key),
				ConsistentRead = consistent,
			};
			if(projection != null)
			{
				request.ProjectionExpression = projection.Expression;
				request.ExpressionAttributeNames = AwsAttributeNames.Get(projection);
			}
			var result = await Region.DB.GetItemAsync(request, cancellationToken).ConfigureAwait(false);
			return result.Item.ToValue();
		}

		public Task Update(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return Update(new ItemKey(hashKey), update, null, values, cancellationToken);
		}
		public Task Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, null, values, cancellationToken);
		}
		public Task Update(ItemKey key, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return Update(key, update, null, values, cancellationToken);
		}
		public Task Update(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return Update(new ItemKey(hashKey), update, condition, values, cancellationToken);
		}
		public Task Update(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return Update(new ItemKey(hashKey, rangeKey), update, condition, values, cancellationToken); ;
		}
		public async Task Update(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			var request = new Aws.UpdateItemRequest()
			{
				TableName = Name,
				Key = key.ToAws(Schema.Key),
				UpdateExpression = update.Expression,
				ExpressionAttributeNames = AwsAttributeNames.GetCombined(update, condition),
			};
			if(condition != null)
				request.ConditionExpression = condition.Expression;
			request.ExpressionAttributeValues = AwsAttributeValues.GetCombined(update, condition, values);

			await Region.DB.UpdateItemAsync(request, cancellationToken).ConfigureAwait(false);
		}

		public Task<bool> TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdate(new ItemKey(hashKey), update, null, values, cancellationToken);
		}
		public Task<bool> TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdate(new ItemKey(hashKey, rangeKey), update, null, values, cancellationToken);
		}
		public Task<bool> TryUpdate(ItemKey key, UpdateExpression update, Values values, CancellationToken cancellationToken)
		{
			return TryUpdate(key, update, null, values, cancellationToken);
		}
		public Task<bool> TryUpdate(DynamoDBKeyValue hashKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return TryUpdate(new ItemKey(hashKey), update, condition, values, cancellationToken);
		}
		public Task<bool> TryUpdate(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			return TryUpdate(new ItemKey(hashKey, rangeKey), update, condition, values, cancellationToken);
		}
		public async Task<bool> TryUpdate(ItemKey key, UpdateExpression update, PredicateExpression condition, Values values, CancellationToken cancellationToken)
		{
			try
			{
				await Update(key, update, condition, values, cancellationToken);
				return true;
			}
			catch(Aws.ConditionalCheckFailedException)
			{
				return false;
			}
		}

		public IScanContext Scan(ReadAhead readAhead)
		{
			return new ScanContext(Region, Name, null, null, null, readAhead);
		}
		public IScanContext Scan(PredicateExpression filter, ReadAhead readAhead )
		{
			return new ScanContext(Region, Name, null, filter, null, readAhead);
		}
		public IScanContext Scan(PredicateExpression filter, Values values, ReadAhead readAhead )
		{
			return new ScanContext(Region, Name, null, filter, values, readAhead);
		}
		public IScanContext Scan(ProjectionExpression projection, ReadAhead readAhead )
		{
			return new ScanContext(Region, Name, projection, null, null, readAhead);
		}
		public IScanContext Scan(ProjectionExpression projection, PredicateExpression filter, ReadAhead readAhead )
		{
			return new ScanContext(Region, Name, projection, filter, null, readAhead);
		}
		public IScanContext Scan(ProjectionExpression projection, PredicateExpression filter, Values values, ReadAhead readAhead )
		{
			return new ScanContext(Region, Name, projection, filter, values, readAhead);
		}
	}
}
