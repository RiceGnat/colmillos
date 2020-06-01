using System;
using Colmillos.Stats;

namespace Colmillos.UnitModifiers
{
	[Serializable]
	public abstract class UnitStatsModifierTemplate<TUnit> : UnitModifierTemplate<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		public IStatsModifications Stats { get; }

		protected UnitStatsModifierTemplate(Enum key, StatsModifier statsModifier)
		{
			Stats = statsModifier ?? throw new ArgumentNullException(nameof(statsModifier));
			AddPropertyModifier(key, statsModifier);
		}
	}
}
