using System.Linq;

namespace Adamantworks.Amazon.DynamoDB.CodeGen
{
	public static class Methods
	{
		public static readonly Method GetAsync = new Method("Task<DynamoDBMap>", "GetAsync",
			Params.GenOverloads(false, Params.KeyOverloads, Params.CancellationToken));

		public static readonly Method Get = new Method("DynamoDBMap", "Get",
			Params.GenOverloads(false, Params.KeyOverloads));

		public static readonly Method BatchGetAsync = new Method("IAsyncEnumerable<DynamoDBMap>", "BatchGetAsync",
			Params.GenOverloads(true, Params.KeysSyncToAsync, Params.ReadAhead)
			.Concat(Params.GenOverloads(Params.KeysAsync, Params.ReadAhead)));

		public static readonly Method BatchGet = new Method("IEnumerable<DynamoDBMap>", "BatchGet",
			Params.GenOverloads(Params.Keys));

		public static readonly Method BatchGetJoinAsync = new Method("IAsyncEnumerable<TResult>", "BatchGetJoinAsync<T, TResult>",
			Params.GenOverloads(true, Params.OuterItemsSyncToAsync, Params.KeySelector, Params.ResultSelector, Params.ReadAhead)
			.Concat(Params.GenOverloads(Params.OuterItemsAsync, Params.KeySelector, Params.ResultSelector, Params.ReadAhead)));

		public static readonly Method BatchGetJoin = new Method("IEnumerable<TResult>", "BatchGetJoin<T, TResult>",
			Params.GenOverloads(Params.OuterItemsSync, Params.KeySelector, Params.ResultSelector));

		public static readonly Method Scan = new Method("IScanLimitToOrPagedSyntax", "Scan", "new ScanContext",
			Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.Projection, Params.Filter, Params.Values)
			.Where(Params.NoValuesWithoutFilter));

		public static readonly Method ScanCount = new Method("IScanCountOptionsSyntax", "ScanCount", "new ScanCountContext",
			Params.GenOverloads(true, Args.Table, Args.IndexNone, Params.Filter, Params.Values)
			.Where(Params.NoValuesWithoutFilter));

		public static readonly Method Query = new Method("IReverseSyntax", "Query", "new QueryContext",
			Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.Projection, Args.ConsistentRead, Params.HashKey, Params.Filter, Params.Values)
			.Concat(Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.Projection, Args.ConsistentRead, Params.HashKey, Params.ConverterSkipped, Params.Filter, Params.Values))
			.Concat(Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.Projection, Args.ConsistentRead, Params.HashKeyObject, Params.Filter, Params.Values))
			.Concat(Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.Projection, Args.ConsistentRead, Params.HashKeyObjectConverter, Params.ConverterSkipped, Params.Filter, Params.Values))
			.Where(Params.NoValuesWithoutFilter));

		public static readonly Method QueryCount = new Method("IQueryCountRangeSyntax", "QueryCount", "new QueryCountContext",
			Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.ConsistentRead, Params.HashKey, Params.Filter, Params.Values)
			.Concat(Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.ConsistentRead, Params.HashKey, Params.ConverterSkipped, Params.Filter, Params.Values))
			.Concat(Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.ConsistentRead, Params.HashKeyObject, Params.Filter, Params.Values))
			.Concat(Params.GenOverloads(true, Args.Table, Args.IndexNone, Args.ConsistentRead, Params.HashKeyObjectConverter, Params.ConverterSkipped, Params.Filter, Params.Values))
			.Where(Params.NoValuesWithoutFilter));

		// Query Count Range
		public static readonly Method CountAllKeysAsync = new Method("Task<long>", "AllKeysAsync", Params.GenOverloads(Params.CancellationToken));
		public static readonly Method CountRangeKeyBeginsWithAsync = new Method("Task<long>", "RangeKeyBeginsWithAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method CountRangeKeyBeginsWith = new Method("long", "RangeKeyBeginsWith",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method CountRangeKeyEqualsAsync = new Method("Task<long>", "RangeKeyEqualsAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method CountRangeKeyEquals = new Method("long", "RangeKeyEquals",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method CountRangeKeyLessThanAsync = new Method("Task<long>", "RangeKeyLessThanAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method CountRangeKeyLessThan = new Method("long", "RangeKeyLessThan",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method CountRangeKeyLessThanOrEqualToAsync = new Method("Task<long>", "RangeKeyLessThanOrEqualToAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method CountRangeKeyLessThanOrEqualTo = new Method("long", "RangeKeyLessThanOrEqualTo",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method CountRangeKeyGreaterThanAsync = new Method("Task<long>", "RangeKeyGreaterThanAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method CountRangeKeyGreaterThan = new Method("long", "RangeKeyGreaterThan",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method CountRangeKeyGreaterThanOrEqualToAsync = new Method("Task<long>", "RangeKeyGreaterThanOrEqualToAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method CountRangeKeyGreaterThanOrEqualTo = new Method("long", "RangeKeyGreaterThanOrEqualTo",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method CountRangeKeyBetweenAsync = new Method("Task<long>", "RangeKeyBetweenAsync",
			Params.GenOverloads(false, Params.RangeKeyBetweenOverloads, Params.CancellationToken));
		public static readonly Method CountRangeKeyBetween = new Method("long", "RangeKeyBetween",
			Params.GenOverloads(false, Params.RangeKeyBetweenOverloads));

		public static readonly Method UpdateAsync = new Method("Task<DynamoDBMap>", "UpdateAsync",
			Params.GenOverloads(Params.UpdateExp, Params.Values, Params.UpdateReturnValue, Params.CancellationToken));
		public static readonly Method Update = new Method("DynamoDBMap", "Update",
			Params.GenOverloads(Params.UpdateExp, Params.Values, Params.UpdateReturnValue));

		public static readonly Method DeleteAsync = new Method("Task<DynamoDBMap>", "DeleteAsync",
			Params.GenOverloads(Params.ReturnOldItem, Params.CancellationToken));
		public static readonly Method Delete = new Method("DynamoDBMap", "Delete",
			Params.GenOverloads(Params.ReturnOldItem));

		// Query Range Paged
		public static readonly Method AllKeysAsyncPaged = new Method("Task<ItemPage>", "AllKeysAsync", Params.GenOverloads(Params.CancellationToken));
		public static readonly Method RangeKeyBeginsWithAsyncPaged = new Method("Task<ItemPage>", "RangeKeyBeginsWithAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method RangeKeyBeginsWithPaged = new Method("ItemPage", "RangeKeyBeginsWith",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method RangeKeyEqualsAsyncPaged = new Method("Task<ItemPage>", "RangeKeyEqualsAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method RangeKeyEqualsPaged = new Method("ItemPage", "RangeKeyEquals",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method RangeKeyLessThanAsyncPaged = new Method("Task<ItemPage>", "RangeKeyLessThanAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method RangeKeyLessThanPaged = new Method("ItemPage", "RangeKeyLessThan",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method RangeKeyLessThanOrEqualToAsyncPaged = new Method("Task<ItemPage>", "RangeKeyLessThanOrEqualToAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method RangeKeyLessThanOrEqualToPaged = new Method("ItemPage", "RangeKeyLessThanOrEqualTo",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method RangeKeyGreaterThanAsyncPaged = new Method("Task<ItemPage>", "RangeKeyGreaterThanAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method RangeKeyGreaterThanPaged = new Method("ItemPage", "RangeKeyGreaterThan",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method RangeKeyGreaterThanOrEqualToAsyncPaged = new Method("Task<ItemPage>", "RangeKeyGreaterThanOrEqualToAsync",
			Params.GenOverloads(false, Params.RangeKeyOverloads, Params.CancellationToken));
		public static readonly Method RangeKeyGreaterThanOrEqualToPaged = new Method("ItemPage", "RangeKeyGreaterThanOrEqualTo",
			Params.GenOverloads(false, Params.RangeKeyOverloads));
		public static readonly Method RangeKeyBetweenAsyncPaged = new Method("Task<ItemPage>", "RangeKeyBetweenAsync",
			Params.GenOverloads(false, Params.RangeKeyBetweenOverloads, Params.CancellationToken));
		public static readonly Method RangeKeyBetweenPaged = new Method("ItemPage", "RangeKeyBetween",
			Params.GenOverloads(false, Params.RangeKeyBetweenOverloads));

		// Scan Options Paged
		public static readonly Method ScanAllAsyncPaged = new Method("Task<ItemPage>", "AllAsync", Params.GenOverloads(Params.CancellationToken));
		public static readonly Method ScanSegmentAsyncPaged = new Method("Task<ItemPage>", "SegmentAsync", Params.GenOverloads(Params.Segment, Params.TotalSegments, Params.CancellationToken));

		public static readonly Method PutAsync = new Method("Task<DynamoDBMap>", "PutAsync",
		Params.GenOverloads(Params.Item, Params.ReturnOldItem, Params.CancellationToken));
		public static readonly Method Put = new Method("DynamoDBMap", "Put",
			Params.GenOverloads(Params.Item, Params.ReturnOldItem));
	}
}
