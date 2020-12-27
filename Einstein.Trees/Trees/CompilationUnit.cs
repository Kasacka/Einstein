using System.Collections.Generic;
using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public sealed class CompilationUnitTree : ITree
    {
        internal CompilationUnitTree() {}

        public IEnumerable<ClassTree> Classes { get; internal set; }

        public void Accept(ITreeVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var @class in Classes)
                @class.Accept(visitor);
            visitor.VisitAfter(this);
        }
    }
}