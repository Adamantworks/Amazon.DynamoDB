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

using System.ComponentModel;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	internal static class UpdateReturnValueExtension
	{
		public static AwsEnums.ReturnValue ToAws(this UpdateReturnValue returnValue)
		{
			switch(returnValue)
			{
				case UpdateReturnValue.None:
					return AwsEnums.ReturnValue.NONE;
				case UpdateReturnValue.AllOld:
					return AwsEnums.ReturnValue.ALL_OLD;
				case UpdateReturnValue.UpdatedOld:
					return AwsEnums.ReturnValue.UPDATED_OLD;
				case UpdateReturnValue.AllNew:
					return AwsEnums.ReturnValue.ALL_NEW;
				case UpdateReturnValue.UpdatedNew:
					return AwsEnums.ReturnValue.UPDATED_NEW;
				default:
					throw new InvalidEnumArgumentException("returnValue", (int)returnValue, typeof(UpdateReturnValue));
			}
		}
	}
}
