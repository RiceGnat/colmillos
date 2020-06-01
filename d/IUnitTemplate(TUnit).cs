using System;

namespace Colmillos
{
	public interface IUnitTemplate<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		string Name { get; }
		IModifierCollection<TUnit> Modifiers { get; }
		T GetProperty<T>(Enum key);
		T GetModifiableProperty<T>(Enum key);
		T GetUnmodifiedProperty<T>(Enum key);
	}
}
