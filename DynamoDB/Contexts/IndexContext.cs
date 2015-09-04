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
using Adamantworks.Amazon.DynamoDB.Syntax;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal partial class IndexContext : IConsistentQuerySyntax
	{
		private readonly Index index;
		private readonly ProjectionExpression projection;
		private bool? consistentRead;

		public IndexContext(Index index, ProjectionExpression projection)
		{
			this.index = index;
			this.projection = projection;
		}
		public IndexContext(Index index, ProjectionExpression projection, bool consistentRead)
		{
			this.index = index;
			this.projection = projection;
			this.consistentRead = consistentRead;
		}

		public IQuerySyntax Consistent { get { return ConsistentIf(true); } }
		public IQuerySyntax ConsistentIf(bool consistent)
		{
			if(consistentRead != null)
				throw new InvalidOperationException("Can't set Consistent twice");
			consistentRead = consistent;
			return this;
		}
	}
}
