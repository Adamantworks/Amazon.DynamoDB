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

using Adamantworks.Amazon.DynamoDB.Converters;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;

namespace Adamantworks.Amazon.DynamoDB.Syntax.Delete
{
	public partial interface ITryDeleteItemSyntax
	{
		bool Item(DynamoDBKeyValue hashKey);
		bool Item(DynamoDBKeyValue hashKey, IValueConverter converter);
		bool Item(object hashKey);
		bool Item(object hashKey, IValueConverter converter);
		bool Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey);
		bool Item(DynamoDBKeyValue hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		bool Item(object hashKey, DynamoDBKeyValue rangeKey);
		bool Item(object hashKey, DynamoDBKeyValue rangeKey, IValueConverter converter);
		bool Item(DynamoDBKeyValue hashKey, object rangeKey);
		bool Item(DynamoDBKeyValue hashKey, object rangeKey, IValueConverter converter);
		bool Item(object hashKey, object rangeKey);
		bool Item(object hashKey, object rangeKey, IValueConverter converter);
	}
}