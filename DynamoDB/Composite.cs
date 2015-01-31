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
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Syntax;

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

		public static DynamoDBString ValueStrict(params DynamoDBKeyValue[] values)
		{
			if(values == null || values.Length == 0)
				throw new ArgumentException("Must provide a value", "values");

			return String.Join(separatorString, values.Select(v => v != null ? v.ToString() : null));
		}
		public static DynamoDBString ValueStrict(IEnumerable<DynamoDBKeyValue> values)
		{
			if(values == null)
				throw new ArgumentNullException("values");

			return String.Join(separatorString, values.Select(v => v != null ? v.ToString() : null));
		}

		public static DynamoDBString Value<TKey1, TKey2>(TKey1 key1, TKey2 key2)
		{
			return ValueStrict(
				DynamoDBKeyValue.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBKeyValue.Convert(key2, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value<TKey1, TKey2>(TKey1 key1, TKey2 key2, IValueConverter converter)
		{
			return ValueStrict(
				DynamoDBKeyValue.Convert(key1, converter),
				DynamoDBKeyValue.Convert(key2, converter));
		}
		public static DynamoDBString Value<TKey1, TKey2, TKey3>(TKey1 key1, TKey2 key2, TKey3 key3)
		{
			return ValueStrict(
				DynamoDBKeyValue.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBKeyValue.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				DynamoDBKeyValue.Convert(key3, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value<TKey1, TKey2, TKey3>(TKey1 key1, TKey2 key2, TKey3 key3, IValueConverter converter)
		{
			return ValueStrict(
				DynamoDBKeyValue.Convert(key1, converter),
				DynamoDBKeyValue.Convert(key2, converter),
				DynamoDBKeyValue.Convert(key3, converter));
		}
		public static DynamoDBString Value<TKey1, TKey2, TKey3, TKey4>(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
		{
			return ValueStrict(
				DynamoDBKeyValue.Convert(key1, DynamoDBValueConverters.DefaultComposite),
				DynamoDBKeyValue.Convert(key2, DynamoDBValueConverters.DefaultComposite),
				DynamoDBKeyValue.Convert(key3, DynamoDBValueConverters.DefaultComposite),
				DynamoDBKeyValue.Convert(key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static DynamoDBString Value<TKey1, TKey2, TKey3, TKey4>(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, IValueConverter converter)
		{
			return ValueStrict(
				DynamoDBKeyValue.Convert(key1, converter),
				DynamoDBKeyValue.Convert(key2, converter),
				DynamoDBKeyValue.Convert(key3, converter),
				DynamoDBKeyValue.Convert(key4, converter));
		}

		public static CompositeHashKeySyntax HashKeyStrict(DynamoDBKeyValue key1)
		{
			return new CompositeHashKeySyntax(key1);
		}
		public static CompositeHashKeySyntax HashKeyStrict(DynamoDBKeyValue key1, DynamoDBKeyValue key2)
		{
			return new CompositeHashKeySyntax(ValueStrict(key1, key2));
		}
		public static CompositeHashKeySyntax HashKeyStrict(DynamoDBKeyValue key1, DynamoDBKeyValue key2, DynamoDBKeyValue key3)
		{
			return new CompositeHashKeySyntax(ValueStrict(key1, key2, key3));
		}
		public static CompositeHashKeySyntax HashKeyStrict(DynamoDBKeyValue key1, DynamoDBKeyValue key2, DynamoDBKeyValue key3, DynamoDBKeyValue key4)
		{
			return new CompositeHashKeySyntax(ValueStrict(key1, key2, key3, key4));
		}

		public static CompositeHashKeySyntax HashKey<TKey1>(TKey1 key1)
		{
			return new CompositeHashKeySyntax(DynamoDBKeyValue.Convert(key1, DynamoDBValueConverters.DefaultComposite));
		}
		public static CompositeHashKeySyntax HashKey<TKey1>(TKey1 key1, IValueConverter converter)
		{
			return new CompositeHashKeySyntax(DynamoDBKeyValue.Convert(key1, converter));
		}
		public static CompositeHashKeySyntax HashKey<TKey1, TKey2>(TKey1 key1, TKey2 key2)
		{
			return new CompositeHashKeySyntax(Value(key1, key2, DynamoDBValueConverters.DefaultComposite));
		}
		public static CompositeHashKeySyntax HashKey<TKey1, TKey2>(TKey1 key1, TKey2 key2, IValueConverter converter)
		{
			return new CompositeHashKeySyntax(Value(key1, key2, converter));
		}
		public static CompositeHashKeySyntax HashKey<TKey1, TKey2, TKey3>(TKey1 key1, TKey2 key2, TKey3 key3)
		{
			return new CompositeHashKeySyntax(Value(key1, key2, key3, DynamoDBValueConverters.DefaultComposite));
		}
		public static CompositeHashKeySyntax HashKey<TKey1, TKey2, TKey3>(TKey1 key1, TKey2 key2, TKey3 key3, IValueConverter converter)
		{
			return new CompositeHashKeySyntax(Value(key1, key2, key3, converter));
		}
		public static CompositeHashKeySyntax HashKey<TKey1, TKey2, TKey3, TKey4>(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
		{
			return new CompositeHashKeySyntax(Value(key1, key2, key3, key4, DynamoDBValueConverters.DefaultComposite));
		}
		public static CompositeHashKeySyntax HashKey<TKey1, TKey2, TKey3, TKey4>(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, IValueConverter converter)
		{
			return new CompositeHashKeySyntax(Value(key1, key2, key3, key4, converter));
		}

		public static string Name(params string[] names)
		{
			return String.Join(separatorString, names);
		}
		public static string Name(IEnumerable<string> names)
		{
			return String.Join(separatorString, names);
		}

		private static string[] SplitString(DynamoDBString value)
		{
			return value.ToString().Split(separatorArray);
		}

		public static Tuple<T1, T2> Split<T1, T2>(DynamoDBString value)
		{
			return Split<T1, T2>(value, DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2> Split<T1, T2>(DynamoDBString value, IValueConverter converter)
		{
			var values = SplitString(value);
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
			var values = SplitString(value);
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
			var values = SplitString(value);
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
			var stringValue = value as DynamoDBString;
			if(stringValue == null && value != null)
				throw new ArgumentException("Argument must be a DynamoDBString, was");

			return Split<T1, T2>(stringValue, DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2> Split<T1, T2>(DynamoDBValue value, IValueConverter converter)
		{
			var stringValue = value as DynamoDBString;
			if(stringValue == null && value != null)
				throw new ArgumentException("Argument must be a DynamoDBString, was");

			return Split<T1, T2>(stringValue, converter);
		}
		public static Tuple<T1, T2, T3> Split<T1, T2, T3>(DynamoDBValue value)
		{
			var stringValue = value as DynamoDBString;
			if(stringValue == null && value != null)
				throw new ArgumentException("Argument must be a DynamoDBString, was");

			return Split<T1, T2, T3>(stringValue, DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2, T3> Split<T1, T2, T3>(DynamoDBValue value, IValueConverter converter)
		{
			var stringValue = value as DynamoDBString;
			if(stringValue == null && value != null)
				throw new ArgumentException("Argument must be a DynamoDBString, was");

			return Split<T1, T2, T3>(stringValue, converter);
		}
		public static Tuple<T1, T2, T3, T4> Split<T1, T2, T3, T4>(DynamoDBValue value)
		{
			var stringValue = value as DynamoDBString;
			if(stringValue == null && value != null)
				throw new ArgumentException("Argument must be a DynamoDBString, was");

			return Split<T1, T2, T3, T4>(stringValue, DynamoDBValueConverters.DefaultComposite);
		}
		public static Tuple<T1, T2, T3, T4> Split<T1, T2, T3, T4>(DynamoDBValue value, IValueConverter converter)
		{
			var stringValue = value as DynamoDBString;
			if(stringValue == null && value != null)
				throw new ArgumentException("Argument must be a DynamoDBString, was");

			return Split<T1, T2, T3, T4>(stringValue, converter);
		}
	}
}
