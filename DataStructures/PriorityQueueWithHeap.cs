using DataStructures.Helpers;
using DataStructures.Interfaces;
using System;

namespace DataStructures
{
	/// <summary>
	/// Priority queue implementation using heap.
	/// Objects that will be added to the queue must implement INode
	/// </summary>
	/// <typeparam name="T">Type of Id property in an object that will be added to the priority queue</typeparam>
	public class PriorityQueueWithHeap<T> where T : IComparable
	{
		#region Internals and properties
		private readonly PriorityQueueType type;
		private readonly Heap<T> maxHeap;
		private readonly MinHeap<T> minHeap;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="size">Size of the queue</param>
		/// <param name="type">Enum which indicates whether items with the lowest or
		/// highest priority value will be used to determine maximum priority </param>
		public PriorityQueueWithHeap(int size, PriorityQueueType type = PriorityQueueType.Max)
		{
			this.type = type;
			maxHeap = new Heap<T>(size);
			minHeap = new MinHeap<T>(size);
		}
		#endregion

		#region Public methods
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
