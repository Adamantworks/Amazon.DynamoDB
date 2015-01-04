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
using System;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	internal class BooleanConverter : DynamoDBValueConverter<bool>
	{
		public override bool TryConvertFrom(Type type, bool fromValue, out DynamoDBValue toValue, IDynamoDBValueConverter context)
		{
			toValue = (DynamoDBBoolean)fromValue;
			return true;
		}

		public override bool TryConvertTo(DynamoDBValue fromValue, Type type, out bool toValue, IDynamoDBValueConverter context)
		{
			var value = fromValue as DynamoDBBoolean;
			if(value != null)
			{
				toValue = value;
				return true;
			}

			toValue = false;
			return false;
		}
	}
}
