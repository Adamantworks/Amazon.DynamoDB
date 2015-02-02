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
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public sealed class DynamoDBList : DynamoDBValue, IList<DynamoDBValue>
	{
		private readonly IList<DynamoDBValue> values;

		public DynamoDBList()
		{
			values = new List<DynamoDBValue>();
		}

		public DynamoDBList(IEnumerable<DynamoDBValue> values)
		{
			this.values = values.ToList();
		}

		public override DynamoDBValueType Type
		{
			get { return DynamoDBValueType.List; }
		}

		protected override DynamoDBValue DeepCopy()
		{
			return new DynamoDBList(values.Select(v => v != null ? v.DeepClone() : null));
		}

		internal override Aws.AttributeValue ToAws()
		{
			return new Aws.AttributeValue() { L = values.Select(v => v == null ? new Aws.AttributeValue() { NULL = true } : v.ToAws()).ToList() };
		}

		public new static DynamoDBList Convert(object value)
		{
			DynamoDBList toValue;
			if(DynamoDBValueConverter.Default.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}
		public new static DynamoDBList Convert(object value, IValueConverter converter)
		{
			DynamoDBList toValue;
			if(converter.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}

		public IEnumerator<DynamoDBValue> GetEnumerator()
		{
			return values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(DynamoDBValue value)
		{
			values.Add(value);
		}
		public void Add(DynamoDBValue value, IValueConverter converter)
		{
			values.Add(value);
		}
		public void Add(object value)
		{
			values.Add(DynamoDBValue.Convert(value));
		}
		public void Add(object value, IValueConverter converter)
		{
			values.Add(DynamoDBValue.Convert(value, converter));
		}

		public void Clear()
		{
			values.Clear();
		}

		public bool Contains(DynamoDBValue item)
		{
			return values.Contains(item);
		}

		public void CopyTo(DynamoDBValue[] array, int arrayIndex)
		{
			values.CopyTo(array, arrayIndex);
		}

		public bool Remove(DynamoDBValue item)
		{
			return values.Remove(item);
		}

		public int Count { get { return values.Count; } }
		public bool IsReadOnly { get { return values.IsReadOnly; } }
		public int IndexOf(DynamoDBValue item)
		{
			return values.IndexOf(item);
		}

		public void Insert(int index, DynamoDBValue value)
		{
			values.Insert(index, value);
		}
		public void Insert(int index, DynamoDBValue value, IValueConverter converter)
		{
			values.Insert(index, value);
		}
		public void Insert(int index, object value)
		{
			values.Insert(index, DynamoDBValue.Convert(value));
		}
		public void Insert(int index, object value, IValueConverter converter)
		{
			values.Insert(index, DynamoDBValue.Convert(value, converter));
		}

		public void RemoveAt(int index)
		{
			values.RemoveAt(index);
		}

		public DynamoDBValue this[int index]
		{
			get { return values[index]; }
			set { values[index] = value; }
		}
	}
}
