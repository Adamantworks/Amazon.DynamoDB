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
			return PossibleListOfTypes(toType, context).Any();
		}

		private static IEnumerable<Type> PossibleListOfTypes(Type toType, IValueConverter context)
		{
			return toType.GetInterfaces().Concat(new[] { toType }) // The to type might itself be a type we can convert to
				.Where(i =>
				{
					if(!i.IsGenericType) return false;
					var openGeneric = i.GetGenericTypeDefinition();
					return openGeneric == typeof(List<>) || openGeneric == typeof(IList<>) || openGeneric == typeof(ICollection<>) || openGeneric == typeof(IEnumerable<>);
				})
				.Select(i => i.GenericTypeArguments[0]); // We must assume we can convert the values, because we don't know their concrete type yet inorder to check
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
				if(!context.TryConvert(fromElementValue, out toElementValue) || toElementValue == null)
					return false;
				toValue.Add(toElementValue);
			}

			return true;
		}

		public override bool TryConvert(DynamoDBList fromValue, Type toType, out object toValue, IValueConverter context)
		{
			toValue = null;

			var possibleListTypes = PossibleListOfTypes(toType, context).Distinct().ToList();
			if(possibleListTypes.Count == 0) return false;
			if(fromValue == null) return true;

			foreach(var possibleSetType in possibleListTypes)
				if(TryConvertToList(fromValue, possibleSetType, out toValue, context))
					return true;

			return false;
		}

		private static bool TryConvertToList(DynamoDBList fromValue, Type possibleListType, out object toValue, IValueConverter context)
		{
			var listType = typeof(List<>).MakeGenericType(possibleListType);
			var list = (IList)Activator.CreateInstance(listType);
			//var dynamicList = (dynamic)list;

			foreach(var fromElementValue in fromValue)
			{
				object toElementValue;
				if(!context.TryConvert(fromElementValue, possibleListType, out toElementValue, context))
				{
					toValue = null;
					return false;
				}
				list.Add(toElementValue);
				//dynamicList.Add((dynamic)toElementValue);
				// Cast to dynamic seems to be needed because otherwise it wants an Add(object) method
			}
			toValue = list;
			return true;
		}
	}
}
