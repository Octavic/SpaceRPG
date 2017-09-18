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
	public class NpcDialogItemTests
	{
		[TestMethod()]
		public void TryConsumeTest_OneLine()
		{
			var dialogItem = new NpcDialogItem();
			var data = new Queue<string>();
			data.Enqueue("Hello traveler");
			data.Enqueue("<RETURN 1>");

			Assert.AreEqual(dialogItem.TryConsume(data), true);
			Assert.AreEqual(dialogItem.Pages.Count, 1);
			Assert.AreEqual(dialogItem.Pages[0], "Hello traveler");
		}

		[TestMethod()]
		public void TryConsumeTest_TwoLines()
		{
			var dialogItem = new NpcDialogItem();
			var l1 = "Hello traveler";
			var l2 = "Welcome to the shop!";
			var data = new Queue<string>();
			data.Enqueue(l1);
			data.Enqueue(l2);
			data.Enqueue("<RETURN 1>");

			Assert.AreEqual(dialogItem.TryConsume(data), true);
			Assert.AreEqual(dialogItem.Pages.Count, 2);
			Assert.AreEqual(dialogItem.Pages[0], l1);
			Assert.AreEqual(dialogItem.Pages[1], l2);
		}
	}
}