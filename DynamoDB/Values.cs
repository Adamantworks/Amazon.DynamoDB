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
using System.ComponentModel;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public class Values : IAwsAttributeValuesProvider
	{
		public static readonly Values Empty = new Values();

		private readonly DynamoDBMap values;

		public Values()
		{
			values = new DynamoDBMap();
		}
		private Values(DynamoDBMap values)
		{
			this.values = values;
		}

		#region Static Factory Methods
		public static Values Named(object namedValues) // overload needed to avoid ambiguous call when no positional args supplied
		{
			var values = new DynamoDBMap();
			AddValues(values, namedValues, DynamoDBValueConverter.Default);
			return new Values(values);
		}
		public static Values Named(object namedValues, params object[] positionalValues)
		{
			var values = new DynamoDBMap();
			AddValues(values, namedValues, DynamoDBValueConverter.Default);
			AddValues(values, positionalValues, DynamoDBValueConverter.Default);
			return new Values(values);
		}
		public static Values Named(object namedValues, params DynamoDBValue[] positionalValues)
		{
			var values = new DynamoDBMap();
			AddValues(values, namedValues, DynamoDBValueConverter.Default);
			AddValues(values, positionalValues);
			return new Values(values);
		}
		public static Values Named(IValueConverter converter, object namedValues) // overload needed to avoid ambiguous call when no positional args supplied
		{
			var values = new DynamoDBMap();
			AddValues(values, namedValues, converter);
			return new Values(values);
		}
		public static Values Named(IValueConverter converter, object namedValues, params object[] positionalValues)
		{
			var values = new DynamoDBMap();
			AddValues(values, namedValues, converter);
			AddValues(values, positionalValues, converter);
			return new Values(values);
		}
		public static Values Named(IValueConverter converter, object namedValues, params DynamoDBValue[] positionalValues)
		{
			var values = new DynamoDBMap();
			AddValues(values, namedValues, converter);
			AddValues(values, positionalValues);
			return new Values(values);
		}

		// TODO add comments explaining positional values
		public static Values Of(params object[] positionalValues)
		{
			return Of(DynamoDBValueConverter.Default, positionalValues);
		}
		public static Values Of(IValueConverter converter, params object[] positionalValues)
		{
			var values = new DynamoDBMap();
			AddValues(values, positionalValues, converter);
			return new Values(values);
		}
		public static Values Of(params DynamoDBValue[] positionalValues)
		{
			var values = new DynamoDBMap();
			AddValues(values, positionalValues);
			return new Values(values);
		}

		private static void AddValues(DynamoDBMap values, object namedValues, IValueConverter converter)
		{
			foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(namedValues))
			{
				var fromValue = descriptor.GetValue(namedValues);
				DynamoDBValue toValue;
				if(fromValue == null)
					toValue = null;
				else if(!converter.TryConvert(fromValue, out toValue))
					throw new InvalidCastException(string.Format("Can't convert value named '{0}'", descriptor.Name));

				values.Add(BuildName(descriptor.Name), toValue);
			}
		}
		private static void AddValues(DynamoDBMap values, IList<object> positionalValues, IValueConverter converter)
		{
			for(var i = 0; i < positionalValues.Count; i++)
			{
				var fromValue = positionalValues[i];
				DynamoDBValue toValue;
				if(fromValue == null)
					toValue = null;
				else if(!converter.TryConvert(fromValue, out toValue))
					throw new InvalidCastException("Can't convert value at position " + i);

				values.Add(BuildName(i), toValue);
			}
		}
		private static void AddValues(DynamoDBMap values, DynamoDBValue[] positionalValues)
		{
			for(var i = 0; i < positionalValues.Length; i++)
				values.Add(BuildName(i), positionalValues[i]);
		}

		private static string BuildName(string name)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Empty or null attribute value name");
			if(name.StartsWith("#"))
				throw new ArgumentException("Do not prefix attribute value names with : when declaring them,  (only when using them)");

			return ":" + name;
		}
		private static string BuildName(int position)
		{
			return ":p" + position;
		}
		#endregion

		public Values Add(string name, DynamoDBValue value)
		{
			values.Add(name, value);
			return this;
		}
		public Values Add(int position, DynamoDBValue value)
		{
			values.Add(BuildName(position), value);
			return this;
		}
		public Values Add(string name, object value)
		{
			values.Add(name, DynamoDBValue.Convert(value));
			return this;
		}
		public Values Add(string name, object value, IValueConverter converter)
		{
			values.Add(name, DynamoDBValue.Convert(value, converter));
			return this;
		}
		public Values Add(int position, object value)
		{
			values.Add(BuildName(position), DynamoDBValue.Convert(value));
			return this;
		}
		public Values Add(int position, object value, IValueConverter converter)
		{
			values.Add(BuildName(position), DynamoDBValue.Convert(value, converter));
			return this;
		}

		internal DynamoDBMap DynamoDBMapDeepClone()
		{
			return values.DeepClone();
		}

		Dictionary<string, Aws.AttributeValue> IAwsAttributeValuesProvider.ToAwsAttributeValues()
		{
			return values.ToAwsDictionary();
		}
	}
}
