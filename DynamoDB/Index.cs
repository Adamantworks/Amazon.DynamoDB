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

		IQueryContext Query(DynamoDBKeyValue hashKey, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, ReadAhead readAhead = ReadAhead.Some);
		IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent, ReadAhead readAhead = ReadAhead.Some);
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
		}
		internal void UpdateDescription(Aws.LocalSecondaryIndexDescription description, IndexSchema schema)
		{
			//TODO in debug check that index names match
			Schema = schema;
		}

		public ITable Table { get { return table; } }
		public string Name { get; private set; }
		public IndexSchema Schema { get; private set; }

		public IQueryContext Query(DynamoDBKeyValue hashKey, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, null, null, false, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, bool consistent, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, null, null, consistent, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, filter, null, false, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, bool consistent, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, filter, null, consistent, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, filter, values, false, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values, bool consistent, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, null, filter, values, consistent, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, null, null, false, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, bool consistent, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, null, null, consistent, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, filter, null, false, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, bool consistent, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, filter, null, consistent, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, filter, values, false, readAhead);
		}
		public IQueryContext Query(DynamoDBKeyValue hashKey, ProjectionExpression projection, PredicateExpression filter, Values values, bool consistent, ReadAhead readAhead)
		{
			return new QueryContext(table.Region, table.Name, Name, Schema.Key, hashKey, projection, filter, values, consistent, readAhead);
		}
	}
}
