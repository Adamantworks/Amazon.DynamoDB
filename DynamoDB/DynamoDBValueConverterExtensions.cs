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
using Adamantworks.Amazon.DynamoDB.Syntax;

namespace Adamantworks.Amazon.DynamoDB
{
	public static class DynamoDBValueConverterExtensions
	{
		public static T To<T>(this DynamoDBValue value)
		{
			object toValue;
			var converter = DynamoDBValueConverters.Default;
			if(converter.TryConvertTo(value, typeof(T), out toValue, converter))
				return (T)toValue;

			throw new InvalidCastException();
		}
		public static T To<T>(this DynamoDBValue value, IDynamoDBValueConverter converter)
		{
			object toValue;
			if(converter.TryConvertTo(value, typeof(T), out toValue, converter))
				return (T)toValue;

			throw new InvalidCastException();
		}

		// TODO Replace these with convert methods on each DynamoDB type
		public static ConvertableSyntax<T> ToDynamoDB<T>(T value)
		{
			return new ConvertableSyntax<T>(value, DynamoDBValueConverters.Default);
		}
		public static ConvertableSyntax<T> ToDynamoDB<T>(T value, IDynamoDBValueConverter converter)
		{
			return new ConvertableSyntax<T>(value, converter);
		}
	}
}
