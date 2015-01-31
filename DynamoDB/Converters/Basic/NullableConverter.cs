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

namespace Adamantworks.Amazon.DynamoDB.Converters.Basic
{
	// TODO spin this off as a different library with IValueConverter?
	public class NullableConverter : IValueConverter
	{
		public bool CanConvert(Type fromType, Type toType, IValueConverter context)
		{
			var underlyingFromType = Nullable.GetUnderlyingType(fromType);
			var underlyingToType = Nullable.GetUnderlyingType(toType);
			return (underlyingFromType != null || underlyingToType != null)
				&& context.CanConvert(underlyingFromType ?? fromType, underlyingToType ?? toType, context);
		}

		public bool TryConvert(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			// Note there is no such thing as a boxed nullable so fromValue and toValue can't be of type Nullable<X>
			var underlyingType = Nullable.GetUnderlyingType(toType);
			if(underlyingType != null)
			{
				if(fromValue == null)
				{
					toValue = null;
					return true;
				}
				return context.TryConvert(fromValue, underlyingType, out toValue, context);
			}

			toValue = null;
			return false;
		}
	}
}
