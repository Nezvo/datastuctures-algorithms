using System;

namespace DataStructures
{
	public class PriorityQueueWithHeap<T> where T : IComparable
	{
		private Heap<T> heap;

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
	}
}
