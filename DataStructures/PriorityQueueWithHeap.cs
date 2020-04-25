using DataStructures.Helpers;
using System;

namespace DataStructures
{
	public class PriorityQueueWithHeap<TSource, TKey> where TKey : IComparable
	{
		#region Internals and properties
		private readonly PriorityQueueType type;
		private readonly Heap<TSource, TKey> maxHeap;
		private readonly MinHeap<TSource, TKey> minHeap;
		#endregion

		#region Public methods
		public PriorityQueueWithHeap(int size, Func<TSource, TKey> selector, PriorityQueueType type = PriorityQueueType.Max)
		{
			this.type = type;
			maxHeap = new Heap<TSource, TKey>(size, selector);
			minHeap = new MinHeap<TSource, TKey>(size, selector);
		}

		public void Enqueue(TSource item)
		{
			maxHeap.Add(item);
			minHeap.Add(item);
		}

		public TSource Dequeue() => type == PriorityQueueType.Max ? maxHeap.Remove() : minHeap.Remove();

		public bool IsEmpty() => maxHeap.IsEmpty();
		#endregion
	}
}
