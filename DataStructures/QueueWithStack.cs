using System;

namespace DataStructures
{
	public class QueueWithStack<T>
	{
		#region Internals and properties
		private Stack<T> stack1;
		private Stack<T> stack2;

		public QueueWithStack(int length)
		{
			stack1 = new Stack<T>(length);
			stack2 = new Stack<T>(length);
		} 
		#endregion

		#region Public methods
		public void Enqueue(T item)
		{
			stack1.Push(item);
		}

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

		public bool IsEmpty()
		{
			return stack1.IsEmpty() && stack2.IsEmpty();
		}
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
