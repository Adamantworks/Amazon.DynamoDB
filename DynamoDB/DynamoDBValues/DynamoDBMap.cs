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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		public bool ContainsKey(params string[] keys)
		{
			return values.ContainsKey(ItemKey.CompositeName(keys));
		}
		public bool ContainsKey(IEnumerable<string> keys)
		{
			return values.ContainsKey(ItemKey.CompositeName(keys));
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
			values.Add(key, value.ToDynamoDBValue());
		}
		public void Add<TValue>(string key, TValue value, IDynamoDBValueConverter converter)
		{
			values.Add(key, value.ToDynamoDBValue());
		}
		public void AddStrict(IEnumerable<string> keys, params DynamoDBKeyValue[] values)
		{
			this.values.Add(ItemKey.CompositeName(keys), ItemKey.CompositeStrict(values));
		}
		public void AddStrict(IEnumerable<string> keys, IEnumerable<DynamoDBKeyValue> values)
		{
			this.values.Add(ItemKey.CompositeName(keys), ItemKey.CompositeStrict(values));
		}
		public void Add<T1, T2>(IEnumerable<string> keys, T1 value1, T2 value2)
		{
			values.Add(ItemKey.CompositeName(keys), ItemKey.Composite(value1, value2));
		}
		public void Add<T1, T2>(IEnumerable<string> keys, T1 value1, T2 value2, IDynamoDBValueConverter converter)
		{
			values.Add(ItemKey.CompositeName(keys), ItemKey.Composite(value1, value2, converter));
		}
		public void Add<T1, T2, T3>(IEnumerable<string> keys, T1 value1, T2 value2, T3 value3)
		{
			values.Add(ItemKey.CompositeName(keys), ItemKey.Composite(value1, value2, value3));
		}
		public void Add<T1, T2, T3>(IEnumerable<string> keys, T1 value1, T2 value2, T3 value3, IDynamoDBValueConverter converter)
		{
			values.Add(ItemKey.CompositeName(keys), ItemKey.Composite(value1, value2, value3, converter));
		}
		public void Add<T1, T2, T3, T4>(IEnumerable<string> keys, T1 value1, T2 value2, T3 value3, T4 value4)
		{
			values.Add(ItemKey.CompositeName(keys), ItemKey.Composite(value1, value2, value3, value4));
		}
		public void Add<T1, T2, T3, T4>(IEnumerable<string> keys, T1 value1, T2 value2, T3 value3, T4 value4, IDynamoDBValueConverter converter)
		{
			values.Add(ItemKey.CompositeName(keys), ItemKey.Composite(value1, value2, value3, value4));
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
			return AddStrictIfNotNull(key, value.ToDynamoDBValue());
		}
		public bool AddIfNotNull<TValue>(string key, TValue value, IDynamoDBValueConverter converter)
		{
			return AddStrictIfNotNull(key, value.ToDynamoDBValue(converter));
		}

		public void SetStrict(string key, DynamoDBValue value)
		{
			values[key] = value;
		}
		public void Set<TValue>(string key, TValue value)
		{
			values[key] = value.ToDynamoDBValue();
		}
		public void Set<TValue>(string key, TValue value, IDynamoDBValueConverter converter)
		{
			values[key] = value.ToDynamoDBValue(converter);
		}
		public void SetStrict(IEnumerable<string> keys, params DynamoDBKeyValue[] values)
		{
			this.values[ItemKey.CompositeName(keys)] = ItemKey.CompositeStrict(values);
		}
		public void SetStrict(IEnumerable<string> keys, IEnumerable<DynamoDBKeyValue> values)
		{
			this.values[ItemKey.CompositeName(keys)] = ItemKey.CompositeStrict(values);
		}
		public void Set<T1, T2>(IEnumerable<string> keys, T1 value1, T2 value2)
		{
			values[ItemKey.CompositeName(keys)] = ItemKey.Composite(value1, value2);
		}
		public void Set<T1, T2>(IEnumerable<string> keys, T1 value1, T2 value2, IDynamoDBValueConverter converter)
		{
			values[ItemKey.CompositeName(keys)] = ItemKey.Composite(value1, value2, converter);
		}
		public void Set<T1, T2, T3>(IEnumerable<string> keys, T1 value1, T2 value2, T3 value3)
		{
			values[ItemKey.CompositeName(keys)] = ItemKey.Composite(value1, value2, value3);
		}
		public void Set<T1, T2, T3>(IEnumerable<string> keys, T1 value1, T2 value2, T3 value3, IDynamoDBValueConverter converter)
		{
			values[ItemKey.CompositeName(keys)] = ItemKey.Composite(value1, value2, value3, converter);
		}
		public void Set<T1, T2, T3, T4>(IEnumerable<string> keys, T1 value1, T2 value2, T3 value3, T4 value4)
		{
			values[ItemKey.CompositeName(keys)] = ItemKey.Composite(value1, value2, value3, value4);
		}
		public void Set<T1, T2, T3, T4>(IEnumerable<string> keys, T1 value1, T2 value2, T3 value3, T4 value4, IDynamoDBValueConverter converter)
		{
			values[ItemKey.CompositeName(keys)] = ItemKey.Composite(value1, value2, value3, value4, converter);
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
			return SetStrictIfNotNull(key, value.ToDynamoDBValue());
		}
		public bool SetIfNotNull<TValue>(string key, TValue value, IDynamoDBValueConverter converter)
		{
			return SetStrictIfNotNull(key, value.ToDynamoDBValue(converter));
		}

		public bool Remove(string key)
		{
			return values.Remove(key);
		}
		public bool Remove(params string[] keys)
		{
			return values.Remove(ItemKey.CompositeName(keys));
		}
		public bool Remove(IEnumerable<string> keys)
		{
			return values.Remove(ItemKey.CompositeName(keys));
		}

		public bool TryGetValue(string key, out DynamoDBValue value)
		{
			return values.TryGetValue(key, out value);
		}
		public bool TryGetValue(IEnumerable<string> keys, out DynamoDBValue value)
		{
			return values.TryGetValue(ItemKey.CompositeName(keys), out value);
		}
		public DynamoDBValue TryGetValue(string key)
		{
			DynamoDBValue value;
			values.TryGetValue(key, out value);
			return value;
		}
		public DynamoDBValue TryGetValue(params string[] keys)
		{
			DynamoDBValue value;
			values.TryGetValue(ItemKey.CompositeName(keys), out value);
			return value;
		}
		public DynamoDBValue TryGetValue(IEnumerable<string> keys)
		{
			DynamoDBValue value;
			values.TryGetValue(ItemKey.CompositeName(keys), out value);
			return value;
		}

		public DynamoDBValue this[string key]
		{
			get { return values[key]; }
			set { values[key] = value; }
		}
		public DynamoDBValue this[params string[] keys]
		{
			get { return values[ItemKey.CompositeName(keys)]; }
			set { values[ItemKey.CompositeName(keys)] = value; }
		}
		public DynamoDBValue this[IEnumerable<string> keys]
		{
			get { return values[ItemKey.CompositeName(keys)]; }
			set { values[ItemKey.CompositeName(keys)] = value; }
		}

		public ICollection<string> Keys { get { return values.Keys; } }
		IEnumerable<string> IReadOnlyDictionary<string, DynamoDBValue>.Keys { get { return values.Keys; } }
		public ICollection<DynamoDBValue> Values { get { return values.Values; } }
		IEnumerable<DynamoDBValue> IReadOnlyDictionary<string, DynamoDBValue>.Values { get { return values.Values; } }
	}
}
