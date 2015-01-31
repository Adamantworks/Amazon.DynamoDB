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
using System.Text.RegularExpressions;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Converters.Composite
{
	public class ViaNumberConverter : ValueConverter<DynamoDBString>
	{
		private readonly Regex numberPattern = new Regex(@"^-?\d+(\.\d+)(E[+-]?\d+)?$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

		public override bool CanConvertTo(Type toType, IValueConverter context)
		{
			return context.CanConvert(typeof(DynamoDBNumber), toType, context);
		}

		public override bool CanConvertFrom(Type fromType, IValueConverter context)
		{
			// via converter is only for going from DynamoDBString to something else
			return false;
		}

		public override bool TryConvert(object fromValue, out DynamoDBString toValue, IValueConverter context)
		{
			// via converter is only for going from DynamoDBString to something else
			toValue = null;
			return false;
		}

		public override bool TryConvert(DynamoDBString fromValue, Type toType, out object toValue, IValueConverter context)
		{
			if(fromValue == null || !numberPattern.IsMatch(fromValue))
			{
				toValue = null;
				return false;
			}

			return context.TryConvert(new DynamoDBNumber(fromValue), toType, out toValue, context);
		}
	}
}
