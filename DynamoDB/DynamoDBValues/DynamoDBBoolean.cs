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
using Adamantworks.Amazon.DynamoDB.Converters;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public sealed class DynamoDBBoolean : DynamoDBScalar, IEquatable<DynamoDBBoolean>
	{
		public static readonly DynamoDBBoolean True = new DynamoDBBoolean(true);
		public static readonly DynamoDBBoolean False = new DynamoDBBoolean(false);

		private readonly bool value;

		private DynamoDBBoolean(bool value)
		{
			this.value = value;
		}

		public override DynamoDBValueType Type
		{
			get { return DynamoDBValueType.Boolean; }
		}

		public new DynamoDBBoolean DeepClone()
		{
			return this;
		}

		internal override Aws.AttributeValue ToAws()
		{
			return new Aws.AttributeValue() { BOOL = value };
		}

		public override bool Equals(object other)
		{
			// Reference Equals safe because there are only two boolean objects
			return ReferenceEquals(this, other);
		}

		public bool Equals(DynamoDBBoolean other)
		{
			// Reference Equals safe because there are only two boolean objects
			return ReferenceEquals(this, other);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public new static DynamoDBBoolean Convert(object value)
		{
			DynamoDBBoolean toValue;
			if(DynamoDBValueConverter.Default.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}
		public new static DynamoDBBoolean Convert(object value, IValueConverter converter)
		{
			DynamoDBBoolean toValue;
			if(converter.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}

		public static bool operator ==(DynamoDBBoolean a, DynamoDBBoolean b)
		{
			// Reference Equals safe because there are only two boolean objects
			return ReferenceEquals(a, b);
		}

		public static bool operator !=(DynamoDBBoolean a, DynamoDBBoolean b)
		{
			// Reference Equals safe because there are only two boolean objects
			return !ReferenceEquals(a, b);
		}

		public static implicit operator bool(DynamoDBBoolean value)
		{
			return value.value;
		}

		public static implicit operator DynamoDBBoolean(bool value)
		{
			return value ? True : False;
		}

		public override string ToString()
		{
			return value.ToString();
		}
	}
}
