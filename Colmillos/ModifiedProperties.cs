using System;
using System.Collections;
using System.Collections.Generic;

namespace Colmillos
{
	public class ModifiedProperties : IProperties, IEnumerable<KeyValuePair<Enum, object>>
	{
		private readonly IDictionary<Enum, object> propertyModifiers;
		private readonly IProperties properties;
		private readonly IEntity modifier;

		public ModifiedProperties(IProperties properties, IEntity modifier)
			: this(properties, modifier, new Dictionary<Enum, object>()) { }

		public ModifiedProperties(IProperties properties, IEntity modifier, IDictionary<Enum, object> propertyModifiers)
		{
			this.properties = properties ?? throw new ArgumentNullException(nameof(properties));
			this.modifier = modifier ?? throw new ArgumentNullException(nameof(modifier));
			this.propertyModifiers = propertyModifiers ?? throw new ArgumentNullException(nameof(propertyModifiers));
		}

		public T GetProperty<T>(Enum key) => propertyModifiers.ContainsKey(key)
			? ((IPropertyModifier<T>)propertyModifiers[key]).GetModifiedProperty(properties.GetProperty<T>(key), properties, modifier)
			: properties.GetProperty<T>(key);

		public void Add<T>(Enum key, IPropertyModifier<T> propertyModifier) => propertyModifiers[key] = propertyModifier;

		IEnumerator<KeyValuePair<Enum, object>> IEnumerable<KeyValuePair<Enum, object>>.GetEnumerator() => propertyModifiers.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => propertyModifiers.GetEnumerator();
	}
}
