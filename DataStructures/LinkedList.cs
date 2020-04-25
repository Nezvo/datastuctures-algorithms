using System;
using System.Collections.Generic;

namespace DataStructures
{
	public class LinkedList<T> where T : IComparable
	{
		#region Internals and properties
		private Node first;
		private Node last;

		public int Count { get; private set; } 
		#endregion

		#region Public methods
		public void AddLast(T item)
		{
			var node = new Node(item);

			if (IsEmpty())
				first = last = node;
			else
			{
				last.Next = node;
				last = node;
			}

			Count++;
		}

		public void AddFirst(T item)
		{
			var node = new Node(item);

			if (IsEmpty())
				first = last = node;
			else
			{
				node.Next = first;
				first = node;
			}

			Count++;
		}

		public int IndexOf(T item)
		{
			int index = 0;
			var current = first;
			while (current != null)
			{
				if (current.Value.CompareTo(item) == 0)
					return index;

				current = current.Next;
				index++;
			}
			return -1;
		}

		public bool Contains(T item) => IndexOf(item) != -1;

		public void RemoveFirst()
		{
			if (IsEmpty())
				return;

			if (first == last)
				first = last = null;
			else
			{
				var second = first.Next;
				first.Next = null;
				first = second;
			}

			Count--;
		}

		public void RemoveLast()
		{
			if (IsEmpty())
				return;

			if (first == last)
				first = last = null;
			else
			{
				var previous = GetPrevious(last);
				last = previous;
				last.Next = null;
			}

			Count--;
		}

		public T[] ToArray()
		{
			var array = new T[Count];
			var current = first;
			var index = 0;
			while (current != null)
			{
				array[index++] = current.Value;
				current = current.Next;
			}

			return array;
		}

		public List<T> ToList()
		{
			var list = new List<T>();
			var current = first;
			while (current != null)
			{
				list.Add(current.Value);
				current = current.Next;
			}

			return list;
		}

		public void Reverse()
		{
			if (IsEmpty()) return;

			var previous = first;
			var current = first.Next;
			while (current != null)
			{
				var next = current.Next;
				current.Next = previous;
				previous = current;
				current = next;
			}

			last = first;
			last.Next = null;
			first = previous;
		}

		public T GetKthFromTheEnd(int k)
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			var a = first;
			var b = first;
			for (int i = 0; i < k - 1; i++)
			{
				b = b.Next;
				if (b == null)
					throw new ArgumentException();
			}
			while (b != last)
			{
				a = a.Next;
				b = b.Next;
			}
			return a.Value;
		}

		public string GetMiddle()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			var a = first;
			var b = first;
			while (b != last && b.Next != last)
			{
				b = b.Next.Next;
				a = a.Next;
			}

			if (b == last)
				return a.Value.ToString();
			else
				return a.Value + ", " + a.Next.Value;
		}

		public bool HasLoop()
		{
			var slow = first;
			var fast = first;

			while (fast != null && fast.Next != null)
			{
				slow = slow.Next;
				fast = fast.Next.Next;

				if (slow == fast)
					return true;
			}

			return false;
		}
		#endregion

		#region Private methods
		private Node GetPrevious(Node node)
		{
			var current = first;
			while (current != null)
			{
				if (current.Next == node) return current;
				current = current.Next;
			}
			return null;
		}

		private bool IsEmpty() => first == null;
		#endregion

		#region Helper classes
		private class Node
		{
			public T Value { get; set; }
			public Node Next { get; set; }

			public Node(T value)
			{
				Value = value;
			}
		} 
		#endregion
	}
}
