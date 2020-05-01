using DataStructures.Interfaces;
using System;

namespace DataStructures
{
	/// <summary>
	/// Min Stack implementation, where item on top will always have the lowest Id.
	/// Object that will be added to the tree must implement INode
	/// </summary>
	/// <typeparam name="T">Type of Id property in an object that will be added to the heap</typeparam>
	public class MinStack<T> where T : IComparable
	{
		#region Internals and properties
		private readonly Stack stack;
		private readonly Stack minStack;

		public MinStack(int size)
		{
			stack = new Stack(size);
			minStack = new Stack(size);
		}
		#endregion

		#region Public methods
		public void Push(INode<T> item)
		{
			stack.Push(item);

			if (minStack.IsEmpty())
				minStack.Push(item);
			else if (item.Id.CompareTo(minStack.Peek()) < 0)
				minStack.Push(item);
		}

		public INode<T> Pop()
		{
			if (stack.IsEmpty())
				throw new InvalidOperationException();

			var top = (INode<T>)stack.Pop();

			if (((T)minStack.Peek()).CompareTo(top) == 0)
				minStack.Pop();

			return top;
		}

		public INode<T> Min() => (INode<T>)minStack.Peek();
		#endregion
	}
}
