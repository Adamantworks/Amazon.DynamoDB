using System.Threading.Tasks;

namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public interface IScanCountOptions
	{
		Task<long> AllAsync();
		long All();

		Task<long> SegmentAsync(int segment, int totalSegments);
		long Segment(int segment, int totalSegments);

		Task<long> InParallelAsync(int totalSegments);
		long InParallel(int totalSegments);

		Task<long> InParallelAsync();
		long InParallel();
	}
}
