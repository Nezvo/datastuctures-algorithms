using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public static class QueueReverser<T>
	{
		/// <summary>
		/// Reverser the order of the <paramref name="k"/> items in the queue.
		/// </summary>
		/// <param name="queue">Queue that you want to reverse</param>
		/// <param name="k">Number of items that you want to reverse</param>
		public static void Reverse(Queue<T> queue, int k)
		{
			if (k < 0 || k > queue.Count)
				throw new ArgumentOutOfRangeException();

			var stack = new Stack<T>();

			// Dequeue the first K elements from the queue
			// and push them onto the stack
			for (int i = 0; i < k; i++)
				stack.Push(queue.Dequeue());

			// Enqueue the content of the stack at the
			// back of the queue
			while (stack.Any())
				queue.Enqueue(stack.Pop());

			// Add the remaining items in the queue (items
			// after the first K elements) to the back of the
			// queue and remove them from the beginning of the queue
			for (int i = 0; i < queue.Count - k; i++)
				queue.Enqueue(queue.Dequeue());
		}
	}
}
