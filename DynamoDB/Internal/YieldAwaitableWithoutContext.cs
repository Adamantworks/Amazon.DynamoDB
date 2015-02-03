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
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	/// <summary>
	/// Task.Yield() does not support .ConfigureAwait(false).  Thus the context is captured and
	/// can cause deadlocks in code mixing async and sync.  This class provides an equivalent to
	/// "await Task.Yield().ConfigureAwait(false)".  Simply do "await new YieldAwaitableWithoutContext()"
	/// </summary>
	internal struct YieldAwaitableWithoutContext
	{
		public YieldAwaiterWithoutContext GetAwaiter()
		{
			return new YieldAwaiterWithoutContext();
		}

		public struct YieldAwaiterWithoutContext : INotifyCompletion
		{
			public bool IsCompleted { get { return false; } }
			public void OnCompleted(Action continuation)
			{
				var scheduler = TaskScheduler.Current;
				if(scheduler == TaskScheduler.Default)
					ThreadPool.QueueUserWorkItem(RunAction, continuation);
				else
					Task.Factory.StartNew(continuation, CancellationToken.None, TaskCreationOptions.PreferFairness, scheduler);
			}

			public void GetResult() { }
			private static void RunAction(object state) { ((Action)state)(); }
		}
	}
}
