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
using System.Collections.Immutable;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.Schema;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public abstract class DynamoDBKeyValue : DynamoDBScalar, IEquatable<DynamoDBKeyValue>
	{
		public abstract override bool Equals(object other);

		public bool Equals(DynamoDBKeyValue other)
		{
			// unfortunately, there isn't a better way to implement this
			return Equals((object)other);
		}

		public static bool operator ==(DynamoDBKeyValue a, DynamoDBKeyValue b)
		{
			return Equals(a, b);
		}

		public static bool operator !=(DynamoDBKeyValue a, DynamoDBKeyValue b)
		{
			return !Equals(a, b);
		}

		public abstract override int GetHashCode();

		public new static DynamoDBKeyValue Convert(object value)
		{
			DynamoDBKeyValue toValue;
			if(DynamoDBValueConverters.Default.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}
		public new static DynamoDBKeyValue Convert(object value, IValueConverter converter)
		{
			DynamoDBKeyValue toValue;
			if(converter.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}

		#region conversions
		public static explicit operator ImmutableArray<byte>(DynamoDBKeyValue value)
		{
			if(value == null) return default(ImmutableArray<byte>);
			var binaryValue = value as DynamoDBBinary;
			if(binaryValue == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to ImmutableArray<byte>", value.GetType().Name));

			return binaryValue;
		}
		public static implicit operator DynamoDBKeyValue(ImmutableArray<byte> value)
		{
			return (DynamoDBBinary)value;
		}
		public static explicit operator byte[](DynamoDBKeyValue value)
		{
			if(value == null) return null;
			var binaryValue = value as DynamoDBBinary;
			if(binaryValue == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to ImmutableArray<byte>", value.GetType().Name));

			return binaryValue;
		}
		public static implicit operator DynamoDBKeyValue(byte[] value)
		{
			return (DynamoDBBinary)value;
		}

		public static explicit operator string(DynamoDBKeyValue value)
		{
			if(value == null) return null;
			var stringValue = value as DynamoDBString;
			if(stringValue == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to string", value.GetType().Name));

			return stringValue;
		}
		public static implicit operator DynamoDBKeyValue(string value)
		{
			return (DynamoDBString)value;
		}

		public static implicit operator DynamoDBKeyValue(byte value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator byte(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to Byte", value == null ? "null" : value.GetType().Name));

			return (byte)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBKeyValue(sbyte value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator sbyte(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to SByte", value == null ? "null" : value.GetType().Name));

			return (sbyte)number;
		}


		public static implicit operator DynamoDBKeyValue(short value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator short(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to Int16", value == null ? "null" : value.GetType().Name));

			return (short)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBKeyValue(ushort value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator ushort(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to UInt16", value == null ? "null" : value.GetType().Name));

			return (ushort)number;
		}

		public static implicit operator DynamoDBKeyValue(int value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator int(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to Int32", value == null ? "null" : value.GetType().Name));

			return (int)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBKeyValue(uint value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator uint(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to UInt32", value == null ? "null" : value.GetType().Name));

			return (uint)number;
		}

		public static implicit operator DynamoDBKeyValue(long value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator long(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to Int64", value == null ? "null" : value.GetType().Name));

			return (long)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBKeyValue(ulong value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator ulong(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to UInt64", value == null ? "null" : value.GetType().Name));

			return (ulong)number;
		}

		public static implicit operator DynamoDBKeyValue(float value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator float(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to Float", value == null ? "null" : value.GetType().Name));

			return (float)number;
		}

		public static implicit operator DynamoDBKeyValue(double value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator double(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to Double", value == null ? "null" : value.GetType().Name));

			return (double)number;
		}

		public static implicit operator DynamoDBKeyValue(decimal value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator decimal(DynamoDBKeyValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(String.Format("Can't cast {0} to Decimal", value == null ? "null" : value.GetType().Name));

			return (decimal)number;
		}
		#endregion

		// TODO move this to AttributeSchema
		internal Dictionary<string, Aws.Condition> ToAws(AttributeSchema hashKeySchema)
		{
			if(Type != hashKeySchema.Type)
				throw new InvalidOperationException(String.Format("Can't provide {0} value for key {1} of type {2}", Type, hashKeySchema.Name, hashKeySchema.Type));

			return new Dictionary<string, Aws.Condition>() { { hashKeySchema.Name, new Aws.Condition() { ComparisonOperator = "EQ", AttributeValueList = new List<Aws.AttributeValue>() { ToAws() } } } };
		}

		// TODO move this to AttributeSchema
		internal void ToAws(AttributeSchema rangeKeySchema, Dictionary<string, Aws.Condition> keyConditions, string comparisonOperator)
		{
			if(rangeKeySchema == null)
				throw new NotSupportedException("Can't specify range key condition for table or index without a range key");
			if(Type != rangeKeySchema.Type)
				throw new InvalidOperationException(String.Format("Can't provide {0} value for key {1} of type {2}", Type, rangeKeySchema.Name, rangeKeySchema.Type));

			keyConditions.Add(rangeKeySchema.Name, new Aws.Condition() { ComparisonOperator = comparisonOperator, AttributeValueList = new List<Aws.AttributeValue>() { ToAws() } });
		}
	}
}
