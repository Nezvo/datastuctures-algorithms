using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
	public static class StringManipulationAlgorithms
	{
		public static string ReverseUsingStack(this string input)
		{
			if (input == null)
				return string.Empty;

			var stack = new Stack<char>();

			foreach (char ch in input.ToCharArray())
				stack.Push(ch);

			var reversed = new StringBuilder();
			while (stack.Any())
				reversed.Append(stack.Pop());

			return reversed.ToString();
		}

		public static char? FindFirstNonRepeatingChar(this string input)
		{
			if (input == null)
				return null;

			var hashTable = new Dictionary<char, int>(20);

			var chars = input.ToCharArray();
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

		public static char? FindFirstRepeatedChar(this string input)
		{
			if (input == null)
				return null;

			var set = new HashSet<char>();

			foreach (var ch in input.ToCharArray())
			{
				if (set.Contains(ch))
					return ch;

				set.Add(ch);
			}

			return null;
		}

		public static int CountVowels(this string input)
		{
			if (input == null)
				return 0;

			var vowelsSet = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
			var count = 0;

			foreach (var ch in input.ToLower())
				if (vowelsSet.Contains(ch))
					count++;

			return count;
		}

		public static string ReverseUsingIteration(this string input)
		{
			if (input == null)
				return string.Empty;

			var reversedString = new StringBuilder();

			for (int i = input.Length - 1; i >= 0; i--)
				reversedString.Append(input[i]);

			return reversedString.ToString();
		}

		public static string ReverseUsingArray(this string input)
		{
			if (input == null)
				return string.Empty;

			var inputArray = input.ToCharArray();
			Array.Reverse(inputArray);

			return new string(inputArray);
		}

		public static string ReverseWords(this string input)
		{
			if (input == null)
				return string.Empty;

			var words = input.Trim().Split(' ');
			Array.Reverse(words);

			return string.Join(" ", words);
		}

		public static bool IsRotationOf(this string input1, string input2) => input1 != null && input2 != null && (input1.Length == input2.Length) && $"{input1}{input1}".Contains(input2);

		public static string RemoveDuplicates(this string input)
		{
			if (input == null)
				return string.Empty;

			var output = new StringBuilder();
			var seen = new HashSet<char>();
			foreach (var ch in input)
				if (!seen.Contains(ch))
				{
					seen.Add(ch);
					output.Append(ch);
				}

			return output.ToString();
		}
	}
}
