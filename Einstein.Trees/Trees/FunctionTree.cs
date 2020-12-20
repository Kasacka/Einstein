using System.Collections.Generic;
using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class FunctionTree
    {
        internal FunctionTree() {}

        public string Name { get; init; }
        public string TypeName { get; init; }
        public IEnumerable<VariableTree> Parameters { get; init; }

        public void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var parameter in Parameters)
                parameter.Accept(visitor);
            visitor.VisitAfter(this);
        }
    }
}