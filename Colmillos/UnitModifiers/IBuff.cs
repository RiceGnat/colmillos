using System.Collections.Generic;

namespace Colmillos.UnitModifiers
{
	public interface IBuff : IPropertiesModifier
	{
		string Description { get; }
	}
}
