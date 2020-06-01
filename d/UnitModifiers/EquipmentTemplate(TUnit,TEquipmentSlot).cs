using System;
using Colmillos.Stats;

namespace Colmillos.UnitModifiers
{
	public abstract class EquipmentTemplate<TUnit, TEquipmentSlot> : UnitStatsModifierTemplate<TUnit>, IEquipmentTemplate<TUnit, TEquipmentSlot>
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentSlot : Enum
	{
		public TEquipmentSlot SlotType { get; }

		protected EquipmentTemplate(Enum key, StatsModifier statsModifier) : base(key, statsModifier) {}
	}
}
