using System;
using System.Linq;

namespace DataStructures
{
	public class Heap<TSource, TKey> where TKey : IComparable
	{
		#region Internals and properties
		private readonly TSource[] items;
		private readonly Func<TSource, TKey> selector;

		public int Count { get; private set; }

		public Heap(int size, Func<TSource, TKey> selector)
		{
			items = new TSource[size];
			this.selector = selector;
		}

		public override string ToString() => $"[{string.Join(", ", items.Take(Count))}]";
		#endregion

		#region Public methods
		public void Add(TSource value)
		{
			if (IsFull())
				throw new InvalidOperationException();

			items[Count++] = value;

			BubbleUp();
		}

		public TSource Remove()
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

		public TSource Max()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			return items[0];
		}

		public TSource GetKthLargest(int k)
		{
			if (k < 1 || k > Count)
				throw new ArgumentOutOfRangeException();

			var heap = new Heap<TSource, TKey>(items.Length, this.selector);
			foreach (var number in items)
				heap.Add(number);

			for (var i = 0; i < k - 1; i++)
				heap.Remove();

			return heap.Max();
		}

		public static bool IsMaxHeap(TSource[] array, Func<TSource, TKey> selector) => IsMaxHeap(array, 0, selector);

		public static void Heapify(TSource[] array, Func<TSource, TKey> selector)
		{
			var lastParentIndex = array.Length / 2 - 1;
			for (var i = lastParentIndex; i >= 0; i--)
				Heapify(array, i, selector);
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

			return (selector(LeftChild(index)).CompareTo(selector(RightChild(index))) > 0) ?
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

			var isValid = selector(items[index]).CompareTo(selector(LeftChild(index))) >= 0;

			if (HasRightChild(index))
				isValid &= selector(items[index]).CompareTo(selector(RightChild(index))) >= 0;

			return isValid;
		}

		private TSource RightChild(int index) => items[RightChildIndex(index)];

		private TSource LeftChild(int index) => items[LeftChildIndex(index)];

		private int LeftChildIndex(int index) => index * 2 + 1;

		private int RightChildIndex(int index) => index * 2 + 2;

		private void BubbleUp()
		{
			var index = Count - 1;
			while (index > 0 && selector(items[index]).CompareTo(selector(items[ParentIndex(index)])) > 0)
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

		private static bool IsMaxHeap(TSource[] array, int index, Func<TSource, TKey> selector)
		{
			// All leaf nodes are valid
			var lastParentIndex = (array.Length - 2) / 2;
			if (index > lastParentIndex)
				return true;

			var leftChildIndex = index * 2 + 1;
			var rightChildIndex = index * 2 + 2;

			var isValidParent =
					selector(array[index]).CompareTo(selector(array[leftChildIndex])) >= 0 &&
					selector(array[index]).CompareTo(selector(array[rightChildIndex])) >= 0;

			return isValidParent &&
							IsMaxHeap(array, leftChildIndex, selector) &&
							IsMaxHeap(array, rightChildIndex, selector);
		}

		private static void Heapify(TSource[] array, int index, Func<TSource, TKey> selector)
		{
			var largerIndex = index;

			var leftIndex = index * 2 + 1;
			if (leftIndex < array.Length &&
					selector(array[leftIndex]).CompareTo(selector(array[largerIndex])) > 0)
				largerIndex = leftIndex;

			var rightIndex = index * 2 + 2;
			if (rightIndex < array.Length &&
				selector(array[rightIndex]).CompareTo(selector(array[largerIndex])) > 0)
				largerIndex = rightIndex;

			if (index == largerIndex)
				return;

			Swap(array, index, largerIndex);
			Heapify(array, largerIndex, selector);
		}

		private static void Swap(TSource[] array, int first, int second)
		{
			var temp = array[first];
			array[first] = array[second];
			array[second] = temp;
		}
		#endregion
	}
}
