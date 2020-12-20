using Einstein.Trees.TreeVisitors;

namespace Einstein.Trees.Trees
{
    public interface ITree
    {
        void Accept(ITreeVisitor visitor);
    }
}