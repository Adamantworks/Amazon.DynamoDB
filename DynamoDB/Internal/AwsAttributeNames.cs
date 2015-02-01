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
using System.ComponentModel;
using System.Linq;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	internal static class AwsAttributeNames
	{
		public static Dictionary<string, string> Build(IEnumerable<KeyValuePair<string, string>> attributeNames)
		{
			return attributeNames.ToDictionary(e => BuildName(e.Key), e => e.Value);
		}
		public static Dictionary<string, string> Build(string[] attributeNames)
		{
			if(attributeNames.Length % 2 != 0)
				throw new ArgumentException("Each attribute name must have a value");

			var dictionary = new Dictionary<string, string>();
			for(var i = 1; i < attributeNames.Length; i += 2)
				dictionary.Add(BuildName(attributeNames[i - 1]), attributeNames[i]);

			return dictionary;
		}
		public static Dictionary<string, string> Build(object attributeNames)
		{
			var dictionary = new Dictionary<string, string>();
			foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(attributeNames))
			{
				var value = descriptor.GetValue(attributeNames);
				dictionary.Add(BuildName(descriptor.Name), (string)value);
			}
			return dictionary;
		}

		private static string BuildName(string name)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Empty or null attribute name");
			if(name.StartsWith("#"))
				throw new ArgumentException("Do not prefix attribute names with # when declaring them,  (only when using them)");

			return "#" + name;
		}

		public static Dictionary<string, string> Get(IAwsAttributeNameProvider attributeNameProvider)
		{
			return attributeNameProvider.GetAwsAttributeNames();
		}

		public static Dictionary<string, string> GetCombined(params IAwsAttributeNameProvider[] nameProviders)
		{
			Dictionary<string, string> existingNames = null, combinedNames = null;
			foreach(var nameProvider in nameProviders.Where(nameProvider => nameProvider != null))
			{
				var names = nameProvider.GetAwsAttributeNames();
				if(names == null || names.Count == 0)
					continue;

				if(existingNames == null)
				{
					existingNames = names;
					continue;
				}
				if(combinedNames == null)
					combinedNames = new Dictionary<string, string>(existingNames);

				// Merge names
				foreach(var name in names)
				{
					string existingValue;
					if(!combinedNames.TryGetValue(name.Key, out existingValue))
						combinedNames.Add(name.Key, name.Value);
					else if(existingValue != name.Value)
						throw new Exception(string.Format("Two values provided for attribute name " + name.Key));
				}
			}

			return combinedNames ?? existingNames;
		}
	}
}
