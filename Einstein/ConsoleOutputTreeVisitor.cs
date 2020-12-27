using System;
using Einstein.Trees.TreeVisitors;
using Einstein.Trees.Trees;

namespace Einstein
{
    public static partial class Program
    {
        internal sealed class ConsoleOutputTreeVisitor : AbstractTreeVisitor
        {
            public override void Visit(CompilationUnitTree tree)
            {
            }

            public override void Visit(ClassTree tree)
            {
                Console.WriteLine("CLASS " + tree.Name + " EXTENDS " + tree.SuperName);
            }

            public override void Visit(FunctionTree tree)
            {
                Console.WriteLine("FUNCTION " + tree.Name + " RETURNS " + tree.TypeName);
            }

            public override void Visit(VariableTree tree)
            {
                Console.WriteLine("VARIABLE " + tree.Name);
            }

            public override void Visit(LiteralExpressionTree tree)
            {
                Console.WriteLine("LITERAL " + tree.Value);
            }

            public override void Visit(BinaryOperatorExpressionTree tree)
            {
                Console.WriteLine("BINARY_EXPRESSION" + tree.Type);
            }
        }
    }
}
