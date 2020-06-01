using System;
using System.Collections.Generic;
using System.Linq;

namespace Colmillos
{
	[Serializable]
	public class PropertiesModifierCollection<T> : List<T>, IPropertiesModifier where T : IPropertiesModifier
	{
		public string Name { get; set; }

		public IProperties GetModifiedProperties(IProperties properties) => this.Aggregate(properties, (p, m) => m.GetModifiedProperties(p));
	}

	[Serializable]
	public class PropertiesModifierCollection : PropertiesModifierCollection<IPropertiesModifier> { }
}
