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
using System.Linq;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Internal;
using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests.Internal
{
	[TestFixture]
	public class AsyncEnumerableExTests
	{
		[Test]
		public void Create()
		{
			var enumerator = new FakeAsyncEnumerator<int>();
			var enumerable = AsyncEnumerableEx.Create(() => enumerator);
			Assert.AreSame(enumerator, enumerable.GetEnumerator());
			enumerator = new FakeAsyncEnumerator<int>();
			Assert.AreSame(enumerator, enumerable.GetEnumerator());
			enumerator = null;
			Assert.IsNull(enumerable.GetEnumerator());
		}

		private static readonly List<int> ExpectedNumbers;
		private static readonly List<int> ExpectedSkipped = new List<int>() { 1, 3, 5, 7, 9 };

		static AsyncEnumerableExTests()
		{
			ExpectedNumbers = new List<int>();
			var state = new List<int>() { 1 };
			while(state.Count <= 10)
			{
				state = state.Select(n => n * 2).Concat(state.Select(n => n * 2 + 1)).ToList();
				ExpectedNumbers.AddRange(state);
			}
		}

		/// <summary>
		/// This is a basic test of the AsyncEnumerableEx.GenerateChunked
		/// </summary>
		private static async Task GenerateChunked_Basic(ReadAhead readAhead)
		{
			var enumerable = AsyncEnumerableEx.GenerateChunked(new List<int>() { 1 }, async (previousState, token) =>
			{
				await Task.Delay(1, token);
				return previousState.Select(n => n * 2).Concat(previousState.Select(n => n * 2 + 1)).ToList();
			},
			state => state,
			state => state.Count > 10,
			readAhead);
			CollectionAssert.AreEqual(ExpectedNumbers, await enumerable.ToList(), "Basic");
		}

		/// <summary>
		/// This tests handling no results in some chunks
		/// </summary>
		/// <param name="readAhead"></param>
		private static async Task GenerateChunked_Skipping(ReadAhead readAhead)
		{
			var enumerable = AsyncEnumerableEx.GenerateChunked(0, async (previousState, token) =>
			{
				await Task.Delay(1, token);
				return previousState + 1;
			},
			state => state % 2 == 0 ? Enumerable.Empty<int>() : Enumerable.Repeat(state, 1),
			state => state >= 10,
			readAhead);
			CollectionAssert.AreEqual(ExpectedSkipped, await enumerable.ToList(), "Skipping");
		}

		/// <summary>
		/// This tests cases where getMore is synchronous
		/// </summary>
		private static async Task GenerateChunked_Sync(ReadAhead readAhead)
		{
			var enumerable = AsyncEnumerableEx.GenerateChunked(new List<int>() { 1 }, async (previousState, token) =>
			{
				return previousState.Select(n => n * 2).Concat(previousState.Select(n => n * 2 + 1)).ToList();
			},
			state => state,
			state => state.Count > 10,
			readAhead);
			CollectionAssert.AreEqual(ExpectedNumbers, await enumerable.ToList(), "Sync");
		}

		[Test]
		public async Task GenerateChunkedReadAheadNone()
		{
			await GenerateChunked_Basic(ReadAhead.None);
			await GenerateChunked_Skipping(ReadAhead.None);
			await GenerateChunked_Sync(ReadAhead.None);
		}

		[Test]
		public async Task GenerateChunkedReadAheadSome()
		{
			await GenerateChunked_Basic(ReadAhead.Some);
			await GenerateChunked_Skipping(ReadAhead.Some);
			await GenerateChunked_Sync(ReadAhead.Some);
		}

		[Test]
		public async Task GenerateChunkedReadAheadAll()
		{
			await GenerateChunked_Basic(ReadAhead.All);
			await GenerateChunked_Skipping(ReadAhead.All);
			await GenerateChunked_Sync(ReadAhead.All);
		}
	}
}
