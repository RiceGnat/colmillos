using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Colmillos.Nodes
{
    public class Node<T> : INode<T>, IEnumerable<INode<T>>
    {
        public Node() {}

        public Node(T value) => Value = value;

        public virtual T Value { get; }

        protected IEnumerable<INode<T>> Nodes { get; set; } = Enumerable.Empty<INode<T>>();

        IEnumerator<INode<T>> IEnumerable<INode<T>>.GetEnumerator()
            => Nodes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => Nodes.GetEnumerator();

        public override string ToString()
            => $"{Value} [{GetType().Name}]";
    }
}
