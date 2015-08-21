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
	internal class ListConverter : ValueConverter<DynamoDBList>
	{
		public override bool CanConvertTo(Type toType, IValueConverter context)
		{
			// Must be assignable from List<X>
			return PossibleListOfType(toType, context) != null;
		}

		private static Type PossibleListOfType(Type toType, IValueConverter context)
		{
			if(!toType.IsGenericType) return null;
			var openGeneric = toType.GetGenericTypeDefinition();
			if(openGeneric == typeof(List<>) || openGeneric == typeof(IList<>) || openGeneric == typeof(ICollection<>) || openGeneric == typeof(IEnumerable<>))
			{
				// We must assume we can convert the values, because we don't know their concrete type yet inorder to check
				return toType.GenericTypeArguments[0];
			}
			return null;
		}

		public override bool CanConvertFrom(Type fromType, IValueConverter context)
		{
			// Must implement IEnumerable<X>
			return fromType.GetInterfaces().Concat(new[] { fromType }) // The from type might itself be IEnumerable<X>
				.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				.Any(i => context.CanConvert(i.GenericTypeArguments[0], typeof(DynamoDBValue), context));
		}

		public override bool TryConvert(object fromValue, out DynamoDBList toValue, IValueConverter context)
		{
			toValue = null;
			if(fromValue == null) return true;
			if(!CanConvertFrom(fromValue.GetType(), context)) return false; // Always need this to make sure we are coming from a list

			toValue = new DynamoDBList();
			foreach(var fromElementValue in (IEnumerable)fromValue)
			{
				DynamoDBValue toElementValue;
				if(!context.TryConvert(fromElementValue, out toElementValue))
					return false;
				toValue.Add(toElementValue);
			}

			return true;
		}

		public override bool TryConvert(DynamoDBList fromValue, Type toType, out object toValue, IValueConverter context)
		{
			toValue = null;

			var possibleListType = PossibleListOfType(toType, context);
			if(possibleListType == null) return false;
			if(fromValue == null) return true;

			return TryConvertToList(fromValue, possibleListType, out toValue, context);
		}

		private static bool TryConvertToList(DynamoDBList fromValue, Type possibleListType, out object toValue, IValueConverter context)
		{
			var listType = typeof(List<>).MakeGenericType(possibleListType);
			var list = (IList)Activator.CreateInstance(listType);

			foreach(var fromElementValue in fromValue)
			{
				object toElementValue;
				if(!context.TryConvert(fromElementValue, possibleListType, out toElementValue, context))
				{
					toValue = null;
					return false;
				}
				list.Add(toElementValue);
			}
			toValue = list;
			return true;
		}
	}
}
