using System;

namespace DataStructures
{
	public class QueueWithArray<T>
	{
		#region Internals and properties
		private T[] items;
		private int rear;
		private int front;
		private int count;

		public QueueWithArray(int capacity)
		{
			items = new T[capacity];
		}
		#endregion

		#region Public methods
		public void Enqueue(T item)
		{
			if (IsFull())
				throw new InvalidOperationException();

			items[rear] = item;
			rear = (rear + 1) % items.Length;
			count++;
		}

		public T Dequeue()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			var item = items[front];
			items[front] = default;
			front = (front + 1) % items.Length;
			count--;

			return item;
		}

		public T Peek()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			return items[front];
		}

		public bool IsEmpty()
		{
			return count == 0;
		}

		public bool IsFull()
		{
			return count == items.Length;
		} 
		#endregion
	}
}
