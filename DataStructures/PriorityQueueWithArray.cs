using System;

namespace DataStructures
{
	public class PriorityQueueWithArray<T> where T : IComparable
	{
		#region Internals and properties
		private T[] items;
		public int Count { get; private set; }

		public PriorityQueueWithArray(int length)
		{
			items = new T[length];
		} 
		#endregion

		#region Public methods
		public void Add(T item)
		{
			if (isFull())
				throw new InvalidOperationException();

			var i = ShiftItemsToInsert(item);
			items[i] = item;
			Count++;
		}

		public bool isFull()
		{
			return Count == items.Length;
		}

		public T Remove()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			return items[--Count];
		}

		public bool IsEmpty()
		{
			return Count == 0;
		}
		#endregion

		#region Private methods
		private int ShiftItemsToInsert(T item)
		{
			int i;
			for (i = Count - 1; i >= 0; i--)
			{
				if (items[i].CompareTo(item) > 0)
					items[i + 1] = items[i];
				else
					break;
			}
			return i + 1;
		}
		#endregion
	}
}
