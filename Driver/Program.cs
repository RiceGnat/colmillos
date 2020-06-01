using System;
using System.Collections.Generic;
using Colmillos;
using Colmillos.Serialization;
using Colmillos.Stats;
using Colmillos.UnitModifiers;

namespace Driver
{
	class Program
	{
		static void Main(string[] args)
		{
			IUnit unit = UnitFactory.CreateUnit("My unit");
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

			IBuff buff = new Buff("STR Up", "Plain stats buff")
			{
				{
					UnitProperties.Stats, ModifierFactory.SetupStatsModifier(
						new StatsMap() { [Attributes.STR] = 5 },
						new StatsMap() { [Attributes.STR] = 50 })
				}
			};

			IBuff sheep = new Buff("Sheep", "Is a sheep")
			{
				{
					UnitProperties.Name,
					new DelegatePropertyModifier<string>("Sheep")
				},
				{
					UnitProperties.Stats,
					new DelegatePropertyModifier<IStatsProperty>((IStatsProperty property, IProperties _, IEntity __) =>
						new StatsProperty(ConstantStats.One, property.Source)
					)
				}
			};

			unit.UseProperty<IList<IBuff>>(UnitProperties.Buffs, buffs =>
			{
				buffs.Add(buff);
			});

			unit.Print();


			Console.WriteLine();
			Console.WriteLine("Testing unit serialization...");
			IUnit clone = unit.DeepClone();

			clone.UseProperty<IEquipmentManager<EquipmentSlots>>(UnitProperties.Equipment, manager =>
			{
				manager.Unequip(1);
			});

			clone.UseProperty<IList<IBuff>>(UnitProperties.Buffs, buffs =>
			{
				buffs.Add(sheep);
				buffs.Add(buff);
			});

			clone.Print();
			Console.ReadKey();
		}
	}
}
