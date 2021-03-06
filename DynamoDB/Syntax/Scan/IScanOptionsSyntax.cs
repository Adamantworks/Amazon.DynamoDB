﻿// Copyright 2015 Adamantworks.  All Rights Reserved.
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
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Syntax.Scan
{
	public partial interface IScanOptionsSyntax
	{
		IAsyncEnumerable<DynamoDBMap> AllAsync(ReadAhead readAhead);
		IEnumerable<DynamoDBMap> All();

		/// <param name="segment">zero based index of the segment to scan</param>
		/// <param name="totalSegments">the total number of segments being scanned i.e. the number of workers</param>
		IAsyncEnumerable<DynamoDBMap> SegmentAsync(int segment, int totalSegments, ReadAhead readAhead);

		/// <param name="segment">zero based index of the segment to scan</param>
		/// <param name="totalSegments">the total number of segments being scanned i.e. the number of workers</param>
		IEnumerable<DynamoDBMap> Segment(int segment, int totalSegments);

		/// <summary>
		/// Scan by making multiple async request in parallel from this machine.  This can help better utilize
		/// read capacity for large tables by splitting reads more evenly across the table.
		/// Note: this method is always ReadAhead.All and doesn't return results in order
		/// </summary>
		/// <param name="totalSegments">the number of parallel requests to make</param>
		IAsyncEnumerable<DynamoDBMap> InParallelAsync(int totalSegments);

		/// <summary>
		/// Scan by making multiple async request in parallel from this machine.  This can help better utilize
		/// read capacity for large tables by splitting reads more evenly across the table.
		/// Note: this method is always ReadAhead.All and doesn't return results in order
		/// </summary>
		IAsyncEnumerable<DynamoDBMap> InParallelAsync();

		/// <summary>
		/// Scan by making multiple async request in parallel from this machine.  This can help better utilize
		/// read capacity for large tables by splitting reads more evenly across the table.
		/// Note: this method is always ReadAhead.All and doesn't return results in order
		/// </summary>
		/// <param name="totalSegments">the number of parallel requests to make</param>
		IEnumerable<DynamoDBMap> InParallel(int totalSegments);

		/// <summary>
		/// Scan by making multiple async request in parallel from this machine.  This can help better utilize
		/// read capacity for large tables by splitting reads more evenly across the table.
		/// Note: this method is always ReadAhead.All and doesn't return results in order
		/// </summary>
		IEnumerable<DynamoDBMap> InParallel();
	}
}
