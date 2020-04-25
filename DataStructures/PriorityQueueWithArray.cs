using DataStructures.Helpers;
using System;

namespace DataStructures
{
	public class PriorityQueueWithArray<T> where T : IComparable
	{
		#region Internals and properties
		private readonly T[] items;
		private readonly PriorityQueueType type;
		public int Count { get; private set; }

		public PriorityQueueWithArray(int size, PriorityQueueType type = PriorityQueueType.Max)
		{
			items = new T[size];
			this.type = type;
		} 
		#endregion

		#region Public methods
		public void Enqueue(T item)
		{
			if (IsFull())
				throw new InvalidOperationException();

			var i = ShiftItemsToInsert(item);
			items[i] = item;
			Count++;
		}

		public bool IsFull()
		{
			return Count == items.Length;
		}

		public T Dequeue()
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
				if (type == PriorityQueueType.Max ? items[i].CompareTo(item) > 0 : items[i].CompareTo(item) < 0)
					items[i + 1] = items[i];
				else
					break;
			}
			return i + 1;
		}
		#endregion
	}

	public class PriorityQueueWithArray<TSource, TKey> where TKey : IComparable
	{
		#region Internals and properties
		private readonly TSource[] items;
		private readonly Func<TSource, TKey> selector;
		private readonly PriorityQueueType type;
		public int Count { get; private set; }

		public PriorityQueueWithArray(int size, Func<TSource, TKey> selector, PriorityQueueType type = PriorityQueueType.Max)
		{
			items = new TSource[size];
			this.selector = selector;
			this.type = type;
		}
		#endregion

		#region Public methods
		public void Enqueue(TSource item)
		{
			if (IsFull())
				throw new InvalidOperationException();

			var i = ShiftItemsToInsert(item);
			items[i] = item;
			Count++;
		}

		public bool IsFull() => Count == items.Length;

		public TSource Dequeue()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			return items[--Count];
		}

		public bool IsEmpty() => Count == 0;
		#endregion

		#region Private methods
		private int ShiftItemsToInsert(TSource item)
		{
			int i;
			for (i = Count - 1; i >= 0; i--)
			{
				if (type == PriorityQueueType.Max ? selector(items[i]).CompareTo(selector(item)) > 0 : selector(items[i]).CompareTo(selector(item)) < 0)
					items[i + 1] = items[i];
				else
					break;
			}
			return i + 1;
		}
		#endregion
	}
}
