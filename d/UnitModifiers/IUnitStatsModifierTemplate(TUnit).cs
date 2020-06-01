using Colmillos.Stats;

namespace Colmillos.UnitModifiers
{
	interface IUnitStatsModifierTemplate<TUnit> : IModifier<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		IStatsModifications Stats { get; }
	}
}
