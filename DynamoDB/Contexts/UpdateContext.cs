﻿using Adamantworks.Amazon.DynamoDB.Syntax;

namespace Adamantworks.Amazon.DynamoDB.Contexts
{
	internal class UpdateContext : ISetSyntax
	{
		private readonly Table table;
		private readonly UpdateExpression update;
		private readonly Values values;
		private readonly UpdateReturnValue returnValue;

		public UpdateContext(
			Table table,
			UpdateExpression update,
			Values values,
			UpdateReturnValue returnValue)
		{
			this.table = table;
			this.update = update;
			this.values = values;
			this.returnValue = returnValue;
		}
	}
}
