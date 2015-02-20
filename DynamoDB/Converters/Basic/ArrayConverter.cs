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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	internal class ArrayConverter : ValueConverter<DynamoDBList>
	{
		public override bool CanConvertTo(Type toType, IValueConverter context)
		{
			// Must be assignable from X[]
			return PossibleArrayOfType(toType, context) != null;
		}

		private static Type PossibleArrayOfType(Type toType, IValueConverter context)
		{
			if(toType.IsArray && toType.GetArrayRank() == 1)
				return toType.GetElementType();

			// To be a full array converter, we have to allow things arrays implement
			if(!toType.IsGenericType) return null;
			var openGeneric = toType.GetGenericTypeDefinition();
			if(openGeneric == typeof(IList<>) || openGeneric == typeof(ICollection<>) || openGeneric == typeof(IEnumerable<>))
			{
				// We must assume we can convert the values, because we don't know their concrete type yet inorder to check
				return toType.GenericTypeArguments[0];
			}

			return null;
		}

		public override bool CanConvertFrom(Type fromType, IValueConverter context)
		{
			// Must be an array
			return fromType.IsArray && fromType.GetArrayRank() == 1
				&& context.CanConvert(fromType.GetElementType(), typeof(DynamoDBValue), context);
		}

		public override bool TryConvert(object fromValue, out DynamoDBList toValue, IValueConverter context)
		{
			toValue = null;
			if(fromValue == null) return true;
			if(!CanConvertFrom(fromValue.GetType(), context)) return false; // Always need this to make sure we are coming from an array

			toValue = new DynamoDBList();
			foreach(var fromElementValue in (IEnumerable)fromValue)
			{
				DynamoDBValue toElementValue;
				if(!context.TryConvert(fromElementValue, out toElementValue) || toElementValue == null)
					return false;
				toValue.Add(toElementValue);
			}

			return true;
		}

		public override bool TryConvert(DynamoDBList fromValue, Type toType, out object toValue, IValueConverter context)
		{
			toValue = null;

			var possibleArrayType = PossibleArrayOfType(toType, context);
			if(possibleArrayType == null) return false;
			if(fromValue == null) return true;

			return TryConvertToArray(fromValue, possibleArrayType, out toValue, context);
		}

		private static bool TryConvertToArray(DynamoDBList fromValue, Type possibleArrayType, out object toValue, IValueConverter context)
		{
			var arrayType = possibleArrayType.MakeArrayType();
			var array = (IList)Activator.CreateInstance(arrayType, fromValue.Count);

			for(var i = 0; i < fromValue.Count; i++)
			{
				object toElementValue;
				if(!context.TryConvert(fromValue[i], possibleArrayType, out toElementValue, context))
				{
					toValue = null;
					return false;
				}
				array[i] = toElementValue;
			}
			toValue = array;
			return true;
		}
	}
}
