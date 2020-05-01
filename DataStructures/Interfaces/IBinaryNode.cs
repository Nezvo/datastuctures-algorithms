using System;

namespace DataStructures.Interfaces
{
	public interface IBinaryNode<TKey> where TKey : IComparable
	{
		TKey Id { get; set; }
		int Height { get; set; }
		IBinaryNode<TKey> LeftChild { get; set; }
		IBinaryNode<TKey> RightChild { get; set; }
	}
}
