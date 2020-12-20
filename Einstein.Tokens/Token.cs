namespace Einstein.Tokens
{
    public sealed class Token
    {
        public TokenType Type { get; init; }
        public string Value { get; init; }
        public TokenPosition Position { get; init; }
    }
}
