using System.Collections.Generic;
using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class ClassTree : ITree
    {
        internal ClassTree() {}

        public string Name { get; internal set; }
        public string SuperName { get; internal set; }
        public IEnumerable<FunctionTree> Functions { get; internal set; }
        public IEnumerable<VariableTree> Parameters { get; internal set; }

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