using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public abstract class StatementTree : ITree
    {
        private protected StatementTree() {}
        public abstract void Accept(ITreeVisitor visitor);
    }
}