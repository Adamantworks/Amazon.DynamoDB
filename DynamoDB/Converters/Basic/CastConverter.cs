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

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	internal class CastConverter : IDynamoDBValueConverter
	{
		public bool CanConvertFrom<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			// Only work on DynamoDBValues
			if(!typeof(DynamoDBValue).IsAssignableFrom(type)) return false;
			var dynamoDBValueType = typeof(T);
			return dynamoDBValueType.IsAssignableFrom(type) // up-cast
				|| type.IsAssignableFrom(dynamoDBValueType); // down-cast
		}

		public bool CanConvertTo<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			// Only work on DynamoDBValues
			if(!typeof(DynamoDBValue).IsAssignableFrom(type)) return false;
			var dynamoDBValueType = typeof(T);
			return type.IsAssignableFrom(dynamoDBValueType) // up-cast
				|| dynamoDBValueType.IsAssignableFrom(type); // down-cast
		}

		public bool TryConvertFrom<T>(Type type, object fromValue, out T toValue, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			toValue = null;

			// Only work on DynamoDBValues
			if(!typeof(DynamoDBValue).IsAssignableFrom(type)) return false;

			// Handle null
			if(fromValue == null) return CanConvertFrom<T>(type, context);

			toValue = fromValue as T;
			return toValue != null;
		}

		public bool TryConvertTo<T>(T fromValue, Type type, out object toValue, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			toValue = null;

			// Only work on DynamoDBValues
			if(!typeof(DynamoDBValue).IsAssignableFrom(type)) return false;

			// Handle null
			if(fromValue == null) return CanConvertTo<T>(type, context);

			if(type.IsInstanceOfType(fromValue))
			{
				toValue = fromValue;
				return true;
			}
			return false;
		}
	}
}
