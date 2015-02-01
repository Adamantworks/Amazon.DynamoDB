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

namespace Adamantworks.Amazon.DynamoDB
{
	public static class DynamoDBValueConverter
	{
		private static CompositeConverter @default;
		private static CompositeConverter compositeValueDefault;

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
		public static CompositeConverter CompositeValueDefault
		{
			get { return compositeValueDefault; }
			set
			{
				if(value == null)
					throw new ArgumentNullException("value");

				compositeValueDefault = value;
			}
		}

		static DynamoDBValueConverter()
		{
			@default = CreateStandardConverter();
			compositeValueDefault = CreateCompositeValueConverter(@default);
		}

		public static CompositeConverter CreateBasicConverter()
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
			converter.Add(BasicConverters.SetOfString, int.MinValue + 7);
			converter.Add(BasicConverters.SetOfNumber, int.MinValue + 8);
			converter.Add(BasicConverters.SetOfBinary, int.MinValue + 9);

			// Casting conversions are always first because no conversion is actually needed
			converter.Add(BasicConverters.Cast, int.MaxValue);

			return converter;
		}

		public static CompositeConverter CreateStandardConverter()
		{
			var converter = CreateBasicConverter();
			converter.Add(StandardConverters.GuidString, int.MinValue + 100);
			// TODO register more standard conversions
			return converter;
		}

		public static CompositeConverter CreateCompositeValueConverter(IValueConverter baseConverter)
		{
			var converter = new CompositeConverter();
			converter.Add(baseConverter);
			converter.Add(CompositeValueConverters.ViaNumber, -1);
			converter.Add(CompositeValueConverters.ViaBinary, -2);
			return converter;
		}
	}
}
