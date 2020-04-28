using DataStructures;
using System.Collections.Generic;

namespace Demo
{
	public static class CharFinder
	{
		public static char? FindFirstNonRepeatingChar(string str)
		{
			var hashTable = new HashTableWithLinearProbing<char, int>(20);

			var chars = str.ToCharArray();
			foreach (var ch in chars)
			{
				var count = hashTable.Contains(ch) ? hashTable.Get(ch) : 0;
				hashTable.Add(ch, count + 1);
			}

			foreach (var ch in chars)
				if (hashTable.Get(ch) == 1)
					return ch;

			return null;
		}

		public static char? FindFirstRepeatedChar(string str)
		{
			var set = new HashSet<char>(20);

			foreach (var ch in str.ToCharArray())
			{
				if (set.Contains(ch))
					return ch;

				set.Add(ch);
			}

			return null;
		}
	}
}
