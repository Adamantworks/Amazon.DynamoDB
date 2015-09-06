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

using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public partial interface IQueryCountRangeSyntax
	{
		Task<long> AllKeysAsync();

		Task<long> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter);
		Task<long> RangeKeyBeginsWithAsync(object rangeKey);
		Task<long> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<long> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey);
		Task<long> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyBeginsWithAsync(object rangeKey, CancellationToken cancellationToken);
		Task<long> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		long RangeKeyBeginsWith(object rangeKey, IValueConverter converter);
		long RangeKeyBeginsWith(object rangeKey);
		long RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<long> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter);
		Task<long> RangeKeyEqualsAsync(object rangeKey);
		Task<long> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<long> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey);
		Task<long> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyEqualsAsync(object rangeKey, CancellationToken cancellationToken);
		Task<long> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		long RangeKeyEquals(object rangeKey, IValueConverter converter);
		long RangeKeyEquals(object rangeKey);
		long RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<long> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter);
		Task<long> RangeKeyLessThanAsync(object rangeKey);
		Task<long> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<long> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey);
		Task<long> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyLessThanAsync(object rangeKey, CancellationToken cancellationToken);
		Task<long> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		long RangeKeyLessThan(object rangeKey, IValueConverter converter);
		long RangeKeyLessThan(object rangeKey);
		long RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<long> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		Task<long> RangeKeyLessThanOrEqualToAsync(object rangeKey);
		Task<long> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<long> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		Task<long> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyLessThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken);
		Task<long> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		long RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter);
		long RangeKeyLessThanOrEqualTo(object rangeKey);
		long RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<long> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter);
		Task<long> RangeKeyGreaterThanAsync(object rangeKey);
		Task<long> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<long> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey);
		Task<long> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyGreaterThanAsync(object rangeKey, CancellationToken cancellationToken);
		Task<long> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		long RangeKeyGreaterThan(object rangeKey, IValueConverter converter);
		long RangeKeyGreaterThan(object rangeKey);
		long RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<long> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		Task<long> RangeKeyGreaterThanOrEqualToAsync(object rangeKey);
		Task<long> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<long> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		Task<long> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken);
		Task<long> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		long RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter);
		long RangeKeyGreaterThanOrEqualTo(object rangeKey);
		long RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<long> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter);
		Task<long> RangeKeyBetweenAsync(object startInclusive, object endExclusive);
		Task<long> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		Task<long> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive);
		Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive);
		Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
		Task<long> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyBetweenAsync(object startInclusive, object endExclusive, CancellationToken cancellationToken);
		Task<long> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken);
		Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, CancellationToken cancellationToken);
		Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		long RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter);
		long RangeKeyBetween(object startInclusive, object endExclusive);
		long RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		long RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive);
		long RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		long RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive);
		long RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
	}
}