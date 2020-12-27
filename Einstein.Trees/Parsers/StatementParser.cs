using System;
using Einstein.Trees.Trees;

namespace Einstein.Trees.Parsers
{
    internal static class StatementParser
    {
        public static StatementTree Parse(TokenReader reader)
        {
            if (reader.IsKeyword("let"))
            {
                return ParseVariableDeclarationStatementTree(reader);
            }
            else
            {
                throw new Exception();
            }
        }

        private static StatementTree ParseVariableDeclarationStatementTree(TokenReader reader)
        {
            var tree = new VariableDeclarationStatementTree();
            reader.ExpectKeyword("let");
            tree.Variable = VariableParser.ParseEnd(reader);
            if (!reader.IsOperator('='))
                return tree;
            reader.Skip();
            tree.Expression = ExpressionParser.Parse(reader);
            return tree;
        }
    }
}