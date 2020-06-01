using System;
using System.Collections.Generic;

namespace Colmillos
{
	public interface IModifiableProperties : IProperties
	{
		IList<IPropertiesModifier> Modifiers { get; }
		T GetUnmodifiedProperty<T>(Enum key);
	}
}
