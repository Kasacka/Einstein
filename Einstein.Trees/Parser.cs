using System.Diagnostics.CodeAnalysis;
using Einstein.Trees.Trees;
using Einstein.Trees.Parsers;

namespace Einstein.Trees
{
    public sealed class Parser
    {
        private readonly TokenReader reader;
        
        public Parser([NotNull] string source) => 
            reader = new TokenReader(source);

        public CompilationUnitTree ParseCompilationUnit() =>
            CompilationUnitParser.Parse(reader);
    }
}
