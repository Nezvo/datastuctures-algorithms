using System;

namespace DataStructures.Interfaces
{
	public interface IBinaryNode<T> where T : IComparable
	{
		T Value { get; set; }
		int Height { get; set; }
		IBinaryNode<T> LeftChild { get; set; }
		IBinaryNode<T> RightChild { get; set; }
	}
}
