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
using System.Collections.Generic;
using Adamantworks.Amazon.DynamoDB.DynamoDBValues;
using Adamantworks.Amazon.DynamoDB.Internal;
using Adamantworks.Amazon.DynamoDB.Schema;
using Aws = Amazon.DynamoDBv2.Model;

namespace Adamantworks.Amazon.DynamoDB
{
	/// <summary>
	/// This is the last evaluated key of a scan or query.  The last key differs from an
	/// item key in that for indexes it will contain the key or the index as well as the table.
	///
	/// Note that for local indexes the IndexHashKey must be null because it is the same
	/// as the TableHashKey.
	/// </summary>
	public struct LastKey : IEquatable<LastKey>
	{
		// Note: this class exists because for indexes the LastEvaluatedKey can't be just an ItemKey.
		// We could have used a DynamoDBMap for this. However, the LastKey struct discourages users
		// from thinking that they can simply use the last item of a previous query and hopefully
		// steers them toward actually using the key returned by the last page.

		public readonly DynamoDBKeyValue TableHashKey;
		public readonly DynamoDBKeyValue TableRangeKey;
		public readonly DynamoDBKeyValue IndexHashKey;
		public readonly DynamoDBKeyValue IndexRangeKey;

		public LastKey(ItemKey key)
		{
			TableHashKey = key.HashKey;
			TableRangeKey = key.RangeKey;
			IndexHashKey = null;
			IndexRangeKey = null;
		}

		public LastKey(DynamoDBKeyValue tableHashKey)
			: this(tableHashKey, null, null, null)
		{
		}

		public LastKey(DynamoDBKeyValue tableHashKey, DynamoDBKeyValue tableRangeKey)
			: this(tableHashKey, tableRangeKey, null, null)
		{
		}

		public LastKey(DynamoDBKeyValue tableHashKey, DynamoDBKeyValue tableRangeKey, DynamoDBKeyValue indexHashKey, DynamoDBKeyValue indexRangeKey)
		{
			TableHashKey = tableHashKey;
			TableRangeKey = tableRangeKey;
			IndexHashKey = indexHashKey;
			IndexRangeKey = indexRangeKey;
		}

		public Dictionary<string, Aws.AttributeValue> ToAws(KeySchema tableKeySchema, KeySchema indexKeySchema)
		{
			var key = new Dictionary<string, Aws.AttributeValue>();
			key.AddKey(tableKeySchema.HashKey, TableHashKey);
			key.AddKey(tableKeySchema.RangeKey, TableRangeKey);
			key.AddKey(indexKeySchema == null ? null : indexKeySchema.HashKey, IndexHashKey, tableKeySchema);
			key.AddKey(indexKeySchema == null ? null : indexKeySchema.RangeKey, IndexRangeKey, tableKeySchema);
			return key;
		}

		public override bool Equals(object obj)
		{
			var itemKey = obj as LastKey?;
			return itemKey != null && Equals(itemKey.Value);
		}

		public bool Equals(LastKey other)
		{
			return TableHashKey.Equals(other.TableHashKey)
				&& Equals(TableRangeKey, other.TableRangeKey)
				&& Equals(IndexHashKey, other.IndexHashKey)
				&& Equals(IndexRangeKey, other.IndexRangeKey);
		}

		public override int GetHashCode()
		{
			var hash = TableHashKey.GetHashCode();
			if(TableRangeKey != null)
				hash ^= TableRangeKey.GetHashCode();
			if(IndexHashKey != null)
				hash ^= IndexHashKey.GetHashCode();
			if(IndexRangeKey != null)
				hash ^= IndexRangeKey.GetHashCode();
			return hash;
		}
	}
}
