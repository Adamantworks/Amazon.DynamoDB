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
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	// TODO implement IConvertable?
	// TODO create TypeDescriptors?
	public abstract class DynamoDBValue 
	{
		public abstract DynamoDBValueType Type { get; }

		protected abstract DynamoDBValue DeepCopy();

		public DynamoDBValue DeepClone()
		{
			return DeepCopy();
		}

		internal abstract Aws.AttributeValue ToAws();

		#region conversions
		public static explicit operator bool(DynamoDBValue value)
		{
			var boolValue = value as DynamoDBBoolean;
			if(boolValue == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Boolean", value == null ? "null" : value.GetType().Name));

			return boolValue;
		}
		public static implicit operator DynamoDBValue(bool value)
		{
			return (DynamoDBBoolean)value;
		}

		public static explicit operator ImmutableArray<byte>(DynamoDBValue value)
		{
			if(value == null) return default(ImmutableArray<byte>);
			var binaryValue = value as DynamoDBBinary;
			if(binaryValue == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to ImmutableArray<byte>", value.GetType().Name));

			return binaryValue;
		}
		public static implicit operator DynamoDBValue(ImmutableArray<byte> value)
		{
			return (DynamoDBBinary)value;
		}
		public static explicit operator byte[](DynamoDBValue value)
		{
			if(value == null) return null;
			var binaryValue = value as DynamoDBBinary;
			if(binaryValue == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to ImmutableArray<byte>", value.GetType().Name));

			return binaryValue;
		}
		public static implicit operator DynamoDBValue(byte[] value)
		{
			return (DynamoDBBinary)value;
		}

		public static explicit operator string(DynamoDBValue value)
		{
			if(value == null) return null;
			var stringValue = value as DynamoDBString;
			if(stringValue == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to string", value.GetType().Name));

			return stringValue;
		}
		public static implicit operator DynamoDBValue(string value)
		{
			return (DynamoDBString)value;
		}

		public static implicit operator DynamoDBValue(byte value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator byte(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Byte", value == null ? "null" : value.GetType().Name));

			return (byte)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBValue(sbyte value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator sbyte(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to SByte", value == null ? "null" : value.GetType().Name));

			return (sbyte)number;
		}


		public static implicit operator DynamoDBValue(short value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator short(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Int16", value == null ? "null" : value.GetType().Name));

			return (short)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBValue(ushort value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator ushort(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to UInt16", value == null ? "null" : value.GetType().Name));

			return (ushort)number;
		}

		public static implicit operator DynamoDBValue(int value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator int(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Int32", value == null ? "null" : value.GetType().Name));

			return (int)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBValue(uint value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator uint(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to UInt32", value == null ? "null" : value.GetType().Name));

			return (uint)number;
		}

		public static implicit operator DynamoDBValue(long value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator long(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Int64", value == null ? "null" : value.GetType().Name));

			return (long)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBValue(ulong value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator ulong(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to UInt64", value == null ? "null" : value.GetType().Name));

			return (ulong)number;
		}

		public static implicit operator DynamoDBValue(float value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator float(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Float", value == null ? "null" : value.GetType().Name));

			return (float)number;
		}

		public static implicit operator DynamoDBValue(double value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator double(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Double", value == null ? "null" : value.GetType().Name));

			return (double)number;
		}

		public static implicit operator DynamoDBValue(decimal value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator decimal(DynamoDBValue value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Decimal", value == null ? "null" : value.GetType().Name));

			return (decimal)number;
		}
		#endregion
	}
}
