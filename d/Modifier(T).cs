using System;

namespace Colmillos
{
	[Serializable]
	public abstract class Modifier<T> : IModifier<T> where T : class
	{
		private Func<T> getTarget;

		public string Name { get; set; }

		public string Description { get; set; }

		public T Target => getTarget?.Invoke();

		public virtual void Bind(T target) =>
			getTarget = () => target;

		public virtual void Bind(Func<T> targetProvider) =>
			getTarget = targetProvider;

		public abstract T AsModified();
	}
}
