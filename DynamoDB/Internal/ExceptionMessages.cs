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

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	internal class ExceptionMessages
	{
		public const string InvalidCast = "Unable to convert object of type '{0}' to type '{1}'.";
		public const string InvalidCastOfNull = "Unable to convert value null to type '{0}'.";
		public const string BatchComplete = "Batch is complete, no operations can be performed on it";
	}
}
