using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
	public static class StringReverser
	{
		public static string Reverse(string input)
		{
			if (input == null)
				return null;

			var stack = new Stack<char>();

			foreach (char ch in input.ToCharArray())
				stack.Push(ch);

			var reversed = new StringBuilder();
			while (stack.Any())
				reversed.Append(stack.Pop());

			return reversed.ToString();
		}
	}
}
