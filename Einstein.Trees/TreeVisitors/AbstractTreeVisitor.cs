using Einstein.Trees.Trees;

namespace Einstein.Trees.TreeVisitors
{
    public abstract class AbstractTreeVisitor : ITreeVisitor
    {
        public virtual void Visit(CompilationUnitTree tree)
        {
        }

        public virtual void Visit(ClassTree tree)
        {
        }

        public virtual void Visit(FunctionTree tree)
        {
        }

        public virtual void Visit(VariableTree tree)
        {
        }

        public virtual void VisitAfter(CompilationUnitTree tree)
        {
        }

        public virtual void VisitAfter(ClassTree tree)
        {
        }

        public virtual void VisitAfter(FunctionTree tree)
        {
        }

        public virtual void VisitAfter(VariableTree tree)
        {
        }
    }
}