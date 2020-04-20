using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public static class SortingAlgorithms
	{
		#region Bubble sort
		public static void BubbleSort<T>(this IList<T> collection) where T : IComparable
		{
			for (int i = 0; i < collection.Count; i++)
			{
				bool isSorted = true;
				for (int j = 1; j < collection.Count - i; j++)
					if (collection[j].CompareTo(collection[j - 1]) < 0)
					{
						collection.Swap(j, j - 1);
						isSorted = false;
					}

				if (isSorted)
					return;
			}
		}
		#endregion

		#region Selection sort
		public static void SelectionSort<T>(this IList<T> collection) where T : IComparable
		{
			for (int i = 0; i < collection.Count; i++)
			{
				// We assume that the min item is at index i
				var minIndex = i;
				for (int j = i; j < collection.Count; j++)
					if (collection[j].CompareTo(collection[minIndex]) < 0)
						minIndex = j;
				collection.Swap(minIndex, i);
			}
		}
		#endregion

		#region InsertionSort
		public static void InsertionSort<T>(this IList<T> collection) where T : IComparable
		{
			// We assume that the correct item is in the first position
			for (int i = 1; i < collection.Count; i++)
			{
				var current = collection[i];
				var j = i - 1;
				while (j >= 0 && collection[j].CompareTo(current) > 0)
				{
					collection[j + 1] = collection[j];
					j--;
				}
				collection[j + 1] = current;
			}
		}
		#endregion

		#region Merge sort
		public static void MergeSort<T>(this IList<T> collection) where T : IComparable
		{
			if (collection.Count < 2)
				return;

			// Divide the collection in half
			var middle = collection.Count / 2;

			IList<T> left = new T[middle];
			for (int i = 0; i < middle; i++)
				left[i] = collection[i];

			IList<T> right = new T[collection.Count - middle];
			for (int i = middle; i < collection.Count; i++)
				right[i - middle] = collection[i];

			left.MergeSort();
			right.MergeSort();

			Merge(left, right);

			void Merge(IList<T> leftInternal, IList<T> rightInternal)
			{
				int i = 0, j = 0, k = 0;

				while (i < leftInternal.Count && j < rightInternal.Count)
				{
					if (leftInternal[i].CompareTo(rightInternal[j]) <= 0)
						collection[k++] = leftInternal[i++];
					else
						collection[k++] = rightInternal[j++];
				}

				while (i < leftInternal.Count)
					collection[k++] = leftInternal[i++];

				while (j < rightInternal.Count)
					collection[k++] = rightInternal[j++];
			}
		}
		#endregion

		#region Quick sort
		public static void QuickSort<T>(this IList<T> collection) where T : IComparable
		{
			QuickSort(collection, 0, collection.Count - 1);

			void QuickSort(IList<T> collectionInternal, int start, int end)
			{
				if (start >= end) // If partitioned collection contains single item or is empty we step out of recursion
					return;

				var boundary = Parititon(collectionInternal, start, end);

				QuickSort(collectionInternal, start, boundary - 1);
				QuickSort(collectionInternal, boundary + 1, end);
			}

			int Parititon(IList<T> collectionInternal, int start, int end)
			{
				var pivot = collectionInternal[end];
				var boundary = start - 1;

				for (int i = start; i <= end; i++)
				{
					if (collectionInternal[i].CompareTo(pivot) <= 0)
						collectionInternal.Swap(i, ++boundary);
				}

				return boundary;
			}
		}
		#endregion

		#region Counting sort
		public static void CountingSort(this IList<int> collection)
		{
			var max = GetMax(collection);
			var counts = new int[max + 1];

			foreach (var item in collection)
				counts[item]++;

			int k = 0;
			for (int i = 0; i < counts.Length; i++)
				for (int j = 0; j < counts[i]; j++)
					collection[k++] = i;
		}
		#endregion

		#region Bucket sort
		public static void BucketSort<T>(this IList<T> collection) where T : IComparable
		{
			var numberOfBuckets = Math.Sqrt(collection.Count);
			var i = 0;
			foreach (var bucket in CreateBuckets())
			{
				bucket.Sort();
				bucket.ForEach(item => collection[i++] = item);
			}

			List<List<T>> CreateBuckets()
			{
				var buckets = new List<List<T>>();
				for (int j = 0; j < numberOfBuckets; j++)
					buckets.Add(new List<T>());

				for (int k = 0; k < collection.Count; k++)
					buckets[GetHash(k)].Add(collection[k]);

				return buckets;
			}

			int GetHash(int index)
			{
				return (int)(index / Convert.ToDouble(GetMax(collection)) * (numberOfBuckets - 1));
			}
		}
		#endregion

		#region Helpers
		private static void Swap<T>(this IList<T> collection, int x, int y)
		{
			var temp = collection[x];
			collection[x] = collection[y];
			collection[y] = temp;
		}

		private static T GetMax<T>(IList<T> collection) where T : IComparable
		{
			var max = collection.First();
			foreach (var item in collection)
			{
				if (item.CompareTo(max) > 0)
					max = item;
			}
			return max;
		}
		#endregion
	}
}
