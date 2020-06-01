using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Colmillos
{
	[Serializable]
	public abstract class UnitTemplate<TUnit> : IUnitTemplate<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		protected UnitTemplate()
		{
			Link();
		}

		public virtual string Name { get; set; }
		public virtual IModifierCollection<TUnit> Modifiers { get; } = new ModifierCollection<TUnit>();
		public virtual IDictionary<Enum, object> Properties { get; } = new Dictionary<Enum, object>();

		public object this[Enum key]
		{
			set => Properties[key] = value;
		}

		public T GetProperty<T>(Enum key) => Modifiers.AsModified().GetModifiableProperty<T>(key);
		public T GetModifiableProperty<T>(Enum key) => (T)Properties[key];
		public T GetUnmodifiedProperty<T>(Enum key) => (T)Properties[key];

		protected abstract TUnit Self { get; }

		protected virtual void Link()
		{
			Modifiers.Bind(Self);
		}

		[OnDeserialized]
		private void Rebind(StreamingContext context)
		{
			// Reset object references after deserialization
			Link();
		}
	}
}
