using System;

namespace DataStructures.Interfaces
{
	public interface IBinaryNode<T> where T : IComparable
	{
		T Id { get; set; }
		int Height { get; set; }
		IBinaryNode<T> LeftChild { get; set; }
		IBinaryNode<T> RightChild { get; set; }
	}
}
