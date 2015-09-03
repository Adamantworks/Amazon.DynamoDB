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
using Adamantworks.Amazon.DynamoDB.Syntax;

namespace Adamantworks.Amazon.DynamoDB
{
	internal partial class Index
	{
		#region Query
		public IReverseSyntax Query(DynamoDBKeyValue hashKey)
		{
			return new QueryContext(Table, this, null, false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryContext(Table, this, null, false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(Table, this, null, false, hashKey, filter, values);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new QueryContext(Table, this, null, false, hashKey, null, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(Table, this, null, false, hashKey, filter, null);
		}
		public IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(Table, this, null, false, hashKey, filter, values);
		}
		public IReverseSyntax Query(object hashKey)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey), null, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey), filter, null);
		}
		public IReverseSyntax Query(object hashKey, PredicateExpression filter, Values values)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey), filter, values);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), null, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, null);
		}
		public IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryContext(Table, this, null, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, values);
		}
		#endregion

		#region QueryCount
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey)
		{
			return new QueryCountContext(Table, this, false, hashKey, null, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, PredicateExpression filter)
		{
			return new QueryCountContext(Table, this, false, hashKey, filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(Table, this, false, hashKey, filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter)
		{
			return new QueryCountContext(Table, this, false, hashKey, null, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryCountContext(Table, this, false, hashKey, filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(Table, this, false, hashKey, filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey)
		{
			return new QueryCountContext(Table, this, false, DynamoDBKeyValue.Convert(hashKey), null, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, PredicateExpression filter)
		{
			return new QueryCountContext(Table, this, false, DynamoDBKeyValue.Convert(hashKey), filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(Table, this, false, DynamoDBKeyValue.Convert(hashKey), filter, values);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter)
		{
			return new QueryCountContext(Table, this, false, DynamoDBKeyValue.Convert(hashKey, converter), null, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter, PredicateExpression filter)
		{
			return new QueryCountContext(Table, this, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, null);
		}
		public IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter, PredicateExpression filter, Values values)
		{
			return new QueryCountContext(Table, this, false, DynamoDBKeyValue.Convert(hashKey, converter), filter, values);
		}
		#endregion

		#region Scan
		public IScanLimitToOrPagedSyntax Scan()
		{
			return new ScanContext(Table, this, null, null, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter)
		{
			return new ScanContext(Table, this, null, filter, null);
		}
		public IScanLimitToOrPagedSyntax Scan(PredicateExpression filter, Values values)
		{
			return new ScanContext(Table, this, null, filter, values);
		}
		#endregion
	}
}
