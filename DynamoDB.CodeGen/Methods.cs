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
	}
}
