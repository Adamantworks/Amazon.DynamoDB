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

using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	internal class SetConverter<T> : ValueConverter<DynamoDBSet<T>> where T : DynamoDBKeyValue
	{
		public override bool CanConvertTo(Type toType, IValueConverter context)
		{
			// Must be assignable from HashSet<X> where T converts to X
			return PossibleSetOfTypes(toType, context).Any();
		}

		private static IEnumerable<Type> PossibleSetOfTypes(Type toType, IValueConverter context)
		{
			return toType.GetInterfaces().Concat(new[] { toType }) // The to type might itself be a type we can convert to
				.Where(i =>
				{
					if(!i.IsGenericType) return false;
					var openGeneric = i.GetGenericTypeDefinition();
					return openGeneric == typeof(HashSet<>) || openGeneric == typeof(ISet<>) || openGeneric == typeof(ICollection<>) || openGeneric == typeof(IEnumerable<>);
				})
				.Select(i => i.GenericTypeArguments[0])
				.Where(setOfType => context.CanConvert(typeof(T), setOfType, context));
		}

		public override bool CanConvertFrom(Type fromType, IValueConverter context)
		{
			// Must implement ISet<X> where X converts to T
			return fromType.GetInterfaces().Concat(new[] { fromType }) // The from type might itself be ISet<X>
				.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISet<>))
				.Any(i => context.CanConvert(i.GenericTypeArguments[0], typeof(T), context));
		}

		public override bool TryConvert(object fromValue, out DynamoDBSet<T> toValue, IValueConverter context)
		{
			toValue = null;
			if(fromValue == null) return true;
			if(!CanConvertFrom(fromValue.GetType(), context)) return false; // Always need this to make sure we are coming from a set

			var values = new List<T>();
			foreach(var fromElementValue in (IEnumerable)fromValue)
			{
				T toElementValue;
				if(!context.TryConvert(fromElementValue, out toElementValue) || toElementValue == null)
					return false;
				values.Add(toElementValue);
			}

			toValue = new DynamoDBSet<T>(values);
			if(toValue.Count != values.Count)
				throw new Exception("Converting values of set caused a collision (i.e. two values converted to the same value)");

			return true;
		}

		public override bool TryConvert(DynamoDBSet<T> fromValue, Type toType, out object toValue, IValueConverter context)
		{
			toValue = null;

			var possibleSetTypes = PossibleSetOfTypes(toType, context).Distinct().ToList();
			if(possibleSetTypes.Count == 0) return false;
			if(fromValue == null) return true;

			foreach(var possibleSetType in possibleSetTypes)
				if(TryConvertToHashSet(fromValue, possibleSetType, out toValue, context))
					return true;

			return false;
		}

		private static bool TryConvertToHashSet(DynamoDBSet<T> fromValue, Type possibleSetType, out object toValue, IValueConverter context)
		{
			var hashSetType = typeof(HashSet<>).MakeGenericType(possibleSetType);
			var hashSet = Activator.CreateInstance(hashSetType);
			var dynamicSet = (dynamic)hashSet;

			foreach(var fromElementValue in fromValue)
			{
				object toElementValue;
				if(!context.TryConvert(fromElementValue, possibleSetType, out toElementValue, context))
				{
					toValue = null;
					return false;
				}
				dynamicSet.Add((dynamic)toElementValue);
				// Cast to dynamic seems to be needed because otherwise it wants an Add(object) method
			}
			if(fromValue.Count != dynamicSet.Count)
				throw new Exception("Converting values of set caused a collision (i.e. two values converted to the same value)");
			toValue = hashSet;
			return true;
		}
	}
}
