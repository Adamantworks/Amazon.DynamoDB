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
	public interface IPagedQueryRangeSyntax
	{
		// TODO change default values to overloads
		// TODO add overloads with object and value converter

		Task<ItemPage> AllKeysAsync(CancellationToken cancellationToken = default(CancellationToken));
		ItemPage AllKeys();

		Task<ItemPage> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken = default(CancellationToken));
		ItemPage RangeKeyBeginsWith(DynamoDBKeyValue rangeKey);

		Task<ItemPage> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken = default(CancellationToken));
		ItemPage RangeKeyEquals(DynamoDBKeyValue rangeKey);

		Task<ItemPage> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken = default(CancellationToken));
		ItemPage RangeKeyLessThan(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken = default(CancellationToken));
		ItemPage RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey);

		Task<ItemPage> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken = default(CancellationToken));
		ItemPage RangeKeyGreaterThan(DynamoDBKeyValue rangeKey);
		Task<ItemPage> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken = default(CancellationToken));
		ItemPage RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey);

		Task<ItemPage> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken = default(CancellationToken));
		ItemPage RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
	}
}
