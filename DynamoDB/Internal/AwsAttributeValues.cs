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
using System.Collections.Generic;
using System.Linq;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	internal static class AwsAttributeValues
	{
		public static Dictionary<string, Aws.AttributeValue> GetCombined(params IAwsAttributeValuesProvider[] valuesProviders)
		{
			Dictionary<string, Aws.AttributeValue> combinedValues = null;
			foreach(var valuesProvider in valuesProviders.Where(valuesProvider => valuesProvider != null))
			{
				var values = valuesProvider.ToAwsAttributeValues();
				if(values == null || values.Count == 0)
					continue;

				if(combinedValues == null)
				{
					combinedValues = values;
					continue;
				}

				// Merge values
				foreach(var value in values)
				{
					Aws.AttributeValue existingValue;
					if(!combinedValues.TryGetValue(value.Key, out existingValue))
						combinedValues.Add(value.Key, value.Value);
					else
						throw new Exception(String.Format("Two values provided for attribute value " + value.Key));
				}
			}

			return combinedValues;
		}
	}
}
