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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	internal class NumberConverter : ValueConverter<DynamoDBNumber>
	{
		public override bool CanConvertTo(Type toType, IValueConverter context)
		{
			return IsPrimitiveNumeric(toType);
		}

		public override bool CanConvertFrom(Type fromType, IValueConverter context)
		{
			return IsPrimitiveNumeric(fromType);
		}

		private static bool IsPrimitiveNumeric(Type type)
		{
			return type == typeof(byte)
				   || type == typeof(sbyte)
				   || type == typeof(short)
				   || type == typeof(ushort)
				   || type == typeof(int)
				   || type == typeof(uint)
				   || type == typeof(long)
				   || type == typeof(float)
				   || type == typeof(double)
				   || type == typeof(decimal);
		}

		public override bool TryConvert(object fromValue, out DynamoDBNumber toValue, IValueConverter context)
		{
			if(fromValue == null)
			{
				toValue = null;
				return false;
			}

			var type = fromValue.GetType();
			if(type == typeof(byte))
				toValue = (byte)fromValue;
			else if(type == typeof(sbyte))
				toValue = (sbyte)fromValue;
			else if(type == typeof(short))
				toValue = (short)fromValue;
			else if(type == typeof(ushort))
				toValue = (ushort)fromValue;
			else if(type == typeof(int))
				toValue = (int)fromValue;
			else if(type == typeof(uint))
				toValue = (uint)fromValue;
			else if(type == typeof(long))
				toValue = (long)fromValue;
			else if(type == typeof(float))
				toValue = (float)fromValue;
			else if(type == typeof(double))
				toValue = (double)fromValue;
			else if(type == typeof(decimal))
				toValue = (decimal)fromValue;
			else
			{
				toValue = null;
				return false;
			}
			return true;
		}

		public override bool TryConvert(DynamoDBNumber fromValue, Type toType, out object toValue, IValueConverter context)
		{
			if(fromValue == null)
			{
				toValue = null;
				return false;
			}

			if(toType == typeof(byte))
				toValue = (byte)fromValue;
			else if(toType == typeof(sbyte))
				toValue = (sbyte)fromValue;
			else if(toType == typeof(short))
				toValue = (short)fromValue;
			else if(toType == typeof(ushort))
				toValue = (ushort)fromValue;
			else if(toType == typeof(int))
				toValue = (int)fromValue;
			else if(toType == typeof(uint))
				toValue = (uint)fromValue;
			else if(toType == typeof(long))
				toValue = (long)fromValue;
			else if(toType == typeof(float))
				toValue = (float)fromValue;
			else if(toType == typeof(double))
				toValue = (double)fromValue;
			else if(toType == typeof(decimal))
				toValue = (decimal)fromValue;
			else
			{
				toValue = null;
				return false;
			}
			return true;
		}
	}
}
