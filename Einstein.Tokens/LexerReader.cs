using System;

namespace Einstein.Tokens
{
    internal sealed class LexerReader
    {
        private readonly string source;
        private int position;
        private int row;
        private int column;

        public LexerReader(string source) =>
            this.source = source.TrimEnd();

        public bool ReachedEnd =>
            position == source.Length;
        
        public bool IsLetter =>
            char.IsLetter(Current);

        public TokenPosition Position =>
            new(position, row, column);

        public char Current =>
            source[position];

        private char? Next =>
            position < source.Length - 1 ? source[position + 1] : (char?) null;

        public char Skip()
        {
            var value = Current;
            if (Current == '\r' && Next == '\n')
                SkipLineBreak(2);
            else if (Current == '\n')
                SkipLineBreak(1);
            else
                SkipNonLineBreak();
            return value;
        }

        public TokenInfo ReadWhile(Predicate<char> predicate)
        {
            var startPosition = Position;
            while (!ReachedEnd && predicate(Current))
                Skip();
            var value = source[startPosition.Position..position];
            return new(Position: startPosition, Value: value);
        }

        private void SkipNonLineBreak()
        {
            ++position;
            ++column;
        }

        private void SkipLineBreak(int crlfChars = 1)
        {
            position += crlfChars;
            column = 0;
            ++row;
        }
    }
}
