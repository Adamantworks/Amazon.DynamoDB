namespace Adamantworks.Amazon.DynamoDB.CodeGen
{
	public static class Args
	{
		public static readonly Parameter IndexTable = Parameter.Argument("Table");
		public static readonly Parameter IndexNone = Parameter.Argument("null");
		public static readonly Parameter Table = Parameter.Argument("table");
		public static readonly Parameter IndexDotTable = Parameter.Argument("index.Table");
		public static readonly Parameter Index = Parameter.Argument("index");
		public static readonly Parameter ProjectionNone = Parameter.Argument("null");
		public static readonly Parameter Projection = Parameter.Argument("projection");
		public static readonly Parameter ConsistentNone = Parameter.Argument("false");
		public static readonly Parameter ConsistentRead = Parameter.Argument("consistentRead ?? false");
		public static readonly Parameter ConditionNone = Parameter.Argument("null");
		public static readonly Parameter ConditionValuesNone = Parameter.Argument("null");
		public static readonly Parameter This = Parameter.Argument("this");
	}
}
