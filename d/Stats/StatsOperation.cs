using System;

namespace Colmillos.Stats
{
	[Serializable]
	public class StatsOperation : Modifier<IStats>, IStats
	{
		private readonly IStats values;
		private readonly Func<int, int, int> operation;

		public StatsOperation(IStats values, Func<int, int, int> operation)
		{
			this.values = values;
			this.operation = operation;
		}

		int IStats.this[Enum stat] => operation(Target[stat], values[stat]);

		public override IStats AsModified() => this;
	}
}
