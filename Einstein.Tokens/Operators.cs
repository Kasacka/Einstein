using System.Collections.Generic;

namespace Einstein.Tokens
{
    internal static class Operators
    {
        private static readonly HashSet<char> operators = new()
        {
            '<', '(', ')', ':', ',', '=', '+', '-', '*', '/'
        };

        public static bool IsOperator(char value) =>
            operators.Contains(value);
    }
}
