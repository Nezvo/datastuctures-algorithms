using System;

namespace DataStructures
{
	public class QueueWithArray<T>
	{
		#region Internals and properties
		private readonly T[] items;
		private int rear;
		private int front;
		private int count;

		public QueueWithArray(int size)
		{
			items = new T[size];
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

		public bool IsEmpty() => count == 0;

		public bool IsFull() => count == items.Length;
		#endregion
	}
}
