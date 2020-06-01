using System;

namespace Colmillos.Stats
{
	[Serializable]
	public class StatsModification
	{
		public StatsModification(Func<int, int, int> operation, int seed) : this(operation, seed, null) { }

		public StatsModification(Func<int, int, int> operation, int seed, IStats values)
		{
			Operation = operation ?? throw new ArgumentNullException(nameof(operation));
			Seed = seed;
			Values = values;
		}

		public Func<int, int, int> Operation { get; }
		public int Seed { get; }
		public IStats Values { get; }
	}
}
