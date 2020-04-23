using System;

namespace DataStructures
{
	public class MinStack<T> where T : IComparable
	{
		#region Internals and properties
		private readonly Stack<T> stack;
		private readonly Stack<T> minStack;

		public MinStack(int size)
		{
			stack = new Stack<T>(size);
			minStack = new Stack<T>(size);
		}
		#endregion

		#region Public methods
		public void Push(T item)
		{
			stack.Push(item);

			if (minStack.IsEmpty())
				minStack.Push(item);
			else if (item.CompareTo(minStack.Peek()) < 0)
				minStack.Push(item);
		}

		public T Pop()
		{
			if (stack.IsEmpty())
				throw new InvalidOperationException();

			var top = stack.Pop();

			if (minStack.Peek().CompareTo(top) == 0)
				minStack.Pop();

			return top;
		}

		public T Min()
		{
			return minStack.Peek();
		} 
		#endregion
	}
}
