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
using System.Globalization;
using Adamantworks.Amazon.DynamoDB.Converters;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public sealed class DynamoDBNumber : DynamoDBKeyValue, IEquatable<DynamoDBNumber>
	{
		private const NumberStyles NumberStyle = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

		private readonly string value;

		internal DynamoDBNumber(string value)
		{
			this.value = value;
		}

		public override DynamoDBValueType Type
		{
			get { return DynamoDBValueType.Number; }
		}

		public new DynamoDBNumber DeepClone()
		{
			return this;
		}

		internal override Aws.AttributeValue ToAws()
		{
			return new Aws.AttributeValue() { N = value };
		}

		public override bool Equals(object other)
		{
			return Equals(other as DynamoDBNumber);
		}

		public bool Equals(DynamoDBNumber other)
		{
			// TODO: could we get two numbers that were equivalent with different string reps?
			return other != null && string.Equals(value, other.value);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public new static DynamoDBNumber Convert(object value)
		{
			DynamoDBNumber toValue;
			if(DynamoDBValueConverters.Default.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}
		public new static DynamoDBNumber Convert(object value, IValueConverter converter)
		{
			DynamoDBNumber toValue;
			if(converter.TryConvert(value, out toValue))
				return toValue;

			throw new InvalidCastException();
		}

		public static bool operator ==(DynamoDBNumber a, DynamoDBNumber b)
		{
			return Equals(a, b);
		}

		public static bool operator !=(DynamoDBNumber a, DynamoDBNumber b)
		{
			return !Equals(a, b);
		}

		public static implicit operator DynamoDBNumber(byte value)
		{
			return new DynamoDBNumber(value.ToString("D", CultureInfo.InvariantCulture));
		}
		public static explicit operator byte(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (byte)decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBNumber(sbyte value)
		{
			return new DynamoDBNumber(value.ToString("D", CultureInfo.InvariantCulture));
		}
		[CLSCompliant(false)]
		public static explicit operator sbyte(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (sbyte)decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}


		public static implicit operator DynamoDBNumber(short value)
		{
			return new DynamoDBNumber(value.ToString("D", CultureInfo.InvariantCulture));
		}
		public static explicit operator short(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (short)decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBNumber(ushort value)
		{
			return new DynamoDBNumber(value.ToString("D", CultureInfo.InvariantCulture));
		}
		[CLSCompliant(false)]
		public static explicit operator ushort(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (ushort)decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		public static implicit operator DynamoDBNumber(int value)
		{
			return new DynamoDBNumber(value.ToString("D", CultureInfo.InvariantCulture));
		}
		public static explicit operator int(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (int)decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBNumber(uint value)
		{
			return new DynamoDBNumber(value.ToString("D", CultureInfo.InvariantCulture));
		}
		[CLSCompliant(false)]
		public static explicit operator uint(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (uint)decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		public static implicit operator DynamoDBNumber(long value)
		{
			return new DynamoDBNumber(value.ToString("D", CultureInfo.InvariantCulture));
		}
		public static explicit operator long(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (long)decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static implicit operator DynamoDBNumber(ulong value)
		{
			return new DynamoDBNumber(value.ToString("D", CultureInfo.InvariantCulture));
		}
		[CLSCompliant(false)]
		public static explicit operator ulong(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (ulong)decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		public static implicit operator DynamoDBNumber(float value)
		{
			return new DynamoDBNumber(value.ToString("R", CultureInfo.InvariantCulture));
		}
		public static explicit operator float(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return (long)double.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		public static implicit operator DynamoDBNumber(double value)
		{
			var stringValue = value.ToString("R", CultureInfo.InvariantCulture);
			if(Environment.Is64BitProcess)
			{
				// There is an issue with 'R' in which sometimes it doesn't round-trip in x64 processes
				// see https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx#RFormatString
				// so we check that it round-trips and if it doesn't, reformat it.

				// ReSharper disable once CompareOfFloatsByEqualityOperator
				if(double.Parse(stringValue) != value) // we want them exactly equal
					stringValue = value.ToString("G17"); // switch to the suggested format that forces full 17 digits of precision
			}
			return new DynamoDBNumber(stringValue);
		}

		public static explicit operator double(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return double.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		public static implicit operator DynamoDBNumber(decimal value)
		{
			// The standard ToString() of decimal is inherently round-trip
			return new DynamoDBNumber(value.ToString(CultureInfo.InvariantCulture));
		}
		public static explicit operator decimal(DynamoDBNumber value)
		{
			if(value == null)
				throw new InvalidOperationException("DynamoDBNumber is null");

			return decimal.Parse(value.value, NumberStyle, CultureInfo.InvariantCulture);
		}

		public override string ToString()
		{
			return value;
		}
	}
}
