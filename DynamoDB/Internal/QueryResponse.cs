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

using System.Collections.Generic;
using Adamantworks.Amazon.DynamoDB.Schema;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB.Internal
{
	internal class QueryResponse
	{
		public readonly int? Limit;
		public readonly long Count;
		public readonly int CurrentLimit;
		public readonly Dictionary<string, Aws.AttributeValue> LastEvaluatedKey;
		public readonly List<Dictionary<string, Aws.AttributeValue>> Items;

		public QueryResponse(int? limit, LastKey? exclusiveStartKey, KeySchema tableKeySchema, KeySchema indexKeySchema = null)
		{
			Limit = limit;
			CurrentLimit = limit ?? -1;
			if(exclusiveStartKey != null)
				LastEvaluatedKey = exclusiveStartKey.Value.ToAws(tableKeySchema, indexKeySchema);
		}

		private QueryResponse(QueryResponse lastResponse, int itemCount, List<Dictionary<string, Aws.AttributeValue>> items, Dictionary<string, Aws.AttributeValue> lastEvaluatedKey)
		{
			Limit = lastResponse.Limit;
			Items = items;
			Count = lastResponse.Count + itemCount;
			CurrentLimit = (int?)(Limit - Count) ?? -1;
			LastEvaluatedKey = lastEvaluatedKey;
		}

		public QueryResponse(QueryResponse lastResponse, Aws.ScanResponse scanResponse)
			: this(lastResponse, scanResponse.Count, scanResponse.Items, scanResponse.LastEvaluatedKey)
		{
		}

		public QueryResponse(QueryResponse lastResponse, Aws.QueryResponse queryResponse)
			: this(lastResponse, queryResponse.Count, queryResponse.Items, queryResponse.LastEvaluatedKey)
		{
		}

		public bool IsComplete()
		{
			return (Limit != null && Count >= Limit)
				   || LastEvaluatedKey == null || LastEvaluatedKey.Count == 0;
		}

		public LastKey? GetLastEvaluatedKey(KeySchema tableKeySchema, KeySchema indexKeySchema = null)
		{
			return LastEvaluatedKey != null && LastEvaluatedKey.Count != 0
				? LastEvaluatedKey.ToLastKey(tableKeySchema, indexKeySchema)
				: default(LastKey?);
		}
	}
}
