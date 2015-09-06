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
	public partial interface IPagedQueryRangeSyntax
	{
		Task<ItemPage> AllKeysAsync();

		Task<ItemPage> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyBeginsWithAsync(object rangeKey);
		Task<ItemPage> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBeginsWithAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyBeginsWith(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyBeginsWith(object rangeKey);
		ItemPage RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyEqualsAsync(object rangeKey);
		Task<ItemPage> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyEqualsAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyEquals(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyEquals(object rangeKey);
		ItemPage RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyLessThanAsync(object rangeKey);
		Task<ItemPage> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyLessThanAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyLessThan(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyLessThan(object rangeKey);
		ItemPage RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(object rangeKey);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyLessThanOrEqualTo(object rangeKey);
		ItemPage RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyGreaterThanAsync(object rangeKey);
		Task<ItemPage> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyGreaterThanAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyGreaterThan(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyGreaterThan(object rangeKey);
		ItemPage RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(object rangeKey);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter);
		ItemPage RangeKeyGreaterThanOrEqualTo(object rangeKey);
		ItemPage RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter);

		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, object endExclusive);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, object endExclusive, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, CancellationToken cancellationToken);
		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken);
		ItemPage RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter);
		ItemPage RangeKeyBetween(object startInclusive, object endExclusive);
		ItemPage RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
		ItemPage RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive);
		ItemPage RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter);
		ItemPage RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive);
		ItemPage RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter);
	}
}