using System.Collections.Generic;
using Einstein.Trees.Trees;

namespace Einstein.Trees.Parsers
{
    internal static class CompilationUnitParser
    {
        public static CompilationUnitTree Parse(TokenReader reader)
        {
            var tree = new CompilationUnitTree();
            var classes = new List<ClassTree>();
            while (reader.IsKeyword("class"))
                classes.Add(ClassParser.Parse(reader));
            reader.ExpectEndOfSource();
            tree.Classes = classes;
            return tree;
        }
    }
}
