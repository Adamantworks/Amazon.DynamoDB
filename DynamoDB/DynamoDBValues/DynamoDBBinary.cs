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
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public sealed class DynamoDBBinary : DynamoDBKeyValue, IEquatable<DynamoDBBinary>
	{
		private readonly ImmutableArray<byte> value;

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
			return other != null && Equals(value, other.value);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
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
			return value.ToString();
		}
	}
}
