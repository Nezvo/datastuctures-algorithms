﻿using System;

namespace DataStructures
{
	public class HashTable<TKey, TValue> where TKey : IComparable
	{
		#region Internals and properties
		private readonly System.Collections.Generic.LinkedList<KeyValuePair>[] Entries;

		public HashTable(int length)
		{
			Entries = new System.Collections.Generic.LinkedList<KeyValuePair>[length];
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
		#endregion

		#region Private methods
		private System.Collections.Generic.LinkedList<KeyValuePair> GetBucket(TKey key)
		{
			return Entries[Hash(key)];
		}

		private System.Collections.Generic.LinkedList<KeyValuePair> GetOrCreateBucket(TKey key)
		{
			var index = Hash(key);
			var bucket = Entries[index];
			if (bucket == null)
				bucket = Entries[index] = new System.Collections.Generic.LinkedList<KeyValuePair>();

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

		private long Hash(TKey key)
		{
			return Convert.ToInt64(key) % Entries.Length;
		}
		#endregion

		#region Helper classes
		private class KeyValuePair
		{
			public TKey Key { get; set; }
			public TValue Value { get; set; }

			public KeyValuePair(TKey key, TValue value)
			{
				this.Key = key;
				this.Value = value;
			}
		} 
		#endregion
	}
}
