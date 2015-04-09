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
using System.ComponentModel;
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
			return Create<TResult>(() =>
			{
				switch(readAhead)
				{
					case ReadAhead.None:
						return new ReadAheadNoneEnumerator<TState, TResult>(initialState, getMore, getResults, isComplete, captureContext);
					case ReadAhead.Some:
						return new ReadAheadSomeEnumerator<TState, TResult>(initialState, getMore, getResults, isComplete, captureContext);
					case ReadAhead.All:
						return new ReadAheadAllEnumerator<TState, TResult>(initialState, getMore, getResults, isComplete, captureContext);
					default:
						throw new InvalidEnumArgumentException("readAhead", (int)readAhead, typeof(ReadAhead));
				}
			});
		}

		public static IAsyncEnumerable<T> Create<T>(Func<IAsyncEnumerator<T>> getEnumerator)
		{
			return new AnonymousAsyncEnumerable<T>(getEnumerator);
		}

		private class AnonymousAsyncEnumerable<T> : IAsyncEnumerable<T>
		{
			private readonly Func<IAsyncEnumerator<T>> getEnumerator;

			public AnonymousAsyncEnumerable(Func<IAsyncEnumerator<T>> getEnumerator)
			{
				this.getEnumerator = getEnumerator;
			}

			public IAsyncEnumerator<T> GetEnumerator()
			{
				return getEnumerator();
			}
		}

		private class ReadAheadNoneEnumerator<TState, TResult> : IAsyncEnumerator<TResult>
		{
			private readonly Func<TState, CancellationToken, Task<TState>> getMore;
			private readonly Func<TState, IEnumerable<TResult>> getResults;
			private readonly Func<TState, bool> isComplete;
			private readonly bool captureContext;
			private readonly Queue<TResult> results = new Queue<TResult>();
			private TState lastState;
			private TResult current;
			private bool complete;

			public ReadAheadNoneEnumerator(
				TState initialState,
				Func<TState, CancellationToken, Task<TState>> getMore,
				Func<TState, IEnumerable<TResult>> getResults,
				Func<TState, bool> isComplete,
				bool captureContext)
			{
				lastState = initialState;
				this.getMore = getMore;
				this.getResults = getResults;
				this.isComplete = isComplete;
				this.captureContext = captureContext;
			}

			public async Task<bool> MoveNext(CancellationToken cancellationToken)
			{
				cancellationToken.ThrowIfCancellationRequested();

				for(; ; ) // Keep getting more until we get some, or it is complete
				{
					if(results.Count > 0)
					{
						current = results.Dequeue();
						return true;
					}
					if(complete)
					{
						current = default(TResult); // Make sure current is cleared out
						return false;
					}

					lastState = await getMore(lastState, cancellationToken).ConfigureAwait(captureContext);
					complete = isComplete(lastState);
					foreach(var result in getResults(lastState))
						results.Enqueue(result);
				}
			}

			public TResult Current
			{
				get { return current; }
			}

			public void Dispose()
			{
				// Nothing to dispose
			}
		}

		private class ReadAheadSomeEnumerator<TState, TResult> : IAsyncEnumerator<TResult>
		{
			private readonly Func<TState, CancellationToken, Task<TState>> getMore;
			private readonly Func<TState, IEnumerable<TResult>> getResults;
			private readonly Func<TState, bool> isComplete;
			private readonly bool captureContext;
			private readonly Queue<TResult> results = new Queue<TResult>();
			private CancellationTokenSource cts = new CancellationTokenSource();
			private Task<TState> nextChunk;
			private TState lastState;
			private TResult current;
			private bool complete;

			public ReadAheadSomeEnumerator(
				TState initialState,
				Func<TState, CancellationToken, Task<TState>> getMore,
				Func<TState, IEnumerable<TResult>> getResults,
				Func<TState, bool> isComplete,
				bool captureContext)
			{
				lastState = initialState;
				this.getMore = getMore;
				this.getResults = getResults;
				this.isComplete = isComplete;
				this.captureContext = captureContext;
				nextChunk = NextChunk(initialState); // Read ahead
			}

			private Task<TState> NextChunk(TState state)
			{
				return getMore(state, cts.Token);
			}

			public async Task<bool> MoveNext(CancellationToken cancellationToken)
			{
				cancellationToken.ThrowIfCancellationRequested();

				for(; ; ) // Keep getting more until we get some, or it is complete
				{
					if(results.Count > 0)
					{
						current = results.Dequeue();
						return true;
					}
					if(complete)
					{
						current = default(TResult); // Make sure current is cleared out
						return false;
					}

					lastState = await nextChunk.WithCancellation(cancellationToken).ConfigureAwait(captureContext);
					complete = isComplete(lastState);
					if(!complete) nextChunk = NextChunk(lastState); // This is a read ahead, so it shouldn't be tied to our token
					foreach(var result in getResults(lastState)) // Ok to do this after NextChunk() becuase the results of it won't be read until later
						results.Enqueue(result);
				}
			}

			public TResult Current { get { return current; } }

			public void Dispose()
			{
				if(cts != null)
				{
					cts.Cancel(); // Dispose does not cancel
					cts.Dispose();
					cts = null;
				}
			}
		}

		private class Result<T>
		{
			public static readonly Result<T> Complete = new Result<T>();

			public readonly T Value;

			public Result(T value)
			{
				Value = value;
			}

			private Result()
			{
				Value = default(T);
			}
		}

		private class ReadAheadAllEnumerator<TState, TResult> : IAsyncEnumerator<TResult>
		{
			private readonly Func<TState, CancellationToken, Task<TState>> getMore;
			private readonly Func<TState, IEnumerable<TResult>> getResults;
			private readonly Func<TState, bool> isComplete;
			private readonly bool captureContext;
			private readonly ConcurrentQueue<Result<TResult>> results = new ConcurrentQueue<Result<TResult>>();
			private CancellationTokenSource cts = new CancellationTokenSource();
			private Task<TState> nextChunk;
			private TResult current;
			private bool complete;

			public ReadAheadAllEnumerator(
				TState initialState,
				Func<TState, CancellationToken, Task<TState>> getMore,
				Func<TState, IEnumerable<TResult>> getResults,
				Func<TState, bool> isComplete,
				bool captureContext)
			{
				this.getMore = getMore;
				this.getResults = getResults;
				this.isComplete = isComplete;
				this.captureContext = captureContext;
				nextChunk = NextChunk(initialState); // Read ahead
			}

			private async Task<TState> NextChunk(TState state)
			{
				var lastState = await getMore(state, cts.Token).ConfigureAwait(captureContext);

				foreach(var result in getResults(lastState))
					results.Enqueue(new Result<TResult>(result));

				if(isComplete(lastState))
					results.Enqueue(Result<TResult>.Complete); // signal complete
				else
					nextChunk = NextChunk(lastState); // to be certain order is preserved, we must do this after enqueuing the results
				return lastState;
			}

			public async Task<bool> MoveNext(CancellationToken cancellationToken)
			{
				if(complete) return false; // complete works differently in the read all case
				cancellationToken.ThrowIfCancellationRequested();

				for(; ; )
				{
					Result<TResult> result;
					if(results.TryDequeue(out result))
					{
						current = result.Value;
						if(result == Result<TResult>.Complete) complete = true;
						return !complete;
					}

					// Wait for a chunk to complete, that should give us more data
					await nextChunk.WithCancellation(cancellationToken).ConfigureAwait(false);
				}
			}

			public TResult Current { get { return current; } }

			public void Dispose()
			{
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
