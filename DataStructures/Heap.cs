using DataStructures.Interfaces;
using System;
using System.Linq;

namespace DataStructures
{
	/// <summary>
	/// Max Heap implementation, where item on top will always have the biggest Id.
	/// Object that will be added to the tree must implement INode
	/// </summary>
	/// <typeparam name="T">Type of Id property in an object that will be added to the heap</typeparam>
	public class Heap<T> where T : IComparable
	{
		#region Internals and properties
		private readonly INode<T>[] items;

		public int Count { get; private set; }
		private INode<T>[] Items => items.Take(Count).ToArray();

		public Heap(int size)
		{
			items = new INode<T>[size];
		}
		#endregion

		#region Public methods
		public void Add(INode<T> node)
		{
			if (IsFull())
				throw new InvalidOperationException();

			items[Count++] = node;

			BubbleUp();
		}

		public INode<T> Remove()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			var root = items[0];
			items[0] = items[--Count];

			BubbleDown();

			return root;
		}

		public bool IsEmpty() => Count == 0;

		public bool IsFull() => Count == items.Length;

		public INode<T> Max()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			return items[0];
		}

		public INode<T> GetKthLargest(int k)
		{
			if (k < 1 || k > Count)
				throw new ArgumentOutOfRangeException();

			var heap = new Heap<T>(items.Length);
			foreach (var item in Items)
				heap.Add(item);

			for (var i = 0; i < k - 1; i++)
				heap.Remove();

			return heap.Max();
		}
		#endregion

		#region Private methods
		private void BubbleDown()
		{
			var index = 0;
			while (index <= Count && !IsValidParent(index))
			{
				var largerChildIndex = LargerChildIndex(index);
				Swap(index, largerChildIndex);
				index = largerChildIndex;
			}
		}

		private int LargerChildIndex(int index)
		{
			if (!HasLeftChild(index))
				return index;

			if (!HasRightChild(index))
				return LeftChildIndex(index);

			return (LeftChild(index).Id.CompareTo(RightChild(index).Id) > 0) ?
							LeftChildIndex(index) :
							RightChildIndex(index);
		}

		private bool HasLeftChild(int index)
		{
			return LeftChildIndex(index) <= Count;
		}

		private bool HasRightChild(int index)
		{
			return RightChildIndex(index) <= Count;
		}

		private bool IsValidParent(int index)
		{
			if (!HasLeftChild(index))
				return true;

			var isValid = items[index].Id.CompareTo(LeftChild(index).Id) >= 0;

			if (HasRightChild(index))
				isValid &= items[index].Id.CompareTo(RightChild(index).Id) >= 0;

			return isValid;
		}

		private INode<T> RightChild(int index) => items[RightChildIndex(index)];

		private INode<T> LeftChild(int index) => items[LeftChildIndex(index)];

		private int LeftChildIndex(int index) => index * 2 + 1;

		private int RightChildIndex(int index) => index * 2 + 2;

		private void BubbleUp()
		{
			var index = Count - 1;
			while (index > 0 && items[index].Id.CompareTo(items[ParentIndex(index)].Id) > 0)
			{
				Swap(index, ParentIndex(index));
				index = ParentIndex(index);
			}
		}

		private int ParentIndex(int index) => (index - 1) / 2;

		private void Swap(int first, int second)
		{
			var temp = items[first];
			items[first] = items[second];
			items[second] = temp;
		}
		#endregion

		#region Heapify array
		public static bool IsMaxHeap(T[] array) => IsMaxHeap(array, 0);

		private static bool IsMaxHeap(T[] array, int index)
		{
			// All leaf nodes are valid
			var lastParentIndex = (array.Length - 2) / 2;
			if (index > lastParentIndex)
				return true;

			var leftChildIndex = index * 2 + 1;
			var rightChildIndex = index * 2 + 2;

			var isValidParent =
					array[index].CompareTo(array[leftChildIndex]) >= 0 &&
					array[index].CompareTo(array[rightChildIndex]) >= 0;

			return isValidParent &&
							IsMaxHeap(array, leftChildIndex) &&
							IsMaxHeap(array, rightChildIndex);
		}

		public static void Heapify(T[] array)
		{
			var lastParentIndex = array.Length / 2 - 1;
			for (var i = lastParentIndex; i >= 0; i--)
				Heapify(array, i);
		}

		private static void Heapify(T[] array, int index)
		{
			var largerIndex = index;

			var leftIndex = index * 2 + 1;
			if (leftIndex < array.Length &&
					array[leftIndex].CompareTo(array[largerIndex]) > 0)
				largerIndex = leftIndex;

			var rightIndex = index * 2 + 2;
			if (rightIndex < array.Length &&
				array[rightIndex].CompareTo(array[largerIndex]) > 0)
				largerIndex = rightIndex;

			if (index == largerIndex)
				return;

			Swap(array, index, largerIndex);
			Heapify(array, largerIndex);
		}

		private static void Swap(T[] array, int first, int second)
		{
			var temp = array[first];
			array[first] = array[second];
			array[second] = temp;
		}
		#endregion
	}
}
