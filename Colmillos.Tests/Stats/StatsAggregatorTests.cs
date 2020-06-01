using System.Collections.Generic;
using Colmillos.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Colmillos.Stats
{
	[TestClass]
	public class StatsAggregatorTests
	{
		[TestMethod]
		public void AdditiveAggregation()
		{
			Mock<IStats> stats = new Mock<IStats>();
			stats.SetupGet(m => m[It.IsAny<Stat>()]).Returns(1);

			Node<IStats> node = new Node<IStats>(stats.Object, null);

			StatsAggregator aggregator = new StatsAggregator(new StatsModification((a, b) => a + b, 0),
				new List<INode<IStats>>
				{
					node, node, node
				});

			Assert.AreEqual(3, aggregator[Stat.A]);
		}

		[TestMethod]
		public void MultiplicativeAggregation()
		{
			Mock<IStats> stats = new Mock<IStats>();
			stats.SetupGet(m => m[It.IsAny<Stat>()]).Returns(2);

			Node<IStats> node = new Node<IStats>(stats.Object, null);

			StatsAggregator aggregator = new StatsAggregator(new StatsModification((a, b) => a * b, 1),
				new List<INode<IStats>>
				{
					node, node, node
				});

			Assert.AreEqual(8, aggregator[Stat.A]);
		}
	}
}
