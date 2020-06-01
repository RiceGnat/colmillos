using System;

namespace Colmillos.UnitModifiers
{
	public interface IEquipmentTemplate<TUnit, TEquipmentSlot>
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentSlot : Enum
	{
		TEquipmentSlot SlotType { get; }
	}
}
