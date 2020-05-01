using System;

namespace DataStructures
{
	/// <summary>
	/// Hash table implementation using linked lists to handle collisions
	/// </summary>
	/// <typeparam name="TKey">Key tyoe that must implement IComparable</typeparam>
	/// <typeparam name="TValue">Value Type</typeparam>
	public class HashTableWithChaining<TKey, TValue> where TKey : IComparable
	{
		#region Internals and properties
		private readonly System.Collections.Generic.LinkedList<Entry>[] entries;

		public HashTableWithChaining(int size)
		{
			entries = new System.Collections.Generic.LinkedList<Entry>[size];
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

			GetOrCreateBucket(key).AddLast(new Entry(key, value));
		}

		public TValue Get(TKey key)
		{
			var entry = GetEntry(key);

			return (entry == null) ? default : entry.Value;
		}

		public void Remove(TKey key)
		{
			var entry = GetEntry(key);
			if (entry == null)
				return;
			GetBucket(key).Remove(entry);
		}

		public bool Contains(TKey key) => GetEntry(key) != null;
		#endregion

		#region Private methods
		private System.Collections.Generic.LinkedList<Entry> GetBucket(TKey key) => entries[Hash(key)];

		private System.Collections.Generic.LinkedList<Entry> GetOrCreateBucket(TKey key)
		{
			var index = Hash(key);
			var bucket = entries[index];
			if (bucket == null)
				bucket = entries[index] = new System.Collections.Generic.LinkedList<Entry>();

			return bucket;
		}

		private Entry GetEntry(TKey key)
		{
			var bucket = GetBucket(key);
			if (bucket != null)
			{
				foreach (var entry in bucket)
				{
					if (entry.Key.CompareTo(key) == 0)
						return entry;
				}
			}
			return null;
		}

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
