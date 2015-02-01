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
	public sealed class DynamoDBSet<T> : DynamoDBValue, ISet<T>, IDynamoDBSet
		where T : DynamoDBKeyValue
	{
		private readonly ISet<T> values;
		private readonly DynamoDBValueType type;

		public DynamoDBSet()
		{
			values = new HashSet<T>();
		}
		public DynamoDBSet(IEnumerable<T> values)
		{
			this.values = new HashSet<T>(values);
			var valueType = typeof(T);
			if(valueType == typeof(DynamoDBString))
				type = DynamoDBValueType.StringSet;
			else if(valueType == typeof(DynamoDBNumber))
				type = DynamoDBValueType.NumberSet;
			else if(valueType == typeof(DynamoDBBinary))
				type = DynamoDBValueType.BinarySet;
			else
				throw new InvalidOperationException("Can't create DynamoDBSet of type " + valueType.FullName);
		}

		public override DynamoDBValueType Type
		{
			get { return type; }
		}

		protected override DynamoDBValue DeepCopy()
		{
			return new DynamoDBSet<T>(values.Select(v => (T)v.DeepClone()));
		}

		internal override Aws.AttributeValue ToAws()
		{
			if(values.Count == 0)
				throw new Exception("Set must contain a value");

			switch(type)
			{
				case DynamoDBValueType.StringSet:
					return new Aws.AttributeValue() { SS = values.Select(v => v.ToString()).ToList() };
				case DynamoDBValueType.NumberSet:
					return new Aws.AttributeValue() { NS = values.Select(v => v.ToString()).ToList() };
				case DynamoDBValueType.BinarySet:
					return new Aws.AttributeValue() { BS = values.Select(v => ((DynamoDBBinary)(DynamoDBKeyValue)v).ToMemoryStream()).ToList() };
				default:
					throw new NotSupportedException("Set type not supported");
			}
		}

		public new static DynamoDBSet<T> Convert(object value)
		{
			DynamoDBSet<T> toValue;
			if(DynamoDBValueConverter.Default.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}
		public new static DynamoDBSet<T> Convert(object value, IValueConverter converter)
		{
			DynamoDBSet<T> toValue;
			if(converter.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)values).GetEnumerator();
		}

		void ICollection<T>.Add(T item)
		{
			((ICollection<T>)values).Add(item);
		}

		public void UnionWith(IEnumerable<T> other)
		{
			values.UnionWith(other);
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			values.IntersectWith(other);
		}

		public void ExceptWith(IEnumerable<T> other)
		{
			values.ExceptWith(other);
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			values.SymmetricExceptWith(other);
		}

		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return values.IsSubsetOf(other);
		}

		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return values.IsSupersetOf(other);
		}

		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return values.IsProperSupersetOf(other);
		}

		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return values.IsProperSubsetOf(other);
		}

		public bool Overlaps(IEnumerable<T> other)
		{
			return values.Overlaps(other);
		}

		public bool SetEquals(IEnumerable<T> other)
		{
			return values.SetEquals(other);
		}

		public bool Add(T item)
		{
			return values.Add(item);
		}

		public void Clear()
		{
			values.Clear();
		}

		public bool Contains(T item)
		{
			return values.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			values.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return values.Remove(item);
		}

		public int Count { get { return values.Count; } }
		public bool IsReadOnly { get { return values.IsReadOnly; } }
	}
}
