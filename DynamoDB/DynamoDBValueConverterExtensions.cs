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

namespace Adamantworks.Amazon.DynamoDB
{
	public static class DynamoDBValueConverterExtensions
	{
		public static T To<T>(this DynamoDBValue value)
		{
			object toValue;
			var converter = DynamoDBValueConverter.Default;
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

		public static Tuple<T1, T2> To<T1, T2>(this DynamoDBValue value)
		{
			return value.To<T1, T2>(DynamoDBValueConverter.Default);
		}
		public static Tuple<T1, T2> To<T1, T2>(this DynamoDBValue value, IDynamoDBValueConverter converter)
		{
			var stringValue = value as DynamoDBString;
			if(stringValue == null)
				throw new ArgumentException();

			var values = ItemKey.Split(stringValue);
			if(values.Length != 2)
				throw new ArgumentException("Must be a composite of 2 values","value");

			return Tuple.Create(To<T1>((DynamoDBString)values[0], converter),
				To<T2>((DynamoDBString)values[1], converter));
		}

		public static DynamoDBValue ToDynamoDBValue<T>(this T value)
		{
			DynamoDBValue toValue;
			var converter = DynamoDBValueConverter.Default;
			if(DynamoDBValueConverter.Default.TryConvertFrom(typeof(T), value, out toValue, converter))
				return toValue;

			throw new InvalidCastException();
		}
		public static DynamoDBValue ToDynamoDBValue<T>(this T value, IDynamoDBValueConverter converter)
		{
			DynamoDBValue toValue;
			if(converter.TryConvertFrom(typeof(T), value, out toValue, converter))
				return toValue;

			throw new InvalidCastException();
		}

		public static DynamoDBKeyValue ToDynamoDBKeyValue<T>(this T value)
		{
			DynamoDBValue toValue;
			var converter = DynamoDBValueConverter.Default;
			if(DynamoDBValueConverter.Default.TryConvertFrom(typeof(T), value, out toValue, converter))
				return (DynamoDBKeyValue)toValue;

			throw new InvalidCastException();
		}
		public static DynamoDBKeyValue ToDynamoDBKeyValue<T>(this T value, IDynamoDBValueConverter converter)
		{
			DynamoDBValue toValue;
			if(converter.TryConvertFrom(typeof(T), value, out toValue, converter))
				return (DynamoDBKeyValue)toValue;

			throw new InvalidCastException();
		}
	}
}
