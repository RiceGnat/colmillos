using System;
using Colmillos.Stats;

namespace Colmillos.UnitModifiers
{
	public interface IEquipment<T> : IPropertiesModifier, IStatsModifier where T : Enum
	{
		T EquipmentType { get; }
	}
}
