using System;
using System.Collections;
using System.Collections.Generic;

namespace Colmillos.UnitModifiers
{
	[Serializable]
	public class Buff : IBuff, IEnumerable<KeyValuePair<Enum, object>>
	{
		private readonly Dictionary<Enum, object> propertyModifiers = new Dictionary<Enum, object>();

		public Buff(string name, string description)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Description = description ?? throw new ArgumentNullException(nameof(description));
		}

		public string Name { get; set; }

		public string Description { get; set; }

		public void Add<T>(Enum key, IPropertyModifier<T> propertyModifier) => propertyModifiers[key] = propertyModifier;

		public void Add<T>(Enum key, Func<T, IProperties, IEntity, T> func) => propertyModifiers[key] = new DelegatePropertyModifier<T>(func);

		IProperties IPropertiesModifier.GetModifiedProperties(IProperties properties) =>
			new ModifiedProperties(properties, this, propertyModifiers);

		IEnumerator<KeyValuePair<Enum, object>> IEnumerable<KeyValuePair<Enum, object>>.GetEnumerator() => propertyModifiers.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => propertyModifiers.GetEnumerator();
	}
}
