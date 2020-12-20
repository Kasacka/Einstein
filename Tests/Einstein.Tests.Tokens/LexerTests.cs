using Einstein.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            TestLexer(string.Empty, new TokenType[0]);
        }

        [TestMethod]
        public void WhiteSpaceTest()
        {
            TestLexer("  \r \t ", new TokenType[0]);
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

        private void TestLexer(string source, IEnumerable<TokenType> expectedTypes)
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
