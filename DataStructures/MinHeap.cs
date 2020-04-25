using System;
using System.Linq;

namespace DataStructures
{
	public class MinHeap<TSource, TKey> where TKey : IComparable
	{
		#region Internals and properties
		private readonly TSource[] items;
		private readonly Func<TSource, TKey> selector;

		public int Count { get; private set; }

		public MinHeap(int size, Func<TSource, TKey> selector)
		{
			items = new TSource[size];
			this.selector = selector;
		}

		public override string ToString() => $"[{string.Join(", ", items.Take(Count))}]";
		#endregion

		#region Public methods
		public void Add(TSource item)
		{
			if (IsFull())
				throw new InvalidOperationException();

			items[Count++] = item;

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
		#endregion

		#region Private methods
		private void BubbleDown()
		{
			var index = 0;
			while (index <= Count && !IsValidParent(index))
			{
				var largerChildIndex = SmallerChildIndex(index);
				Swap(index, largerChildIndex);
				index = largerChildIndex;
			}
		}

		private int SmallerChildIndex(int index)
		{
			if (!HasLeftChild(index))
				return index;

			if (!HasRightChild(index))
				return LeftChildIndex(index);

			return (selector(LeftChild(index)).CompareTo(selector(RightChild(index)))) < 0 ?
							LeftChildIndex(index) :
							RightChildIndex(index);
		}

		private bool HasLeftChild(int index) => LeftChildIndex(index) <= Count;

		private bool HasRightChild(int index) => RightChildIndex(index) <= Count;

		private bool IsValidParent(int index)
		{
			if (!HasLeftChild(index))
				return true;

			var isValid = selector(items[index]).CompareTo(selector(LeftChild(index))) <= 0;

			if (HasRightChild(index))
				isValid &= selector(items[index]).CompareTo(selector(RightChild(index))) <= 0;

			return isValid;
		}

		private TSource RightChild(int index) => items[RightChildIndex(index)];

		private TSource LeftChild(int index) => items[LeftChildIndex(index)];

		private int LeftChildIndex(int index) => index * 2 + 1;

		private int RightChildIndex(int index) => index * 2 + 2;

		private void BubbleUp()
		{
			var index = Count - 1;
			while (index > 0 && selector(items[index]).CompareTo(selector(items[Parent(index)])) < 0)
			{
				Swap(index, Parent(index));
				index = Parent(index);
			}
		}

		private int Parent(int index) => (index - 1) / 2;

		private void Swap(int first, int second)
		{
			var temp = items[first];
			items[first] = items[second];
			items[second] = temp;
		}
		#endregion

		#region Helper classes
		private class Node
		{
			public int Key { get; set; }
			public string Value { get; set; }

			public Node(int key, string value)
			{
				Key = key;
				Value = value;
			}
		}
		#endregion
	}
}
