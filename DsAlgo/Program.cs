using Algorithms;
using DataStructures;
using Demo;
using Demo.Helpers;
using System;
using System.Collections.Generic;

namespace DsAlgo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Sorting Algorithms");
			int[] numbers = {7, 1, 5, 9, 30, 4, 12, 3, 13, 3, 1, 8};
			numbers.BubbleSort();
			Console.WriteLine("BubbleSort: \t" + string.Join(", ", numbers));

			numbers = new int[] { 7, 1, 5, 9, 8, 4, 12, 3, 13, 3, 30, 1};
			numbers.SelectionSort();
			Console.WriteLine("SelectionSort: \t" + string.Join(", ", numbers));

			numbers = new int[] { 7, 1, 5, 9, 8, 4, 12, 30, 13, 3, 1, 3};
			numbers.InsertionSort();
			Console.WriteLine("InsertionSort: \t" + string.Join(", ", numbers));

			numbers = new int[] { 30, 1, 5, 9, 8, 4, 12, 3, 13, 3, 1, 7};
			numbers.MergeSort();
			Console.WriteLine("MergeSort: \t" + string.Join(", ", numbers));

			numbers = new int[] { 7, 1, 30, 9, 8, 4, 12, 3, 13, 3, 1, 5};
			numbers.QuickSort();
			Console.WriteLine("QuickSort: \t" + string.Join(", ", numbers));

			numbers = new int[] { 7, 1, 5, 9, 8, 4, 30, 3, 13, 3, 1, 12};
			numbers.CountingSort();
			Console.WriteLine("CountingSort: \t" + string.Join(", ", numbers));

			numbers = new int[] { 7, 1, 5, 9, 8, 4, 12, 3, 13, 3, 1, 30};
			numbers.BucketSort();
			Console.WriteLine("BucketSort: \t" + string.Join(", ", numbers));

			Console.WriteLine($"First non repeating char in cactus: {CharFinder.FindFirstNonRepeatingChar("cactus")}");

			Console.WriteLine($"First repeating char in banana: {CharFinder.FindFirstRepeatedChar("banana")}");

			Console.WriteLine($"Reverser string of potato is {StringReverser.Reverse("potato")}");

			Console.WriteLine($"Expression '((x + y) = (y + x)' is {(Expression.IsBalanced("((x + y) = (y + x)") ? "balanced" : "not balanced")}");

			var array = new int[] { 1, 2, 3, 4, 5, 6 };
			Console.Write($"Heapified [{string.Join(", ", array)}] is ");
			Heap<int, int>.Heapify(array, (i) => i);
			Console.WriteLine($"[{ string.Join(", ", array)}]");

			var queue = new Queue<string>();
			queue.Enqueue("1.1");
			queue.Enqueue("2.2");
			queue.Enqueue("3.3");
			Console.Write($"Reversed queue [{string.Join(", ", queue.ToArray())}] is ");
			QueueReverser<string>.Reverse(queue, queue.Count);
			Console.WriteLine($"[{string.Join(", ", queue.ToArray())}]");
		}
	}
}
