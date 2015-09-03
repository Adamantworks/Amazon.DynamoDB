using System.Collections.Generic;
using System.Linq;

namespace Adamantworks.Amazon.DynamoDB.CodeGen
{
	public static class Parameters
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
			var paramsList = parameters.ToList();
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
	}
}
