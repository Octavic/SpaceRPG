using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.UI.GalaxyMap;

namespace SpaceRPGTests_new.Assets.GeneralScripts.UI.GalaxyMap
{
	[TestClass]
	public class GalaxyMapDataTest
	{
		[TestMethod]
		public void SquareBracket_Simple()
		{
			var map = new GalaxyMapData(10, 10);
			Assert.AreEqual(map.Tiles[0, 0], map[new MapCoordinate(0, 0)]);
		}

		[TestMethod]
		public void SquareBracket_Multiple()
		{
			var map = new GalaxyMapData(10, 10);
			var rand = new Random();
			for (int i = 0; i < 100; i++)
			{
				var x = rand.Next() % 10;
				var y = rand.Next() % 10;
				Assert.AreEqual(map.Tiles[x, y], map[new MapCoordinate(x, y)]);
			}
		}
	}
}
