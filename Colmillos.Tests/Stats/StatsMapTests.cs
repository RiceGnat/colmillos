using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colmillos.Stats
{
	[TestClass]
	public class StatsMapTests
	{
		[TestMethod]
		public void SetAndGet()
		{
			StatsMap stats = new StatsMap
			{
				[Stat.A] = 1,
				[Stat.B] = 2
			};

			Assert.AreEqual(1, stats[Stat.A]);
			Assert.AreEqual(2, stats[Stat.B]);
		}
	}
}
