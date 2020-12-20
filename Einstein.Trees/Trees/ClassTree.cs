using System.Collections.Generic;
using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class ClassTree : ITree
    {
        internal ClassTree() {}

        public string Name { get; init; }
        public string SuperName { get; init; }
        public IEnumerable<FunctionTree> Functions { get; init; }
        public IEnumerable<VariableTree> Parameters { get; init; }

        public void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var parameter in Parameters)
                parameter.Accept(visitor);
            foreach (var function in Functions)
                function.Accept(visitor);
            visitor.VisitAfter(this);
        }
    }
}