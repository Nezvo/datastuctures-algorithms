using System;
using System.Linq;

namespace DataStructures
{
	public class Heap<T> where T : IComparable
	{
		#region Internals and properties
		private readonly T[] items;

		public int Count { get; private set; }

		public Heap(int size)
		{
			items = new T[size];
		}

		public override string ToString() => $"[{string.Join(", ", items.Take(Count))}]";
		#endregion

		#region Public methods
		public void Add(T value)
		{
			if (IsFull())
				throw new InvalidOperationException();

			items[Count++] = value;

			BubbleUp();
		}

		public T Remove()
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

		public T Max()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			return items[0];
		}

		public static bool IsMaxHeap(T[] array) => IsMaxHeap(array, 0);
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

			return (LeftChild(index).CompareTo(RightChild(index)) > 0) ?
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

			var isValid = items[index].CompareTo(LeftChild(index)) >= 0;

			if (HasRightChild(index))
				isValid &= items[index].CompareTo(RightChild(index)) >= 0;

			return isValid;
		}

		private T RightChild(int index)
		{
			return items[RightChildIndex(index)];
		}

		private T LeftChild(int index)
		{
			return items[LeftChildIndex(index)];
		}

		private int LeftChildIndex(int index)
		{
			return index * 2 + 1;
		}

		private int RightChildIndex(int index)
		{
			return index * 2 + 2;
		}

		private void BubbleUp()
		{
			var index = Count - 1;
			while (index > 0 && items[index].CompareTo(items[ParentIndex(index)]) > 0)
			{
				Swap(index, ParentIndex(index));
				index = ParentIndex(index);
			}
		}

		private int ParentIndex(int index)
		{
			return (index - 1) / 2;
		}

		private void Swap(int first, int second)
		{
			var temp = items[first];
			items[first] = items[second];
			items[second] = temp;
		}

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
		#endregion
	}
}
