using DataStructures.Helpers;
using DataStructures.Interfaces;
using System;

namespace DataStructures
{
	/// <summary>
	/// Priority queue implementation using array.
	/// Objects that will be added to the queue must implement IPriorityQueueItem
	/// </summary>
	public class PriorityQueueWithArray
	{
		#region Internals and properties
		private readonly IPriorityQueueItem[] items;
		private readonly PriorityQueueType type;
		public int Count { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="size">Size of the queue</param>
		/// <param name="type">Enum which indicates whether items with the lowest or
		/// highest priority value will be used to determine maximum priority </param>
		public PriorityQueueWithArray(int size, PriorityQueueType type = PriorityQueueType.Max)
		{
			items = new IPriorityQueueItem[size];
			this.type = type;
		}
		#endregion

		#region Public methods
		public void Enqueue(IPriorityQueueItem item)
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

		public IPriorityQueueItem Dequeue()
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
		private int ShiftItemsToInsert(IPriorityQueueItem item)
		{
			int i;
			for (i = Count - 1; i >= 0; i--)
			{
				if (type == PriorityQueueType.Max ? items[i].Priority > item.Priority : items[i].Priority < item.Priority)
					items[i + 1] = items[i];
				else
					break;
			}
			return i + 1;
		}
		#endregion
	}
}
