using System;
using System.Collections.Generic;
using System.Linq;
using Colmillos.Nodes;

namespace Colmillos.Stats
{
	[Serializable]
	public class StatsAggregator : Node<IStats>, IStats
	{
		private readonly StatsModification modification;

		public StatsAggregator(StatsModification modification, IEnumerable<INode<IStats>> nodes)
		{
			this.modification = modification ?? throw new ArgumentNullException(nameof(modification));
			Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
		}

		public override IStats Value => this;

		public int this[Enum stat] => Nodes.Aggregate(modification.Seed, (value, node) => modification.Operation(value, node.Value[stat]));
	}
}
