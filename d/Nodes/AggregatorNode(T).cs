using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colmillos.Nodes
{
	public class AggregatorNode<T> : Node<T>
	{
		public AggregatorNode(IEnumerable<INode<T>> nodes, Func<T, T, T> aggregator)
			: base(nodes.Select(node => node.Value).Aggregate(aggregator))
		{
			Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
		}

	}
}
