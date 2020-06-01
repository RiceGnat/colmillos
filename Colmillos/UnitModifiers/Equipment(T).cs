using System;
using Colmillos.Stats;

namespace Colmillos.UnitModifiers
{
	[Serializable]
	public class Equipment<T> : IEquipment<T> where T : Enum
	{
		private readonly Enum statsKey;
		private readonly IStatsPropertyModifier statsModifier;

		public Equipment(string name, Enum statsKey, IStatsPropertyModifier statsModifier)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			this.statsKey = statsKey ?? throw new ArgumentNullException(nameof(statsKey));
			this.statsModifier = statsModifier ?? throw new ArgumentNullException(nameof(statsModifier));
		}

		public string Name { get; set; }

		public T EquipmentType { get; set; }

		public IStats this[int index] => statsModifier[index];

		public IProperties GetModifiedProperties(IProperties properties) => new ModifiedProperties(properties, this)
		{
			{ statsKey, statsModifier }
		};
	}
}
