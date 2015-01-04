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
	internal class ByteArrayBinaryConverter : DynamoDBValueConverter<byte[]>
	{
		public override bool TryConvertFrom(Type type, byte[] fromValue, out DynamoDBValue toValue, IDynamoDBValueConverter context)
		{
			toValue = new DynamoDBBinary(fromValue);
			return true;
		}

		public override bool TryConvertTo(DynamoDBValue fromValue, Type type, out byte[] toValue, IDynamoDBValueConverter context)
		{
			var value = fromValue as DynamoDBBinary;
			if(value != null)
			{
				toValue = value.ToArray();
				return true;
			}

			toValue = null;
			return false;
		}
	}
}
