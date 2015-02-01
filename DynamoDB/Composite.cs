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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adamantworks.Amazon.DynamoDB
{
	public static class Composite
	{
		public static char ValueSeparator
		{
			get { return valueSeparator; }
			set
			{
				valueSeparator = value;
				separatorString = new string(value, 1);
				separatorArray = new[] { value };
			}
		}

		private static char valueSeparator;
		private static string separatorString;
		private static char[] separatorArray;

		static Composite()
		{
			ValueSeparator = ';';
		}

		private static DynamoDBString Join(params DynamoDBScalar[] values)
		{
			if(values == null || values.Length == 0)
				throw new ArgumentException("Must provide a value", "values");

			return String.Join(separatorString, values.Select(v => v != null ? v.ToString() : ""));
		}

		public static DynamoDBString Value(IEnumerable<DynamoDBScalar> values)
		{
			if(values == null)
				throw new ArgumentNullException("values");

			return String.Join(separatorString, values.Select(v => v != null ? v.ToString() : ""));
		}

		#region 2 values
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2)
		{
			return Join(
				key1,
				key2);
		}
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, IValueConverter converter)
		{
			return Join(
				key1,
				key2);
		}

		public static DynamoDBString Value(object key1, DynamoDBScalar key2)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				key2);
		}
		public static DynamoDBString Value(object key1, DynamoDBScalar key2, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				key2);
		}

		public static DynamoDBString Value(DynamoDBScalar key1, object key2)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(DynamoDBScalar key1, object key2, IValueConverter converter)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, converter));
		}

		public static DynamoDBString Value(object key1, object key2)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(object key1, object key2, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				DynamoDBScalar.Convert(key2, converter));
		}
		#endregion

		#region 3 values
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, DynamoDBScalar key3)
		{
			return Join(
				key1,
				key2,
				key3);
		}
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, DynamoDBScalar key3, IValueConverter converter)
		{
			return Join(
				key1,
				key2,
				key3);
		}

		public static DynamoDBString Value(object key1, DynamoDBScalar key2, DynamoDBScalar key3)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				key2,
				key3);
		}
		public static DynamoDBString Value(object key1, DynamoDBScalar key2, DynamoDBScalar key3, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				key2,
				key3);
		}

		public static DynamoDBString Value(DynamoDBScalar key1, object key2, DynamoDBScalar key3)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				key3);
		}
		public static DynamoDBString Value(DynamoDBScalar key1, object key2, DynamoDBScalar key3, IValueConverter converter)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, converter),
				key3);
		}

		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, object key3)
		{
			return Join(
				key1,
				key2,
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, object key3, IValueConverter converter)
		{
			return Join(
				key1,
				key2,
				DynamoDBScalar.Convert(key3, converter));
		}

		public static DynamoDBString Value(object key1, object key2, DynamoDBScalar key3)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				key3);
		}
		public static DynamoDBString Value(object key1, object key2, DynamoDBScalar key3, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				DynamoDBScalar.Convert(key2, converter),
				key3);
		}

		public static DynamoDBString Value(object key1, DynamoDBScalar key2, object key3)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				key2,
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(object key1, DynamoDBScalar key2, object key3, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				key2,
				DynamoDBScalar.Convert(key3, converter));
		}

		public static DynamoDBString Value(DynamoDBScalar key1, object key2, object key3)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(DynamoDBScalar key1, object key2, object key3, IValueConverter converter)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, converter),
				DynamoDBScalar.Convert(key3, converter));
		}

		public static DynamoDBString Value(object key1, object key2, object key3)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(object key1, object key2, object key3, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				DynamoDBScalar.Convert(key2, converter),
				DynamoDBScalar.Convert(key3, converter));
		}
		#endregion

		#region 4 values
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, DynamoDBScalar key3, DynamoDBScalar key4)
		{
			return Join(
				key1,
				key2,
				key3,
				key4);
		}
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, DynamoDBScalar key3, DynamoDBScalar key4, IValueConverter converter)
		{
			return Join(
				key1,
				key2,
				key3,
				key4);
		}

		public static DynamoDBString Value(object key1, DynamoDBScalar key2, DynamoDBScalar key3, DynamoDBScalar key4)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				key2,
				key3,
				key4);
		}
		public static DynamoDBString Value(object key1, DynamoDBScalar key2, DynamoDBScalar key3, DynamoDBScalar key4, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				key2,
				key3,
				key4);
		}

		public static DynamoDBString Value(DynamoDBScalar key1, object key2, DynamoDBScalar key3, DynamoDBScalar key4)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				key3,
				key4);
		}
		public static DynamoDBString Value(DynamoDBScalar key1, object key2, DynamoDBScalar key3, DynamoDBScalar key4, IValueConverter converter)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, converter),
				key3,
				key4);
		}

		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, object key3, DynamoDBScalar key4)
		{
			return Join(
				key1,
				key2,
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				key4);
		}
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, object key3, DynamoDBScalar key4, IValueConverter converter)
		{
			return Join(
				key1,
				key2,
				DynamoDBScalar.Convert(key3, converter),
				key4);
		}

		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, DynamoDBScalar key3, object key4)
		{
			return Join(
				key1,
				key2,
				key3,
				DynamoDBScalar.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, DynamoDBScalar key3, object key4, IValueConverter converter)
		{
			return Join(
				key1,
				key2,
				key3,
				DynamoDBScalar.Convert(key4, converter));
		}

		public static DynamoDBString Value(object key1, object key2, DynamoDBScalar key3, DynamoDBScalar key4)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				key3,
				key4);
		}
		public static DynamoDBString Value(object key1, object key2, DynamoDBScalar key3, DynamoDBScalar key4, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				DynamoDBScalar.Convert(key2, converter),
				key3,
				key4);
		}

		public static DynamoDBString Value(object key1, DynamoDBScalar key2, object key3, DynamoDBScalar key4)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				key2,
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				key4);
		}
		public static DynamoDBString Value(object key1, DynamoDBScalar key2, object key3, DynamoDBScalar key4, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				key2,
				DynamoDBScalar.Convert(key3, converter),
				key4);
		}

		public static DynamoDBString Value(object key1, DynamoDBScalar key2, DynamoDBScalar key3, object key4)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				key2,
				key3,
				DynamoDBScalar.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(object key1, DynamoDBScalar key2, DynamoDBScalar key3, object key4, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				key2,
				key3,
				DynamoDBScalar.Convert(key4, converter));
		}

		public static DynamoDBString Value(DynamoDBScalar key1, object key2, object key3, DynamoDBScalar key4)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				key4);
		}
		public static DynamoDBString Value(DynamoDBScalar key1, object key2, object key3, DynamoDBScalar key4, IValueConverter converter)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, converter),
				DynamoDBScalar.Convert(key3, converter),
				key4);
		}

		public static DynamoDBString Value(DynamoDBScalar key1, object key2, DynamoDBScalar key3, object key4)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				key3,
				DynamoDBScalar.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(DynamoDBScalar key1, object key2, DynamoDBScalar key3, object key4, IValueConverter converter)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, converter),
				key3,
				DynamoDBScalar.Convert(key4, converter));
		}

		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, object key3, object key4)
		{
			return Join(
				key1,
				key2,
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(DynamoDBScalar key1, DynamoDBScalar key2, object key3, object key4, IValueConverter converter)
		{
			return Join(
				key1,
				key2,
				DynamoDBScalar.Convert(key3, converter),
				DynamoDBScalar.Convert(key4, converter));
		}

		public static DynamoDBString Value(object key1, object key2, object key3, DynamoDBScalar key4)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				key4);
		}
		public static DynamoDBString Value(object key1, object key2, object key3, DynamoDBScalar key4, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				DynamoDBScalar.Convert(key2, converter),
				DynamoDBScalar.Convert(key3, converter),
				key4);
		}

		public static DynamoDBString Value(object key1, object key2, DynamoDBScalar key3, object key4)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				key3,
				DynamoDBScalar.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(object key1, object key2, DynamoDBScalar key3, object key4, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				DynamoDBScalar.Convert(key2, converter),
				key3,
				DynamoDBScalar.Convert(key4, converter));
		}

		public static DynamoDBString Value(object key1, DynamoDBScalar key2, object key3, object key4)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				key2,
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(object key1, DynamoDBScalar key2, object key3, object key4, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				key2,
				DynamoDBScalar.Convert(key3, converter),
				DynamoDBScalar.Convert(key4, converter));
		}

		public static DynamoDBString Value(DynamoDBScalar key1, object key2, object key3, object key4)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(DynamoDBScalar key1, object key2, object key3, object key4, IValueConverter converter)
		{
			return Join(
				key1,
				DynamoDBScalar.Convert(key2, converter),
				DynamoDBScalar.Convert(key3, converter),
				DynamoDBScalar.Convert(key4, converter));
		}

		public static DynamoDBString Value(object key1, object key2, object key3, object key4)
		{
			return Join(
				DynamoDBScalar.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				DynamoDBScalar.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value(object key1, object key2, object key3, object key4, IValueConverter converter)
		{
			return Join(
				DynamoDBScalar.Convert(key1, converter),
				DynamoDBScalar.Convert(key2, converter),
				DynamoDBScalar.Convert(key3, converter),
				DynamoDBScalar.Convert(key4, converter));
		}
		#endregion

		public static string Name(params string[] names)
		{
			return String.Join(separatorString, names);
		}
		public static string Name(IEnumerable<string> names)
		{
			return String.Join(separatorString, names);
		}

		private static string[] Split(DynamoDBString value)
		{
			return value.ToString().Split(separatorArray);
		}

		public static Tuple<T1, T2> Split<T1, T2>(DynamoDBString value)
		{
			return Split<T1, T2>(value, DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2> Split<T1, T2>(DynamoDBString value, IValueConverter converter)
		{
			var values = Split(value);
			if(values.Length != 2)
				throw new ArgumentException("Must be a composite of 2 values", "value");

			return Tuple.Create(
				((DynamoDBString)values[0]).To<T1>(converter),
				((DynamoDBString)values[1]).To<T2>(converter));
		}
		public static Tuple<T1, T2, T3> Split<T1, T2, T3>(DynamoDBString value)
		{
			return Split<T1, T2, T3>(value, DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2, T3> Split<T1, T2, T3>(DynamoDBString value, IValueConverter converter)
		{
			var values = Split(value);
			if(values.Length != 3)
				throw new ArgumentException("Must be a composite of 2 values", "value");

			return Tuple.Create(
				((DynamoDBString)values[0]).To<T1>(converter),
				((DynamoDBString)values[1]).To<T2>(converter),
				((DynamoDBString)values[2]).To<T3>(converter));
		}
		public static Tuple<T1, T2, T3, T4> Split<T1, T2, T3, T4>(DynamoDBString value)
		{
			return Split<T1, T2, T3, T4>(value, DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2, T3, T4> Split<T1, T2, T3, T4>(DynamoDBString value, IValueConverter converter)
		{
			var values = Split(value);
			if(values.Length != 4)
				throw new ArgumentException("Must be a composite of 2 values", "value");

			return Tuple.Create(
				((DynamoDBString)values[0]).To<T1>(converter),
				((DynamoDBString)values[1]).To<T2>(converter),
				((DynamoDBString)values[2]).To<T3>(converter),
				((DynamoDBString)values[3]).To<T4>(converter));
		}

		public static Tuple<T1, T2> Split<T1, T2>(DynamoDBValue value)
		{
			return Split<T1, T2>(AsString(value), DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2> Split<T1, T2>(DynamoDBValue value, IValueConverter converter)
		{
			return Split<T1, T2>(AsString(value), converter);
		}
		public static Tuple<T1, T2, T3> Split<T1, T2, T3>(DynamoDBValue value)
		{
			return Split<T1, T2, T3>(AsString(value), DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2, T3> Split<T1, T2, T3>(DynamoDBValue value, IValueConverter converter)
		{
			return Split<T1, T2, T3>(AsString(value), converter);
		}
		public static Tuple<T1, T2, T3, T4> Split<T1, T2, T3, T4>(DynamoDBValue value)
		{
			return Split<T1, T2, T3, T4>(AsString(value), DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2, T3, T4> Split<T1, T2, T3, T4>(DynamoDBValue value, IValueConverter converter)
		{
			return Split<T1, T2, T3, T4>(AsString(value), converter);
		}

		private static DynamoDBString AsString(DynamoDBValue value)
		{
			var stringValue = value as DynamoDBString;
			if(stringValue == null)
				throw new ArgumentException(string.Format("Argument must be a DynamoDBString, was {0}", value != null ? value.GetType().Name : "<null>"));
			return stringValue;
		}
	}
}
