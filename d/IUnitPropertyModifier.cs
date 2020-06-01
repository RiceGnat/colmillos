using System;

namespace Colmillos
{
	public interface IUnitPropertyModifier<T> : IModifier<T> where T : class
	{
		Enum PropertyKey { get; }
	}
}
