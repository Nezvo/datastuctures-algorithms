using System.Collections.Generic;
using System.Linq;

namespace Demo.Helpers
{
	public static class Expression
	{
    private static readonly List<char> leftBrackets = new List<char> { '(', '<', '[', '{' };
    private static readonly List<char> rightBrackets = new List<char> { ')', '>', ']', '}' };

    public static bool IsBalanced(string input)
    {
      var stack = new Stack<char>();

      foreach (char ch in input.ToCharArray())
      {
        if (IsLeftBracket(ch))
          stack.Push(ch);

        if (IsRightBracket(ch))
        {
          if (!stack.Any()) return false;

          var top = stack.Pop();
          if (!BracketsMatch(top, ch)) return false;
        }
      }

      return !stack.Any();
    }

    private static bool IsLeftBracket(char ch)
    {
      return leftBrackets.Contains(ch);
    }

    private static bool IsRightBracket(char ch)
    {
      return rightBrackets.Contains(ch);
    }

    private static bool BracketsMatch(char left, char right)
    {
      return leftBrackets.IndexOf(left) == rightBrackets.IndexOf(right);
    }
  }
}
