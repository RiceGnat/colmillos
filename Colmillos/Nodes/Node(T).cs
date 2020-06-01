using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Colmillos.Nodes
{
	[Serializable]
	public class Node<T> : INode<T>, IEnumerable<INode<T>>
	{
		protected Node() { }

		protected Node(object source) : this(default, source) { }

		public Node(T value) : this(value, null) { }

		public Node(T value, object source)
		{
			Value = value;
			Source = source;
		}

		public virtual T Value { get; }

		public virtual object Source { get; }

		protected IEnumerable<INode<T>> Nodes { get; set; } = Enumerable.Empty<INode<T>>();

		IEnumerator<INode<T>> IEnumerable<INode<T>>.GetEnumerator() => Nodes.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => Nodes.GetEnumerator();
	}
}
