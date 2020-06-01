using System;

namespace Colmillos
{
	[Serializable]
	public sealed class Unit : UnitTemplate<IUnit>, IUnit
	{
		public Unit()
		{
			Properties[UnitProperties.Name] = "";
		}

		public override string Name
		{
			get => GetProperty<string>(UnitProperties.Name);
			set => this[UnitProperties.Name] = value;
		}

		protected override IUnit Self => this;

		public static IUnit Create(string name, Action<Unit> setup)
		{
			Unit unit = new Unit
			{
				Name = name
			};
			setup(unit);
			return unit;
		}
	}
}
