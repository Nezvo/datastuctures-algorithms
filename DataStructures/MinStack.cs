using DataStructures.Interfaces;
using System;

namespace DataStructures
{
	/// <summary>
	/// Min Stack implementation, where item on top will always have the lowest Id.
	/// Objects that will be added to the stack must implement INode
	/// </summary>
	/// <typeparam name="T">Type of Id property in an object that will be added to the stack</typeparam>
	public class MinStack<T> where T : IComparable
	{
		#region Internals and properties
		private readonly Stack<INode<T>> stack;
		private readonly Stack<INode<T>> minStack;

		public MinStack(int size)
		{
			stack = new Stack<INode<T>>(size);
			minStack = new Stack<INode<T>>(size);
		}
		#endregion

		#region Public methods
		public void Push(INode<T> item)
		{
			stack.Push(item);

			if (minStack.IsEmpty())
				minStack.Push(item);
			else if (item.Id.CompareTo(minStack.Peek().Id) < 0)
				minStack.Push(item);
		}

		public INode<T> Pop()
		{
			if (stack.IsEmpty())
				throw new InvalidOperationException();

			var top = stack.Pop();

			if (minStack.Peek().Id.CompareTo(top.Id) == 0)
				minStack.Pop();

			return top;
		}

		public INode<T> Min() => minStack.Peek();
		#endregion
	}
}
