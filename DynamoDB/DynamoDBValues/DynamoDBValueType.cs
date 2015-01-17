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
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.DynamoDBValues
{
	public enum DynamoDBValueType
	{
		Number = 0x1,
		String = 0x2,
		Binary = 0x3,
		Boolean = 0x4,
		NumberSet = 0x10 + Number,
		StringSet = 0x10 + String,
		BinarySet = 0x10 + Binary,
		List = 0x20,
		Map = 0x30,
	}

	public static class DynamoDBValueTypeExtensions
	{
		public static bool IsSet(this DynamoDBValueType type)
		{
			switch(type)
			{
				case DynamoDBValueType.NumberSet:
				case DynamoDBValueType.StringSet:
				case DynamoDBValueType.BinarySet:
					return true;
				default:
					return false;
			}
		}

		public static bool IsScalar(this DynamoDBValueType type)
		{
			switch(type)
			{
				case DynamoDBValueType.Number:
				case DynamoDBValueType.String:
				case DynamoDBValueType.Binary:
				case DynamoDBValueType.Boolean:
					return true;
				default:
					return false;
			}
		}

		public static bool IsKeyValue(this DynamoDBValueType type)
		{
			switch(type)
			{
				case DynamoDBValueType.Number:
				case DynamoDBValueType.String:
				case DynamoDBValueType.Binary:
					return true;
				default:
					return false;
			}
		}

		internal static AwsEnums.ScalarAttributeType ToAws(this DynamoDBValueType type)
		{
			switch(type)
			{
				case DynamoDBValueType.Number:
					return AwsEnums.ScalarAttributeType.N;
				case DynamoDBValueType.String:
					return AwsEnums.ScalarAttributeType.S;
				case DynamoDBValueType.Binary:
					return AwsEnums.ScalarAttributeType.B;
				default:
					throw new Exception(string.Format("{0} can't be used as a key type", type));
			}
		}
	}
}
