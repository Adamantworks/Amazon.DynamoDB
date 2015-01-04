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
		private readonly IDictionary<int, ISet<IDynamoDBValueConverter>> converters = new SortedList<int, ISet<IDynamoDBValueConverter>>();

		public void Add(IDynamoDBValueConverter converter, int priority = 0)
		{
			if(!converters.ContainsKey(priority))
				converters[priority] = new HashSet<IDynamoDBValueConverter>();

			converters[priority].Add(converter);
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


		public bool CanConvert(Type type, IDynamoDBValueConverter context)
		{
			return converters.SelectMany(priorityLevel => priorityLevel.Value).Any(converter => converter.CanConvert(type, context));
		}

		public bool TryConvertFrom(Type type, object fromValue, out DynamoDBValue toValue, IDynamoDBValueConverter context)
		{
			toValue = null;
			foreach(var priorityLevel in converters)
			{
				var matchingConverters = 0;
				foreach(var converter in priorityLevel.Value)
					if(converter.TryConvertFrom(type, fromValue, out toValue, context))
						matchingConverters++;

				if(matchingConverters == 1)
					return true;
				if(matchingConverters > 1)
					throw new Exception(string.Format("Ambiguous conversion from {0} to DynamoDBValue, more than one coverter with the same priority matches", type.FullName));
			}
			return false;
		}

		public bool TryConvertTo(DynamoDBValue fromValue, Type type, out object toValue, IDynamoDBValueConverter context)
		{
			toValue = null;
			foreach(var priorityLevel in converters)
			{
				var matchingConverters = 0;
				foreach(var converter in priorityLevel.Value)
					if(converter.TryConvertTo(fromValue, type, out toValue, context))
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
