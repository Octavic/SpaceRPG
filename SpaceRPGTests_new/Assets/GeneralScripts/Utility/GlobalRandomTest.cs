using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Utility;


namespace SpaceRPGTests_new.Assets.GeneralScripts.Utility
{
	[TestClass]
	public class GlobalRandomTest
	{
		[TestMethod]
		public void Next()
		{
			var next = GlobalRandom.Next();
			Assert.AreEqual(next > 0, true);
		}

		[TestMethod]
		public void NextWithMax()
		{
			for (int i = 0; i < 100; i++)
			{
				var next = GlobalRandom.Next(1000);
				Assert.AreEqual(next >= 0 && next < 1000, true);
			}
		}

		[TestMethod]
		public void NextWithMinMax()
		{
			for (int i = 0; i < 100; i++)
			{
				var next = GlobalRandom.Next(100, 1000);
				Assert.AreEqual(next >= 100 && next < 1000, true);
			}
		}

		[TestMethod]
		public void NextFloat()
		{
			for (int i = 0; i < 100; i++)
			{
				var next = GlobalRandom.Next(100, 1000, 100);
				Assert.AreEqual(next >= 100 && next < 1000, true);
			}
		}
	}
}
