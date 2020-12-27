using Einstein.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Einstein.Tests.Tokens
{
    [TestClass]
    public sealed class LexerTests
    {
        [TestMethod]
        public void EmptyStringTest()
        {
            TestLexer(string.Empty, Array.Empty<TokenType>());
        }

        [TestMethod]
        public void WhiteSpaceTest()
        {
            TestLexer("  \r \t ", Array.Empty<TokenType>());
        }

        [TestMethod]
        public void KeywordsTest()
        {
            TestLexer("   class  \n end ", new TokenType[] {
                TokenType.Keyword, TokenType.Keyword
            });
        }

        [TestMethod]
        public void OperatorsTest()
        {
            TestLexer(" < ", new TokenType[] {
                TokenType.Operator
            });
        }

        [TestMethod]
        public void IntegerNumberTest()
        {
            TestLexer(" 45 456  ", new TokenType[] {
                TokenType.Number,
                TokenType.Number
            });
        }

        [TestMethod]
        public void FloatNumberTest()
        {
            TestLexer(" 12.4  34.43 88.4 ", new TokenType[] {
                TokenType.Number,
                TokenType.Number,
                TokenType.Number
            });
        }

        [TestMethod]
        public void TrailingDotFloatNumberTest()
        {
            TestLexer(" 12. ", new TokenType[] {
                TokenType.Number
            });
        }

        [TestMethod]
        public void LeadingDotFloatNumberTest()
        {
            TestLexer(" .4 ", new TokenType[] {
                TokenType.Number
            });
        }

        private static void TestLexer(string source, IEnumerable<TokenType> expectedTypes)
        {
            var lexer = new Lexer(source);
            var tokens = new List<Token>();
            while (lexer.HasNextToken)
                tokens.Add(lexer.ReadNextToken());
            var types = tokens.Select(token => token.Type);
            CollectionAssert.AreEqual(expectedTypes.ToArray(), types.ToArray());
            Assert.IsFalse(lexer.HasNextToken);
        }
    }
}
