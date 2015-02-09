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
			ReadAhead readAhead = ReadAhead.None,
			bool captureContext = false)
		{
			return new ChunkedAsyncEnumerable<TState, TResult>(initialState, getMore, getResults, isComplete, readAhead, captureContext);
		}

		private class ChunkedAsyncEnumerable<TState, TResult> : IAsyncEnumerable<TResult>
		{
			private readonly TState initialState;
			internal readonly Func<TState, CancellationToken, Task<TState>> GetMore;
			internal readonly Func<TState, IEnumerable<TResult>> GetResults;
			internal readonly Func<TState, bool> IsComplete;
			internal readonly ReadAhead ReadAhead;
			internal readonly bool CaptureContext;

			public ChunkedAsyncEnumerable(TState initialState, Func<TState, CancellationToken, Task<TState>> getMore, Func<TState, IEnumerable<TResult>> getResults, Func<TState, bool> isComplete, ReadAhead readAhead, bool captureContext)
			{
				this.initialState = initialState;
				IsComplete = isComplete;
				GetMore = getMore;
				GetResults = getResults;
				ReadAhead = readAhead;
				CaptureContext = captureContext;
			}

			public IAsyncEnumerator<TResult> GetEnumerator()
			{
				return new ChunkedAsyncEnumerator<TState, TResult>(this, initialState);
			}
		}

		private class ChunkedAsyncEnumerator<TState, TResult> : IAsyncEnumerator<TResult>
		{
			private readonly Func<TState, CancellationToken, Task<TState>> getMore;
			private readonly Func<TState, IEnumerable<TResult>> getResults;
			private readonly Func<TState, bool> isComplete;
			private readonly ReadAhead readAhead;
			private readonly bool captureContext;
			private readonly ConcurrentQueue<Task<TState>> chunks = new ConcurrentQueue<Task<TState>>();
			private readonly Queue<TResult> results = new Queue<TResult>();
			private CancellationTokenSource cts = new CancellationTokenSource();
			private TState lastState;
			private TResult current;
			private bool complete;

			public ChunkedAsyncEnumerator(ChunkedAsyncEnumerable<TState, TResult> enumerable, TState initialState)
			{
				getMore = enumerable.GetMore;
				getResults = enumerable.GetResults;
				isComplete = enumerable.IsComplete;
				readAhead = enumerable.ReadAhead;
				captureContext = enumerable.CaptureContext;
				lastState = initialState;
				if(enumerable.ReadAhead != ReadAhead.None)
					chunks.Enqueue(NextChunk(initialState));
			}

			private async Task<TState> NextChunk(TState state, CancellationToken? cancellationToken = null)
			{
				var readAheadAll = readAhead == ReadAhead.All;
				var nextState = await getMore(state, cancellationToken ?? cts.Token).ConfigureAwait(captureContext && readAheadAll);
				if(readAheadAll && !isComplete(nextState))
					chunks.Enqueue(NextChunk(nextState)); // This is a read ahead, so it shouldn't be tied to our token

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
					lastState = await nextStateTask.WithCancellation(cancellationToken).ConfigureAwait(captureContext);
				else
					lastState = await NextChunk(lastState, cancellationToken).ConfigureAwait(captureContext);

				complete = isComplete(lastState);
				foreach(var result in getResults(lastState))
					results.Enqueue(result);

				if(!complete && readAhead == ReadAhead.Some)
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
