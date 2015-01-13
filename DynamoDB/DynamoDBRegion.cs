// Copyright 2015 Adamantworks.  All Rights Reserved.
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

using Amazon;
using Amazon.Runtime;
using AwsEnums = Amazon.DynamoDBv2;

namespace Adamantworks.Amazon.DynamoDB
{
	public static class DynamoDBRegion
	{
		public static IDynamoDBRegion Connect(AwsEnums.IAmazonDynamoDB db)
		{
			return new Region(db);
		}

		public static IDynamoDBRegion Connect()
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient());
		}
		public static IDynamoDBRegion Connect(RegionEndpoint region)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(region));
		}

		public static IDynamoDBRegion Connect(AwsEnums.AmazonDynamoDBConfig config)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(config));
		}

		public static IDynamoDBRegion Connect(AWSCredentials credentials)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(credentials));
		}
		public static IDynamoDBRegion Connect(AWSCredentials credentials, RegionEndpoint region)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(credentials, region));
		}

		public static IDynamoDBRegion Connect(string accessKeyId, string secretAccessKey)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(accessKeyId, secretAccessKey));
		}
		public static IDynamoDBRegion Connect(string accessKeyId, string secretAccessKey, RegionEndpoint region)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(accessKeyId, secretAccessKey, region));
		}

		public static IDynamoDBRegion Connect(string accessKeyId, string secretAccessKey, string awsSessionToken)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(accessKeyId, secretAccessKey, awsSessionToken));
		}
		public static IDynamoDBRegion Connect(string accessKeyId, string secretAccessKey, string awsSessionToken, RegionEndpoint region)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(accessKeyId, secretAccessKey, awsSessionToken, region));
		}
		public static IDynamoDBRegion Connect(string accessKeyId, string secretAccessKey, string awsSessionToken, AwsEnums.AmazonDynamoDBConfig config)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(accessKeyId, secretAccessKey, awsSessionToken, config));
		}
	}
}
