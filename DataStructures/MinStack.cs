using System;

namespace DataStructures
{
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

			var top = (T)stack.Pop();

			if (((T)minStack.Peek()).CompareTo(top) == 0)
				minStack.Pop();

			return top;
		}

		public T Min() => (T)minStack.Peek();
		#endregion
	}
}
