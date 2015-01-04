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
	internal class NumberConverter : IDynamoDBValueConverter
	{
		public bool CanConvert(Type type, IDynamoDBValueConverter context)
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

		public bool TryConvertFrom(Type type, object fromValue, out DynamoDBValue toValue, IDynamoDBValueConverter context)
		{
			if(type == typeof(byte))
				toValue = (DynamoDBNumber)(byte)fromValue;
			else if(type == typeof(sbyte))
				toValue = (DynamoDBNumber)(sbyte)fromValue;
			else if(type == typeof(short))
				toValue = (DynamoDBNumber)(short)fromValue;
			else if(type == typeof(ushort))
				toValue = (DynamoDBNumber)(ushort)fromValue;
			else if(type == typeof(int))
				toValue = (DynamoDBNumber)(int)fromValue;
			else if(type == typeof(uint))
				toValue = (DynamoDBNumber)(uint)fromValue;
			else if(type == typeof(long))
				toValue = (DynamoDBNumber)(long)fromValue;
			else if(type == typeof(float))
				toValue = (DynamoDBNumber)(float)fromValue;
			else if(type == typeof(double))
				toValue = (DynamoDBNumber)(double)fromValue;
			else if(type == typeof(decimal))
				toValue = (DynamoDBNumber)(decimal)fromValue;
			else
			{
				toValue = null;
				return false;
			}
			return true;
		}

		public bool TryConvertTo(DynamoDBValue fromValue, Type type, out object toValue, IDynamoDBValueConverter context)
		{
			var value = fromValue as DynamoDBNumber;
			if(value == null)
			{
				toValue = null;
				return false;
			}

			if(type == typeof(byte))
				toValue = (byte)value;
			else if(type == typeof(sbyte))
				toValue = (sbyte)value;
			else if(type == typeof(short))
				toValue = (short)value;
			else if(type == typeof(ushort))
				toValue = (ushort)value;
			else if(type == typeof(int))
				toValue = (int)value;
			else if(type == typeof(uint))
				toValue = (uint)value;
			else if(type == typeof(long))
				toValue = (long)value;
			else if(type == typeof(float))
				toValue = (float)value;
			else if(type == typeof(double))
				toValue = (double)value;
			else if(type == typeof(decimal))
				toValue = (decimal)value;
			else
			{
				toValue = null;
				return false;
			}
			return true;
		}
	}
}
