using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Einstein.Tokens;
using Einstein.Trees.Trees;

namespace Einstein.Trees
{
    public sealed class Parser
    {
        private readonly Lexer lexer;
        private Token token;

        public Parser([NotNull] string source)
        {
            lexer = new Lexer(source);
            token = lexer.ReadNextToken();
        }

        public CompilationUnitTree ParseCompilationUnit()
        {
            var classes = new List<ClassTree>();
            while (IsKeyword("class"))
                classes.Add(ParseClass());
            var tree = new CompilationUnitTree { Classes = classes };
            ExpectEndOfSource();
            return tree;
        }

        private ClassTree ParseClass()
        {
            var functions = new List<FunctionTree>();
            var (name, superName, parameters) = ParseClassHeader();
            while (IsKeyword("function"))
                functions.Add(ParseFunction());
            ExpectKeyword("end");
            return new ClassTree { Name = name, SuperName = superName, Functions = functions, Parameters = parameters };
        }

        private FunctionTree ParseFunction()
        {
            ExpectKeyword("function");
            var name = ExpectIdentifier();
            var parameters = ParseParameterList();
            string typeName = null;
            if (IsOperator(':'))
            {
                Skip();
                typeName = ExpectIdentifier();
            }
            ExpectKeyword("end");
            return new FunctionTree { Name = name, TypeName = typeName, Parameters = parameters };
        }

        private ClassHeader ParseClassHeader()
        {
            ExpectKeyword("class");
            var name = ExpectIdentifier();
            string superName = null;
            var parameters = ParseParameterList();
            if (IsOperator('<'))
            {
                Skip();
                superName = ExpectIdentifier();
            }
            return new(name, superName, parameters);
        }

        private IEnumerable<VariableTree> ParseParameterList()
        {
            if (IsOperator('('))
                return ParseEnclosedParameterList();
            else
                return Array.Empty<VariableTree>();
        }

        private IEnumerable<VariableTree> ParseEnclosedParameterList()
        {
            ExpectOperator('(');
            var parameters = new List<VariableTree>();
            var firstParameter = true;
            while (!IsOperator(')'))
            {
                if (!firstParameter)
                {
                    ExpectOperator(',');
                }
                else
                {
                    firstParameter = false;
                }
                parameters.Add(ParseParameterTree());
            }
            ExpectOperator(')');
            return parameters;
        }

        private VariableTree ParseParameterTree()
        {
            var name = ExpectIdentifier();
            ExpectOperator(':');
            var typeName = ExpectIdentifier();
            return new()
            {
                Name = name,
                TypeName = typeName
            };
        }

        private bool IsKeyword(string keyword) =>
            token.Type == TokenType.Keyword && token.Value == keyword;

        private bool IsIdentifier() =>
            token.Type == TokenType.Identifier;

        private bool IsOperator(char @operator) =>
            token.Type == TokenType.Operator && token.Value == @operator.ToString();

        private bool IsEndOfSource() =>
            !lexer.HasNextToken;

        private string ExpectKeyword(string keyword)
        {
            if (IsKeyword(keyword))
                return Skip();
            else
                throw new Exception();
        }

        private string ExpectIdentifier()
        {
            if (IsIdentifier())
                return Skip();
            else
                throw new Exception();
        }

        private void ExpectEndOfSource()
        {
            if (!IsEndOfSource())
                throw new Exception();
        }

        private string ExpectOperator(char @operator)
        {
            if (IsOperator(@operator))
                return Skip();
            else
                throw new Exception();
        }

        private string Skip()
        {
            var value = token.Value;
            if (lexer.HasNextToken)
                token = lexer.ReadNextToken();
            return value;
        }

        private record ClassHeader(string Name, string SuperName, IEnumerable<VariableTree> Parameters);
    }
}
