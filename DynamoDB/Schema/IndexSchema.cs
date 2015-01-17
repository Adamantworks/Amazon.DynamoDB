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
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB.Schema
{
	public class IndexSchema
	{
		public readonly bool IsGlobal;
		public readonly KeySchema Key;
		public readonly IndexProjectionType ProjectionType;
		public readonly IReadOnlyList<string> IncludedNonKeyAttributes;

		public IndexSchema(bool isGlobal, KeySchema key, bool projectAllAttributes = true)
		{
			if(key == null)
				throw new ArgumentNullException("key");

			IsGlobal = isGlobal;
			Key = key;
			ProjectionType = projectAllAttributes ? IndexProjectionType.All : IndexProjectionType.KeysOnly;
			IncludedNonKeyAttributes = new List<string>();
		}

		public IndexSchema(bool isGlobal, KeySchema key, IEnumerable<string> includeAttributes)
		{
			if(key == null)
				throw new ArgumentNullException("key");

			IsGlobal = isGlobal;
			Key = key;
			IncludedNonKeyAttributes = includeAttributes.ToList(); ;
			ProjectionType = IncludedNonKeyAttributes.Count > 0 ? IndexProjectionType.Include : IndexProjectionType.KeysOnly;
		}

		private static readonly Aws.Projection AwsProjectionAll = new Aws.Projection()
		{
			ProjectionType = AwsEnums.ProjectionType.ALL,
		};
		private static readonly Aws.Projection AwsProjectionKeysOnly = new Aws.Projection()
		{
			ProjectionType = AwsEnums.ProjectionType.KEYS_ONLY,
		};

		internal Aws.Projection ToAwsProjection()
		{
			switch(ProjectionType)
			{
				case IndexProjectionType.All:
					return AwsProjectionAll;
				case IndexProjectionType.KeysOnly:
					return AwsProjectionKeysOnly;
				case IndexProjectionType.Include:
					return new Aws.Projection()
					{
						ProjectionType = AwsEnums.ProjectionType.INCLUDE,
						NonKeyAttributes = IncludedNonKeyAttributes.ToList(),
					};
				default:
					throw new NotSupportedException("ProjectionType not supported");
			}
		}
	}
}
