using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class LiteralExpressionTree : ExpressionTree
    {
        internal LiteralExpressionTree() {}

        public LiteralType Type { get; init; }
        public string Value { get; init; }

        public override void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            visitor.VisitAfter(this);
        }

        public enum LiteralType
        {
            Boolean,
            Number
        }
    }
}