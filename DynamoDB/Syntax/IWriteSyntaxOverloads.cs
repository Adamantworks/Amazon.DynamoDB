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

using System.Threading;
using System.Threading.Tasks;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Syntax.Delete;
using Adamantworks.Amazon.DynamoDB.Syntax.Update;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public partial interface IWriteSyntax
	{
		Task<DynamoDBMap> PutAsync(DynamoDBMap item);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, bool returnOldItem);
		Task<DynamoDBMap> PutAsync(DynamoDBMap item, CancellationToken cancellationToken);
		DynamoDBMap Put(DynamoDBMap item);

		IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update);
		IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, Values values);
		IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, UpdateReturnValue returnValue);
		IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, Values values, UpdateReturnValue returnValue);
		IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, CancellationToken cancellationToken);
		IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, Values values, CancellationToken cancellationToken);
		IUpdateOnItemAsyncSyntax UpdateAsync(UpdateExpression update, UpdateReturnValue returnValue, CancellationToken cancellationToken);
		IUpdateOnItemSyntax Update(UpdateExpression update);
		IUpdateOnItemSyntax Update(UpdateExpression update, Values values);
		IUpdateOnItemSyntax Update(UpdateExpression update, UpdateReturnValue returnValue);

		IDeleteItemAsyncSyntax DeleteAsync();
		IDeleteItemAsyncSyntax DeleteAsync(bool returnOldItem);
		IDeleteItemAsyncSyntax DeleteAsync(CancellationToken cancellationToken);
		IDeleteItemSyntax Delete();
	}
}