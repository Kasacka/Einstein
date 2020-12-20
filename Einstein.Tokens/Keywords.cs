using System.Collections.Generic;

namespace Einstein.Tokens
{
    internal static class Keywords
    {
        private static readonly HashSet<string> keywords = new()
        {
            "class", "function", "end"
        };

        public static bool IsKeyword(string value) =>
            keywords.Contains(value);
    }
}
