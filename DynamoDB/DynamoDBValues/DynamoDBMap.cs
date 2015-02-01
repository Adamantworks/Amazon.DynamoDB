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
using System.Linq;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.Internal;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	// TODO Support dynamic
	public sealed class DynamoDBMap : DynamoDBValue, IDictionary<string, DynamoDBValue>, IReadOnlyDictionary<string, DynamoDBValue>
	{
		private readonly IDictionary<string, DynamoDBValue> values;

		public DynamoDBMap()
		{
			values = new Dictionary<string, DynamoDBValue>();
		}
		public DynamoDBMap(IDictionary<string, DynamoDBValue> values)
		{
			this.values = new Dictionary<string, DynamoDBValue>(values);
		}
		private DynamoDBMap(Dictionary<string, DynamoDBValue> values)
		{
			this.values = values;
		}

		internal DynamoDBMap(Dictionary<string, Aws.AttributeValue> values)
		{
			this.values = values.ToDictionary(e => e.Key, e => e.Value.ToValue());
		}

		public override DynamoDBValueType Type
		{
			get { return DynamoDBValueType.Map; }
		}

		protected override DynamoDBValue DeepCopy()
		{
			return DeepClone();
		}

		public new DynamoDBMap DeepClone()
		{
			return new DynamoDBMap(values.ToDictionary(e => e.Key, e => e.Value != null ? e.Value.DeepClone() : null));
		}

		internal override Aws.AttributeValue ToAws()
		{
			return new Aws.AttributeValue() { M = ToAwsDictionary() };
		}
		internal Dictionary<string, Aws.AttributeValue> ToAwsDictionary()
		{
			return values.ToDictionary(e => e.Key, e => e.Value == null ? new Aws.AttributeValue() { NULL = true } : e.Value.ToAws());
		}

		public new static DynamoDBMap Convert(object value)
		{
			DynamoDBMap toValue;
			if(DynamoDBValueConverter.Default.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}
		public new static DynamoDBMap Convert(object value, IValueConverter converter)
		{
			DynamoDBMap toValue;
			if(converter.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}

		public IEnumerator<KeyValuePair<string, DynamoDBValue>> GetEnumerator()
		{
			return values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		void ICollection<KeyValuePair<string, DynamoDBValue>>.Add(KeyValuePair<string, DynamoDBValue> item)
		{
			values.Add(item);
		}

		public void Clear()
		{
			values.Clear();
		}

		bool ICollection<KeyValuePair<string, DynamoDBValue>>.Contains(KeyValuePair<string, DynamoDBValue> item)
		{
			return values.Contains(item);
		}

		void ICollection<KeyValuePair<string, DynamoDBValue>>.CopyTo(KeyValuePair<string, DynamoDBValue>[] array, int arrayIndex)
		{
			values.CopyTo(array, arrayIndex);
		}

		bool ICollection<KeyValuePair<string, DynamoDBValue>>.Remove(KeyValuePair<string, DynamoDBValue> item)
		{
			return values.Remove(item);
		}

		public int Count { get { return values.Count; } }
		bool ICollection<KeyValuePair<string, DynamoDBValue>>.IsReadOnly { get { return values.IsReadOnly; } }

		public bool ContainsKey(string key)
		{
			return values.ContainsKey(key);
		}

		void IDictionary<string, DynamoDBValue>.Add(string key, DynamoDBValue value)
		{
			values.Add(key, value);
		}
		public void AddStrict(string key, DynamoDBValue value)
		{
			values.Add(key, value);
		}
		public void Add<TValue>(string key, TValue value)
		{
			values.Add(key, Convert(value));
		}
		public void Add<TValue>(string key, TValue value, IValueConverter converter)
		{
			values.Add(key, Convert(value));
		}

		public bool AddStrictIfNotNull(string key, DynamoDBValue value)
		{
			var notNull = value != null;
			if(notNull)
				values.Add(key, value);
			return notNull;
		}
		public bool AddIfNotNull<TValue>(string key, TValue value)
		{
			return AddStrictIfNotNull(key, Convert(value));
		}
		public bool AddIfNotNull<TValue>(string key, TValue value, IValueConverter converter)
		{
			return AddStrictIfNotNull(key, Convert(value, converter));
		}

		public void SetStrict(string key, DynamoDBValue value)
		{
			values[key] = value;
		}
		public void Set<TValue>(string key, TValue value)
		{
			values[key] = Convert(value);
		}
		public void Set<TValue>(string key, TValue value, IValueConverter converter)
		{
			values[key] = Convert(value, converter);
		}

		public bool SetStrictIfNotNull(string key, DynamoDBValue value)
		{
			var notNull = value != null;
			if(notNull)
				values[key] = value;
			return notNull;
		}
		public bool SetIfNotNull<TValue>(string key, TValue value)
		{
			return SetStrictIfNotNull(key, Convert(value));
		}
		public bool SetIfNotNull<TValue>(string key, TValue value, IValueConverter converter)
		{
			return SetStrictIfNotNull(key, Convert(value, converter));
		}

		public bool Remove(string key)
		{
			return values.Remove(key);
		}

		public bool TryGetValue(string key, out DynamoDBValue value)
		{
			return values.TryGetValue(key, out value);
		}
		public DynamoDBValue TryGetValue(string key)
		{
			DynamoDBValue value;
			values.TryGetValue(key, out value);
			return value;
		}

		public DynamoDBValue this[string key]
		{
			get { return values[key]; }
			set { values[key] = value; }
		}

		public ICollection<string> Keys { get { return values.Keys; } }
		IEnumerable<string> IReadOnlyDictionary<string, DynamoDBValue>.Keys { get { return values.Keys; } }
		public ICollection<DynamoDBValue> Values { get { return values.Values; } }
		IEnumerable<DynamoDBValue> IReadOnlyDictionary<string, DynamoDBValue>.Values { get { return values.Values; } }
	}
}
