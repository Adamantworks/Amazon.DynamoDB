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

namespace Adamantworks.Amazon.DynamoDB.Converters
{
	public abstract class DynamoDBValueConverter<T> : IDynamoDBValueConverter
	{
		public bool CanConvert(Type type, IDynamoDBValueConverter context)
		{
			return typeof(T).IsAssignableFrom(type);
		}

		bool IDynamoDBValueConverter.TryConvertFrom(Type type, object fromValue, out DynamoDBValue toValue, IDynamoDBValueConverter context)
		{
			if(CanConvert(type, context))
				return TryConvertFrom(type, (T)fromValue, out toValue, context);

			toValue = null;
			return false;
		}

		bool IDynamoDBValueConverter.TryConvertTo(DynamoDBValue fromValue, Type type, out object toValue, IDynamoDBValueConverter context)
		{
			if(CanConvert(type, context))
			{
				T typedToValue;
				var result = TryConvertTo(fromValue, type, out typedToValue, context);
				toValue = typedToValue;
				return result;
			}

			toValue = null;
			return false;
		}

		public abstract bool TryConvertFrom(Type type, T fromValue, out DynamoDBValue toValue, IDynamoDBValueConverter context);
		public abstract bool TryConvertTo(DynamoDBValue fromValue, Type type, out T toValue, IDynamoDBValueConverter context);
	}
}
