using System;
using System.Collections.Generic;
using System.Linq;
using Colmillos.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Colmillos.Stats
{
	[TestClass]
	public class DerivableStatsPropertyTests
	{
		private IStatsProperty stats;

		[TestInitialize]
		public void Setup()
		{
			Mock<IStats> baseStats = new Mock<IStats>();
			baseStats.SetupGet(m => m[It.Is<Stat>(stat => stat == Stat.A)]).Returns(1);

			stats = new DerivableStatsProperty(baseStats.Object, null)
			{
				{ Stat.B, s => s[Stat.A] * 2 }
			};
		}

		[TestMethod]
		public void GetDerivedStat()
		{
			Assert.AreEqual(1, stats[Stat.A]);
			Assert.AreEqual(2, stats[Stat.B]);
		}

		[TestMethod]
		public void GetResolver()
		{
			Mock<IStats> mod = new Mock<IStats>();
			mod.SetupGet(m => m[It.IsAny<Stat>()]).Returns(2);

			Mock<INode<IStats>> node = new Mock<INode<IStats>>();
			node.SetupGet(m => m.Value).Returns(mod.Object);

			IStatsProperty resolver = stats.GetResolver(mods => mods[0] * mods[1] + mods[2], new INode<IStats>[] { node.Object, node.Object });

			Assert.AreEqual(4, resolver[Stat.A]);
			Assert.AreEqual(18, resolver[Stat.B]);
			Assert.AreEqual(3, resolver.Count());
			Assert.AreEqual(1, resolver.First().Value[Stat.A]);
			Assert.AreEqual(8, resolver.First().Value[Stat.B]);
			Assert.AreEqual(2, resolver.ElementAt(1).Value[Stat.A]);
			Assert.AreEqual(2, resolver.ElementAt(1).Value[Stat.B]);
			Assert.AreEqual(2, resolver.ElementAt(2).Value[Stat.A]);
			Assert.AreEqual(2, resolver.ElementAt(2).Value[Stat.B]);
		}
	}
}
