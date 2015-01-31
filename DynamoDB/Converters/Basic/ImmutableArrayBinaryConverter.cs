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

using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using System.Collections.Immutable;

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	internal class ImmutableArrayBinaryConverter : ValueConverter<ImmutableArray<byte>, DynamoDBBinary>
	{
		public override bool TryConvert(ImmutableArray<byte> fromValue, out DynamoDBBinary toValue, IValueConverter context)
		{
			toValue = fromValue;
			return true;
		}

		public override bool TryConvert(DynamoDBBinary fromValue, out ImmutableArray<byte> toValue, IValueConverter context)
		{
			toValue = fromValue;
			return true;
		}
	}
}
