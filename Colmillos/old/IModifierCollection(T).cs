using System.Collections.Generic;

namespace Colmillos
{
	public interface IModifierCollection<T> : IModifier<T>, IList<IModifier<T>> where T : class
	{
		// Inherited members only
	}
}
