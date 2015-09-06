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

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class QueryCountContext
	{
		#region AllKeysAsync
		public Task<long> AllKeysAsync()
		{
			return AllKeysAsync(CancellationToken.None);
		}
		#endregion

		#region RangeKeyBeginsWithAsync
		public Task<long> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		public Task<long> RangeKeyBeginsWithAsync(object rangeKey)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		public Task<long> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWithAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyBeginsWithAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		public Task<long> RangeKeyBeginsWithAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		public Task<long> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyBeginsWithAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region RangeKeyBeginsWith
		public long RangeKeyBeginsWith(object rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public long RangeKeyBeginsWith(object rangeKey)
		{
			return RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey));
		}
		public long RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWith(rangeKey);
		}
		#endregion

		#region RangeKeyEqualsAsync
		public Task<long> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		public Task<long> RangeKeyEqualsAsync(object rangeKey)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		public Task<long> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyEqualsAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyEqualsAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		public Task<long> RangeKeyEqualsAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		public Task<long> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyEqualsAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region RangeKeyEquals
		public long RangeKeyEquals(object rangeKey, IValueConverter converter)
		{
			return RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public long RangeKeyEquals(object rangeKey)
		{
			return RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey));
		}
		public long RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyEquals(rangeKey);
		}
		#endregion

		#region RangeKeyLessThanAsync
		public Task<long> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		public Task<long> RangeKeyLessThanAsync(object rangeKey)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		public Task<long> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyLessThanAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		public Task<long> RangeKeyLessThanAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		public Task<long> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyLessThanAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region RangeKeyLessThan
		public long RangeKeyLessThan(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public long RangeKeyLessThan(object rangeKey)
		{
			return RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		public long RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThan(rangeKey);
		}
		#endregion

		#region RangeKeyLessThanOrEqualToAsync
		public Task<long> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		public Task<long> RangeKeyLessThanOrEqualToAsync(object rangeKey)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		public Task<long> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		public Task<long> RangeKeyLessThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		public Task<long> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region RangeKeyLessThanOrEqualTo
		public long RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public long RangeKeyLessThanOrEqualTo(object rangeKey)
		{
			return RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		public long RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualTo(rangeKey);
		}
		#endregion

		#region RangeKeyGreaterThanAsync
		public Task<long> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		public Task<long> RangeKeyGreaterThanAsync(object rangeKey)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		public Task<long> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyGreaterThanAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		public Task<long> RangeKeyGreaterThanAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		public Task<long> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyGreaterThanAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region RangeKeyGreaterThan
		public long RangeKeyGreaterThan(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public long RangeKeyGreaterThan(object rangeKey)
		{
			return RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		public long RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThan(rangeKey);
		}
		#endregion

		#region RangeKeyGreaterThanOrEqualToAsync
		public Task<long> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		public Task<long> RangeKeyGreaterThanOrEqualToAsync(object rangeKey)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		public Task<long> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		public Task<long> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		public Task<long> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		public Task<long> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region RangeKeyGreaterThanOrEqualTo
		public long RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public long RangeKeyGreaterThanOrEqualTo(object rangeKey)
		{
			return RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		public long RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualTo(rangeKey);
		}
		#endregion

		#region RangeKeyBetweenAsync
		public Task<long> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), CancellationToken.None);
		}
		public Task<long> RangeKeyBetweenAsync(object startInclusive, object endExclusive)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), CancellationToken.None);
		}
		public Task<long> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, CancellationToken.None);
		}
		public Task<long> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, CancellationToken.None);
		}
		public Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), CancellationToken.None);
		}
		public Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), CancellationToken.None);
		}
		public Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, CancellationToken.None);
		}
		public Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, CancellationToken.None);
		}
		public Task<long> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), cancellationToken);
		}
		public Task<long> RangeKeyBetweenAsync(object startInclusive, object endExclusive, CancellationToken cancellationToken)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), cancellationToken);
		}
		public Task<long> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, cancellationToken);
		}
		public Task<long> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, cancellationToken);
		}
		public Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), cancellationToken);
		}
		public Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, CancellationToken cancellationToken)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), cancellationToken);
		}
		public Task<long> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, cancellationToken);
		}
		#endregion

		#region RangeKeyBetween
		public long RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		public long RangeKeyBetween(object startInclusive, object endExclusive)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive));
		}
		public long RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive);
		}
		public long RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), endExclusive);
		}
		public long RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		public long RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive));
		}
		public long RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(startInclusive, endExclusive);
		}
		#endregion
	}
}