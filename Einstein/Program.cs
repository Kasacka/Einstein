using Einstein.Trees;

namespace Einstein
{
    public static partial class Program
    {
        public static void Main(string[] _)
        {
            var visitor = new JavaTreeVisitor();
            var parser = new Parser(System.IO.File.ReadAllText(@"source.es"));
            var tree = parser.ParseCompilationUnit();
            tree.Accept(visitor);
            System.Console.WriteLine(visitor.Source);
        }
    }
}
