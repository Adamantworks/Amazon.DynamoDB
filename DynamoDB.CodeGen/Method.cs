using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adamantworks.Amazon.DynamoDB.CodeGen
{
	public class Method
	{
		public readonly string ReturnType;
		public readonly string Name;
		public readonly string Call;
		public readonly IReadOnlyList<IReadOnlyList<Parameter>> ParameterOverloads;

		public Method(string returnType, string name, IEnumerable<IReadOnlyList<Parameter>> parameterOverloads)
			: this(returnType, name, name, parameterOverloads)
		{
		}

		public Method(string returnType, string name, string call, IEnumerable<IReadOnlyList<Parameter>> parameterOverloads)
		{
			ReturnType = returnType;
			Name = name;
			Call = call;
			ParameterOverloads = parameterOverloads.ToList();
		}

		public string GenInterface()
		{
			var builder = new StringBuilder();
			foreach(var overload in ParameterOverloads)
			{
				builder.AppendLine(string.Format("		{0};", GenDeclaration(overload)));
			}
			return builder.ToString().TrimEnd();
		}

		public string GenMethods()
		{
			var builder = new StringBuilder();
			builder.AppendLine(string.Format("		#region {0}", Name));
			foreach(var overload in ParameterOverloads)
			{
				builder.Append(string.Format(
	@"		public {0}
		{{
{1}
		}}
", GenDeclaration(overload), GenMethodCall(overload)));
			}
			builder.AppendLine("		#endregion");
			return builder.ToString().TrimEnd();
		}

		public string GenExplicitImplementations(string @interface)
		{
			var builder = new StringBuilder();
			builder.AppendLine(string.Format("		#region {0}.{1}", @interface, Name));
			foreach(var overload in ParameterOverloads)
			{
				builder.Append(string.Format(
	@"		{0}
		{{
{1}
		}}
", GenDeclaration(overload, @interface), GenMethodCall(overload, @interface)));
			}
			builder.Append("		#endregion");
			return builder.ToString();
		}

		public string GenDeclaration(IReadOnlyList<Parameter> overload, string @interface = null)
		{
			var builder = new StringBuilder();
			builder.AppendFormat("{0} {1}{2}(", ReturnType, @interface != null ? @interface + "." : "", Name);
			var firstParam = true;
			foreach(var param in overload.Where(param => param.Name != null))
			{
				if(firstParam)
					firstParam = false;
				else
					builder.Append(", ");
				builder.AppendFormat("{0} {1}", param.Type, param.Name);
			}
			builder.Append(")");
			return builder.ToString();
		}

		public string GenMethodCall(IReadOnlyList<Parameter> overload, string @interface = null)
		{
			var builder = new StringBuilder();
			builder.Append("			");
			if(ReturnType != "void")
				builder.Append("return ");
			builder.AppendFormat("{0}{1}(", @interface != null ? "((" + @interface + ")this)." : "", Call);
			var firstParam = true;
			foreach(var param in overload.Where(param => param.Value != null))
			{
				if(firstParam)
					firstParam = false;
				else
					builder.Append(", ");
				builder.AppendFormat("{0}", param.Value);
			}
			builder.Append(");");
			return builder.ToString();
		}
	}
}
