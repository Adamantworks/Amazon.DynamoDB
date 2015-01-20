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
using System.Collections.Generic;
using System.Linq;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	internal class SetConverter : IDynamoDBValueConverter
	{
		public bool CanConvertFrom<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			Type setOfType;
			return CanConvertFrom<T>(type, context, out setOfType);
		}
		private static bool CanConvertFrom<T>(Type type, IDynamoDBValueConverter context, out Type setOfType) where T : DynamoDBValue
		{
			setOfType = null;
			return IsAssignableFromDynamoDBSet<T>()
				   && type.IsGenericType
				   && type.GetGenericTypeDefinition() == typeof(ISet<>)
				// DynamoDBKeyValue because only set of string, number or binary is allowed
				   && context.CanConvertFrom<DynamoDBKeyValue>(setOfType = type.GetGenericArguments()[0], context);
		}

		public bool CanConvertTo<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			Type setOfType;
			return CanConvertTo<T>(type, context, out setOfType);
		}
		private static bool CanConvertTo<T>(Type type, IDynamoDBValueConverter context, out Type setOfType) where T : DynamoDBValue
		{
			setOfType = null;
			return IsAssignableFromDynamoDBSet<T>()
				   && type.IsGenericType
				// DynamoDBKeyValue because only set of string, number or binary is allowed
				   && context.CanConvertTo<DynamoDBKeyValue>(setOfType = type.GetGenericArguments()[0], context);
		}

		private static bool IsAssignableFromDynamoDBSet<T>() where T : DynamoDBValue
		{
			return (typeof(T).IsAssignableFrom(typeof(DynamoDBSet<DynamoDBString>))
					|| typeof(T).IsAssignableFrom(typeof(DynamoDBSet<DynamoDBNumber>))
					|| typeof(T).IsAssignableFrom(typeof(DynamoDBSet<DynamoDBBinary>)));
		}

		public bool TryConvertFrom<T>(Type type, object fromValue, out T toValue, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			toValue = null;

			Type setOfType;
			if(!CanConvertFrom<T>(type, context, out setOfType)) return false;

			var values = new List<DynamoDBKeyValue>();
			foreach(var fromElementValue in (IEnumerable<object>)fromValue)
			{
				DynamoDBKeyValue toElementValue;
				if(!context.TryConvertFrom(setOfType, fromElementValue, out toElementValue, context) || toElementValue == null)
					return false;
				values.Add(toElementValue);
			}

			if(values.Count == 0) return true; // Empty set becomes null
			setOfType = values[0].GetType();
			if(setOfType == typeof(DynamoDBString))
				return NewSet<DynamoDBString, T>(ref toValue, values);
			if(setOfType == typeof(DynamoDBNumber))
				return NewSet<DynamoDBNumber, T>(ref toValue, values);
			if(setOfType == typeof(DynamoDBBinary))
				return NewSet<DynamoDBBinary, T>(ref toValue, values);

			return false;
		}

		private static bool NewSet<TOf, T>(ref T toValue, ICollection<DynamoDBKeyValue> values)
			where TOf : DynamoDBKeyValue
			where T : DynamoDBValue
		{
			var set = new DynamoDBSet<TOf>(values.OfType<TOf>());
			var allOfType = set.Count == values.Count;
			if(allOfType) toValue = (T)(object)set; // we know because of CanConvertFrom that this is safe
			return allOfType;
		}

		public bool TryConvertTo<T>(T fromValue, Type type, out object toValue, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			toValue = null;

			Type setOfType;
			if(!CanConvertTo<T>(type, context, out setOfType)) return false;

			if(fromValue == null) return true;

			var fromSetType = fromValue.GetType();
			if(!fromSetType.IsGenericType
				|| type.GetGenericTypeDefinition() == typeof(DynamoDBSet<>)) return false;

			var hashSetType = typeof(HashSet<>).MakeGenericType(setOfType);
			var hashSet = Activator.CreateInstance(hashSetType);
			var dynamicSet = (dynamic)hashSet;

			foreach(var fromElementValue in (IEnumerable<DynamoDBKeyValue>)fromValue)
			{
				object toElementValue;
				if(!context.TryConvertTo(fromElementValue, setOfType, out toElementValue, context))
					return false;
				dynamicSet.Add((dynamic)toElementValue); // Cast to dynamic seems to be needed because otherwise it wants an Add(object) method
			}

			toValue = hashSet;
			return true;
		}
	}
}
