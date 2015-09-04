namespace Adamantworks.Amazon.DynamoDB.Syntax
{
	public interface IForKeySyntax
	{
		void ForKey(ItemKey key);
		bool TryForKey(ItemKey key);
	}
}
