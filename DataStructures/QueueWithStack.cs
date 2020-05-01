using System;

namespace DataStructures
{
	/// <summary>
	/// Queue implementation using stack
	/// </summary>
	/// <typeparam name="T">Type of items that will be added to the queue</typeparam>
	public class QueueWithStack<T>
	{
		#region Internals and properties
		private readonly Stack stack1;
		private readonly Stack stack2;

		public QueueWithStack(int size)
		{
			stack1 = new Stack(size);
			stack2 = new Stack(size);
		} 
		#endregion

		#region Public methods
		public void Enqueue(T item) => stack1.Push(item);

		public T Dequeue()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			MoveStack1ToStack2();

			return (T)stack2.Pop();
		}

		public T Peek()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			MoveStack1ToStack2();

			return (T)stack2.Peek();
		}

		public bool IsEmpty() => stack1.IsEmpty() && stack2.IsEmpty();
		#endregion

		#region Private methods
		private void MoveStack1ToStack2()
		{
			if (stack2.IsEmpty())
				while (!stack1.IsEmpty())
					stack2.Push(stack1.Pop());
		}
		#endregion
	}
}
