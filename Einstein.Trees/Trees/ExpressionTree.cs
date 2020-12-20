using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public abstract class ExpressionTree : ITree
    {
        private protected ExpressionTree() {}
        public abstract void Accept(ITreeVisitor visitor);
    }
}