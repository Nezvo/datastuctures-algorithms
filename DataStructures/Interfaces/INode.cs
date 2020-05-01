using System;

namespace DataStructures.Interfaces
{
	public interface INode<T> where T : IComparable
	{
		string Label { get; set; }
		T Value { get; set; }
	}
}
