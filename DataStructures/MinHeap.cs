using DataStructures.Interfaces;
using System;
using System.Linq;

namespace DataStructures
{
	/// <summary>
	/// Min Heap implementation, where item on top will always have the lowest Id.
	/// Objects that will be added to the heap must implement INode
	/// </summary>
	/// <typeparam name="T">Type of Id property in an object that will be added to the heap</typeparam>
	public class MinHeap<T> where T : IComparable
	{
		#region Internals and properties
		private readonly INode<T>[] items;

		public int Count { get; private set; }

		public MinHeap(int size)
		{
			items = new INode<T>[size];
		}

		public override string ToString() => $"[{string.Join(", ", items.Take(Count))}]";
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

			return (LeftChild(index).Id.CompareTo(RightChild(index).Id)) < 0 ?
							LeftChildIndex(index) :
							RightChildIndex(index);
		}

		private bool HasLeftChild(int index) => LeftChildIndex(index) <= Count;

		private bool HasRightChild(int index) => RightChildIndex(index) <= Count;

		private bool IsValidParent(int index)
		{
			if (!HasLeftChild(index))
				return true;

			var isValid = items[index].Id.CompareTo(LeftChild(index).Id) <= 0;

			if (HasRightChild(index))
				isValid &= items[index].Id.CompareTo(RightChild(index).Id) <= 0;

			return isValid;
		}

		private INode<T> RightChild(int index) => items[RightChildIndex(index)];

		private INode<T> LeftChild(int index) => items[LeftChildIndex(index)];

		private int LeftChildIndex(int index) => index * 2 + 1;

		private int RightChildIndex(int index) => index * 2 + 2;

		private void BubbleUp()
		{
			var index = Count - 1;
			while (index > 0 && items[index].Id.CompareTo(items[Parent(index)].Id) < 0)
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
	}
}
