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

using Adamantworks.Amazon.DynamoDB.Internal;
using System;

namespace Adamantworks.Amazon.DynamoDB.Converters
{
	// TODO spin this off as a different library?
	public interface IValueConverter
	{
		bool CanConvert(Type fromType, Type toType, IValueConverter context);
		bool TryConvert(object fromValue, Type toType, out object toValue, IValueConverter context);
	}

	public abstract class ValueConverter<TValue> : IValueConverter
	{
		public bool CanConvert(Type fromType, Type toType, IValueConverter context)
		{
			return (typeof(TValue).IsAssignableFrom(fromType) && CanConvertTo(toType, context))
				|| (toType.IsAssignableFrom(typeof(TValue)) && CanConvertFrom(fromType, context));
		}

		public abstract bool CanConvertTo(Type toType, IValueConverter context);
		public abstract bool CanConvertFrom(Type fromType, IValueConverter context);

		bool IValueConverter.TryConvert(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			return TryConvertTo(fromValue, toType, out toValue, context)
				   || TryConvertFrom(fromValue, toType, out toValue, context);
		}

		private bool TryConvertTo(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			if(toType.IsAssignableFrom(typeof(TValue)))
			{
				TValue value;
				var converted = TryConvert(fromValue, out value, context);
				toValue = value;
				return converted;
			}
			toValue = null;
			return false;
		}
		private bool TryConvertFrom(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			if(typeof(TValue).IsAssignableFrom(fromValue))
				return TryConvert((TValue)fromValue, toType, out toValue, context);

			toValue = null;
			return false;
		}

		public abstract bool TryConvert(object fromValue, out TValue toValue, IValueConverter context);
		public abstract bool TryConvert(TValue fromValue, Type toType, out object toValue, IValueConverter context);
	}

	public abstract class ValueConverter<TValue1, TValue2> : IValueConverter
	{
		public bool CanConvert(Type fromType, Type toType, IValueConverter context)
		{
			return (typeof(TValue1).IsAssignableFrom(fromType) && toType.IsAssignableFrom(typeof(TValue2)))
				|| (typeof(TValue2).IsAssignableFrom(fromType) && toType.IsAssignableFrom(typeof(TValue1)));
		}

		bool IValueConverter.TryConvert(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			return TryConvertForward(fromValue, toType, out toValue, context) || TryConvertBackward(fromValue, toType, out toValue, context);
		}

		private bool TryConvertForward(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			if(typeof(TValue1).IsAssignableFrom(fromValue) && toType.IsAssignableFrom(typeof(TValue2)))
			{
				TValue2 value;
				var converted = TryConvert((TValue1)fromValue, out value, context);
				toValue = value;
				return converted;
			}
			toValue = null;
			return false;
		}
		private bool TryConvertBackward(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			if(typeof(TValue2).IsAssignableFrom(fromValue) && toType.IsAssignableFrom(typeof(TValue1)))
			{
				TValue1 value;
				var converted = TryConvert((TValue2)fromValue, out value, context);
				toValue = value;
				return converted;
			}
			toValue = null;
			return false;
		}

		public abstract bool TryConvert(TValue1 fromValue, out TValue2 toValue, IValueConverter context);
		public abstract bool TryConvert(TValue2 fromValue, out TValue1 toValue, IValueConverter context);
	}
}
