using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class VariableTree : ITree
    {
        internal VariableTree() {}

        public string Name { get; internal set; }
        public string TypeName { get; internal set; }

        public void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            visitor.VisitAfter(this);
        }
    }
}