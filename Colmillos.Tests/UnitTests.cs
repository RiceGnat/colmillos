using Colmillos.Stats;
using Colmillos.UnitModifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colmillos
{
	[TestClass]
	public class UnitTests
	{
		private const string NAME = "My unit";

		private IUnit unit;

		[TestInitialize]
		public void Setup()
		{
			unit = UnitFactory.CreateUnit(NAME);
		}

		[TestMethod]
		public void Name()
		{
			Assert.AreEqual(NAME, unit.Name);
			Assert.AreEqual(NAME, unit.GetProperty<string>(UnitProperties.Name));
		}

		[TestMethod]
		public void Stats()
		{
			unit.UseProperty<IStatsProperty>(UnitProperties.Stats, stats =>
			{
				Assert.AreEqual(5, stats[Attributes.STR]);
				Assert.AreEqual(5, stats[Attributes.VIT]);
				Assert.AreEqual(5, stats[Attributes.AGI]);
				Assert.AreEqual(5, stats[Attributes.INT]);
				Assert.AreEqual(5, stats[Attributes.LUK]);
				Assert.AreEqual(30, stats[CombatStats.ATK]);
				Assert.AreEqual(15, stats[CombatStats.DEF]);
				Assert.AreEqual(30, stats[CombatStats.MAG]);
				Assert.AreEqual(20, stats[CombatStats.RST]);
				Assert.AreEqual(5, stats[CombatStats.HIT]);
				Assert.AreEqual(5, stats[CombatStats.AVD]);
				Assert.AreEqual(5, stats[VolatileStats.HP]);
			});
		}

		[TestMethod]
		public void Equipment()
		{
			IEquipment<EquipmentSlots> weapon = ModifierFactory.CreateEquipment("Weapon", EquipmentSlots.Weapon, add =>
			{
				add[CombatStats.ATK] = 5;
			});
			IEquipment<EquipmentSlots> armor = ModifierFactory.CreateEquipment("Armor", EquipmentSlots.Armor, add =>
			{
				add[CombatStats.DEF] = 10;
			}, mult =>
			{
				mult[Attributes.VIT] = 20;
			});

			unit.UseProperty<IEquipmentManager<EquipmentSlots>>(UnitProperties.Equipment, manager =>
			{
				manager.Equip(0, weapon);
				manager.Equip(1, armor);
			});

			unit.UseProperty<IStatsProperty>(UnitProperties.Stats, stats =>
			{
				Assert.AreEqual(6, stats[Attributes.VIT]);
				Assert.AreEqual(35, stats[CombatStats.ATK]);
				Assert.AreEqual(28, stats[CombatStats.DEF]);
				Assert.AreEqual(6, stats[VolatileStats.HP]);
			});
		}

		[TestMethod]
		public void Buffs()
		{
		}
	}
}
