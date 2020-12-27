using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class LiteralExpressionTree : ExpressionTree
    {
        internal LiteralExpressionTree() {}

        public LiteralType Type { get; internal set; }
        public string Value { get; internal set; }

        public override void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            visitor.VisitAfter(this);
        }
    }
}