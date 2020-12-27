using System;
using System.Linq;
using Einstein.Tokens;

namespace Einstein.Trees
{
    internal sealed class TokenReader
    {
        private readonly Lexer lexer;
        private Token token;

        public TokenReader(string source)
        {
            lexer = new Lexer(source);
            token = lexer.ReadNextToken();  
        }

        public string Value => token.Value;

        public bool IsKeyword(string keyword) =>
            token.Type == TokenType.Keyword && token.Value == keyword;

        public bool IsIdentifier() =>
            token.Type == TokenType.Identifier;

        public bool IsNumber() =>
            token.Type == TokenType.Number;

        public bool IsOperator(char @operator) =>
            token.Type == TokenType.Operator && token.Value == @operator.ToString();

        public bool IsEndOfSource() =>
            !lexer.HasNextToken;

        public string ExpectKeyword(string keyword)
        {
            if (IsKeyword(keyword))
                return Skip();
            else
                throw new Exception();
        }

        public string ExpectIdentifier()
        {
            if (IsIdentifier())
                return Skip();
            else
                throw new Exception();
        }

        public void ExpectEndOfSource()
        {
            if (!IsEndOfSource())
                throw new Exception();
        }

        public string ExpectNumber()
        {
            if (IsNumber())
                return Skip();
            else
                throw new Exception();
        }

        public char ExpectOperator(char @operator)
        {
            if (IsOperator(@operator))
                return Skip().First();
            else
                throw new Exception();
        }

        public string Skip()
        {
            var value = token.Value;
            if (lexer.HasNextToken)
                token = lexer.ReadNextToken();
            return value;
        }
    }
}
