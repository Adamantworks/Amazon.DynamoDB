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

namespace Adamantworks.Amazon.DynamoDB.Converters
{
	// TODO spin this off as a different library with IValueConverter?
	public class CompositeConverter : IValueConverter
	{
		private static readonly Comparer<int> ReverseIntComparer = Comparer<int>.Create((x, y) => -x.CompareTo(y));
		private readonly IDictionary<int, IList<IValueConverter>> converters = new SortedList<int, IList<IValueConverter>>(ReverseIntComparer);

		public CompositeConverter Add(IValueConverter converter, int priority = 0)
		{
			if(!converters.ContainsKey(priority))
				converters[priority] = new List<IValueConverter>();

			converters[priority].Add(converter);
			return this;
		}

		public bool Remove(IValueConverter converter)
		{
			foreach(var priorityLevel in converters)
				if(priorityLevel.Value.Remove(converter))
				{
					if(priorityLevel.Value.Count == 0)
						converters.Remove(priorityLevel.Key);

					return true;
				}

			return false;
		}

		public bool CanConvert(Type fromType, Type toType, IValueConverter context)
		{
			return converters.SelectMany(priorityLevel => priorityLevel.Value).Any(converter => converter.CanConvert(fromType, toType, context));
		}

		public bool TryConvert(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			toValue = null;
			foreach(var priorityLevel in converters)
			{
				var matchingConverters = 0;
				object convertedValue;
				foreach(var converter in priorityLevel.Value)
					if(converter.TryConvert(fromValue, toType, out convertedValue, context))
					{
						matchingConverters++;
						toValue = convertedValue; // only set for success, so failures can't overwrite success values
					}

				if(matchingConverters == 1)
					return true;
				if(matchingConverters > 1)
					throw new Exception(string.Format("Ambiguous conversion from {0} to {1}, more than one converter with the same priority matches", fromValue == null ? "<null>" : fromValue.GetType().FullName, toType.FullName));
			}
			return false;
		}
	}
}
