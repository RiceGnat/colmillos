using System;
using System.Collections.Generic;

namespace Colmillos
{
	[Serializable]
	public class Unit : IUnit
	{
		private readonly IPropertiesManager propertyManager;

		public Unit(string name) : this(name, new PropertyManager()) { }

		public Unit(string name, IPropertiesManager propertyManager)
		{
			this.propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public string Name
		{
			get => GetProperty<string>(UnitProperties.Name);
			set => this[UnitProperties.Name] = value;
		}

		public IList<IPropertiesModifier> Modifiers => propertyManager.Modifiers;
		public object this[Enum key] { set => propertyManager[key] = value; }

		public T GetProperty<T>(Enum key) => propertyManager.GetProperty<T>(key);
		public T GetUnmodifiedProperty<T>(Enum key) => propertyManager.GetUnmodifiedProperty<T>(key);
	}
}
