using System;

namespace Colmillos.Stats
{
	[Serializable]
	public sealed class ConstantStats : IStats
	{
		private readonly int value;

		public ConstantStats(int value)
		{
			this.value = value;
		}

		public int this[Enum stat] => value;

		public static ConstantStats Zero { get; } = new ConstantStats(0);
		public static ConstantStats One { get; } = new ConstantStats(1);
	}
}
