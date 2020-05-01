namespace DataStructures.Interfaces
{
	public interface IBinaryNode<T>
	{
		T Value { get; set; }
		IBinaryNode<T> LeftChild { get; set; }
		IBinaryNode<T> RightChild { get; set; }
	}
}
