
namespace StringValidation.Library.Helper
{
	public static class StringValidationHelper
	{
		public static async Task<bool> IsValidInput(string input)
		{
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var stack = new Stack<char>();

            foreach (var c in input)
            {
                if (c == '(' || c == '[' || c == '{')
                {
                    stack.Push(c);
                }
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }

                    var top = stack.Pop();
                    if (!IsMatchingPair(top, c))
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }

        private static bool IsMatchingPair(char open, char close)
        {
            return (open == '(' && close == ')') ||
                   (open == '[' && close == ']') ||
                   (open == '{' && close == '}');
        }
    }
}

