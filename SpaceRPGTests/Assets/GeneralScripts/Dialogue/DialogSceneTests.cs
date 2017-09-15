using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GeneralScripts.Dialogue.Tests
{
	[TestClass()]
	public class DialogSceneTests
	{
		[TestMethod()]
		public void TryConsumeTest()
		{
			var s = new DialogScene();
			var data = new Queue<string>();
			data.Enqueue("<SCENE 1>");
			data.Enqueue("<RETURN 1>");
			Assert.AreEqual(s.TryConsume(data), true);
			Assert.AreEqual(s.SceneId, 1);
			Assert.AreEqual(s.ReturnValue, 1);
		}
	}
}