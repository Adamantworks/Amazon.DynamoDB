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
	internal class ViaBinaryConverter : IDynamoDBValueConverter
	{
		private readonly Regex binaryPattern = new Regex(@"^[0-9a-fA-F]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

		public bool CanConvertFrom<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			// via converter is only for going from DynamoDBString to something else
			return false;
		}

		public bool CanConvertTo<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			return typeof(T).IsAssignableFrom(typeof(DynamoDBString)) && context.CanConvertTo<DynamoDBBinary>(type, context);
		}

		public bool TryConvertFrom<T>(Type type, object fromValue, out T toValue, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			// via converter is only for going from DynamoDBString to something else
			toValue = null;
			return false;
		}

		public bool TryConvertTo<T>(T fromValue, Type type, out object toValue, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			toValue = null;
			if(fromValue == null)
				return CanConvertTo<T>(type, context) && context.TryConvertTo<DynamoDBBinary>(null, type, out toValue, context);

			var stringValue = fromValue as DynamoDBString;
			if(stringValue == null || stringValue.Length % 2 != 0 || !binaryPattern.IsMatch(stringValue)) return false;
			return context.TryConvertTo(new DynamoDBBinary(ToBytes(stringValue)), type, out toValue, context);
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
