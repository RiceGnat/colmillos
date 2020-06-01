using System;
using System.Collections.Generic;

namespace Colmillos.Stats
{
	[Serializable]
	public class StatsModifier : Modifier<IModifiableStats>, IStatsModifications, IModifiableStats
	{
		private readonly Dictionary<Enum, IStats> values = new Dictionary<Enum, IStats>();
		private readonly Dictionary<Enum, IModifier<IStats>> aggregators = new Dictionary<Enum, IModifier<IStats>>();
		private readonly Dictionary<Enum, IStats> defaults = new Dictionary<Enum, IStats>();
		private readonly Dictionary<Enum, IStats> results = new Dictionary<Enum, IStats>();
		private readonly Func<IStats, IReadOnlyDictionary<Enum, IStats>, Enum, int> resolver;
		
		public StatsModifier(Func<IStats, IReadOnlyDictionary<Enum, IStats>, Enum, int> resolver)
		{
			this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
		}

		IStats IModifiableStats.BaseStats => Target.BaseStats;

		IStats IModifiableStats.ModificationBase => Target.ModificationBase;

		int IStats.this[Enum stat] => resolver(AsModified().ModificationBase, results, stat);

		public IStats this[Enum modificationType] => values[modificationType];

		public IStats this[Enum modificationType, Func<int, int, int> aggregator, int defaultValue]
		{
			set
			{
				values[modificationType] = value;
				defaults[modificationType] = new ConstantStats(defaultValue);

				StatsOperation m = new StatsOperation(values[modificationType], aggregator);
				m.Bind(() => GetPreviousModifications(modificationType));
				aggregators[modificationType] = m;
				results[modificationType] = m.AsModified();
			}
		}

		public override IModifiableStats AsModified() => this;

		public IStats GetModifications(Enum modificationType) => aggregators[modificationType].AsModified();

		private IStats GetPreviousModifications(Enum modificationType) => Target.GetModifications(modificationType) ?? defaults[modificationType];
	}
}
