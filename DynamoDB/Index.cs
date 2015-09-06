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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Adamantworks.Amazon.DynamoDB.Syntax;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public interface IIndex : IIndexConsistentSyntax
	{
		ITable Table { get; }
		string Name { get; }
		IndexSchema Schema { get; }
		long ItemCount { get; }
		long SizeInBytes { get; }
		CollectionStatus Status { get; }
		IProvisionedThroughputInfo ProvisionedThroughput { get; }

		ItemKey GetKey(DynamoDBMap item);

		IIndexConsistentSyntax With(ProjectionExpression projection);
	}

	internal partial class Index : IIndex
	{
		internal readonly Table Table;
		private readonly IndexReadContext consistentReadContext;
		private readonly IndexReadContext eventuallyConsistentReadContext;

		public Index(Table table, string name)
		{
			Table = table;
			Name = name;
			consistentReadContext = new IndexReadContext(this, null, true);
			eventuallyConsistentReadContext = new IndexReadContext(this, null, false);
		}

		internal void UpdateDescription(Aws.GlobalSecondaryIndexDescription description, IndexSchema schema)
		{
			//TODO in debug check that index names match
			Schema = schema;
			ItemCount = description.ItemCount;
			SizeInBytes = description.IndexSizeBytes;
			Status = description.IndexStatus.ToCollectionStatus();
			ProvisionedThroughput = description.ProvisionedThroughput.ToInfo();
			// TODO support the backfilling flag
		}
		internal void UpdateDescription(Aws.LocalSecondaryIndexDescription description, IndexSchema schema)
		{
			//TODO in debug check that index names match
			Schema = schema;
			ItemCount = description.ItemCount;
			SizeInBytes = description.IndexSizeBytes;
			Status = Table.Status;
		}

		ITable IIndex.Table { get { return Table; } }
		public string Name { get; private set; }
		public IndexSchema Schema { get; private set; }
		public long ItemCount { get; private set; }
		public long SizeInBytes { get; private set; }
		public CollectionStatus Status { get; private set; }
		public IProvisionedThroughputInfo ProvisionedThroughput { get; private set; }

		public ItemKey GetKey(DynamoDBMap item)
		{
			return Schema.Key.GetKey(item);
		}

		public IIndexConsistentSyntax With(ProjectionExpression projection)
		{
			return new IndexReadContext(this, projection);
		}

		public IIndexReadSyntax Consistent
		{
			get { return consistentReadContext; }
		}
		public IIndexReadSyntax ConsistentIf(bool consistent)
		{
			return consistent ? consistentReadContext : eventuallyConsistentReadContext;
		}
	}
}
