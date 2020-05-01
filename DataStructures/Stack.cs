using System;

namespace DataStructures
{
	public class Stack
	{
		#region Internals and properties
		private readonly object[] items;
		public int Count { get; private set; }

		public Stack(int size)
		{
			items = new object[size];
		} 
		#endregion

		#region Public methods
		public void Push(object item)
		{
			if (Count == items.Length)
				throw new OverflowException();

			items[Count++] = item;
		}

		public object Pop()
		{
			if (Count == 0)
				throw new InvalidOperationException();

			return items[--Count];
		}

		public object Peek()
		{
			if (Count == 0)
				throw new InvalidOperationException();

			return items[Count - 1];
		}

		public bool IsEmpty() => Count == 0;
		#endregion
	}
}
