namespace Colmillos.Stats
{
	public interface IStatsModifier
	{
		IStats this[int index] { get; }
	}
}
