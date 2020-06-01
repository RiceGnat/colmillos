using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colmillos.Stats
{
	[TestClass]
	public class ConstantStatsTests
	{
		[TestMethod]
		public void ReturnsValue()
		{
			ConstantStats stats = new ConstantStats(1);

			Assert.AreEqual(1, stats[Stat.A]);
		}
	}
}
