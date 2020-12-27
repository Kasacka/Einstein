using System;
using System.Collections.Generic;
using Einstein.Trees.Trees;

namespace Einstein.Trees.Parsers
{
    internal static class VariableParser
    {
        public static VariableTree ParseEnd(TokenReader reader)
        {
            var tree = new VariableTree
            {
                Name = reader.ExpectIdentifier()
            };
            reader.ExpectOperator(':');
            tree.TypeName = reader.ExpectIdentifier();
            return tree;
        }
        
        public static IEnumerable<VariableTree> ParseParameterList(TokenReader reader)
        {
            if (reader.IsOperator('('))
                return ParseEnclosedParameterList(reader);
            else
                return Array.Empty<VariableTree>();
        }

        private static IEnumerable<VariableTree> ParseEnclosedParameterList(TokenReader reader)
        {
            reader.ExpectOperator('(');
            var parameters = new List<VariableTree>();
            var firstParameter = true;
            while (!reader.IsOperator(')'))
            {
                if (!firstParameter)
                {
                    reader.ExpectOperator(',');
                }
                else
                {
                    firstParameter = false;
                }
                parameters.Add(ParseEnd(reader));
            }
            reader.ExpectOperator(')');
            return parameters;
        }
    }
}
