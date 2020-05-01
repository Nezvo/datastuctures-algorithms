using System;

namespace DataStructures
{
	/// <summary>
	/// Hash table implementation using linear probing to handle collisions
	/// </summary>
	/// <typeparam name="TKey">Key tyoe that must implement IComparable</typeparam>
	/// <typeparam name="TValue">Value Type</typeparam>
	public class HashTableWithLinearProbing<TKey, TValue> where TKey : IComparable
	{
		#region Internals and properties
		private readonly Entry[] entries;
		public int Count { get; private set; }

		public HashTableWithLinearProbing(int size)
		{
			entries = new Entry[size];
		}
		#endregion

		#region Public methods
		public void Add(TKey key, TValue value)
		{
			var entry = GetEntry(key);
			if (entry != null)
			{
				entry.Value = value;
				return;
			}

			if (IsFull())
				throw new InvalidOperationException();

			entries[GetIndex(key)] = new Entry(key, value);
			Count++;
		}

		public TValue Get(TKey key)
		{
			var entry = GetEntry(key);
			return entry != null ? entry.Value : default;
		}

		public void Remove(TKey key)
		{
			var index = GetIndex(key);
			if (index == -1 || entries[index] == null)
				return;

			entries[index] = null;
			Count--;
		}

		public bool Contains(TKey key) => GetIndex(key) != -1;
		#endregion

		#region Private methods
		private Entry GetEntry(TKey key)
		{
			var index = GetIndex(key);
			return index >= 0 ? entries[index] : null;
		}

		private long GetIndex(TKey key)
		{
			int steps = 0;

			// Linear probing algorithm: we keep looking until we find an empty
			// slot or a slot with the same key.

			// We use this loop conditional to prevent an infinite loop that
			// will happen if the array is full and we keep probing with no
			// success. So, the number of steps (or probing attempts) should
			// be less than the size of our table.
			while (steps < entries.Length)
			{
				var index = Index(key, steps++);
				var entry = entries[index];
				if (entry == null || entry.Key.CompareTo(key) == 0)
					return index;
			}

			// This will happen if we looked at every slot in the array
			// and couldn't find a place for this key. That basically means
			// the table is full.
			return -1;
		}

		private bool IsFull() => Count == entries.Length;

		private long Index(TKey key, int i) => (Hash(key) + i) % entries.Length;

		private long Hash(TKey key) => key.GetHashCode() % entries.Length; 
		#endregion

		#region Helper classes
		private class Entry
		{
			public TKey Key { get; set; }
			public TValue Value { get; set; }

			public Entry(TKey key, TValue value)
			{
				Key = key;
				Value = value;
			}
		}
		#endregion
	}
}
