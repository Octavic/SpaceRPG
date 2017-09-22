using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.UI.GalaxyMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRPGTests_new.Assets.GeneralScripts.UI.GalaxyMap
{
	[TestClass()]
	public class GalaxyMapPathTest
	{
		[TestMethod()]
		public void SimplifyTest_CombineSameDirection()
		{
			var path = new GalaxyMapPath();
			var source = new MapCoordinate(0, 0);
			var dest = new MapCoordinate(0, 2);
			path.Nodes.Add(source);
			path.Nodes.Add(new MapCoordinate(0, 1));
			path.Nodes.Add(dest);
			path.Simplify();
			Assert.AreEqual(path.Nodes.Count, 2);
			Assert.AreEqual(path.Nodes[0], source);
			Assert.AreEqual(path.Nodes[1], dest);
		}

		[TestMethod()]
		public void SimplifyTest_CombineSameNode()
		{
			var path = new GalaxyMapPath();
			var source = new MapCoordinate(0, 0);
			var dest = new MapCoordinate(0, 2);
			path.Nodes.Add(source);
			path.Nodes.Add(source);
			path.Nodes.Add(dest);
			path.Simplify();
			Assert.AreEqual(path.Nodes.Count, 2);
			Assert.AreEqual(path.Nodes[0], source);
			Assert.AreEqual(path.Nodes[1], dest);
		}

		[TestMethod()]
		public void SimplifyTest_MultiplePathsNoSimplify()
		{
			var path = new GalaxyMapPath();
			var turn = new MapCoordinate(0, 2);
			var source = new MapCoordinate(0, 0);
			var dest = new MapCoordinate(2, 2);
			path.Nodes.Add(source);
			path.Nodes.Add(turn);
			path.Nodes.Add(dest);
			path.Simplify();
			Assert.AreEqual(path.Nodes.Count, 3);
			Assert.AreEqual(path.Nodes[0], source);
			Assert.AreEqual(path.Nodes[1], turn);
			Assert.AreEqual(path.Nodes[2], dest);
		}

		[TestMethod()]
		public void SimplifyTest_MultiplePathsCombine()
		{
			var path = new GalaxyMapPath();
			var source = new MapCoordinate(0, 0);
			var turn = new MapCoordinate(0, 2);
			var dest = new MapCoordinate(2, 2);
			path.Nodes.Add(source);
			path.Nodes.Add(new MapCoordinate(0, 1));
			path.Nodes.Add(turn);
			path.Nodes.Add(new MapCoordinate(1, 2));
			path.Nodes.Add(dest);
			path.Simplify();
			Assert.AreEqual(path.Nodes.Count, 3);
			Assert.AreEqual(path.Nodes[0], source);
			Assert.AreEqual(path.Nodes[1], turn);
			Assert.AreEqual(path.Nodes[2], dest);
		}

		[TestMethod()]
		public void GenerateFastestPath_SimpleTest()
		{
			var map = new GalaxyMapData(10, 10);
			var path = new GalaxyMapPath(map, new MapCoordinate(0, 0), new MapCoordinate(0, 9), GalaxyMapPathPriorityEnum.MostFuelEfficient);
			Assert.AreEqual(path.Nodes.Count, 2);
			Assert.AreEqual(path.Nodes[0], new MapCoordinate(0, 0));
			Assert.AreEqual(path.Nodes[1], new MapCoordinate(0, 9));
		}

		[TestMethod()]
		public void GenerateFastestPath_TurnTest()
		{
			var map = new GalaxyMapData(10, 10);
			var path = new GalaxyMapPath(map, new MapCoordinate(0, 0), new MapCoordinate(9, 9), GalaxyMapPathPriorityEnum.MostFuelEfficient);
			var possibleTurn1 = new MapCoordinate(0, 9);
			var possibleTurn2 = new MapCoordinate(9, 0);
			Assert.AreEqual(path.Nodes.Count, 3);
			Assert.AreEqual(path.Nodes[0], new MapCoordinate(0, 0));
			Assert.IsTrue(path.Nodes[1] == possibleTurn1 || path.Nodes[1] == possibleTurn2);
			Assert.AreEqual(path.Nodes[2], new MapCoordinate(9, 9));
		}

		[TestMethod()]
		public void GenerateSafestPath_SimpleTest()
		{
			var cur = new MapCoordinate(1, 1);
			var dic = new Dictionary<MapCoordinate, float>();
			dic[cur] = 0.1f;
			var map = new GalaxyMapData(2, 2);
			var tile1 = new GalaxyMapTile();
			tile1.CrimeRating = 0.1f;
			var tile2 = new GalaxyMapTile();
			tile2.CrimeRating = 0.1f;
			var tile3 = new GalaxyMapTile();
			tile3.CrimeRating = 0.9f;
			var tile4 = new GalaxyMapTile();
			tile4.CrimeRating = 0.1f;
			map.Tiles[0, 0] = tile1;
			map.Tiles[0, 1] = tile2;
			map.Tiles[1, 0] = tile3;
			map.Tiles[1, 1] = tile4;

			var newPath = new GalaxyMapPath(map, new MapCoordinate(0, 0), cur, GalaxyMapPathPriorityEnum.LeastCrimeRating);
			Assert.AreEqual(newPath.Nodes.Count, 3);
			Assert.AreEqual(newPath.Nodes[0], new MapCoordinate(0,0));
			Assert.AreEqual(newPath.Nodes[1], new MapCoordinate(0,1));
			Assert.AreEqual(newPath.Nodes[2], cur);
		}

		[TestMethod()]
		public void GenerateSafestPath_ComplexTest()
		{
			var source = new MapCoordinate(0, 1);
			var dest = new MapCoordinate(2, 1);
			var tile1 = new GalaxyMapTile();
			tile1.CrimeRating = 0.1f;
			var tile2 = new GalaxyMapTile();
			tile2.CrimeRating = 0.1f;
			var tile3 = new GalaxyMapTile();
			tile3.CrimeRating = 0.1f;
			var tile4 = new GalaxyMapTile();
			tile4.CrimeRating = 0.1f;
			var tile5 = new GalaxyMapTile();
			tile5.CrimeRating = 0.9f;
			var tile6 = new GalaxyMapTile();
			tile6.CrimeRating = 0.1f;
			var map = new GalaxyMapData(3, 2);
			map.Tiles[0, 0] = tile1;
			map.Tiles[1, 0] = tile2;
			map.Tiles[2, 0] = tile3;
			map.Tiles[0, 1] = tile4;
			map.Tiles[1, 1] = tile5;
			map.Tiles[2, 1] = tile6;

			var newPath = new GalaxyMapPath(map, source, dest, GalaxyMapPathPriorityEnum.LeastCrimeRating);
			Assert.AreEqual(newPath.Nodes.Count, 4);
			Assert.AreEqual(newPath.Nodes[0], source);
			Assert.AreEqual(newPath.Nodes[1], new MapCoordinate(0, 0));
			Assert.AreEqual(newPath.Nodes[2], new MapCoordinate(2, 0));
			Assert.AreEqual(newPath.Nodes[3], dest);
		}
	}
}
