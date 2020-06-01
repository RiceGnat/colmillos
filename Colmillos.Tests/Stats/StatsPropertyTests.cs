using System;
using System.Linq;
using Colmillos.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Colmillos.Stats
{
	[TestClass]
	public class StatsPropertyTests
	{
		private IStatsProperty stats;

		[TestInitialize]
		public void Setup()
		{
			Mock<IStats> baseStats = new Mock<IStats>();
			baseStats.SetupGet(m => m[It.IsAny<Stat>()]).Returns(1);

			stats = new StatsProperty(baseStats.Object, null);
		}

		[TestMethod]
		public void GetStat()
		{
			Assert.AreEqual(1, stats[Stat.A]);
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
			Assert.AreEqual(3, resolver.Count());
		}

		[TestMethod]
		public void StatsPropertyModifier()
		{
			Mock<IStats> mod = new Mock<IStats>();
			mod.SetupGet(m => m[It.IsAny<Stat>()]).Returns(3);

			StatsPropertyModifier modifier = new StatsPropertyModifier(mods => mods[0] * mods[1] + mods[2])
			{
				new StatsModification((a, b) => a * b, 1, mod.Object),
				new StatsModification((a, b) => a + b, 0, mod.Object)
			};

			IStatsProperty modified = modifier.GetModifiedProperty(stats, null, null);

			Assert.AreEqual(6, modified[Stat.A]);
			Assert.AreEqual(3, modified.Count());
			Assert.AreEqual(3, modified.ElementAt(1).Value[Stat.A]);
			Assert.AreEqual(3, modified.ElementAt(2).Value[Stat.A]);

			modified = modifier.GetModifiedProperty(modified, null, null);

			Assert.AreEqual(15, modified[Stat.A]);
			Assert.AreEqual(9, modified.ElementAt(1).Value[Stat.A]);
			Assert.AreEqual(6, modified.ElementAt(2).Value[Stat.A]);
		}
	}
}
