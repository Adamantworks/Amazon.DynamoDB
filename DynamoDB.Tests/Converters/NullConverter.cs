using System;
using Adamantworks.Amazon.DynamoDB.Converters;

namespace Adamantworks.Amazon.DynamoDB.Tests.Converters
{
	public class NullConverter : IValueConverter
	{
		#region Singleton
		private static readonly IValueConverter instance = new NullConverter();

		public static IValueConverter Instance
		{
			get { return instance; }
		}

		private NullConverter()
		{
		}
		#endregion

		public bool CanConvert(Type fromType, Type toType, IValueConverter context)
		{
			return false;
		}

		public bool TryConvert(object fromValue, Type toType, out object toValue, IValueConverter context)
		{
			toValue = null;
			return false;
		}
	}
}
