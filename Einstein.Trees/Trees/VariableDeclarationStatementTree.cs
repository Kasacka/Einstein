using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class VariableDeclarationStatementTree : StatementTree
    {
        internal VariableDeclarationStatementTree() {}

        public VariableTree Variable { get; init; }
        public ExpressionTree Expression { get; init; }

        public override void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            Variable.Accept(visitor);
            Expression.Accept(visitor);
            visitor.VisitAfter(this);
        }
    }
}