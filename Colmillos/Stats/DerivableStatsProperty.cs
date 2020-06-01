using System;
using System.Collections.Generic;
using Colmillos.Nodes;

namespace Colmillos.Stats
{
	[Serializable]
	public class DerivableStatsProperty : StatsProperty
	{
		private Dictionary<Enum, Func<IStats, int>> derivations = new Dictionary<Enum, Func<IStats, int>>();
		private readonly IStats baseStats;

		protected DerivableStatsProperty(object owner) : base(owner) { }

		protected DerivableStatsProperty(IStats baseStats, object owner, IEnumerable<INode<IStats>> nodes) : this(baseStats, owner)
		{
			AddBaseStatNode(Base);
			AddModifierNodes(nodes);
		}

		public DerivableStatsProperty(IStats baseStats, object owner) : base(owner)
		{
			// closures are not serializable, so baseStats must be assigned to a field
			this.baseStats = baseStats ?? throw new ArgumentNullException(nameof(baseStats));
			Base = new Proxy(stat => derivations.ContainsKey(stat) ? derivations[stat](this) : this.baseStats[stat]);
		}

		public void Add(Enum stat, Func<IStats, int> derivation) => derivations[stat] = derivation;

		public override IStatsProperty GetResolver(Func<int[], int> resolver, IEnumerable<INode<IStats>> nodes) =>
			new DerivableStatsProperty(Base, Source, nodes)
			{
				Resolver = resolver,
				derivations = derivations
			};

		[Serializable]
		private class Proxy : IStats
		{
			private readonly Func<Enum, int> provider;

			public Proxy(Func<Enum, int> provider) => this.provider = provider;

			public int this[Enum stat] => provider(stat);
		}
	}
}
