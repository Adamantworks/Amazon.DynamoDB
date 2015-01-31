﻿// Copyright 2015 Adamantworks.  All Rights Reserved.
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
using System.Collections.Generic;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public interface IQueryRangeSyntax
	{
		// TODO change default values to overloads

		IAsyncEnumerable<DynamoDBMap> AllKeysAsync(ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> AllKeys();

		IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> RangeKeyBeginsWith(DynamoDBKeyValue rangeKey);

		IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> RangeKeyEquals(DynamoDBKeyValue rangeKey);

		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> RangeKeyLessThan(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey);

		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThan(DynamoDBKeyValue rangeKey);
		IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey);

		IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, ReadAhead readAhead = ReadAhead.Some);
		IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive);
	}
}