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
using System.Diagnostics.Eventing.Reader;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters
{
	public abstract class DynamoDBValueConverter<TDynamoDBValue> : IValueConverter where TDynamoDBValue : DynamoDBValue
	{
		public bool CanConvert(Type fromType, Type toType, IDynamoDBValueConverter context)
		{
			return typeof(TDynamoDBValue).IsAssignableFrom(fromType) || typeof(TDynamoDBValue).IsAssignableFrom(toType);
		}

		bool IValueConverter.TryConvert(object fromValue, Type toType, out object toValue, IDynamoDBValueConverter context)
		{
			TDynamoDBValue dynamoDBValue;
			if(typeof(TDynamoDBValue).IsAssignableFrom(toType))
			{
				var result = TryConvert(fromValue, out dynamoDBValue, context);
				toValue = dynamoDBValue;
				return result;
			}
			dynamoDBValue = fromValue as TDynamoDBValue;
			if(dynamoDBValue != null || fromValue == null)
				return TryConvert(dynamoDBValue, toType, out toValue, context);

			toValue = null;
			return false;
		}

		public abstract bool TryConvert(object fromValue, out TDynamoDBValue toValue, IDynamoDBValueConverter context);
		public abstract bool TryConvert(TDynamoDBValue fromValue, Type toType, out object toValue, IDynamoDBValueConverter context);
	}

	public abstract class DynamoDBValueConverter<TValue, TDynamoDBValue> : IDynamoDBValueConverter
		where TDynamoDBValue : DynamoDBValue
	{
		public bool CanConvertFrom<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			// type assigns to TValue converts to TDynamoDBValue assigns to T
			return typeof(TValue).IsAssignableFrom(type) && typeof(T).IsAssignableFrom(typeof(TDynamoDBValue));
		}

		public bool CanConvertTo<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			// T encompasses type TDynamoDBValue converts to TValue assigns to type
			return typeof(T).IsAssignableFrom(typeof(TDynamoDBValue)) && type.IsAssignableFrom(typeof(TValue));
		}

		bool IDynamoDBValueConverter.TryConvertFrom<T>(Type type, object fromValue, out T toValue, IDynamoDBValueConverter context)
		{
			if(CanConvertFrom<T>(type, context))
			{
				TDynamoDBValue typedToValue;
				var result = TryConvert((TValue)fromValue, out typedToValue, context);
				toValue = (T)(object)typedToValue; // We know from CanConvertFrom that this assignment is safe
				return result;
			}

			toValue = null;
			return false;
		}

		bool IDynamoDBValueConverter.TryConvertTo<T>(T fromValue, Type type, out object toValue, IDynamoDBValueConverter context)
		{
			var typedFromValue = fromValue as TDynamoDBValue;
			if(CanConvertTo<T>(type, context) && (typedFromValue != null || fromValue == null))
			{
				TValue typedToValue;
				var result = TryConvert(typedFromValue, out typedToValue, context);
				toValue = typedToValue;
				return result;
			}

			toValue = null;
			return false;
		}

		public abstract bool TryConvert(TValue fromValue, out TDynamoDBValue toValue, IDynamoDBValueConverter context);
		public abstract bool TryConvert(TDynamoDBValue fromValue, out TValue toValue, IDynamoDBValueConverter context);
	}
}
