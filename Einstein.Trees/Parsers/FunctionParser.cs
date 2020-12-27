using System.Collections.Generic;
using Einstein.Trees.Trees;

namespace Einstein.Trees.Parsers
{
    internal static class FunctionParser
    {
        public static FunctionTree Parse(TokenReader reader)
        {
            var tree = new FunctionTree();
            ParseFunctionHeader(reader, tree);
            tree.Statements = ParseStatements(reader);
            reader.ExpectKeyword("end");
            return tree;
        }

        private static void ParseFunctionHeader(TokenReader reader, FunctionTree tree)
        {
            reader.ExpectKeyword("function");
            tree.Name = reader.ExpectIdentifier();
            tree.Parameters = VariableParser.ParseParameterList(reader);
            if (!reader.IsOperator(':'))
                return;
            reader.Skip();
            tree.TypeName = reader.ExpectIdentifier();
        }
        
        private static IEnumerable<StatementTree> ParseStatements(TokenReader reader)
        {
            var statements = new List<StatementTree>();
            while (!reader.IsKeyword("end"))
                statements.Add(StatementParser.Parse(reader));
            return statements;
        }
    }
}
