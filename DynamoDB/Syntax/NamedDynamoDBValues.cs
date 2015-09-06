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
using System.ComponentModel;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public abstract class NamedDynamoDBValues : IAwsAttributeValuesProvider, IReadOnlyCollection<KeyValuePair<string, DynamoDBValue>>
	{
		private readonly DynamoDBMap values;

		protected NamedDynamoDBValues()
		{
			values = new DynamoDBMap();
		}

		private static string BuildName(string name)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Empty or null attribute value name");
			if(name.StartsWith("#"))
				throw new ArgumentException("Do not prefix attribute value names with : when declaring them,  (only when using them)");

			return ":" + name;
		}

		protected abstract string BuildName(int position);

		public int Count { get { return values.Count; } }

		protected void AddValue(string name, DynamoDBValue value)
		{
			values.Add(BuildName(name), value);
		}
		protected void AddValue(int position, DynamoDBValue value)
		{
			values.Add(BuildName(position), value);
		}

		protected void AddValues(object namedValues, IValueConverter converter)
		{
			foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(namedValues))
			{
				var fromValue = descriptor.GetValue(namedValues);
				DynamoDBValue toValue;
				if(fromValue == null)
					toValue = null;
				else if(!converter.TryConvert(fromValue, out toValue))
					throw new InvalidCastException(string.Format("Can't convert value named '{0}'", descriptor.Name));

				AddValue(descriptor.Name, toValue);
			}
		}
		protected void AddValues(IList<object> positionalValues, IValueConverter converter)
		{
			for(var i = 0; i < positionalValues.Count; i++)
			{
				var fromValue = positionalValues[i];
				DynamoDBValue toValue;
				if(fromValue == null)
					toValue = null;
				else if(!converter.TryConvert(fromValue, out toValue))
					throw new InvalidCastException("Can't convert value at position " + i);

				AddValue(i, toValue);
			}
		}
		protected void AddValues(DynamoDBValue[] positionalValues)
		{
			for(var i = 0; i < positionalValues.Length; i++)
				AddValue(i, positionalValues[i]);
		}

		internal DynamoDBMap DynamoDBMapDeepClone()
		{
			return values.DeepClone();
		}

		Dictionary<string, Aws.AttributeValue> IAwsAttributeValuesProvider.ToAwsAttributeValues()
		{
			return values.ToAwsDictionary();
		}

		public IEnumerator<KeyValuePair<string, DynamoDBValue>> GetEnumerator()
		{
			return values.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return values.GetEnumerator();
		}
	}
}
