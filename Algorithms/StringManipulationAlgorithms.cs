using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Algorithms
{
	public static class StringManipulationAlgorithms
	{
		/// <summary>
		/// Reverses string.
		/// </summary>
		/// <param name="input">String that you want to reverse</param>
		/// <returns>Reversed string</returns>
		public static string Reverse(this string input) => input.ReverseUsingIteration();

		private static string ReverseUsingStack(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			var stack = new Stack<char>();

			foreach (char ch in input.ToCharArray())
				stack.Push(ch);

			var reversed = new StringBuilder();
			while (stack.Any())
				reversed.Append(stack.Pop());

			return reversed.ToString();
		}

		private static string ReverseUsingIteration(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			var reversedString = new StringBuilder();

			for (int i = input.Length - 1; i >= 0; i--)
				reversedString.Append(input[i]);

			return reversedString.ToString();
		}

		private static string ReverseUsingArray(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			var inputArray = input.ToCharArray();
			Array.Reverse(inputArray);

			return new string(inputArray);
		}

		/// <summary>
		/// Reverses the order of words in a string.
		/// </summary>
		/// <param name="input">String that you want to perform a operation on</param>
		/// <returns>String with the reversed order of words</returns>
		public static string ReverseWords(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			var words = input.Trim().Split(' ');
			Array.Reverse(words);

			return string.Join(" ", words);
		}

		/// <summary>
		/// Finds first non repeting character inside a string.
		/// </summary>
		/// <param name="input">String that you want to perform search on</param>
		/// <returns>First non repeting character</returns>
		public static char? FindFirstNonRepeatingChar(this string input)
		{
			if (string.IsNullOrEmpty(input))
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

		/// <summary>
		/// Finds first repeated character inside a string.
		/// </summary>
		/// <param name="input">String that you want to perform search on</param>
		/// <returns>First repeated character</returns>
		public static char? FindFirstRepeatedChar(this string input)
		{
			if (string.IsNullOrEmpty(input))
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

		/// <summary>
		/// Counts vowels inside a string.
		/// </summary>
		/// <param name="input">String that you want to count vowels for</param>
		/// <returns>Vowels count</returns>
		public static int CountVowels(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return 0;

			var vowelsSet = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
			var count = 0;

			foreach (var ch in input.ToLower())
				if (vowelsSet.Contains(ch))
					count++;

			return count;
		}

		/// <summary>
		/// Checks if a string is a rotation of target string.
		/// </summary>
		/// <param name="input1">First string that you want to compare</param>
		/// <param name="input2">Second string that you want to compare</param>
		/// <returns></returns>
		public static bool IsRotationOf(this string input1, string input2) => input1 != null && input2 != null && (input1.Length == input2.Length) && $"{input1}{input1}".Contains(input2);

		public static string RemoveDuplicates(this string input)
		{
			if (string.IsNullOrEmpty(input))
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

		/// <summary>
		/// Gets character that occured the most times inside a string.
		/// </summary>
		/// <param name="input">String that you want to perform search on</param>
		/// <returns>Character that occured the most times</returns>
		public static char? GetMaxOccuringChar(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return null;

			const int asciiSize = 255;
			var frequencies = new int[asciiSize];

			foreach (var ch in input)
				frequencies[ch]++;

			var maxIndex = 0;
			for (int i = 0; i < frequencies.Length; i++)
			{
				if (frequencies[i] > frequencies[maxIndex])
					maxIndex = i;
			}

			return (char)maxIndex;
		}

		/// <summary>
		/// Capitalizes every word inside an string.
		/// </summary>
		/// <param name="input">String that you want to perform a operation on</param>
		/// <returns>String with every word capitalized</returns>
		public static string CapitalizeEveryWord(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			var words = Regex.Replace(input.Trim(), @"\s+", " ").Split(' ');
			for (int i = 0; i < words.Length; i++)
				words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1).ToLower();

			return string.Join(" ", words);
		}

		/// <summary>
		/// Checks if a string is an anagram of second string.
		/// </summary>
		/// <param name="input1">First string that you want to compare</param>
		/// <param name="input2">Second string that you want to compare</param>
		/// <returns>True if the second string is an anagram of the first, false otherwise</returns>
		public static bool IsAnagramOf(this string input1, string input2) => AreAnagramUsingHistogramming(input1, input2);

		private static bool AreAnagramUsingSorting(string input1, string input2)
		{
			if (string.IsNullOrEmpty(input1) ||
				string.IsNullOrEmpty(input2) ||
				input1.Length != input2.Length)
				return false;

			var array1 = input1.ToLower().ToCharArray();
			var array2 = input2.ToLower().ToCharArray();
			Array.Sort(array1);
			Array.Sort(array2);

			return Enumerable.SequenceEqual(array1, array2);
		}

		private static bool AreAnagramUsingHistogramming(string input1, string input2)
		{
			if (string.IsNullOrEmpty(input1) ||
				string.IsNullOrEmpty(input2) ||
				input1.Length != input2.Length)
				return false;

			input1 = input1.ToLower();
			input2 = input2.ToLower();

			const int englishAlphabetCount = 26;
			var frequencies = new int[englishAlphabetCount];

			for (int i = 0; i < input1.Length; i++)
				frequencies[input1[i] - 'a']++;

			for (int i = 0; i < input2.Length; i++)
			{
				int index = input1[i] - 'a';

				if (frequencies[index] == 0)
					return false;

				frequencies[index]--;
			}
			return true;
		}

		/// <summary>
		/// Cheks if a string is a palindrome.
		/// </summary>
		/// <param name="input">String that you want to check</param>
		/// <returns>true if string is a palindrome, false otherwise</returns>
		public static bool IsPalindrome(this string input) => IsPalindromeUsingPointers(input);

		private static bool IsPalindromeUsingReverse(string input)
		{
			if (string.IsNullOrEmpty(input))
				return false;

			return Enumerable.SequenceEqual(input, input.Reverse());
		}

		private static bool IsPalindromeUsingPointers(string input)
		{
			if (input == null)
				return false;

			input = input.ToLower();
			int left = 0, right = input.Length - 1;

			while (left < right)
			{
				if (input[left++] != input[right--])
					return false;
			}

			return true;
		}
	}
}
