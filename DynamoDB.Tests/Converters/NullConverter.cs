﻿// Copyright 2015 Adamantworks.  All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
