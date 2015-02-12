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

using NUnit.Framework;

namespace Adamantworks.Amazon.DynamoDB.Tests
{
	[TestFixture]
	public class RegionTests
	{
		[Test]
		[TestCase("-", null)]
		[TestCase("---", "-z")]
		[TestCase("hello-", "hellz")]
		[TestCase("hello", "helln")]
		[TestCase(".", "-")]
		[TestCase("0", ".")]
		[TestCase("A", "9")]
		[TestCase("_", "Z")]
		[TestCase("a", "_")]
		public void TableNameBefore(string name, string nameBefore)
		{
			if(nameBefore != null)
				nameBefore = nameBefore.PadRight(255, 'z');
			Assert.AreEqual(nameBefore, Region.TableNameBefore(name));
		}
	}
}
