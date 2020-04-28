using System.Collections.Generic;

namespace Demo
{
	public static class CharFinder
	{
		public static char? FindFirstNonRepeatingChar(string str)
		{
			var hashTable = new Dictionary<char, int>(20);

			var chars = str.ToCharArray();
			foreach (var ch in chars)
			{
				if (hashTable.ContainsKey(ch))
					hashTable[ch]++; 
				else
					hashTable.Add(ch, 1);
			}

			foreach (var ch in chars)
				if (hashTable[ch] == 1)
					return ch;

			return null;
		}

		public static char? FindFirstRepeatedChar(string str)
		{
			var set = new HashSet<char>();

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
