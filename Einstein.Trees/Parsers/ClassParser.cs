using System.Collections.Generic;
using Einstein.Trees.Trees;

namespace Einstein.Trees.Parsers
{
    internal static class ClassParser
    {
        public static ClassTree Parse(TokenReader reader)
        {
            var tree = new ClassTree();
            var functions = new List<FunctionTree>();
            ParseClassHeader(reader, tree);
            while (reader.IsKeyword("function"))
                functions.Add(FunctionParser.Parse(reader));
            reader.ExpectKeyword("end");
            tree.Functions = functions;
            return tree;
        }

        private static void ParseClassHeader(TokenReader reader, ClassTree tree)
        {
            reader.ExpectKeyword("class");
            tree.Name = reader.ExpectIdentifier();
            tree.Parameters = VariableParser.ParseParameterList(reader);
            if (!reader.IsOperator('<'))
                return;
            reader.Skip();
            tree.SuperName = reader.ExpectIdentifier();
        }
    }
}
