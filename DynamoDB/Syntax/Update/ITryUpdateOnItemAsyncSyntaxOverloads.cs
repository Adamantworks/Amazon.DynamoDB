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

using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Syntax.Update
{
	public partial interface ITryUpdateOnItemAsyncSyntax
	{
		Task<bool> OnItem(DynamoDBKeyValue hashKey);
		Task<bool> OnItem(DynamoDBKeyValue hashKey, IValueConverter converter);
		Task<bool> OnItem(object hashKey);
		Task<bool> OnItem(object hashKey, IValueConverter converter);
		Task<bool> OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		Task<bool> OnItem(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<bool> OnItem(object hashKey, DynamoDBKeyValue rangeKey);
		Task<bool> OnItem(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		Task<bool> OnItem(DynamoDBKeyValue hashKey, object rangeKey);
		Task<bool> OnItem(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter);
		Task<bool> OnItem(object hashKey, object rangeKey);
		Task<bool> OnItem(object hashKey, object rangeKey, IValueConverter converter);
	}
}