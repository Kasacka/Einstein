using Einstein.Trees.Trees;

namespace Einstein.Trees.TreeVisitors
{
    public interface ITreeVisitor
    {
        void Visit(CompilationUnitTree tree);
        void Visit(ClassTree tree);
        void Visit(FunctionTree tree);
        void Visit(VariableTree tree);

        void VisitAfter(CompilationUnitTree tree);
        void VisitAfter(ClassTree tree);
        void VisitAfter(FunctionTree tree);
        void VisitAfter(VariableTree tree);
    }
}