using System;

namespace Colmillos.Nodes
{
    public static class NodeExtensions
    {
        public static void Traverse<T>(this INode<T> node, Action<INode<T>, int, int[]> func) => node.Walk(func, 0, new int[1] { 0 });

        private static void Walk<T>(this INode<T> node, Action<INode<T>, int, int[]> func, int depth, int[] i)
        {
            func(node, depth, i);
            int ii = 0;
            foreach (INode<T> n in node)
            {
                int[] i_ = new int[i.Length + 1];
                Array.Copy(i, i_, i.Length);
                i_[depth + 1] = ii;
                n.Walk(func, depth + 1, i_);
                ii++;
            }
        }
    }
}
