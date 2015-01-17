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

namespace Adamantworks.Amazon.DynamoDB
{
	public interface IProvisionedThroughputInfo
	{
		DateTime LastDecreaseDateTime { get; }
		DateTime LastIncreaseDateTime { get; }
		long NumberOfDecreasesToday { get; }
		long ReadCapacityUnits { get; }
		long WriteCapacityUnits { get; }
	}

	internal class ProvisionedThroughputInfo : IProvisionedThroughputInfo
	{
		private readonly DateTime lastDecreaseDateTime;
		private readonly DateTime lastIncreaseDateTime;
		private readonly long numberOfDecreasesToday;
		private readonly long readCapacityUnits;
		private readonly long writeCapacityUnits;

		public ProvisionedThroughputInfo(DateTime lastDecreaseDateTime, DateTime lastIncreaseDateTime, long numberOfDecreasesToday, long readCapacityUnits, long writeCapacityUnits)
		{
			this.lastDecreaseDateTime = lastDecreaseDateTime;
			this.lastIncreaseDateTime = lastIncreaseDateTime;
			this.numberOfDecreasesToday = numberOfDecreasesToday;
			this.readCapacityUnits = readCapacityUnits;
			this.writeCapacityUnits = writeCapacityUnits;
		}

		public DateTime LastDecreaseDateTime
		{
			get { return lastDecreaseDateTime; }
		}

		public DateTime LastIncreaseDateTime
		{
			get { return lastIncreaseDateTime; }
		}

		public long NumberOfDecreasesToday
		{
			get { return numberOfDecreasesToday; }
		}

		public long ReadCapacityUnits
		{
			get { return readCapacityUnits; }
		}

		public long WriteCapacityUnits
		{
			get { return writeCapacityUnits; }
		}
	}
}
