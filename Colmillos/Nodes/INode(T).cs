using System.Collections.Generic;

namespace Colmillos.Nodes
{
    public interface INode<T> : IEnumerable<INode<T>>
    {
        T Value { get; }
        object Source { get; }
    }
}
