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

using System;
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
		public static IDynamoDBRegion Connect(AWSCredentials credentials, AwsEnums.AmazonDynamoDBConfig config)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(credentials, config));
		}

		public static IDynamoDBRegion Connect(string accessKeyId, string secretAccessKey)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(accessKeyId, secretAccessKey));
		}
		public static IDynamoDBRegion Connect(string accessKeyId, string secretAccessKey, RegionEndpoint region)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(accessKeyId, secretAccessKey, region));
		}
		public static IDynamoDBRegion Connect(string accessKeyId, string secretAccessKey, AwsEnums.AmazonDynamoDBConfig config)
		{
			return new Region(new AwsEnums.AmazonDynamoDBClient(accessKeyId, secretAccessKey, config));
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

		public const long DefaultBytesPerSegment = 2L * 1024 * 1024 * 1024; // 2 GB
		public const int DefaultSegmentsPerProcessor = 2;
		public const int DefaultMaxSegments = 4096;

		/// <summary>
		/// Estimates the optimal number of segments to use to Scan based on guidelines from amazon (see http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/QueryAndScanGuidelines.html#QueryAndScanGuidelines.ParallelScan).
		/// </summary>
		/// <param name="sizeInBytes">size of table or index in bytes</param>
		/// <param name="bytesPerSegment">how many bytes should be in each segment</param>
		/// <param name="segmentsPerProcessor">how many segments there should be for each processor (null for no limit)</param>
		/// <param name="maxSegments">the max number of segments to use (null for no limit)</param>
		/// <returns></returns>
		public static int EstimateScanSegments(long sizeInBytes, long bytesPerSegment, int? segmentsPerProcessor, int? maxSegments)
		{
			if(sizeInBytes < 0) throw new ArgumentOutOfRangeException("sizeInBytes", "can't be negative");
			if(bytesPerSegment < 1) throw new ArgumentOutOfRangeException("bytesPerSegment", "must be greater than zero");
			if(segmentsPerProcessor != null && segmentsPerProcessor.Value < 1) throw new ArgumentOutOfRangeException("segmentsPerProcessor", "must be greater than zero");
			if(maxSegments != null && maxSegments.Value < 1) throw new ArgumentOutOfRangeException("maxSegments", "must be greater than zero");

			var segments = (int)Math.Round((decimal)sizeInBytes / bytesPerSegment, MidpointRounding.ToEven); // convert to decimal so we can round and not lose precision
			segments = Math.Max(segments, 1); // in case there was a round down
			if(segmentsPerProcessor != null)
				segments = Math.Min(segments, Environment.ProcessorCount * segmentsPerProcessor.Value);
			if(maxSegments != null)
				segments = Math.Min(segments, maxSegments.Value);
			return segments;
		}
		public static int EstimateScanSegments(long sizeInBytes, long bytesPerSegment, int? segmentsPerProcessor)
		{
			return EstimateScanSegments(sizeInBytes, bytesPerSegment, segmentsPerProcessor, DefaultMaxSegments);
		}
		public static int EstimateScanSegments(long sizeInBytes, long bytesPerSegment)
		{
			return EstimateScanSegments(sizeInBytes, bytesPerSegment, DefaultSegmentsPerProcessor, DefaultMaxSegments);
		}
		public static int EstimateScanSegments(long sizeInBytes)
		{
			return EstimateScanSegments(sizeInBytes, DefaultBytesPerSegment, DefaultSegmentsPerProcessor, DefaultMaxSegments);
		}
	}
}
