using System;

namespace Colmillos
{
	public interface IPropertiesManager : IModifiableProperties
	{
		object this[Enum key] { set; }
	}
}
