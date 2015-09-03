namespace Adamantworks.Amazon.DynamoDB.CodeGen
{
	public class Parameter
	{
		public readonly string Type;
		public readonly string Name;
		public readonly string DefaultValue;
		public readonly string Value;
		public readonly Parameter Default;

		private Parameter(string type, string name, string defaultValue, string value)
		{
			Type = type;
			Name = name;
			DefaultValue = defaultValue;
			Value = value;
			if(name != null)
				Default = new Parameter(null, null, null, defaultValue ?? value);
		}

		public static Parameter Of(string type, string name)
		{
			return new Parameter(type, name, null, name);
		}
		public static Parameter Of(string type, string name, string defaultValue)
		{
			return new Parameter(type, name, defaultValue, name);
		}
		public static Parameter Transform(string type, string name, string value)
		{
			return new Parameter(type, name, null, value);
		}
		public static Parameter Transform(string type, string name, string value, string defaultValue)
		{
			return new Parameter(type, name, defaultValue, value);
		}
		public static Parameter Argument(string value)
		{
			return new Parameter(null, null, null, value);
		}
	}
}
