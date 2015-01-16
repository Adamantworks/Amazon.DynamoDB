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

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public abstract class DynamoDBScalar : DynamoDBValue, IEquatable<DynamoDBScalar>
	{
		protected sealed override DynamoDBValue DeepCopy()
		{
			// Because all scalars are immutable, we are our own DeepCopy()
			return this;
		}

		public new DynamoDBScalar DeepClone()
		{
			return this;
		}

		public abstract override bool Equals(object other);

		public bool Equals(DynamoDBScalar other)
		{
			// unfortunately, there isn't a better way to implement this
			return Equals((object)other);
		}

		public static bool operator ==(DynamoDBScalar a, DynamoDBScalar b)
		{
			return Equals(a, b);
		}

		public static bool operator !=(DynamoDBScalar a, DynamoDBScalar b)
		{
			return !Equals(a, b);
		}

		public abstract override int GetHashCode();

		#region conversions
		public static implicit operator bool(DynamoDBScalar value)
		{
			var boolValue = value as DynamoDBBoolean;
			if(boolValue == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Boolean", value == null ? "null" : value.GetType().Name));

			return boolValue;
		}
		public static implicit operator DynamoDBScalar(bool value)
		{
			return (DynamoDBBoolean)value;
		}

		public static implicit operator ImmutableArray<byte>(DynamoDBScalar value)
		{
			if(value == null) return default(ImmutableArray<byte>);
			var binaryValue = value as DynamoDBBinary;
			if(binaryValue == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to ImmutableArray<byte>", value.GetType().Name));

			return binaryValue;
		}
		public static implicit operator DynamoDBScalar(ImmutableArray<byte> value)
		{
			return (DynamoDBBinary)value;
		}
		public static implicit operator DynamoDBScalar(byte[] value)
		{
			return (DynamoDBBinary)value;
		}

		public static explicit operator string(DynamoDBScalar value)
		{
			if(value == null) return null;
			var stringValue = value as DynamoDBString;
			if(stringValue == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to string", value.GetType().Name));

			return stringValue;
		}
		public static implicit operator DynamoDBScalar(string value)
		{
			return (DynamoDBString)value;
		}

		public static implicit operator DynamoDBScalar(byte value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator byte(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Byte", value == null ? "null" : value.GetType().Name));

			return (byte)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBScalar(sbyte value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator sbyte(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to SByte", value == null ? "null" : value.GetType().Name));

			return (sbyte)number;
		}


		public static implicit operator DynamoDBScalar(short value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator short(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Int16", value == null ? "null" : value.GetType().Name));

			return (short)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBScalar(ushort value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator ushort(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to UInt16", value == null ? "null" : value.GetType().Name));

			return (ushort)number;
		}

		public static implicit operator DynamoDBScalar(int value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator int(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Int32", value == null ? "null" : value.GetType().Name));

			return (int)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBScalar(uint value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator uint(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to UInt32", value == null ? "null" : value.GetType().Name));

			return (uint)number;
		}

		public static implicit operator DynamoDBScalar(long value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator long(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Int64", value == null ? "null" : value.GetType().Name));

			return (long)number;
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBScalar(ulong value)
		{
			return (DynamoDBNumber)value;
		}
		[CLSCompliant(false)]
		public static explicit operator ulong(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to UInt64", value == null ? "null" : value.GetType().Name));

			return (ulong)number;
		}

		public static implicit operator DynamoDBScalar(float value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator float(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Float", value == null ? "null" : value.GetType().Name));

			return (float)number;
		}

		public static implicit operator DynamoDBScalar(double value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator double(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Double", value == null ? "null" : value.GetType().Name));

			return (double)number;
		}

		public static implicit operator DynamoDBScalar(decimal value)
		{
			return (DynamoDBNumber)value;
		}
		public static explicit operator decimal(DynamoDBScalar value)
		{
			var number = value as DynamoDBNumber;
			if(number == null)
				throw new InvalidCastException(string.Format("Can't cast {0} to Decimal", value == null ? "null" : value.GetType().Name));

			return (decimal)number;
		}
		#endregion
	}
}
