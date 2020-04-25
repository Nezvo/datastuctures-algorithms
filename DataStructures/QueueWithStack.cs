using System;

namespace DataStructures
{
	public class QueueWithStack<T>
	{
		#region Internals and properties
		private readonly Stack<T> stack1;
		private readonly Stack<T> stack2;

		public QueueWithStack(int size)
		{
			stack1 = new Stack<T>(size);
			stack2 = new Stack<T>(size);
		} 
		#endregion

		#region Public methods
		public void Enqueue(T item) => stack1.Push(item);

		public T Dequeue()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			MoveStack1ToStack2();

			return stack2.Pop();
		}

		public T Peek()
		{
			if (IsEmpty())
				throw new InvalidOperationException();

			MoveStack1ToStack2();

			return stack2.Peek();
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
