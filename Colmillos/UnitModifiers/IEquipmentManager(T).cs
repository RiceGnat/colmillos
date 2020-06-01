using System;
using System.Collections.Generic;

namespace Colmillos.UnitModifiers
{
	public interface IEquipmentManager<T> : IEnumerable<EquipmentSlot<T>>, IPropertiesModifier
		where T : Enum
	{
		IEquipment<T> Equip(int index, IEquipment<T> equipment);

		IEquipment<T> Unequip(int index);
	}
}
