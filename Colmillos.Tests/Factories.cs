using System;
using Colmillos.Stats;
using Colmillos.UnitModifiers;

namespace Colmillos
{
	public static class UnitFactory
	{
		public static IUnit CreateUnit(string name, Action<StatsMap> statsSetup = null)
		{
			StatsMap baseStats = new StatsMap
			{
				[UnitProperties.Level] = 1,
				[Attributes.STR] = 5,
				[Attributes.VIT] = 5,
				[Attributes.AGI] = 5,
				[Attributes.INT] = 5,
				[Attributes.LUK] = 5
			};

			statsSetup?.Invoke(baseStats);

			Unit unit = new Unit(name);

			unit[UnitProperties.Stats] = new DerivableStatsProperty(baseStats, unit)
			{
				{ CombatStats.ATK, stats => 6 * stats[Attributes.STR] },
				{ CombatStats.DEF, stats => 3 * stats[Attributes.VIT] },
				{ CombatStats.MAG, stats => 6 * stats[Attributes.INT] },
				{ CombatStats.RST, stats => 4 * stats[Attributes.INT] },
				{ CombatStats.HIT, stats => (stats[Attributes.STR] + stats[Attributes.LUK]) / 2 },
				{ CombatStats.AVD, stats => (stats[Attributes.AGI] + stats[Attributes.LUK]) / 2 },
				{ VolatileStats.HP, stats => stats[Attributes.VIT] * stats[UnitProperties.Level] }
			};

			EquipmentManager<EquipmentSlots> equipment = new EquipmentManager<EquipmentSlots>()
			{
				EquipmentSlots.Weapon,
				EquipmentSlots.Armor,
				EquipmentSlots.Accessory,
				EquipmentSlots.Accessory
			};
			equipment.Name = "Equipment";
			unit[UnitProperties.Equipment] = equipment;
			unit.Modifiers.Add(equipment);

			PropertiesModifierCollection<IBuff> buffs = new PropertiesModifierCollection<IBuff>()
			{
				Name = "Buffs"
			};
			unit[UnitProperties.Buffs] = buffs;
			unit.Modifiers.Add(buffs);

			return unit;
		}
	}

	public static class ModifierFactory
	{
		private static readonly Func<int[], int> resolver = mods => (mods[0] + mods[1]).Scale(mods[2]);

		public static IStatsPropertyModifier SetupStatsModifier(IStats add, IStats mult) =>
			new StatsPropertyModifier(resolver)
			{
				new StatsModification(Operations.Add, 0, add),
				new StatsModification(Operations.Add, 0, mult)
			};

		public static IEquipment<EquipmentSlots> CreateEquipment(string name, EquipmentSlots slot,
			Action<StatsMap> addSetup = null, Action<StatsMap> multSetup = null)
		{
			StatsMap add = new StatsMap();
			StatsMap mult = new StatsMap();
			addSetup?.Invoke(add);
			multSetup?.Invoke(mult);
			return new Equipment<EquipmentSlots>(name, UnitProperties.Stats, SetupStatsModifier(add, mult))
			{
				EquipmentType = slot,
			};
		}
	}

	public static class Operations
	{
		public static int Add(this int a, int b) => a + b;
		public static int Scale(this int a, int b) => a * (100 + b) / 100;
	}
}
