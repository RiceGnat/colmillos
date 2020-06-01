using System;
using System.Collections.Generic;
using System.Linq;
using Colmillos.Nodes;

namespace Colmillos.Stats
{
	[Serializable]
	public class StatsProperty : Node<IStats>, IStatsProperty
	{
		private readonly List<INode<IStats>> nodes = new List<INode<IStats>>();

		protected StatsProperty(object owner) : base(owner)
		{
			Nodes = nodes;
		}

		protected StatsProperty(IStats baseStats, object owner, IEnumerable<INode<IStats>> nodes) : this(baseStats, owner)
		{
			AddBaseStatNode(Base);
			AddModifierNodes(nodes);
		}

		public StatsProperty(IStats baseStats, object owner) : this(owner)
		{
			Base = baseStats ?? throw new ArgumentNullException(nameof(baseStats));
		}

		IStats INode<IStats>.Value => this;

		protected IStats Base { get; set; }

		protected Func<int[], int> Resolver { get; set; }

		public virtual int this[Enum stat] => Resolver?.Invoke(nodes.Select(n => n.Value[stat]).ToArray()) ?? Base[stat];

		protected void AddBaseStatNode(IStats baseStats) => nodes.Add(new Node<IStats>(baseStats, Source));

		protected void AddModifierNodes(IEnumerable<INode<IStats>> modifierNodes) => nodes.AddRange(modifierNodes);

		public virtual IStatsProperty GetResolver(Func<int[], int> resolver, IEnumerable<INode<IStats>> nodes) =>
			new StatsProperty(Base, Source, nodes)
			{
				Resolver = resolver
			};
	}
}
