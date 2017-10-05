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
	public class PlayerDialogItemTests
	{
		[TestMethod()]
		public void TryConsumeTest_OneLine()
		{
			var item = new PlayerDialogItem();
			var data = new Queue<string>();
			data.Enqueue("    Hi");
			data.Enqueue("<RETURN 1>");
			Assert.AreEqual(item.TryConsume(data), true);
			Assert.AreEqual(item.Options.Count, 1);
			Assert.AreEqual(item.Options[0].Option, "Hi");
			Assert.AreEqual(item.Options[0].ChangeSceneId, -1);
		}

		[TestMethod()]
		public void TryConsumeTest_OneLineWithBranch()
		{
			var item = new PlayerDialogItem();
			var data = new Queue<string>();
			data.Enqueue("    Hi<JUMP 1>");
			data.Enqueue("<RETURN 1>");
			Assert.AreEqual(item.TryConsume(data), true);
			Assert.AreEqual(item.Options.Count, 1);
			Assert.AreEqual(item.Options[0].Option, "Hi");
			Assert.AreEqual(item.Options[0].ChangeSceneId, 1);
		}

		[TestMethod()]
		public void TryConsumeTest_TwoLinesNoBranch()
		{
			var item = new PlayerDialogItem();
			var data = new Queue<string>();
			data.Enqueue("    Hi");
			data.Enqueue("    Hello");
			data.Enqueue("<RETURN 1>");
			Assert.AreEqual(item.TryConsume(data), true);
			Assert.AreEqual(item.Options.Count, 2);
			Assert.AreEqual(item.Options[0].Option, "Hi");
			Assert.AreEqual(item.Options[0].ChangeSceneId, -1);

			Assert.AreEqual(item.Options[1].Option, "Hello");
			Assert.AreEqual(item.Options[1].ChangeSceneId, -1);
		}

		[TestMethod()]
		public void TryConsumeTest_TwoLinesTwoBranches()
		{
			var item = new PlayerDialogItem();
			var data = new Queue<string>();
			data.Enqueue("    Hi<JUMP 1>");
			data.Enqueue("    Hello<JUMP 2>");
			data.Enqueue("<RETURN 1>");
			Assert.AreEqual(item.TryConsume(data), true);
			Assert.AreEqual(item.Options.Count, 2);
			Assert.AreEqual(item.Options[0].Option, "Hi");
			Assert.AreEqual(item.Options[0].ChangeSceneId, 1);

			Assert.AreEqual(item.Options[1].Option, "Hello");
			Assert.AreEqual(item.Options[1].ChangeSceneId, 2);
		}
	}
}