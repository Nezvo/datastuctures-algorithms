using System;

namespace DataStructures
{
	public class PriorityQueueWithHeap<T> where T : IComparable
	{
		#region Internals and properties
		private Heap<T> heap;
		#endregion

		#region Public methods
		public PriorityQueueWithHeap(int size)
		{
			heap = new Heap<T>(size);
		}

		public void Enqueue(T item)
		{
			heap.Add(item);
		}

		public T Dequeue()
		{
			return heap.Remove();
		}

		public bool isEmpty() => heap.IsEmpty();
		#endregion
	}
}
