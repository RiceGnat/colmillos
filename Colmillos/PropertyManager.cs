using System;
using System.Collections.Generic;

namespace Colmillos
{
	[Serializable]
	public class PropertyManager : IPropertiesManager
	{
		private readonly BaseProperties baseProperties;
		private readonly PropertiesModifierCollection modifiers = new PropertiesModifierCollection();

		public PropertyManager() => baseProperties = new BaseProperties(key => Properties[key]);

		public IDictionary<Enum, object> Properties { get; } = new Dictionary<Enum, object>();
		public IList<IPropertiesModifier> Modifiers => modifiers;

		public object this[Enum key] { set => Properties[key] = value; }

		public T GetProperty<T>(Enum key) => modifiers.GetModifiedProperties(baseProperties).GetProperty<T>(key);

		public T GetUnmodifiedProperty<T>(Enum key) => baseProperties.GetProperty<T>(key);

		[Serializable]
		private class BaseProperties : IProperties
		{
			private readonly Func<Enum, object> provider;

			public BaseProperties(Func<Enum, object> provider) => this.provider = provider;

			public T GetProperty<T>(Enum key) => (T)provider(key);
		}
	}
}
