using System;
using System.Collections.Generic;

namespace Colmillos.Stats
{
	[Serializable]
	public class StatsMap : IStats
	{
		private readonly Dictionary<Enum, int> map = new Dictionary<Enum, int>();

		public virtual int this[Enum stat]
		{
			get => map.ContainsKey(stat) ? map[stat] : 0;
			set => map[stat] = value;
		}
	}
}
