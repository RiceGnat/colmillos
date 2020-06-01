using System;

namespace Colmillos
{
	public interface IPropertyModifier<T>
	{
		T GetModifiedProperty(T property, IProperties properties, IEntity modifier);
	}
}
