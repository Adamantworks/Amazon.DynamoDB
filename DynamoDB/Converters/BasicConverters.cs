﻿// Copyright 2015 Adamantworks.  All Rights Reserved.
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

namespace Adamantworks.Amazon.DynamoDB.Converters
{
	public static class BasicConverters
	{
		public static readonly IDynamoDBValueConverter Number = new NumberConverter();
		public static readonly IDynamoDBValueConverter String = new StringConverter();
		public static readonly IDynamoDBValueConverter Boolean = new BooleanConverter();
		// TODO combine the three binary converters
		public static readonly IDynamoDBValueConverter MemoryStreamBinary = new MemoryStreamBinaryConverter();
		public static readonly IDynamoDBValueConverter ImmutableArrayBinary = new ImmutableArrayBinaryConverter();
		public static readonly IDynamoDBValueConverter ByteArrayBinary = new ByteArrayBinaryConverter();
		public static readonly IDynamoDBValueConverter Nullable = new NullableConverter();
		public static readonly IDynamoDBValueConverter Set = new SetConverter();
	}
}
