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

using Adamantworks.Amazon.DynamoDB.Contexts;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aws = Amazon.DynamoDBv2.Model;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB
{
	// See Overloads.tt and Overloads.cs for more method overloads of this interface
	public partial interface ITable : IConsistentOrScanSyntax, IPutSyntax
	{
		string Name { get; }
		TableSchema Schema { get; }
		IReadOnlyDictionary<string, IIndex> Indexes { get; }
		DateTime CreationDateTime { get; }
		long ItemCount { get; }
		long SizeInBytes { get; }
		CollectionStatus Status { get; }
		IProvisionedThroughputInfo ProvisionedThroughput { get; }

		Task ReloadAsync(CancellationToken cancellationToken);
		void Reload();

		Task WaitUntilNotAsync(CollectionStatus status, CancellationToken cancellationToken);
		Task WaitUntilNotAsync(CollectionStatus status, TimeSpan timeout, CancellationToken cancellationToken);
		void WaitUntilNot(CollectionStatus status);
		void WaitUntilNot(CollectionStatus status, TimeSpan timeout);

		// The overloads of these methods are in Overloads.tt and call the private implementations
		// Task UpdateTableAsync(...);
		// void UpdateTable(...);

		ItemKey GetKey(DynamoDBMap item);

		IConsistentOrScanSyntax With(ProjectionExpression projection);

		ITryPutSyntax If(PredicateExpression condition);
		ITryPutSyntax If(PredicateExpression condition, Values values);

		IIfSyntax ForKey(DynamoDBKeyValue hashKey);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, IValueConverter valueConverter);
		IIfSyntax ForKey(object hashKey);
		IIfSyntax ForKey(object hashKey, IValueConverter valueConverter);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter valueConverter);
		IIfSyntax ForKey(object hashKey, DynamoDBKeyValue rangeKey);
		IIfSyntax ForKey(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter valueConverter);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, object rangeKey);
		IIfSyntax ForKey(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter valueConverter);
		IIfSyntax ForKey(object hashKey, object rangeKey);
		IIfSyntax ForKey(object hashKey, object rangeKey, IValueConverter valueConverter);
		IIfSyntax ForKey(ItemKey key);

		void PutAsync(IBatchWriteAsync batch, DynamoDBMap item);
		void Put(IBatchWrite batch, DynamoDBMap item);

		Task InsertAsync(DynamoDBMap item);
		Task InsertAsync(DynamoDBMap item, CancellationToken cancellationToken);
		void Insert(DynamoDBMap item);

		Task<bool> TryInsertAsync(DynamoDBMap item);
		Task<bool> TryInsertAsync(DynamoDBMap item, CancellationToken cancellationToken);
		bool TryInsert(DynamoDBMap item);

		void DeleteAsync(IBatchWriteAsync batch, ItemKey key);
		void Delete(IBatchWrite batch, ItemKey key);
	}

	// See Overloads.tt and Overloads.cs for more method overloads of this class
	internal partial class Table : ITable
	{
		internal readonly Region Region;
		private readonly GetContext consistentContext;
		private readonly GetContext eventuallyConsistentContext;
		private readonly PutContext putContext;
		private readonly PutContext insertContext;

		public Table(Region region, Aws.TableDescription tableDescription)
		{
			Region = region;
			UpdateTableDescription(tableDescription);
			consistentContext = new GetContext(this, null, true);
			eventuallyConsistentContext = new GetContext(this, null, false);
			putContext = new PutContext(this, null, null);
			insertContext = new PutContext(this, new PredicateExpression("attribute_not_exists(#key)", "key", Schema.Key.HashKey.Name), null);
		}

		private void UpdateTableDescription(Aws.TableDescription tableDescription)
		{
			Name = tableDescription.TableName;
			Status = tableDescription.TableStatus.ToCollectionStatus();
			// Handle state where all schema info is gone
			if(Status == CollectionStatus.Deleting && tableDescription.AttributeDefinitions.Count == 0) return;
			Schema = tableDescription.ToSchema();
			CreationDateTime = tableDescription.CreationDateTime;
			ItemCount = tableDescription.ItemCount;
			SizeInBytes = tableDescription.TableSizeBytes;
			ProvisionedThroughput = tableDescription.ProvisionedThroughput.ToInfo();

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
		public CollectionStatus Status { get; private set; }
		public IProvisionedThroughputInfo ProvisionedThroughput { get; private set; }

		#region Reload
		public async Task ReloadAsync(CancellationToken cancellationToken)
		{
			try
			{
				var response = await Region.DB.DescribeTableAsync(new Aws.DescribeTableRequest(Name), cancellationToken).ConfigureAwait(false);
				UpdateTableDescription(response.Table);
			}
			catch(Aws.ResourceNotFoundException)
			{
				Status = CollectionStatus.Deleted;
			}
		}

		public void Reload()
		{
			try
			{
				var response = Region.DB.DescribeTable(Name);
				UpdateTableDescription(response.Table);
			}
			catch(Aws.ResourceNotFoundException)
			{
				Status = CollectionStatus.Deleted;
			}
		}
		#endregion

		#region WaitUntilNot
		public async Task WaitUntilNotAsync(CollectionStatus status, CancellationToken cancellationToken)
		{
			CheckCanWaitUntilNot(status);
			await ReloadAsync(cancellationToken).ConfigureAwait(false);
			while((Status == status || Indexes.Values.Any(i => i.Status == status)) && !cancellationToken.IsCancellationRequested)
			{
				await Task.Delay(Region.WaitStatusPollingInterval, cancellationToken).ConfigureAwait(false);
				await ReloadAsync(cancellationToken).ConfigureAwait(false);
			}
		}
		public async Task WaitUntilNotAsync(CollectionStatus status, TimeSpan timeout, CancellationToken cancellationToken)
		{
			CheckCanWaitUntilNot(status);
			await ReloadAsync(cancellationToken).ConfigureAwait(false);
			var start = DateTime.UtcNow;
			TimeSpan timeRemaining;
			while((Status == status || Indexes.Values.Any(i => i.Status == status)) && !cancellationToken.IsCancellationRequested && (timeRemaining = DateTime.UtcNow - start) < timeout)
			{
				await Task.Delay(TimeSpanEx.Min(Region.WaitStatusPollingInterval, timeRemaining), cancellationToken).ConfigureAwait(false);
				await ReloadAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		public void WaitUntilNot(CollectionStatus status)
		{
			CheckCanWaitUntilNot(status);
			Reload();
			while(Status == status || Indexes.Values.Any(i => i.Status == status))
			{
				Thread.Sleep(Region.WaitStatusPollingInterval);
				Reload();
			}
		}
		public void WaitUntilNot(CollectionStatus status, TimeSpan timeout)
		{
			CheckCanWaitUntilNot(status);
			Reload();
			var start = DateTime.UtcNow;
			TimeSpan timeRemaining;
			while((Status == status || Indexes.Values.Any(i => i.Status == status)) && (timeRemaining = DateTime.UtcNow - start) < timeout)
			{
				Thread.Sleep(TimeSpanEx.Min(Region.WaitStatusPollingInterval, timeRemaining));
				Reload();
			}
		}

		private static void CheckCanWaitUntilNot(CollectionStatus status)
		{
			switch(status)
			{
				case CollectionStatus.Creating:
				case CollectionStatus.Updating:
				case CollectionStatus.Deleting:
					return;
				default:
					throw new ArgumentException("Can only wait on transient status (Creating, Updating, Deleting)", "status");
			}
		}
		#endregion

		#region UpdateTable
		private Task UpdateTableAsync(ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs, CancellationToken cancellationToken)
		{
			var request = BuildUpdateTableRequest(provisionedThroughput, indexProvisionedThroughputs);
			return Region.DB.UpdateTableAsync(request, cancellationToken);
		}

		private void UpdateTable(ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = BuildUpdateTableRequest(provisionedThroughput, indexProvisionedThroughputs);
			Region.DB.UpdateTable(request);
		}

		private Aws.UpdateTableRequest BuildUpdateTableRequest(ProvisionedThroughput? provisionedThroughput, IReadOnlyDictionary<string, ProvisionedThroughput> indexProvisionedThroughputs)
		{
			var request = new Aws.UpdateTableRequest()
			{
				TableName = Name,
			};
			if(provisionedThroughput != null)
				request.ProvisionedThroughput = provisionedThroughput.Value.ToAws();
			if(indexProvisionedThroughputs != null && indexProvisionedThroughputs.Count > 0)
				request.GlobalSecondaryIndexUpdates = indexProvisionedThroughputs.Select(i => new Aws.GlobalSecondaryIndexUpdate()
				{
					Update = new Aws.UpdateGlobalSecondaryIndexAction()
					{
						IndexName = i.Key,
						ProvisionedThroughput = i.Value.ToAws(),
					}
				}).ToList();

			return request;
		}
		#endregion

		public ItemKey GetKey(DynamoDBMap item)
		{
			return Schema.Key.GetKey(item);
		}

		public IConsistentOrScanSyntax With(ProjectionExpression projection)
		{
			return new GetContext(this, projection);
		}

		public IGetSyntax Consistent
		{
			get { return consistentContext; }
		}
		public IGetSyntax ConsistentIf(bool consistent)
		{
			return consistent ? consistentContext : eventuallyConsistentContext;
		}

		public ITryPutSyntax If(PredicateExpression condition)
		{
			return new PutContext(this, condition, null);
		}
		public ITryPutSyntax If(PredicateExpression condition, Values values)
		{
			return new PutContext(this, condition, values);
		}

		#region ForKey
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey));
		}
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, IValueConverter valueConverter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, valueConverter));
		}

		public IIfSyntax ForKey(object hashKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey));
		}
		public IIfSyntax ForKey(object hashKey, IValueConverter valueConverter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, valueConverter));
		}

		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey));
		}
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter valueConverter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey, valueConverter));
		}

		public IIfSyntax ForKey(object hashKey, DynamoDBKeyValue rangeKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey));
		}
		public IIfSyntax ForKey(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter valueConverter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey, valueConverter));
		}

		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, object rangeKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey));
		}
		public IIfSyntax ForKey(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter valueConverter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey, valueConverter));
		}

		public IIfSyntax ForKey(object hashKey, object rangeKey)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey));
		}
		public IIfSyntax ForKey(object hashKey, object rangeKey, IValueConverter valueConverter)
		{
			return new ModifyContext(this, ItemKey.Create(hashKey, rangeKey, valueConverter));
		}

		public IIfSyntax ForKey(ItemKey key)
		{
			return new ModifyContext(this, key);
		}
		#endregion

		#region Put
		public void PutAsync(IBatchWriteAsync batch, DynamoDBMap item)
		{
			((IBatchWriteOperations)batch).Put(this, item);
		}

		public void Put(IBatchWrite batch, DynamoDBMap item)
		{
			((IBatchWriteOperations)batch).Put(this, item);
		}
		#endregion

		#region Insert
		public Task InsertAsync(DynamoDBMap item)
		{
			return insertContext.PutAsync(item, false, CancellationToken.None);
		}
		public Task InsertAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			return insertContext.PutAsync(item, false, cancellationToken);
		}

		public void Insert(DynamoDBMap item)
		{
			insertContext.Put(item, false);
		}
		#endregion

		#region TryInsert
		public Task<bool> TryInsertAsync(DynamoDBMap item)
		{
			return insertContext.TryPutAsync(item, CancellationToken.None);
		}
		public Task<bool> TryInsertAsync(DynamoDBMap item, CancellationToken cancellationToken)
		{
			return insertContext.TryPutAsync(item, cancellationToken);
		}

		public bool TryInsert(DynamoDBMap item)
		{
			return insertContext.TryPut(item);
		}
		#endregion

		#region Delete
		public void DeleteAsync(IBatchWriteAsync batch, ItemKey key)
		{
			((IBatchWriteOperations)batch).Delete(this, key);
		}

		public void Delete(IBatchWrite batch, ItemKey key)
		{
			((IBatchWriteOperations)batch).Delete(this, key);
		}
		#endregion
	}
}
