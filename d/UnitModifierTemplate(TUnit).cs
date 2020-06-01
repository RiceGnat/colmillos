using System;
using System.Collections.Generic;

namespace Colmillos
{
	[Serializable]
	public abstract class UnitModifierTemplate<TUnit> : Modifier<TUnit>, IUnitTemplate<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		private readonly IDictionary<Enum, Func<TUnit, object>> propertyModifiers = new Dictionary<Enum, Func<TUnit, object>>();

		string IUnitTemplate<TUnit>.Name => GetProperty<string>(UnitProperties.Name);

		IModifierCollection<TUnit> IUnitTemplate<TUnit>.Modifiers => Target.Modifiers;

		T IUnitTemplate<TUnit>.GetProperty<T>(Enum key) => Target.GetProperty<T>(key);

		T IUnitTemplate<TUnit>.GetModifiableProperty<T>(Enum key) => GetProperty<T>(key);

		T IUnitTemplate<TUnit>.GetUnmodifiedProperty<T>(Enum key) => Target.GetUnmodifiedProperty<T>(key);

		public void AddPropertyModifier<T>(Enum key, Func<T, TUnit, T> modifier)
		{
			propertyModifiers.Add(key, unit => modifier(unit.GetModifiableProperty<T>(key), unit));
		}

		public void AddPropertyModifier<T>(Enum key, IModifier<T> modifier) where T : class
		{
			modifier.Bind(() => Target.GetModifiableProperty<T>(key));
			propertyModifiers.Add(key, _ => modifier.AsModified());
		}

		public void AddPropertyModifier<T>(IUnitPropertyModifier<T> modifier) where T : class
		{
			modifier.Bind(() => Target.GetModifiableProperty<T>(modifier.PropertyKey));
			propertyModifiers.Add(modifier.PropertyKey, _ => modifier.AsModified());
		}

		private T GetProperty<T>(Enum key) =>
			propertyModifiers.ContainsKey(key) ? (T)propertyModifiers[key](Target) : Target.GetModifiableProperty<T>(key);
	}
}
