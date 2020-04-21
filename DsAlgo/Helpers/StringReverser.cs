using DataStructures;
using System.Text;

namespace Demo
{
	public static class StringReverser
	{
    public static string Reverse(string input)
    {
      if (input == null)
        return null;

      var stack = new Stack<char>(10);

      foreach (char ch in input.ToCharArray())
        stack.Push(ch);

      var reversed = new StringBuilder();
      while (!stack.IsEmpty())
        reversed.Append(stack.Pop());

      return reversed.ToString();
    }
  }
}
