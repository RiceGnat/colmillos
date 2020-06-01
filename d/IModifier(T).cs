using System;

namespace Colmillos
{
	public interface IModifier<T> where T : class
	{
		string Name { get; }
		T Target { get; }

		void Bind(T target);
		void Bind(Func<T> targetProvider);

		T AsModified();
	}
}
