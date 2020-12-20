using System;
using System.Diagnostics.CodeAnalysis;

namespace Einstein.Tokens
{
    public sealed class Lexer : ILexer
    {
        private readonly LexerReader reader;

        public Lexer([NotNull] string source) => 
            reader = new LexerReader(source);

        public bool HasNextToken =>
            !reader.ReachedEnd;

        public Token ReadNextToken()
        {
            SkipWhite();
            if (reader.ReachedEnd)
                return CreateEndOfSourceToken();
            if (reader.IsLetter)
                return ReadKeywordOrIdentifier();
            if (Operators.IsOperator(reader.Current))
                return ReadOperatorToken();
            return ReadUnknownToken();
        }

        private void SkipWhite() =>
            reader.ReadWhile(char.IsWhiteSpace);

        private Token ReadKeywordOrIdentifier()
        {
            var identifier = reader.ReadWhile(char.IsLetterOrDigit);
            if (Keywords.IsKeyword(identifier.Value))
                return CreateKeyword(identifier);
            return CreateIdentifier(identifier);
        }

        private Token ReadOperatorToken()
        {
            var position = reader.Position;
            var value = reader.Skip().ToString();
            return ToToken(value, TokenType.Operator, position);
        }

        private Token ReadUnknownToken()
        {
            var position = reader.Position;
            var value = reader.Skip().ToString();
            return ToToken(value, TokenType.Unknown, position);
        }

        private Token CreateEndOfSourceToken() =>
            ToToken(string.Empty, TokenType.EndOfSource, reader.Position);

        private static Token CreateIdentifier(TokenInfo tokenInfo) =>
            ToToken(tokenInfo, TokenType.Identifier);

        private static Token CreateKeyword(TokenInfo tokenInfo) =>
            ToToken(tokenInfo, TokenType.Keyword);

        private static Token ToToken(TokenInfo tokenInfo, TokenType type) =>
            ToToken(tokenInfo.Value, type, tokenInfo.Position);

        private static Token ToToken(string value, TokenType type, TokenPosition position) =>
            new()
            {
                Type = type,
                Value = value,
                Position = position
            };
    }
}
