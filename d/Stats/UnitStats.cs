using System;
using System.Collections.Generic;

namespace Colmillos.Stats
{
	[Serializable]
	public class UnitStats : BaseStats, IModifiableStats
	{
		private readonly Dictionary<Enum, Func<IStats, int>> proxies = new Dictionary<Enum, Func<IStats, int>>();

		public UnitStats(IUnit unit, Enum key)
		{
			ModificationBase = new Proxy(stat => proxies.ContainsKey(stat) ? proxies[stat](unit.GetProperty<IStats>(key)) : BaseStats[stat]);
		}

		public IStats BaseStats => this;

		public IStats ModificationBase { get; }

		public override int this[Enum stat] => proxies.ContainsKey(stat) ? proxies[stat](this) : base[stat];

		public IStats GetModifications(Enum modificationType) => null;

		public void SetStatProxy(Enum stat, Func<IStats, int> proxy) => proxies[stat] = proxy;

		private class Proxy : IStats
		{
			private readonly Func<Enum, int> provider;

			public Proxy(Func<Enum, int> provider)
			{
				this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
			}

			public int this[Enum stat] => provider(stat);
		}
	}
}
