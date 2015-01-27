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
	public interface IIndex
	{
		ITable Table { get; }
		string Name { get; }
		IndexSchema Schema { get; }
		TableStatus Status { get; }
		IProvisionedThroughputInfo ProvisionedThroughput { get; }

		// In the following interface API, the only one argument per method group should have a default value. The priority of those arguments is:
		//     cancellationToken for async methods (not returning IAsyncEnumerable)
		//     readAhead for methods returning IAsyncEnumerable
		//     consistent (either non-async or not returning IAsyncEnumerable)
		// That will ensure a consistent set of overloads and prevent users from needing to specify parameter names

		IReverseSyntax Query(DynamoDBKeyValue hashKey, bool consistent = false);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent = false);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent = false);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent = false);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent = false);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent = false);
	}

	internal class Index : IIndex
	{
		private readonly Table table;

		public Index(Table table, string name)
		{
			this.table = table;
			Name = name;
		}

		internal void UpdateDescription(Aws.GlobalSecondaryIndexDescription description, IndexSchema schema)
		{
			//TODO in debug check that index names match
			Schema = schema;
			ProvisionedThroughput = description.ProvisionedThroughput.ToInfo();
			Status = description.IndexStatus.ToTableStatus();
		}
		internal void UpdateDescription(Aws.LocalSecondaryIndexDescription description, IndexSchema schema)
		{
			//TODO in debug check that index names match
			Schema = schema;
			Status = table.Status;
		}

		public ITable Table { get { return table; } }
		public string Name { get; private set; }
		public IndexSchema Schema { get; private set; }
		public TableStatus Status { get; private set; }
		public IProvisionedThroughputInfo ProvisionedThroughput { get; private set; }

		#region Query
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, bool consistent)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, null, null, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, filter, null, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, filter, values, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, null, null, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, filter, null, consistent);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, filter, values, consistent);
		}
		#endregion
	}
}
