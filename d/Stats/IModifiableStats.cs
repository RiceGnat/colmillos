using System;
using Colmillos.Nodes;

namespace Colmillos.Stats
{
	// TODO: name is vague
	public interface IModifiableStats : IStats, INode<IStats>
	{
		IStats BaseStats { get; }
		IStats ModificationBase { get; }
		IStats GetModifications(Enum modificationType);

		// TODO: rework interface to incorporate INode to allow traversal of stat modifications
	}
}
