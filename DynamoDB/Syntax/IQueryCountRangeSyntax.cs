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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	// See Overloads.tt and Overloads.cs for more methods of this interface
	public partial interface IQueryCountRangeSyntax
	{
		Task<long> AllKeysAsync(CancellationToken cancellationToken);
		long AllKeys();

		Task<long> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		long RangeKeyBeginsWith(DynamoDBKeyValue rangeKey);

		Task<long> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		long RangeKeyEquals(DynamoDBKeyValue rangeKey);

		Task<long> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		long RangeKeyLessThan(DynamoDBKeyValue rangeKey);
		Task<long> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		long RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey);

		Task<long> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		long RangeKeyGreaterThan(DynamoDBKeyValue rangeKey);
		Task<long> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		long RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey);

		Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken);
		long RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
	}
}
