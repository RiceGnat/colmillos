using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Colmillos.Nodes;

namespace Colmillos.Stats
{
	[Serializable]
	public class StatsPropertyModifier : IStatsPropertyModifier, IEnumerable<StatsModification>
	{
		private readonly List<StatsModification> mods = new List<StatsModification>();
		private readonly Func<int[], int> resolver;

		public StatsPropertyModifier(Func<int[], int> resolver)
		{
			this.resolver = resolver;
		}

		public void Add(StatsModification modification) => mods.Add(modification);

		public IStats this[int index] => mods[index].Values;

		public IStatsProperty GetModifiedProperty(IStatsProperty property, IProperties properties, IEntity modifier)
		{
			INode<IStats>[] previous = property.ToArray();
			INode<IStats>[] nodes = new INode<IStats>[mods.Count];
			for (int i = 0; i < mods.Count; i++)
			{
				INode<IStats> current = new Node<IStats>(mods[i].Values, modifier);
				nodes[i] = new StatsAggregator(mods[i], previous.Length > 0 ? previous[i + 1].Append(current) : Enumerable.Repeat(current, 1));
			}
			return property.GetResolver(resolver, nodes);
		}

		IEnumerator<StatsModification> IEnumerable<StatsModification>.GetEnumerator() => mods.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => mods.GetEnumerator();
	}
}
