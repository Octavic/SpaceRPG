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
	}
}
