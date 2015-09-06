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

using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Syntax;

namespace Adamantworks.Amazon.DynamoDB
{
	/// <summary>
	/// This class is used to create collections of values for use in expression.  Named values
	/// can be referenced using :name.  Note that constant names can conflict with value names. Positional
	/// values are referenced using :v{N} where N is the value index starting with 0.
	/// </summary>
	public class Values : NamedDynamoDBValues
	{
		#region Static Factory Methods
		public static Values Named(object namedValues) // overload needed to avoid ambiguous call when no positional args supplied
		{
			var values = new Values();
			values.AddValues(namedValues, DynamoDBValueConverter.Default);
			return values;
		}
		public static Values Named(object namedValues, params object[] positionalValues)
		{
			var values = new Values();
			values.AddValues(namedValues, DynamoDBValueConverter.Default);
			values.AddValues(positionalValues, DynamoDBValueConverter.Default);
			return values;
		}
		public static Values Named(object namedValues, params DynamoDBValue[] positionalValues)
		{
			var values = new Values();
			values.AddValues(namedValues, DynamoDBValueConverter.Default);
			values.AddValues(positionalValues);
			return values;
		}
		public static Values Named(IValueConverter converter, object namedValues) // overload needed to avoid ambiguous call when no positional args supplied
		{
			var values = new Values();
			values.AddValues(namedValues, converter);
			return values;
		}
		public static Values Named(IValueConverter converter, object namedValues, params object[] positionalValues)
		{
			var values = new Values();
			values.AddValues(namedValues, converter);
			values.AddValues(positionalValues, converter);
			return values;
		}
		public static Values Named(IValueConverter converter, object namedValues, params DynamoDBValue[] positionalValues)
		{
			var values = new Values();
			values.AddValues(namedValues, converter);
			values.AddValues(positionalValues);
			return values;
		}

		// TODO add comments explaining positional values
		public static Values Of(params object[] positionalValues)
		{
			return Of(DynamoDBValueConverter.Default, positionalValues);
		}
		public static Values Of(IValueConverter converter, params object[] positionalValues)
		{
			var values = new Values();
			values.AddValues(positionalValues, converter);
			return values;
		}
		public static Values Of(params DynamoDBValue[] positionalValues)
		{
			var values = new Values();
			values.AddValues(positionalValues);
			return values;
		}

		protected override string BuildName(int position)
		{
			return ":v" + position;
		}
		#endregion

		public Values Add(string name, DynamoDBValue value)
		{
			AddValue(name, value);
			return this;
		}
		public Values Add(int position, DynamoDBValue value)
		{
			AddValue(position, value);
			return this;
		}
		public Values Add(string name, object value)
		{
			AddValue(name, DynamoDBValue.Convert(value));
			return this;
		}
		public Values Add(string name, object value, IValueConverter converter)
		{
			AddValue(name, DynamoDBValue.Convert(value, converter));
			return this;
		}
		public Values Add(int position, object value)
		{
			AddValue(position, DynamoDBValue.Convert(value));
			return this;
		}
		public Values Add(int position, object value, IValueConverter converter)
		{
			AddValue(position, DynamoDBValue.Convert(value, converter));
			return this;
		}
	}
}
