using System;

namespace DataStructures
{
	public class Stack<T>
	{
		#region Internals and properties
		private T[] items;
		public int Count { get; private set; }

		public Stack(int length)
		{
			items = new T[length];
		} 
		#endregion

		#region Public methods
		public void Push(T item)
		{
			if (Count == items.Length)
				throw new OverflowException();

			items[Count++] = item;
		}

		public T Pop()
		{
			if (Count == 0)
				throw new InvalidOperationException();

			return items[--Count];
		}

		public T Peek()
		{
			if (Count == 0)
				throw new InvalidOperationException();

			return items[Count - 1];
		}

		public bool IsEmpty()
		{
			return Count == 0;
		}
		#endregion
	}
}
