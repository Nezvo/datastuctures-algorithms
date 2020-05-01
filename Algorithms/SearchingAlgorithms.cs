using System;
using System.Collections.Generic;

namespace Algorithms
{
	public static class SearchingAlgorithms
	{
		#region Linear search
		/// <summary>
		/// Find <paramref name="item"> in the collection using linear search algorithm.
		/// Collection needs to be sorted.
		/// Complexity: O(n)
		/// </summary>
		/// <typeparam name="T">Type that implements IComparable</typeparam>
		/// <param name="collection">IList collection</param>
		/// <param name="item">item that you want to find</param>
		/// <returns>Index of an item, -1 if it doesn't exist</returns>
		public static int LinearSearch<T>(this IList<T> collection, T item) where T : IComparable
		{
			for (int i = 0; i < collection.Count; i++)
				if (collection[i].CompareTo(item) == 0)
					return i;

			return -1;
		}
		#endregion

		#region Binary search
		/// <summary>
		/// Find <paramref name="item"> in the collection using binary search algorithm.
		/// Collection needs to be sorted.
		/// Complexity: O(log n)
		/// </summary>
		/// <typeparam name="T">Type that implements IComparable</typeparam>
		/// <param name="collection">IList collection</param>
		/// <param name="item">item that you want to find</param>
		/// <returns>Index of an item, -1 if it doesn't exist</returns>
		public static int BinarySearch<T>(this IList<T> collection, T item) where T : IComparable => BinarySearch(collection, item, 0, collection.Count - 1);

		private static int BinarySearch<T>(IList<T> collection, T item, int start, int end) where T : IComparable
		{
			if (end < start)
				return -1;

			var middle = (start + end) / 2;
			if (collection[middle].CompareTo(item) == 0)
				return middle;

			if (collection[middle].CompareTo(item) > 0)
				return BinarySearch(collection, item, middle + 1, end);

			return BinarySearch(collection, item, start, middle - 1);
		}
		#endregion

		#region Ternary search
		/// <summary>
		/// Find <paramref name="item"> in the collection using ternary search algorithm.
		/// Collection needs to be sorted.
		/// Complexity: O(log3 n)
		/// </summary>
		/// <typeparam name="T">Type that implements IComparable</typeparam>
		/// <param name="collection">IList collection</param>
		/// <param name="item">item that you want to find</param>
		/// <returns>Index of an item, -1 if it doesn't exist</returns>
		public static int TernarySearch<T>(this IList<T> collection, T item) where T : IComparable => TernarySearch(collection, item, 0, collection.Count - 1);

		private static int TernarySearch<T>(IList<T> collection, T item, int start, int end) where T : IComparable
		{
			if (end < start)
				return -1;

			var partitionSize = (end - start) / 3;
			var mid1 = start + partitionSize;
			var mid2 = end - partitionSize;

			if (collection[mid1].CompareTo(item) == 0)
				return mid1;
			if (collection[mid2].CompareTo(item) == 0)
				return mid2;

			if (item.CompareTo(collection[mid1]) < 0)
				return TernarySearch(collection, item, start, mid1 - 1);
			if (item.CompareTo(collection[mid2]) > 0)
				return TernarySearch(collection, item, mid2 + 1, end);

			return TernarySearch(collection, item, mid1 + 1, mid2 - 1);
		}
		#endregion

		#region Jump search
		/// <summary>
		/// Find <paramref name="item"> in the collection using jump search algorithm.
		/// Collection needs to be sorted.
		/// Complexity: O(sqrt(n))
		/// </summary>
		/// <typeparam name="T">Type that implements IComparable</typeparam>
		/// <param name="collection">IList collection</param>
		/// <param name="item">item that you want to find</param>
		/// <returns>Index of an item, -1 if it doesn't exist</returns>
		public static int JumpSearch<T>(this IList<T> collection, T item) where T : IComparable
		{
			int blockSize = (int)Math.Sqrt(collection.Count);
			int start = 0;
			int next = blockSize;

			while (start.CompareTo(collection.Count) < 0 && collection[next - 1].CompareTo(item) < 0)
			{
				start = next;
				next += blockSize;
				if (next.CompareTo(collection.Count) > 0)
					next = collection.Count;
			}

			for (int i = start; i < next; i++)
				if (collection[i].CompareTo(item) == 0)
					return i;

			return -1;
		}
		#endregion

		#region Exponential search
		/// <summary>
		/// Find <paramref name="item"> in the collection using exponential search algorithm.
		/// Collection needs to be sorted.
		/// Complexity: O(log i)
		/// </summary>
		/// <typeparam name="T">Type that implements IComparable</typeparam>
		/// <param name="collection">IList collection</param>
		/// <param name="item">item that you want to find</param>
		/// <returns>Index of an item, -1 if it doesn't exist</returns>
		public static int ExponentialSearch<T>(this IList<T> collection, T item) where T : IComparable
		{
			int bound = 1;
			while (collection[bound].CompareTo(item) < 0 && bound < collection.Count)
			{
				bound *= 2;
			}

			return BinarySearch(collection, item, bound / 2, Math.Min(bound, collection.Count - 1));
		}
		#endregion
	}
}
