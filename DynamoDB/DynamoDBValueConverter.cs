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
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB
{
	public interface IDynamoDBValueConverter
	{
		bool CanConvertFrom<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue;
		bool CanConvertTo<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue;
		bool TryConvertFrom<T>(Type type, object fromValue, out T toValue, IDynamoDBValueConverter context) where T : DynamoDBValue;
		bool TryConvertTo<T>(T fromValue, Type type, out object toValue, IDynamoDBValueConverter context) where T : DynamoDBValue;
	}

	public static class DynamoDBValueConverter
	{
		private static CompositeConverter @default;

		public static CompositeConverter Default
		{
			get { return @default; }
			set
			{
				if(value == null)
					throw new ArgumentNullException("value");

				@default = value;
			}
		}

		static DynamoDBValueConverter()
		{
			@default = NewStandardConverter();
		}

		public static CompositeConverter NewBasicConverter()
		{
			var converter = new CompositeConverter();
			// We use the lowest possible priorities so any user ones will be higher
			// Also, each priority is used only once to avoid ever needing to check wasted
			// conversions (from multiple at the same priority)
			converter.Add(BasicConverters.Nullable, int.MinValue);
			converter.Add(BasicConverters.String, int.MinValue + 1);
			converter.Add(BasicConverters.Boolean, int.MinValue + 2);
			converter.Add(BasicConverters.Number, int.MinValue + 3);
			converter.Add(BasicConverters.MemoryStreamBinary, int.MinValue + 4);
			converter.Add(BasicConverters.ByteArrayBinary, int.MinValue + 5);
			converter.Add(BasicConverters.ImmutableArrayBinary, int.MinValue + 6);
			converter.Add(BasicConverters.Set, int.MinValue + 7);
			return converter;
		}

		public static CompositeConverter NewStandardConverter()
		{
			var converter = NewBasicConverter();
			converter.Add(StandardConverters.GuidString, int.MinValue + 100);
			// TODO register more standard conversions
			return converter;
		}
	}
}
