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
using System.Collections.Generic;
using Adamantworks.Amazon.DynamoDB.Internal;

namespace Adamantworks.Amazon.DynamoDB
{
	public class ProjectionExpression : IAwsAttributeNameProvider
	{
		private readonly Dictionary<string, string> names;

		public ProjectionExpression(string expression)
		{
			Expression = expression;
			names = new Dictionary<string, string>();
		}
		public ProjectionExpression(string expression, IEnumerable<KeyValuePair<string, string>> names)
		{
			Expression = expression;
			this.names = AwsAttributeNames.Build(names);
		}
		public ProjectionExpression(string expression, params string[] names)
		{
			Expression = expression;
			this.names = AwsAttributeNames.Build(names);
		}
		public ProjectionExpression(string expression, object names)
		{
			Expression = expression;
			this.names = AwsAttributeNames.Build(names);
		}

		public string Expression { get; private set; }
		public IReadOnlyDictionary<string, string> Names { get { return names; } }

		Dictionary<string, string> IAwsAttributeNameProvider.GetAwsAttributeNames()
		{
			return names;
		}
	}
}
