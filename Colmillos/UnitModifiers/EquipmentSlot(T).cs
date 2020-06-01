using System;

namespace Colmillos.UnitModifiers
{
	[Serializable]
	public class EquipmentSlot<T> where T : Enum
	{
		private IEquipment<T> equipped;

		public EquipmentSlot(T slotType) => SlotType = slotType;

		public T SlotType { get; }

		public IEquipment<T> Equipped {
			get => equipped;
			set {
				if (value != null && !SlotType.Equals(value.EquipmentType))
				{
					throw new ArgumentException("Equipment slot type mismatch");
				}
				equipped = value;
			}
		}
	}
}
