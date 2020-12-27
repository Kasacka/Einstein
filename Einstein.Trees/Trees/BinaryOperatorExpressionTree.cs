using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class BinaryOperatorExpressionTree : ExpressionTree
    {
        internal BinaryOperatorExpressionTree() {}

        public OperatorType Type { get; internal set; }
        public ExpressionTree LeftHandSide { get; internal set; }
        public ExpressionTree RightHandSide { get; internal set; }

        public override void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            RightHandSide.Accept(visitor);
            LeftHandSide.Accept(visitor);
            visitor.VisitAfter(this);
        }
    }
}