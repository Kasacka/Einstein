using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class VariableTree : ITree
    {
        internal VariableTree() {}

        public string Name { get; init; }
        public string TypeName { get; init; }

        public void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            visitor.VisitAfter(this);
        }
    }
}