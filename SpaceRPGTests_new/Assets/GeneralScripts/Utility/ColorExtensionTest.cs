using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Utility;
using UnityEngine;

namespace SpaceRPGTests_new.Assets.GeneralScripts.Utility
{
	[TestClass]
	public class ColorExtensionTest
	{
		[TestMethod]
		public void Lerp_Half()
		{
			var color = new Color(0, 0, 0, 0);
			var target = new Color(1, 1, 1, 1);
			var colorLerp = color.Lerp(target, 0.5f);
			Assert.AreEqual(colorLerp.r, 0.5f);
			Assert.AreEqual(colorLerp.g, 0.5f);
			Assert.AreEqual(colorLerp.b, 0.5f);
			Assert.AreEqual(colorLerp.a, 0.5f);
		}
	}
}
