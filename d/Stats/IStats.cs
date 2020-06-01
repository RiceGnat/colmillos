using System;

namespace Colmillos.Stats
{
	public interface IStats
	{
		int this[Enum stat] { get; }
	}
}
