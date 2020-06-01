using System;

namespace Colmillos.Stats
{
	public interface IStatsModifications
	{
		IStats this[Enum modificationType] { get; }
	}
}
