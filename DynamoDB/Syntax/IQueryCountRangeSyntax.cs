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
		Task<int> AllKeysAsync(CancellationToken cancellationToken);
		int AllKeys();

		Task<int> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		int RangeKeyBeginsWith(DynamoDBKeyValue rangeKey);

		Task<int> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		int RangeKeyEquals(DynamoDBKeyValue rangeKey);

		Task<int> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		int RangeKeyLessThan(DynamoDBKeyValue rangeKey);
		Task<int> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		int RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey);

		Task<int> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		int RangeKeyGreaterThan(DynamoDBKeyValue rangeKey);
		Task<int> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, CancellationToken cancellationToken);
		int RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey);

		Task<int> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken);
		int RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
	}
}
