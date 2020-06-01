using System;

namespace Colmillos
{
	public interface IProperties
	{
		T GetProperty<T>(Enum key);
	}
}
