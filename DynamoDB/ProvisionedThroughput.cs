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

using System;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	public struct ProvisionedThroughput
	{
		private readonly long readCapacityUnits;
		private readonly long writeCapacityUnits;

		public ProvisionedThroughput(long readCapacityUnits, long writeCapacityUnits)
		{
			if(readCapacityUnits < 1)
				throw new ArgumentOutOfRangeException("readCapacityUnits", "Must be >= 1");
			if(writeCapacityUnits < 1)
				throw new ArgumentOutOfRangeException("writeCapacityUnits", "Must be >= 1");
			this.readCapacityUnits = readCapacityUnits;
			this.writeCapacityUnits = writeCapacityUnits;
		}

		public long ReadCapacityUnits { get { return readCapacityUnits; } }
		public long WriteCapacityUnits { get { return writeCapacityUnits; } }

		internal Aws.ProvisionedThroughput ToAws()
		{
			return new Aws.ProvisionedThroughput(readCapacityUnits, writeCapacityUnits);
		}
	}
}
