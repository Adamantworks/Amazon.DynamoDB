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

using System.Collections.Generic;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Syntax.Query
{
	public partial interface IQueryRangeSyntax
	{
		IAsyncEnumerable<DynamoDBMap> AllKeysAsync();

		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyBeginsWith(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyBeginsWith(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyEquals(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyEquals(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyLessThan(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyLessThan(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThan(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThan(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(object rangeKey);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, ReadAhead readAhead);
		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, ReadAhead readAhead);
		IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, object endExclusive);
		IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive);
		IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive);
		IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
	}
}