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
			Params.GenOverloads(true, Params.TableArg, Params.IndexNoneArg, Params.ProjectionArg, Params.Filter, Params.Values)
			.Where(Params.NoValuesWithoutFilter));
	}
}
