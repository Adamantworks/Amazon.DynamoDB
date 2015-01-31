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
using System.Text.RegularExpressions;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters.Composite
{
	internal class ViaBinaryConverter : ValueConverter<DynamoDBString>
	{
		private readonly Regex binaryPattern = new Regex(@"^[0-9a-fA-F]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

		public override bool CanConvertTo(Type toType, IValueConverter context)
		{
			return context.CanConvert(typeof(DynamoDBBinary), toType, context);
		}

		public override bool CanConvertFrom(Type fromType, IValueConverter context)
		{
			// via converter is only for going from DynamoDBString to something else
			return false;
		}

		public override bool TryConvert(object fromValue, out DynamoDBString toValue, IValueConverter context)
		{
			// via converter is only for going from DynamoDBString to something else
			toValue = null;
			return false;
		}

		public override bool TryConvert(DynamoDBString fromValue, Type toType, out object toValue, IValueConverter context)
		{
			// Can't make a binary in these cases
			if(fromValue == null || fromValue.Length % 2 != 0 || !binaryPattern.IsMatch(fromValue))
			{
				toValue = null;
				return false;
			}

			return context.TryConvert(new DynamoDBBinary(ToBytes(fromValue)), toType, out toValue, context);
		}

		private static byte[] ToBytes(string hex)
		{
			var bytesCount = (hex.Length) / 2;
			var bytes = new byte[bytesCount];
			for(var i = 0; i < bytesCount; ++i)
				bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

			return bytes;
		}
	}
}
