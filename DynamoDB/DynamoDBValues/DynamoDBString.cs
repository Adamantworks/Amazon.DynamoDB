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
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public sealed class DynamoDBString : DynamoDBKeyValue, IEquatable<DynamoDBString>
	{
		private readonly string value;

		public DynamoDBString(string value)
		{
			if(string.IsNullOrEmpty(value))
				throw new ArgumentException("DynamoDB strings can't be null or empty", "value");

			this.value = value;
		}

		public override DynamoDBValueType Type
		{
			get { return DynamoDBValueType.String; }
		}

		public new DynamoDBString DeepClone()
		{
			return this;
		}

		internal override Aws.AttributeValue ToAws()
		{
			return new Aws.AttributeValue() { S = value };
		}

		public override bool Equals(object other)
		{
			return Equals(other as DynamoDBString);
		}

		public bool Equals(DynamoDBString other)
		{
			return other != null && string.Equals(value, other.value);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public static bool operator ==(DynamoDBString a, DynamoDBString b)
		{
			return Equals(a, b);
		}

		public static bool operator !=(DynamoDBString a, DynamoDBString b)
		{
			return !Equals(a, b);
		}

		public static implicit operator string(DynamoDBString value)
		{
			return value == null ? null : value.value;
		}

		public static implicit operator DynamoDBString(string value)
		{
			return string.IsNullOrEmpty(value) ? null : new DynamoDBString(value);
		}

		public override string ToString()
		{
			return value;
		}
	}
}
