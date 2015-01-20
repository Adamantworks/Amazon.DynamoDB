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

using System;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters
{
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
				var result = TryConvertFrom(type, (TValue)fromValue, out typedToValue, context);
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
				var result = TryConvertTo(typedFromValue, type, out typedToValue, context);
				toValue = typedToValue;
				return result;
			}

			toValue = null;
			return false;
		}

		public abstract bool TryConvertFrom(Type type, TValue fromValue, out TDynamoDBValue toValue, IDynamoDBValueConverter context);
		public abstract bool TryConvertTo(TDynamoDBValue fromValue, Type type, out TValue toValue, IDynamoDBValueConverter context);
	}
}
