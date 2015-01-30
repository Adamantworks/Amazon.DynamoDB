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

namespace Adamantworks.Amazon.DynamoDB.Converters
{
	public class CompositeConverter : IDynamoDBValueConverter
	{
		private static readonly Comparer<int> ReverseIntComparer = Comparer<int>.Create((x, y) => -x.CompareTo(y));
		private readonly IDictionary<int, ISet<IDynamoDBValueConverter>> converters = new SortedList<int, ISet<IDynamoDBValueConverter>>(ReverseIntComparer);

		public CompositeConverter Add(IDynamoDBValueConverter converter, int priority = 0)
		{
			if(!converters.ContainsKey(priority))
				converters[priority] = new HashSet<IDynamoDBValueConverter>();

			converters[priority].Add(converter);
			return this;
		}

		public bool Remove(IDynamoDBValueConverter converter)
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

		public bool CanConvertFrom<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			return converters.SelectMany(priorityLevel => priorityLevel.Value).Any(converter => converter.CanConvertFrom<T>(type, context));
		}

		public bool CanConvertTo<T>(Type type, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			return converters.SelectMany(priorityLevel => priorityLevel.Value).Any(converter => converter.CanConvertTo<T>(type, context));
		}

		public bool TryConvertFrom<T>(Type type, object fromValue, out T toValue, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			toValue = null;
			foreach(var priorityLevel in converters)
			{
				var matchingConverters = 0;
				foreach(var converter in priorityLevel.Value)
					if(converter.TryConvertFrom<T>(type, fromValue, out toValue, context))
						matchingConverters++;

				if(matchingConverters == 1)
					return true;
				if(matchingConverters > 1)
					throw new Exception(string.Format("Ambiguous conversion from {0} to DynamoDBValue, more than one coverter with the same priority matches", type.FullName));
			}
			return false;
		}

		public bool TryConvertTo<T>(T fromValue, Type type, out object toValue, IDynamoDBValueConverter context) where T : DynamoDBValue
		{
			toValue = null;
			foreach(var priorityLevel in converters)
			{
				var matchingConverters = 0;
				foreach(var converter in priorityLevel.Value)
					if(converter.TryConvertTo<T>(fromValue, type, out toValue, context))
						matchingConverters++;

				if(matchingConverters == 1)
					return true;
				if(matchingConverters > 1)
					throw new Exception(string.Format("Ambiguous conversion from DynamoDBValue to {0}, more than one coverter with the same priority matches", type.FullName));
			}
			return false;
		}
	}
}
