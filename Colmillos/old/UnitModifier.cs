using System;

namespace Colmillos
{
	[Serializable]
	public sealed class UnitModifier : UnitModifierTemplate<IUnit>, IUnit
	{
		public override IUnit AsModified() => this;

		public static IModifier<IUnit> Create(string name, Action<UnitModifier> setup)
		{
			UnitModifier modifier = new UnitModifier
			{
				Name = name
			};
			setup(modifier);
			return modifier;
		}
	}
}
