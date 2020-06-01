using System;

namespace Colmillos
{
	[Serializable]
	public class DelegatePropertyModifier<T> : IPropertyModifier<T>
	{
		private readonly Func<T, IProperties, IEntity, T> func;
		private readonly T property;

		public DelegatePropertyModifier(Func<T, IProperties, IEntity, T> func)
		{
			this.func = func ?? throw new ArgumentNullException(nameof(func));
		}

		public DelegatePropertyModifier(T property) {
			this.property = property;
			func = (T _, IProperties __, IEntity ___) => this.property;
		}

		public T GetModifiedProperty(T property, IProperties properties, IEntity modifier) => func(property, properties, modifier);
	}
}
