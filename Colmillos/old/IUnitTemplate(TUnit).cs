using System;

namespace Colmillos
{
	public interface IUnitTemplate<TUnit> : IModifiableProperties where TUnit : class, IUnitTemplate<TUnit>
	{
		string Name { get; }
	}
}
