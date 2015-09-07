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
using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Syntax.Query;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class QueryContext
	{
		#region AllKeysAsync
		public IAsyncEnumerable<DynamoDBMap> AllKeysAsync()
		{
			return AllKeysAsync(ReadAhead.Some);
		}
		#endregion

		#region RangeKeyBeginsWithAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWithAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyBeginsWithAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBeginsWithAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyBeginsWith
		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(object rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(object rangeKey)
		{
			return RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyBeginsWith(rangeKey);
		}
		#endregion

		#region RangeKeyEqualsAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyEqualsAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyEqualsAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyEqualsAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyEquals
		public IEnumerable<DynamoDBMap> RangeKeyEquals(object rangeKey, IValueConverter converter)
		{
			return RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyEquals(object rangeKey)
		{
			return RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyEquals(rangeKey);
		}
		#endregion

		#region RangeKeyLessThanAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyLessThanAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyLessThanAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyLessThan
		public IEnumerable<DynamoDBMap> RangeKeyLessThan(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyLessThan(object rangeKey)
		{
			return RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThan(rangeKey);
		}
		#endregion

		#region RangeKeyLessThanOrEqualToAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyLessThanOrEqualToAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyLessThanOrEqualTo
		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(object rangeKey)
		{
			return RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyLessThanOrEqualTo(rangeKey);
		}
		#endregion

		#region RangeKeyGreaterThanAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyGreaterThanAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyGreaterThan
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(object rangeKey)
		{
			return RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThan(rangeKey);
		}
		#endregion

		#region RangeKeyGreaterThanOrEqualToAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(object rangeKey, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyGreaterThanOrEqualToAsync(rangeKey, readAhead);
		}
		#endregion

		#region RangeKeyGreaterThanOrEqualTo
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(object rangeKey)
		{
			return RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		public IEnumerable<DynamoDBMap> RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return RangeKeyGreaterThanOrEqualTo(rangeKey);
		}
		#endregion

		#region RangeKeyBetweenAsync
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, ReadAhead.Some);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, object endExclusive, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), readAhead);
		}
		public IAsyncEnumerable<DynamoDBMap> RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, ReadAhead readAhead)
		{
			return RangeKeyBetweenAsync(startInclusive, endExclusive, readAhead);
		}
		#endregion

		#region RangeKeyBetween
		public IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, object endExclusive)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive);
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), endExclusive);
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive));
		}
		public IEnumerable<DynamoDBMap> RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return RangeKeyBetween(startInclusive, endExclusive);
		}
		#endregion

		#region IPagedQueryRangeSyntax.AllKeysAsync
		Task<ItemPage> IPagedQueryRangeSyntax.AllKeysAsync()
		{
			return ((IPagedQueryRangeSyntax)this).AllKeysAsync(CancellationToken.None);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBeginsWithAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWithAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyBeginsWith
		ItemPage IPagedQueryRangeSyntax.RangeKeyBeginsWith(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBeginsWith(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWith(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBeginsWith(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBeginsWith(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyEqualsAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyEqualsAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEqualsAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyEquals
		ItemPage IPagedQueryRangeSyntax.RangeKeyEquals(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyEquals(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEquals(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyEquals(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyEquals(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyLessThanAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyLessThan
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThan(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThan(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThan(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualToAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyLessThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyLessThanOrEqualTo(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyGreaterThan
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThan(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThan(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThan(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThan(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThan(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(rangeKey, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(object rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(object rangeKey, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue.Convert(rangeKey), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualToAsync(DynamoDBKeyValue rangeKey, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualToAsync(rangeKey, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo(object rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo(object rangeKey)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue.Convert(rangeKey));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyGreaterThanOrEqualTo(DynamoDBKeyValue rangeKey, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyGreaterThanOrEqualTo(rangeKey);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyBetweenAsync
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, object endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, endExclusive, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, endExclusive, CancellationToken.None);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, object endExclusive, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive, cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(object startInclusive, DynamoDBKeyValue endExclusive, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(DynamoDBKeyValue.Convert(startInclusive), endExclusive, cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, object endExclusive, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, DynamoDBKeyValue.Convert(endExclusive), cancellationToken);
		}
		Task<ItemPage> IPagedQueryRangeSyntax.RangeKeyBetweenAsync(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter, CancellationToken cancellationToken)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetweenAsync(startInclusive, endExclusive, cancellationToken);
		}
		#endregion

		#region IPagedQueryRangeSyntax.RangeKeyBetween
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(object startInclusive, object endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(object startInclusive, object endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), DynamoDBKeyValue.Convert(endExclusive));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive, converter), endExclusive);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(object startInclusive, DynamoDBKeyValue endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(DynamoDBKeyValue.Convert(startInclusive), endExclusive);
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive, converter));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(DynamoDBKeyValue startInclusive, object endExclusive)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(startInclusive, DynamoDBKeyValue.Convert(endExclusive));
		}
		ItemPage IPagedQueryRangeSyntax.RangeKeyBetween(DynamoDBKeyValue startInclusive, DynamoDBKeyValue endExclusive, IValueConverter converter)
		{
			return ((IPagedQueryRangeSyntax)this).RangeKeyBetween(startInclusive, endExclusive);
		}
		#endregion
	}
}