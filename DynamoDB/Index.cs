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
	public interface IIndex : IConsistentQuerySyntax
	{
		ITable Table { get; }
		string Name { get; }
		IndexSchema Schema { get; }
		CollectionStatus Status { get; }
		IProvisionedThroughputInfo ProvisionedThroughput { get; }

		ItemKey GetKey(DynamoDBMap item);

		IConsistentQuerySyntax With(ProjectionExpression projection);
	}

	// See Overloads.tt and Overloads.cs for more methods of this class
	internal partial class Index : IIndex
	{
		internal readonly Table Table;
		private readonly IndexContext consistentContext;
		private readonly IndexContext eventuallyConsistentContext;

		public Index(Table table, string name)
		{
			Table = table;
			Name = name;
			consistentContext = new IndexContext(this, null, true);
			eventuallyConsistentContext = new IndexContext(this, null, false);
		}

		internal void UpdateDescription(Aws.GlobalSecondaryIndexDescription description, IndexSchema schema)
		{
			//TODO in debug check that index names match
			Schema = schema;
			ProvisionedThroughput = description.ProvisionedThroughput.ToInfo();
			Status = description.IndexStatus.ToCollectionStatus();
		}
		internal void UpdateDescription(Aws.LocalSecondaryIndexDescription description, IndexSchema schema)
		{
			//TODO in debug check that index names match
			Schema = schema;
			Status = Table.Status;
		}

		ITable IIndex.Table { get { return Table; } }
		public string Name { get; private set; }
		public IndexSchema Schema { get; private set; }
		public CollectionStatus Status { get; private set; }
		public IProvisionedThroughputInfo ProvisionedThroughput { get; private set; }

		public ItemKey GetKey(DynamoDBMap item)
		{
			return Schema.Key.GetKey(item);
		}

		public IConsistentQuerySyntax With(ProjectionExpression projection)
		{
			return new IndexContext(this, projection);
		}

		public IQuerySyntax Consistent
		{
			get { return consistentContext; }
		}
		public IQuerySyntax ConsistentIf(bool consistent)
		{
			return consistent ? consistentContext : eventuallyConsistentContext;
		}
	}
}
