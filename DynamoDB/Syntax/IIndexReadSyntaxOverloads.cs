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

using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public partial interface IIndexReadSyntax
	{
		IReverseSyntax Query(DynamoDBKeyValue hashKey);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter);
		IReverseSyntax Query(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values);
		IReverseSyntax Query(object hashKey);
		IReverseSyntax Query(object hashKey, PredicateExpression filter);
		IReverseSyntax Query(object hashKey, PredicateExpression filter, Values values);
		IReverseSyntax Query(object hashKey, IValueConverter converter);
		IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter);
		IReverseSyntax Query(object hashKey, IValueConverter converter, PredicateExpression filter, Values values);

		IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey);
		IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, PredicateExpression filter);
		IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, PredicateExpression filter, Values values);
		IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter);
		IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter);
		IQueryCountRangeSyntax QueryCount(DynamoDBKeyValue hashKey, IValueConverter converter, PredicateExpression filter, Values values);
		IQueryCountRangeSyntax QueryCount(object hashKey);
		IQueryCountRangeSyntax QueryCount(object hashKey, PredicateExpression filter);
		IQueryCountRangeSyntax QueryCount(object hashKey, PredicateExpression filter, Values values);
		IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter);
		IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter, PredicateExpression filter);
		IQueryCountRangeSyntax QueryCount(object hashKey, IValueConverter converter, PredicateExpression filter, Values values);

		IScanLimitToOrPagedSyntax Scan();
		IScanLimitToOrPagedSyntax Scan(PredicateExpression filter);
		IScanLimitToOrPagedSyntax Scan(PredicateExpression filter, Values values);

		IScanCountOptionsSyntax ScanCount();
		IScanCountOptionsSyntax ScanCount(PredicateExpression filter);
		IScanCountOptionsSyntax ScanCount(PredicateExpression filter, Values values);
	}
}