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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	// TODO this functionality should be in Ix
	internal static class AsyncEnumerableEx
	{
		public static IAsyncEnumerable<TResult> GenerateChunked<TState, TResult>(
			TState initialState,
			Func<TState, CancellationToken, Task<TState>> getMore,
			Func<TState, IEnumerable<TResult>> getResults,
			Func<TState, bool> isComplete,
			ReadAhead readAhead = ReadAhead.None)
		{
			return new ChunkedAsyncEnumerable<TState, TResult>(initialState, getMore, getResults, isComplete, readAhead);
		}

		private class ChunkedAsyncEnumerable<TState, TResult> : IAsyncEnumerable<TResult>
		{
			private readonly TState initialState;
			internal readonly Func<TState, CancellationToken, Task<TState>> GetMore;
			internal readonly Func<TState, IEnumerable<TResult>> GetResults;
			internal readonly Func<TState, bool> IsComplete;
			internal readonly ReadAhead ReadAhead;

			public ChunkedAsyncEnumerable(
				TState initialState,
				Func<TState, CancellationToken, Task<TState>> getMore,
				Func<TState, IEnumerable<TResult>> getResults,
				Func<TState, bool> isComplete,
				ReadAhead readAhead)
			{
				this.initialState = initialState;
				this.IsComplete = isComplete;
				this.GetMore = getMore;
				this.GetResults = getResults;
				this.ReadAhead = readAhead;
			}

			public IAsyncEnumerator<TResult> GetEnumerator()
			{
				return new ChunkedAsyncEnumerator<TState, TResult>(this, initialState);
			}
		}

		private class ChunkedAsyncEnumerator<TState, TResult> : IAsyncEnumerator<TResult>
		{
			private readonly ChunkedAsyncEnumerable<TState, TResult> enumerable;
			private readonly ConcurrentQueue<Task<TState>> chunks = new ConcurrentQueue<Task<TState>>();
			private readonly Queue<TResult> results = new Queue<TResult>();
			private CancellationTokenSource cts = new CancellationTokenSource();
			private TState lastState;
			private TResult current;
			private bool complete;

			public ChunkedAsyncEnumerator(ChunkedAsyncEnumerable<TState, TResult> enumerable, TState initialState)
			{
				this.enumerable = enumerable;
				lastState = initialState;
				if(enumerable.ReadAhead != ReadAhead.None)
					chunks.Enqueue(NextChunk(initialState));
			}

			private async Task<TState> NextChunk(TState state, CancellationToken? cancellationToken = null)
			{
				await Task.Yield(); // We want to return the task immediately, so as not to delay the operation that triggered getting the next chunk
				var nextState = await enumerable.GetMore(state, cancellationToken ?? cts.Token).ConfigureAwait(false);
				if(enumerable.ReadAhead == ReadAhead.All && !enumerable.IsComplete(nextState))
					chunks.Enqueue(NextChunk(state)); // This is a read ahead, so it shouldn't be tied to our token

				return nextState;
			}

			public Task<bool> MoveNext(CancellationToken cancellationToken)
			{
				cancellationToken.ThrowIfCancellationRequested();

				if(results.Count > 0)
				{
					current = results.Dequeue();
					return TaskConstants.True;
				}
				return complete ? TaskConstants.False : MoveNextAsync(cancellationToken);
			}

			private async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				Task<TState> nextStateTask;
				if(chunks.TryDequeue(out nextStateTask))
					lastState = await nextStateTask.WithCancellation(cancellationToken).ConfigureAwait(false);
				else
					lastState = await NextChunk(lastState, cancellationToken).ConfigureAwait(false);

				complete = enumerable.IsComplete(lastState);
				foreach(var result in enumerable.GetResults(lastState))
					results.Enqueue(result);

				if(!complete && enumerable.ReadAhead == ReadAhead.Some)
					chunks.Enqueue(NextChunk(lastState)); // This is a read ahead, so it shouldn't be tied to our token

				return await MoveNext(cancellationToken).ConfigureAwait(false);
			}

			public TResult Current { get { return current; } }

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if(!disposing) return;

				if(cts != null)
				{
					cts.Cancel(); // Dispose does not cancel
					cts.Dispose();
					cts = null;
				}
			}
		}
	}
}
