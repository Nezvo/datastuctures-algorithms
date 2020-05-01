using System;

namespace DataStructures
{
	/// <summary>
	/// Queue implementation using linked list
	/// </summary>
	/// <typeparam name="T">Type of items that will be added to the queue</typeparam>
	public class QueueWithLinkedList<T>
	{
		#region Internals and properties
		private Node head;
		private Node tail;

		public int Count { get; private set; } 
		#endregion

		#region Public methods
		public void Enqueue(T item)
		{
			var node = new Node(item);

			if (IsEmpty())
				head = tail = node;
			else
			{
				tail.Next = node;
				tail = node;
			}

			Count++;
		}

		public T Dequeue()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			T value;
			if (head == tail)
			{
				value = head.Value;
				head = tail = null;
			}
			else
			{
				value = head.Value;
				var second = head.Next;
				head.Next = null;
				head = second;
			}

			Count--;

			return value;
		}

		public T Peek()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			return head.Value;
		}

		public bool IsEmpty() => head == null;
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
