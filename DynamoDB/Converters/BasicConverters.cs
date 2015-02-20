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

using Adamantworks.Amazon.DynamoDB.Converters.Basic;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters
{
	public static class BasicConverters
	{
		/// <summary>
		/// The cast converter supports converting types inheriting from DynamoDBValue to other types be doing either up or down casts
		/// </summary>
		public static readonly IValueConverter Cast = new CastConverter();
		public static readonly IValueConverter Number = new NumberConverter();
		public static readonly IValueConverter String = new StringConverter();
		public static readonly IValueConverter Boolean = new BooleanConverter();
		public static readonly IValueConverter MemoryStreamBinary = new MemoryStreamBinaryConverter();
		public static readonly IValueConverter ImmutableArrayBinary = new ImmutableArrayBinaryConverter();
		public static readonly IValueConverter ByteArrayBinary = new ByteArrayBinaryConverter();
		public static readonly IValueConverter Nullable = new NullableConverter();
		public static readonly IValueConverter SetOfString = new SetConverter<DynamoDBString>();
		public static readonly IValueConverter SetOfNumber = new SetConverter<DynamoDBNumber>();
		public static readonly IValueConverter SetOfBinary = new SetConverter<DynamoDBBinary>();
		public static readonly IValueConverter List = new ListConverter();
	}
}
