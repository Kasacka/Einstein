using System.Collections.Generic;
using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class FunctionTree
    {
        internal FunctionTree() {}

        public string Name { get; internal set; }
        public string TypeName { get; internal set; }
        public IEnumerable<VariableTree> Parameters { get; internal set; }
        public IEnumerable<StatementTree> Statements { get; internal set; }

        public void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var parameter in Parameters)
                parameter.Accept(visitor);
            foreach (var statement in Statements)
                statement.Accept(visitor);
            visitor.VisitAfter(this);
        }
    }
}