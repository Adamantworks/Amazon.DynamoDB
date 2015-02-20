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
	/// <summary>
	/// This class is a hack for the priority system.  This conversion is used to given priority to converting
	/// to DynamoDBSet over DynamoDBList when the source type implements ISet
	/// </summary>
	internal class SetPriorityConverter<T> : ValueConverter<DynamoDBSet<T>> where T : DynamoDBKeyValue
	{
		private readonly SetConverter<T> setConverter = new SetConverter<T>();

		public override bool CanConvertTo(Type toType, IValueConverter context)
		{
			return false;
		}

		public override bool CanConvertFrom(Type fromType, IValueConverter context)
		{
			return setConverter.CanConvertFrom(fromType, context);
		}

		public override bool TryConvert(object fromValue, out DynamoDBSet<T> toValue, IValueConverter context)
		{
			return setConverter.TryConvert(fromValue, out toValue, context);
		}

		public override bool TryConvert(DynamoDBSet<T> fromValue, Type toType, out object toValue, IValueConverter context)
		{
			toValue = null;
			return false;
		}
	}
}
