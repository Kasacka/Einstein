using System.Text;
using Einstein.Trees.Trees;
using Einstein.Trees.TreeVisitors;

namespace Einstein
{
    public static partial class Program
    {
        internal sealed class JavaTreeVisitor : AbstractTreeVisitor
        {
            private readonly StringBuilder result = new();

            public string Source => result.ToString();

            public override void Visit(ClassTree tree)
            {
                result.Append("public class " + tree.Name);
                if (tree.SuperName != null)
                    result.Append(" extends " + tree.SuperName);
                result.Append(" {\n");
            }

            public override void VisitAfter(ClassTree tree)
            {
                result.Append("}\n");
            }

            public override void Visit(FunctionTree tree)
            {
                var returnType = tree.TypeName ?? "void";
                result.Append("public " + returnType + " " + tree.Name + "() {\n");
            }

            public override void VisitAfter(FunctionTree tree)
            {
                result.Append("}\n");
            }

            public override void Visit(VariableTree tree)
            {
                result.Append(tree.TypeName + " " + tree.Name);
            }
        }
    }
}
