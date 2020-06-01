using System;
using System.Collections.Generic;
using System.Linq;
using Colmillos;
using Colmillos.Nodes;
using Colmillos.Stats;
using Colmillos.UnitModifiers;

namespace Driver
{
	public static class Output
	{
		public static int GetStat(this IUnit unit, Enum stat) => unit.GetProperty<IStatsProperty>(UnitProperties.Stats)[stat];

		public static IEnumerable<Enum> GetAllStatEnums() => Enum.GetValues(typeof(Attributes)).Cast<Enum>()
			.Union(Enum.GetValues(typeof(CombatStats)).Cast<Enum>())
			.Union(Enum.GetValues(typeof(VolatileStats)).Cast<Enum>());

		public static string Format(this IEquipment<EquipmentSlots> equipment) => String.Format("{0} [{1}]",
			equipment.Name,
			String.Join(" ", equipment[0].Format("{0}+{1}", " "), equipment[1].Format("{0}+{1}%", " ")).Trim()
			);

		public static string Format(this IStats stats, string statFormat, string delimiter) => String.Join(delimiter, GetAllStatEnums()
			.Select(stat => stats[stat] != 0 ? String.Format(statFormat, stat, stats[stat]) : null)
			.Where(s => s != null));

		public static void Print(this IUnit unit)
		{
			Console.WriteLine($"{unit.Name} (Lv {unit.GetStat(UnitProperties.Level)})");
			Console.WriteLine(String.Join("   ", Enum.GetValues(typeof(VolatileStats))
				.Cast<VolatileStats>()
				.Select(stat => $"{stat.ToString()}:{unit.GetStat(stat),2}")
			));
			Console.WriteLine(String.Join("   ", Enum.GetValues(typeof(Attributes))
				.Cast<Attributes>()
				.Select(attribute => $"{attribute.ToString()}:{unit.GetStat(attribute),2}")
			));
			Console.WriteLine(String.Join("   ", Enum.GetValues(typeof(CombatStats))
				.Cast<CombatStats>()
				.Select(stat => $"{stat.ToString()}:{unit.GetStat(stat),2}")
			));

			Console.WriteLine();
			Console.WriteLine("Equipment");
			unit.UseProperty<IEquipmentManager<EquipmentSlots>>(UnitProperties.Equipment, manager =>
			{
				foreach (EquipmentSlot<EquipmentSlots> slot in manager)
				{
					Console.WriteLine($"  {slot.SlotType}: \t{slot.Equipped?.Format() ?? "(unequipped)"}");
				}
			});

			Console.WriteLine();
			Console.WriteLine("Buffs");
			unit.UseProperty<IList<IBuff>>(UnitProperties.Buffs, buffs =>
			{
				foreach (IBuff buff in buffs)
				{
					Console.WriteLine($"  {buff.Name} ({buff.Description})");
				}
			});

			Console.WriteLine();
			Console.WriteLine("Stats");
			unit.GetProperty<IStatsProperty>(UnitProperties.Stats).Traverse((node, depth, i) =>
				Console.WriteLine("  {0}[{1}] {2}",
					new string(' ', depth),
					(node.Source as IEntity)?.Name ?? node.GetType().Name,
					node.Value.Format(
						$"{{0}}{(depth >= 1 && i[Math.Min(depth, 1)] > 0 ? "+" : " ")}{{1}}{(depth >= 1 && i[Math.Min(depth, 1)] == 2 ? "%" : "")}", ", "))
				);
		}
	}
}
