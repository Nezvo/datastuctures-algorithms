using System;

namespace DataStructures.Interfaces
{
	public interface INode<T> where T : IComparable
	{
		T Id { get; set; }
		string Name { get; set; }
	}
}
