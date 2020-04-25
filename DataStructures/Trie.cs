using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
	public class Trie
	{
		#region Internals and properties
		private readonly Node root = new Node(' ');
		#endregion

		#region Public methods
		public void Add(string word)
		{
			var current = root;
			foreach (var ch in word.ToCharArray())
			{
				if (!current.HasChild(ch))
					current.AddChild(ch);
				current = current.GetChild(ch);
			}
			current.IsEndOfWord = true;
		}

		public bool Contains(string word)
		{
			if (word == null)
				return false;

			var current = root;
			foreach (var ch in word.ToCharArray())
			{
				if (!current.HasChild(ch))
					return false;
				current = current.GetChild(ch);
			}
			return current.IsEndOfWord;
		}

		public void Traverse()
		{
			Traverse(root);
		}

		public void Remove(string word)
		{
			if (word == null)
				return;

			Remove(root, word, 0);
		}

		public List<string> FindWords(string prefix)
		{
			var words = new List<string>();
			var lastNode = FindLastNodeOf(prefix);
			FindWords(lastNode, prefix, words);

			return words;
		}

		public bool ContainsRecursive(string word)
		{
			if (word == null)
				return false;

			return ContainsRecursive(root, word, 0);
		}

		public int CountWords()
		{
			return CountWords(root);
		}

		public void PrintWords()
		{
			PrintWords(root, "");
		}

		public static string LongestCommonPrefix(string[] words)
		{
			if (words == null)
				return "";

			var trie = new Trie();
			foreach (var word in words)
				trie.Add(word);

			var prefix = new StringBuilder();
			var maxChars = GetShortest(words).Length;
			var current = trie.root;
			while (prefix.Length < maxChars)
			{
				var children = current.GetChildren();
				if (children.Length != 1)
					break;
				current = children[0];
				prefix.Append(current.Value);
			}

			return prefix.ToString();
		}
		#endregion

		#region Private methods
		private void Traverse(Node node)
		{
			foreach (var child in node.GetChildren())
				Traverse(child);

			// Post-order: visit the node last
			Console.WriteLine(node.Value);
		}

		private void Remove(Node node, string word, int index)
		{
			if (index == word.Length)
			{
				node.IsEndOfWord = false;
				return;
			}

			var ch = word[index];
			var child = node.GetChild(ch);
			if (child == null)
				return;

			Remove(child, word, index + 1);

			if (!child.HasChildren() && !child.IsEndOfWord)
				node.RemoveChild(ch);
		}

		private void FindWords(Node node, string prefix, List<string> words)
		{
			if (node == null)
				return;

			if (node.IsEndOfWord)
				words.Add(prefix);

			foreach (var child in node.GetChildren())
				FindWords(child, prefix + child.Value, words);
		}

		private Node FindLastNodeOf(string prefix)
		{
			if (prefix == null)
				return null;

			var current = root;
			foreach (var ch in prefix.ToCharArray())
			{
				var child = current.GetChild(ch);
				if (child == null)
					return null;
				current = child;
			}
			return current;
		}

		private bool ContainsRecursive(Node node, string word, int index)
		{
			// Base condition
			if (index == word.Length)
				return node.IsEndOfWord;

			if (node == null)
				return false;

			var ch = word[index];
			var child = node.GetChild(ch);
			if (child == null)
				return false;

			return ContainsRecursive(child, word, index + 1);
		}

		private int CountWords(Node node)
		{
			var total = 0;

			if (node.IsEndOfWord)
				total++;

			foreach (var child in node.GetChildren())
				total += CountWords(child);

			return total;
		}

		private void PrintWords(Node node, string word)
		{
			if (node.IsEndOfWord)
				Console.WriteLine(word);

			foreach (var child in node.GetChildren())
				PrintWords(child, word + child.Value);
		}

		private static string GetShortest(string[] words)
		{
			if (words == null || words.Length == 0)
				return "";

			var shortest = words[0];
			for (var i = 1; i < words.Length; i++)
			{
				if (words[i].Length < shortest.Length)
					shortest = words[i];
			}

			return shortest;
		}
		#endregion

		#region Helper classes
		private class Node
		{
			public char Value { get; set; }
			public Dictionary<char, Node> Children { get; set; }
			public bool IsEndOfWord { get; set; }

			public Node(char value)
			{
				Value = value;
				Children = new Dictionary<char, Node>();
			}

			public bool HasChild(char ch)
			{
				return Children.ContainsKey(ch);
			}

			public void AddChild(char ch)
			{
				Children.Add(ch, new Node(ch));
			}

			public Node GetChild(char ch)
			{
				return Children[ch];
			}

			public Node[] GetChildren()
			{
				return Children.Values.ToArray();
			}

			public bool HasChildren()
			{
				return Children.Any();
			}

			public void RemoveChild(char ch)
			{
				Children.Remove(ch);
			}
		}
		#endregion
	}
}
