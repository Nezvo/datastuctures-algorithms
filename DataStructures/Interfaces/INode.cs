using System;

namespace DataStructures.Interfaces
{
	public interface INode<TKey> where TKey : IComparable
	{
		TKey Id { get; set; }
		string Name { get; set; }
	}
}
