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
	public class NullableConverter : IDynamoDBValueConverter
	{
		public bool CanConvert(Type type, IDynamoDBValueConverter context)
		{
			var underlyingType = Nullable.GetUnderlyingType(type);
			if(underlyingType == null) return false;
			return context.CanConvert(underlyingType, context);
		}

		public bool TryConvertFrom(Type type, object fromValue, out DynamoDBValue toValue, IDynamoDBValueConverter context)
		{
			toValue = null;
			var underlyingType = Nullable.GetUnderlyingType(type);
			if(underlyingType == null) return false;
			if(fromValue == null) return context.CanConvert(underlyingType, context);
			return context.TryConvertFrom(underlyingType, fromValue, out toValue, context);
		}

		public bool TryConvertTo(DynamoDBValue fromValue, Type type, out object toValue, IDynamoDBValueConverter context)
		{
			toValue = null;
			var underlyingType = Nullable.GetUnderlyingType(type);
			if(underlyingType == null) return false;
			if(fromValue == null) return context.CanConvert(underlyingType, context);
			return context.TryConvertTo(fromValue, underlyingType, out toValue, context);
		}
	}
}
