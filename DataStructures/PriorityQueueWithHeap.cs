using DataStructures.Helpers;
using DataStructures.Interfaces;
using System;

namespace DataStructures
{
	public class PriorityQueueWithHeap<T> where T : IComparable
	{
		#region Internals and properties
		private readonly PriorityQueueType type;
		private readonly Heap<T> maxHeap;
		private readonly MinHeap<T> minHeap;
		#endregion

		#region Public methods
		public PriorityQueueWithHeap(int size, PriorityQueueType type = PriorityQueueType.Max)
		{
			this.type = type;
			maxHeap = new Heap<T>(size);
			minHeap = new MinHeap<T>(size);
		}

		public void Enqueue(INode<T> item)
		{
			maxHeap.Add(item);
			minHeap.Add(item);
		}

		public INode<T> Dequeue() => type == PriorityQueueType.Max ? maxHeap.Remove() : minHeap.Remove();

		public bool IsEmpty() => maxHeap.IsEmpty();
		#endregion
	}
}
