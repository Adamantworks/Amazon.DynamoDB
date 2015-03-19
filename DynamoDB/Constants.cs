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
	/// This class is used to create collections of constant values for use in expression.  Named constant
	/// can be referenced using :name.  Note that constant names can conflict with value names. Positional
	/// constants are referenced using :c{N} where N is the constant index starting with 0.
	/// </summary>
	public class Constants : NamedDynamoDBValues
	{
		#region Static Factory Methods
		public static Constants Named(object namedConstants) // overload needed to avoid ambiguous call when no positional args supplied
		{
			var values = new Constants();
			values.AddValues(namedConstants, DynamoDBValueConverter.Default);
			return values;
		}
		public static Constants Named(object namedConstants, params object[] positionalConstants)
		{
			var values = new Constants();
			values.AddValues(namedConstants, DynamoDBValueConverter.Default);
			values.AddValues(positionalConstants, DynamoDBValueConverter.Default);
			return values;
		}
		public static Constants Named(object namedConstants, params DynamoDBValue[] positionalConstants)
		{
			var values = new Constants();
			values.AddValues(namedConstants, DynamoDBValueConverter.Default);
			values.AddValues(positionalConstants);
			return values;
		}
		public static Constants Named(IValueConverter converter, object namedConstants) // overload needed to avoid ambiguous call when no positional args supplied
		{
			var values = new Constants();
			values.AddValues(namedConstants, converter);
			return values;
		}
		public static Constants Named(IValueConverter converter, object namedConstants, params object[] positionalConstants)
		{
			var values = new Constants();
			values.AddValues(namedConstants, converter);
			values.AddValues(positionalConstants, converter);
			return values;
		}
		public static Constants Named(IValueConverter converter, object namedConstants, params DynamoDBValue[] positionalConstants)
		{
			var values = new Constants();
			values.AddValues(namedConstants, converter);
			values.AddValues(positionalConstants);
			return values;
		}

		public static Constants Of(params object[] positionalConstants)
		{
			return Of(DynamoDBValueConverter.Default, positionalConstants);
		}
		public static Constants Of(IValueConverter converter, params object[] positionalConstants)
		{
			var values = new Constants();
			values.AddValues(positionalConstants, converter);
			return values;
		}
		public static Constants Of(params DynamoDBValue[] positionalConstants)
		{
			var values = new Constants();
			values.AddValues(positionalConstants);
			return values;
		}

		protected override string BuildName(int position)
		{
			return ":c" + position;
		}
		#endregion

		public Constants Add(string name, DynamoDBValue constant)
		{
			AddValue(name, constant);
			return this;
		}
		public Constants Add(int position, DynamoDBValue constant)
		{
			AddValue(position, constant);
			return this;
		}
		public Constants Add(string name, object constant)
		{
			AddValue(name, DynamoDBValue.Convert(constant));
			return this;
		}
		public Constants Add(string name, object constant, IValueConverter converter)
		{
			AddValue(name, DynamoDBValue.Convert(constant, converter));
			return this;
		}
		public Constants Add(int position, object constant)
		{
			AddValue(position, DynamoDBValue.Convert(constant));
			return this;
		}
		public Constants Add(int position, object constant, IValueConverter converter)
		{
			AddValue(position, DynamoDBValue.Convert(constant, converter));
			return this;
		}
	}
}
