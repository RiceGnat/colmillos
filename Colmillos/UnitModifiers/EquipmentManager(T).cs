using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Colmillos.UnitModifiers
{
	[Serializable]
	public class EquipmentManager<T> : IEquipmentManager<T>, IPropertiesModifier where T : Enum
	{
		private readonly List<EquipmentSlot<T>> slots = new List<EquipmentSlot<T>>();

		public EquipmentManager() : this("") { }

		public EquipmentManager(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public string Name { get; set; }

		public void Add(T slotType) => slots.Add(new EquipmentSlot<T>(slotType));

		public IEquipment<T> Equip(int index, IEquipment<T> equipment)
		{
			IEquipment<T> previous = slots[index].Equipped;
			slots[index].Equipped = equipment;
			return previous;
		}
		public IEquipment<T> Unequip(int index) => Equip(index, null);

		IProperties IPropertiesModifier.GetModifiedProperties(IProperties properties)
			=> slots.Aggregate(properties, (p, slot) => slot.Equipped?.GetModifiedProperties(p) ?? p);

		IEnumerator<EquipmentSlot<T>> IEnumerable<EquipmentSlot<T>>.GetEnumerator() => slots.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => slots.GetEnumerator();
	}
}
