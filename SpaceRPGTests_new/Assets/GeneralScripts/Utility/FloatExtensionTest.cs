using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Utility;

namespace SpaceRPGTests_new.Assets.GeneralScripts.Utility
{
	[TestClass]
	public class FloatExtensionTest
	{
		[TestMethod]
		public void Lerp_Half()
		{
			float f1 = 0;
			float goal = 1;
			Assert.AreEqual(f1.Lerp(goal, 0.5f), 0.5f);
		}

		[TestMethod]
		public void Lerp_HalfFromOne()
		{
			float f1 = 1;
			float goal = 3;
			Assert.AreEqual(f1.Lerp(goal, 0.5f), 2);
		}
	}
}
