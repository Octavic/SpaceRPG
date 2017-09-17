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
		public void TryConsumeTest_Return()
		{
			var scene = new DialogScene();
			var data = new Queue<string>();
			data.Enqueue("<SCENE 1>");
			data.Enqueue("<RETURN 1>");
			Assert.AreEqual(scene.TryConsume(data), true);
			Assert.AreEqual(scene.SceneId, 1);
			Assert.AreEqual(scene.ReturnValue, 1);
		}

		[TestMethod()]
		public void TryConsumeTest_Jump()
		{
			var scene = new DialogScene();
			var data = new Queue<string>();
			data.Enqueue("<SCENE 2>");
			data.Enqueue("<JUMP 1>");
			Assert.AreEqual(scene.TryConsume(data), true);
			Assert.AreEqual(scene.SceneId, 2);
			Assert.AreEqual(scene.JumpSceneId, 1);
		}

		[TestMethod()]
		public void TryConsumeTest_OneNPCItem()
		{
			var scene = new DialogScene();
			var data = new Queue<string>();
			data.Enqueue("<SCENE 1>");
			data.Enqueue("Hello, this is npc");
			data.Enqueue("<RETURN 1>");
			Assert.AreEqual(scene.TryConsume(data), true);
			Assert.AreEqual(scene.Dialogues.Count, 1);
			Assert.AreEqual(scene.Dialogues[0] is NpcDialogItem, true);
			var dialogItem = scene.Dialogues[0] as NpcDialogItem;
			Assert.AreEqual(dialogItem.Pages.Count, 1);
			Assert.AreEqual(dialogItem.Pages[0], "Hello, this is npc");
			Assert.AreEqual(scene.SceneId, 1);
			Assert.AreEqual(scene.ReturnValue, 1);
		}

		[TestMethod()]
		public void TryConsumeTest_OneNPCAndOnePlayertem()
		{
			var scene = new DialogScene();
			var data = new Queue<string>();
			data.Enqueue("<SCENE 1>");
			data.Enqueue("Hello, this is npc");
			data.Enqueue("    Hi");
			data.Enqueue("<RETURN 1>");

			Assert.AreEqual(scene.TryConsume(data), true);
			Assert.AreEqual(scene.Dialogues.Count, 2);
			Assert.AreEqual(scene.SceneId, 1);
			Assert.AreEqual(scene.ReturnValue, 1);

			Assert.AreEqual(scene.Dialogues[0] is NpcDialogItem, true);
			var dialogItem = scene.Dialogues[0] as NpcDialogItem;
			Assert.AreEqual(dialogItem.Pages.Count, 1);
			Assert.AreEqual(dialogItem.Pages[0], "Hello, this is npc");

			Assert.AreEqual(scene.Dialogues[1] is PlayerDialogItem, true);
			var playerItem = scene.Dialogues[1] as PlayerDialogItem;
			Assert.AreEqual(playerItem.Options.Count, 1);
			Assert.AreEqual(playerItem.Options[0].Option, "Hi");	
		}
	}
}