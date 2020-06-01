using System;
using System.Collections.Generic;
using Colmillos.Nodes;

namespace Colmillos.Stats
{
	public interface IStatsProperty : IStats, INode<IStats>
	{
		IStatsProperty GetResolver(Func<int[], int> resolver, IEnumerable<INode<IStats>> nodes);
	}
}
