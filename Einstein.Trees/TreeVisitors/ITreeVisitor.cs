using Einstein.Trees.Trees;

namespace Einstein.Trees.TreeVisitors
{
    public interface ITreeVisitor
    {
        void Visit(CompilationUnitTree tree);
        void Visit(ClassTree tree);
        void Visit(FunctionTree tree);
        void Visit(VariableTree tree);
        void Visit(VariableDeclarationStatementTree tree);
        void Visit(LiteralExpressionTree tree);

        void VisitAfter(CompilationUnitTree tree);
        void VisitAfter(ClassTree tree);
        void VisitAfter(FunctionTree tree);
        void VisitAfter(VariableTree tree);
        void VisitAfter(VariableDeclarationStatementTree tree);
        void VisitAfter(LiteralExpressionTree tree);
    }
}