namespace Einstein.Tokens
{
    public interface ILexer
    {
        bool HasNextToken { get; }
        Token ReadNextToken();
    }
}
