using System;

namespace DataStructures
{
	public class HashTableWithChaining<TKey, TValue> where TKey : IComparable
	{
		#region Internals and properties
		private readonly System.Collections.Generic.LinkedList<KeyValuePair>[] entries;

		public HashTableWithChaining(int size)
		{
			entries = new System.Collections.Generic.LinkedList<KeyValuePair>[size];
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

			GetOrCreateBucket(key).AddLast(new KeyValuePair(key, value));
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
		private System.Collections.Generic.LinkedList<KeyValuePair> GetBucket(TKey key) => entries[Hash(key)];

		private System.Collections.Generic.LinkedList<KeyValuePair> GetOrCreateBucket(TKey key)
		{
			var index = Hash(key);
			var bucket = entries[index];
			if (bucket == null)
				bucket = entries[index] = new System.Collections.Generic.LinkedList<KeyValuePair>();

			return bucket;
		}

		private KeyValuePair GetEntry(TKey key)
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
		private class KeyValuePair
		{
			public TKey Key { get; set; }
			public TValue Value { get; set; }

			public KeyValuePair(TKey key, TValue value)
			{
				Key = key;
				Value = value;
			}
		} 
		#endregion
	}
}
