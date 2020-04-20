﻿using Algorithms;
using DataStructures;
using System;

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

			Console.ReadKey();
		}
	}
}
