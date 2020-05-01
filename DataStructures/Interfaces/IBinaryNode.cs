namespace DataStructures.Interfaces
{
	public interface IBinaryNode<T>
	{
		T Value { get; set; }
		int Height { get; set; }
		IBinaryNode<T> LeftChild { get; set; }
		IBinaryNode<T> RightChild { get; set; }
	}
}
