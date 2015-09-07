using System;
using System.Collections.Generic;
using System.Linq;

namespace Adamantworks.Amazon.DynamoDB.CodeGen
{
	public static class Params
	{
		public static IEnumerable<IReadOnlyList<Parameter>> GenOverloads(params Parameter[] parameters)
		{
			return GenOverloads(false, Enumerable.Empty<IReadOnlyList<Parameter>>(), parameters);
		}
		public static IEnumerable<IReadOnlyList<Parameter>> GenOverloads(bool includeAll, params Parameter[] parameters)
		{
			return GenOverloads(includeAll, Enumerable.Empty<IReadOnlyList<Parameter>>(), parameters);
		}
		public static IEnumerable<IReadOnlyList<Parameter>> GenOverloads(bool includeAll, Parameter beforeParam, IEnumerable<IReadOnlyList<Parameter>> baseOverloads)
		{
			var overloads = baseOverloads.Select(overload =>
			{
				var newOverload = overload.ToList();
				newOverload.Insert(0, beforeParam);
				return newOverload;
			}).ToList();
			if(!includeAll)
				overloads.RemoveAt(overloads.Count - 1); // The last one has no defaults
			return overloads;
		}
		public static IEnumerable<IReadOnlyList<Parameter>> GenOverloads(bool includeAll, IEnumerable<IReadOnlyList<Parameter>> baseOverloads, params Parameter[] parameters)
		{
			var overloads = baseOverloads.Select(overload => overload.ToList()).ToList();
			if(overloads.Count == 0) // Empty base to start from
				overloads.Add(new List<Parameter>());
			foreach(var param in parameters)
			{
				// params that exist only to pass values, or aren't optional, we tack onto all overloads
				if(param.Name == null || param.DefaultValue == null)
				{
					foreach(var overload in overloads)
						overload.Add(param);
				}
				else
				{
					var ovarloadsWithParam = overloads.Select(o =>
					{
						var overload = new List<Parameter>(o);
						overload.Add(param);
						return overload;
					}).ToList(); // So we gen them before changing overloads

					// overloads without the param need the default value
					foreach(var overload in overloads)
						overload.Add(param.Default);

					overloads.AddRange(ovarloadsWithParam);
				}
			}
			if(!includeAll)
				overloads.RemoveAt(overloads.Count - 1); // The last one has no defaults
			return overloads;
		}

		public static IEnumerable<IReadOnlyList<Parameter>> RemoveBaseKeyOverload(IEnumerable<IReadOnlyList<Parameter>> baseOverloads)
		{
			var overloads = baseOverloads.ToList();
			var itemKeyOverloads = overloads.Where(o => o[0] == ItemKey && o.All(p => !p.IsArgument)).ToList();
			var longestItemKeyOverloadLength = itemKeyOverloads.Max(o => o.Count);
			var longestItemKeyOverload = itemKeyOverloads.Single(o => o.Count == longestItemKeyOverloadLength);
			return overloads.Except(new[] { longestItemKeyOverload });
		}

		// must pass either provisionedThroughput or indexProvisionedThroughputs
		public static readonly Func<IReadOnlyList<Parameter>, bool> HasThroughput = overload => overload.Contains(ProvisionedThroughput) || overload.Contains(IndexProvisionedThroughputs);
		public static readonly Func<IReadOnlyList<Parameter>, bool> NoIndexThroughputWithoutTableThroughput = overload => !(overload.Contains(IndexProvisionedThroughputs) && !overload.Contains(ProvisionedThroughput));
		public static readonly Func<IReadOnlyList<Parameter>, bool> NoValuesWithoutFilter = overload => !(overload.Contains(Values) && !overload.Contains(Filter));

		public static readonly Parameter ReadAhead = Parameter.Of("ReadAhead", "readAhead", "ReadAhead.Some");
		public static readonly Parameter TableNamePrefix = Parameter.Of("string", "tableNamePrefix");
		public static readonly Parameter TableName = Parameter.Of("string", "tableName");
		public static readonly Parameter Schema = Parameter.Of("TableSchema", "schema");
		public static readonly Parameter CancellationToken = Parameter.Of("CancellationToken", "cancellationToken", "CancellationToken.None");
		public static readonly Parameter ProvisionedThroughput = Parameter.Transform("ProvisionedThroughput", "provisionedThroughput", "(ProvisionedThroughput?)provisionedThroughput", "null");
		public static readonly Parameter IndexProvisionedThroughputs = Parameter.Of("IReadOnlyDictionary<string, ProvisionedThroughput>", "indexProvisionedThroughputs", "null");
		public static readonly Parameter ItemKey = Parameter.Of("ItemKey", "key");
		public static readonly Parameter Projection = Parameter.Of("ProjectionExpression", "projection", "null");
		public static readonly Parameter Consistent = Parameter.Of("bool", "consistent", "false");
		public static readonly Parameter HashKeyOnly = Parameter.Transform("DynamoDBKeyValue", "hashKey", "ItemKey.Create(hashKey)");
		public static readonly Parameter HashKeyObjectOnly = Parameter.Transform("object", "hashKey", "ItemKey.Create(hashKey)");
		public static readonly Parameter HashKeySkipped = Parameter.Transform("DynamoDBKeyValue", "hashKey", null);
		public static readonly Parameter HashKeyObjectSkipped = Parameter.Transform("object", "hashKey", null);
		public static readonly Parameter RangeKey = Parameter.Transform("DynamoDBKeyValue", "rangeKey", "ItemKey.Create(hashKey, rangeKey)");
		public static readonly Parameter RangeKeyObject = Parameter.Transform("object", "rangeKey", "ItemKey.Create(hashKey, rangeKey)");
		public static readonly Parameter RangeKeySkipped = Parameter.Transform("DynamoDBKeyValue", "rangeKey", null);
		public static readonly Parameter RangeKeyObjectSkipped = Parameter.Transform("object", "rangeKey", null);
		public static readonly Parameter ConverterHash = Parameter.Transform("IValueConverter", "converter", "ItemKey.Create(hashKey, converter)");
		public static readonly Parameter ConverterHashAndRange = Parameter.Transform("IValueConverter", "converter", "ItemKey.Create(hashKey, rangeKey, converter)");
		public static readonly Parameter Status = Parameter.Of("CollectionStatus", "status");
		public static readonly Parameter Timeout = Parameter.Of("TimeSpan", "timeout");
		public static readonly Parameter KeysAsync = Parameter.Of("IAsyncEnumerable<ItemKey>", "keys");
		public static readonly Parameter KeysSyncToAsync = Parameter.Transform("IEnumerable<ItemKey>", "keys", "keys.ToAsyncEnumerable()");
		public static readonly Parameter Keys = Parameter.Of("IEnumerable<ItemKey>", "keys");
		public static readonly Parameter OuterItemsAsync = Parameter.Of("IAsyncEnumerable<T>", "outerItems");
		public static readonly Parameter OuterItemsSyncToAsync = Parameter.Transform("IEnumerable<T>", "outerItems", "outerItems.ToAsyncEnumerable()");
		public static readonly Parameter OuterItemsSync = Parameter.Of("IEnumerable<T>", "outerItems");
		public static readonly Parameter KeySelector = Parameter.Of("Func<T, ItemKey>", "keySelector");
		public static readonly Parameter ResultSelector = Parameter.Of("Func<T, DynamoDBMap, TResult>", "resultSelector");
		public static readonly Parameter Item = Parameter.Of("DynamoDBMap", "item");
		public static readonly Parameter Values = Parameter.Of("Values", "values", "null");
		public static readonly Parameter ReturnOldItem = Parameter.Of("bool", "returnOldItem", "false");
		public static readonly Parameter UpdateExp = Parameter.Of("UpdateExpression", "update");
		public static readonly Parameter UpdateReturnValue = Parameter.Of("UpdateReturnValue", "returnValue", "UpdateReturnValue.None");
		public static readonly Parameter HashKey = Parameter.Of("DynamoDBKeyValue", "hashKey");
		public static readonly Parameter HashKeyObject = Parameter.Transform("object", "hashKey", "DynamoDBKeyValue.Convert(hashKey)");
		public static readonly Parameter HashKeyObjectConverter = Parameter.Transform("object", "hashKey", "DynamoDBKeyValue.Convert(hashKey, converter)");
		public static readonly Parameter ConverterSkipped = Parameter.Transform("IValueConverter", "converter", null);
		public static readonly Parameter Filter = Parameter.Of("PredicateExpression", "filter", "null");
		public static readonly Parameter RangeKeyOnly = Parameter.Of("DynamoDBKeyValue", "rangeKey");
		public static readonly Parameter RangeKeyOnlyObject = Parameter.Transform("object", "rangeKey", "DynamoDBKeyValue.Convert(rangeKey)");
		public static readonly Parameter RangeKeyOnlyObjectConverter = Parameter.Transform("object", "rangeKey", "DynamoDBKeyValue.Convert(rangeKey, converter)");
		public static readonly Parameter BatchWrite = Parameter.Of("IBatchWrite", "batch");
		public static readonly Parameter BatchWriteAsync = Parameter.Of("IBatchWriteAsync", "batch");
		public static readonly Parameter Segment = Parameter.Of("int", "segment");
		public static readonly Parameter TotalSegments = Parameter.Of("int", "totalSegments");
		public static readonly Parameter StartInclusive = Parameter.Of("DynamoDBKeyValue", "startInclusive");
		public static readonly Parameter StartInclusiveObject = Parameter.Transform("object", "startInclusive", "DynamoDBKeyValue.Convert(startInclusive)");
		public static readonly Parameter StartInclusiveObjectConverter = Parameter.Transform("object", "startInclusive", "DynamoDBKeyValue.Convert(startInclusive, converter)");
		public static readonly Parameter EndExclusive = Parameter.Of("DynamoDBKeyValue", "endExclusive");
		public static readonly Parameter EndExclusiveObject = Parameter.Transform("object", "endExclusive", "DynamoDBKeyValue.Convert(endExclusive)");
		public static readonly Parameter EndExclusiveObjectConverter = Parameter.Transform("object", "endExclusive", "DynamoDBKeyValue.Convert(endExclusive, converter)");

		public static readonly IList<IReadOnlyList<Parameter>> KeyOverloads =
			GenOverloads(true, HashKeyOnly)
			.Concat(GenOverloads(true, HashKeySkipped, ConverterHash))
			.Concat(GenOverloads(true, HashKeyObjectOnly))
			.Concat(GenOverloads(true, HashKeyObjectSkipped, ConverterHash))
			.Concat(GenOverloads(true, HashKeySkipped, RangeKey))
			.Concat(GenOverloads(true, HashKeySkipped, RangeKeySkipped, ConverterHashAndRange))
			.Concat(GenOverloads(true, HashKeyObjectSkipped, RangeKey))
			.Concat(GenOverloads(true, HashKeyObjectSkipped, RangeKeySkipped, ConverterHashAndRange))
			.Concat(GenOverloads(true, HashKeySkipped, RangeKeyObject))
			.Concat(GenOverloads(true, HashKeySkipped, RangeKeyObjectSkipped, ConverterHashAndRange))
			.Concat(GenOverloads(true, HashKeyObjectSkipped, RangeKeyObject))
			.Concat(GenOverloads(true, HashKeyObjectSkipped, RangeKeyObjectSkipped, ConverterHashAndRange))
			.Concat(GenOverloads(true, ItemKey))
			.ToList();

		public static readonly IList<IReadOnlyList<Parameter>> RangeKeyOverloads =
			GenOverloads(true, RangeKeyOnlyObjectConverter, ConverterSkipped)
			.Concat(GenOverloads(true, RangeKeyOnlyObject))
			.Concat(GenOverloads(true, RangeKeyOnly, ConverterSkipped))
			.Concat(GenOverloads(true, RangeKeyOnly))
			.ToList();

		public static readonly IList<IReadOnlyList<Parameter>> RangeKeyBetweenOverloads =
			GenOverloads(true, StartInclusiveObjectConverter, EndExclusiveObjectConverter, ConverterSkipped)
			.Concat(GenOverloads(true, StartInclusiveObject, EndExclusiveObject))
			.Concat(GenOverloads(true, StartInclusiveObjectConverter, EndExclusive, ConverterSkipped))
			.Concat(GenOverloads(true, StartInclusiveObject, EndExclusive))
			.Concat(GenOverloads(true, StartInclusive, EndExclusiveObjectConverter, ConverterSkipped))
			.Concat(GenOverloads(true, StartInclusive, EndExclusiveObject))
			.Concat(GenOverloads(true, StartInclusive, EndExclusive, ConverterSkipped))
			.Concat(GenOverloads(true, StartInclusive, EndExclusive))
			.ToList();
	}
}
