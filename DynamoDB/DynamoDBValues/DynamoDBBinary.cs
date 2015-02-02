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
using System.Collections.Immutable;
using System.IO;
using System.Text;
using Adamantworks.Amazon.DynamoDB.Converters;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public sealed class DynamoDBBinary : DynamoDBKeyValue, IEquatable<DynamoDBBinary>
	{
		private readonly ImmutableArray<byte> value;
		private int? hashCode;

		public DynamoDBBinary(ImmutableArray<byte> value)
		{
			if(value == null || value.Length == 0)
				throw new ArgumentException("DynamoDB binary can't be null or empty", "value");

			this.value = value;
		}

		public DynamoDBBinary(byte[] value)
		{
			if(value == null || value.Length == 0)
				throw new ArgumentException("DynamoDB binary can't be null or empty", "value");

			this.value = ImmutableArray.Create(value);
		}

		public override DynamoDBValueType Type
		{
			get { return DynamoDBValueType.Binary; }
		}

		public new DynamoDBBinary DeepClone()
		{
			return this;
		}

		internal override Aws.AttributeValue ToAws()
		{
			return new Aws.AttributeValue() { B = ToMemoryStream() };
		}

		public int Length { get { return value.Length; } }

		public byte[] ToArray()
		{
			var array = new byte[value.Length];
			value.CopyTo(array);
			return array;
		}

		public MemoryStream ToMemoryStream()
		{
			return new MemoryStream(ToArray());
		}

		public override bool Equals(object other)
		{
			return Equals(other as DynamoDBBinary);
		}

		public bool Equals(DynamoDBBinary other)
		{
			if(other == null
				|| value.Length != other.value.Length
				|| GetHashCode() != other.GetHashCode())
				return false;

			var otherValue = other.value; // for performance
			for(var i = 0; i < value.Length; i++) // for performance, don't use linq
				if(value[i] != otherValue[i])
					return false;

			return true;
		}

		private int ComputeHash()
		{
			var s = 314;
			const int t = 159;
			var hash = 0;
			for(var i = 0; i < value.Length; i++) // for performance, don't use linq
			{
				hash = hash * s + value[i];
				s *= t;
			}
			return hash;
		}

		public override int GetHashCode()
		{
			if(hashCode == null)
				hashCode = ComputeHash();
			return hashCode.Value;
		}

		public new static DynamoDBBinary Convert(object value)
		{
			DynamoDBBinary toValue;
			if(DynamoDBValueConverter.Default.TryConvert(value, out toValue))
				return toValue;

			throw ConvertFailed(value, typeof(DynamoDBBinary));
		}
		public new static DynamoDBBinary Convert(object value, IValueConverter converter)
		{
			DynamoDBBinary toValue;
			if(converter.TryConvert(value, out toValue))
				return toValue;

			throw ConvertFailed(value, typeof(DynamoDBBinary));
		}

		public static bool operator ==(DynamoDBBinary a, DynamoDBBinary b)
		{
			return Equals(a, b);
		}

		public static bool operator !=(DynamoDBBinary a, DynamoDBBinary b)
		{
			return !Equals(a, b);
		}

		public static implicit operator ImmutableArray<byte>(DynamoDBBinary value)
		{
			return value == null ? default(ImmutableArray<byte>) : value.value;
		}

		public static implicit operator DynamoDBBinary(ImmutableArray<byte> value)
		{
			return value == null || value.Length == 0 ? null : new DynamoDBBinary(value);
		}

		public static implicit operator byte[](DynamoDBBinary value)
		{
			return value == null ? null : value.ToArray();
		}

		public static implicit operator DynamoDBBinary(byte[] value)
		{
			return value == null || value.Length == 0 ? null : new DynamoDBBinary(value);
		}

		public override string ToString()
		{
			var hex = new StringBuilder(value.Length * 2);
			for(var i = 0; i < value.Length; i++) // for performance, don't use foreach
				hex.AppendFormat("{0:X2}", value[i]);
			return hex.ToString();
		}
	}
}
