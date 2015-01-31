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
using System.IO;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	internal class MemoryStreamBinaryConverter : DynamoDBValueConverter<MemoryStream, DynamoDBBinary>
	{
		public override bool TryConvert(MemoryStream fromValue, out DynamoDBBinary toValue, IDynamoDBValueConverter context)
		{
			toValue = fromValue.ToArray();
			return true;
		}

		public override bool TryConvert(DynamoDBBinary fromValue, out MemoryStream toValue, IDynamoDBValueConverter context)
		{
			toValue = fromValue != null ? fromValue.ToMemoryStream() : null;
			return true;
		}
	}
}
