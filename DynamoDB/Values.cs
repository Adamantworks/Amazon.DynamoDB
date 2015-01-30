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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public class Values : IAwsAttributeValuesProvider
	{
		public static Values Empty = new Values();

		private readonly DynamoDBMap values;

		public Values()
		{
			values = new DynamoDBMap();
		}
		internal Values(DynamoDBMap values)
		{
			this.values = values;
		}

		#region Static Factory Methods
		public static Values Named(object namedValues, params object[] positionalValues)
		{
			return Named(DynamoDBValueConverter.Default, namedValues, positionalValues);
		}
		public static Values Named(IDynamoDBValueConverter converter, object namedValues, params object[] positionalValues)
		{
			var values = new DynamoDBMap();
			Build(values, converter, namedValues);
			Build(values, converter, positionalValues);
			return new Values(values);
		}

		public static Values Of(params object[] positionalValues)
		{
			return Of(DynamoDBValueConverter.Default, positionalValues);
		}
		public static Values Of(IDynamoDBValueConverter converter, params object[] positionalValues)
		{
			var values = new DynamoDBMap();
			Build(values, converter, positionalValues);
			return new Values(values);
		}
		public static Values Of(params DynamoDBValue[] positionalValues)
		{
			var values = new DynamoDBMap();
			for(var i = 0; i < positionalValues.Length; i++)
				values.Add(BuildName(i), positionalValues[i]);
			return new Values(values);
		}

		private static void Build(DynamoDBMap values, IDynamoDBValueConverter converter, object namedValues)
		{
			foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(namedValues))
			{
				var fromValue = descriptor.GetValue(namedValues);
				DynamoDBValue toValue;
				if(!converter.TryConvertFrom(descriptor.PropertyType, fromValue, out toValue, converter))
					throw new InvalidCastException(string.Format("Can't convert value named '{0}'", descriptor.Name));

				values.Add(BuildName(descriptor.Name), toValue);
			}
		}
		private static void Build(DynamoDBMap values, IDynamoDBValueConverter converter, IList<object> positionalValues)
		{
			for(var i = 0; i < positionalValues.Count; i++)
			{
				var fromValue = positionalValues[i];
				DynamoDBValue toValue;
				if(fromValue == null)
					toValue = null;
				else if(!converter.TryConvertFrom(fromValue.GetType(), fromValue, out toValue, converter)) // TODO will this fail for subclasses of convertibles?
					throw new InvalidCastException("Can't convert value at position " + i);

				values.Add(BuildName(i), toValue);
			}
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
		public Values Add<T>(string name, T value)
		{
			values.Add(name, DynamoDBValue.Convert(value));
			return this;
		}
		public Values Add<T>(string name, T value, IDynamoDBValueConverter converter)
		{
			values.Add(name, DynamoDBValue.Convert(value, converter));
			return this;
		}
		public Values Add<T>(int position, T value)
		{
			values.Add(BuildName(position), DynamoDBValue.Convert(value));
			return this;
		}
		public Values Add<T>(int position, T value, IDynamoDBValueConverter converter)
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
