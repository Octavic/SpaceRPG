using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Dialogue;
using System.Collections.Generic;

namespace SpaceRPGTests_new.Assets.GeneralScripts.Dialogue
{
    [TestClass]
    public class ConversationTest
    {
        [TestMethod]
        public void ConversationTest_SingleScene()
        {
            var input = new List<string>();
            input.Add("<SCENE 1>");
            input.Add("This is just a tribute");
            input.Add("    Agreed");
            input.Add("<RETURN 1>");
            var c = new Conversation(input);
            Assert.AreEqual(c.Scenes.Count, 1);
            var scene0 = c.Scenes[0];
            Assert.AreEqual(scene0.SceneId, 1);
            Assert.AreEqual(scene0.Dialogues.Count, 2);
            Assert.AreEqual(scene0.ReturnValue, 1);
            Assert.AreEqual(scene0.Dialogues[0] is NpcDialogItem, true);
            Assert.AreEqual(scene0.Dialogues[1] is PlayerDialogItem, true);
            var dialog0 = scene0.Dialogues[0] as NpcDialogItem;
            var dialog1 = scene0.Dialogues[1] as PlayerDialogItem;
            Assert.AreEqual(dialog0.Pages.Count, 1);
            Assert.AreEqual(dialog0.Pages[0], "This is just a tribute");
            Assert.AreEqual(dialog1.Options.Count, 1);
            Assert.AreEqual(dialog1.Options[0].Option, "Agreed");
            Assert.AreEqual(dialog1.Options[0].ChangeSceneId, -1);
        }

        [TestMethod]
        public void ConversationTest_SingleScene_Filter_Comment()
        {
            var input = new List<string>();
            input.Add("<SCENE 1>");
            input.Add("# Comment here");
            input.Add("This is just a tribute");
            input.Add("    Agreed");
            input.Add("<RETURN 1>");
            var c = new Conversation(input);
            Assert.AreEqual(c.Scenes.Count, 1);
            var scene0 = c.Scenes[0];
            Assert.AreEqual(scene0.SceneId, 1);
            Assert.AreEqual(scene0.Dialogues.Count, 2);
            Assert.AreEqual(scene0.ReturnValue, 1);
            Assert.AreEqual(scene0.Dialogues[0] is NpcDialogItem, true);
            Assert.AreEqual(scene0.Dialogues[1] is PlayerDialogItem, true);
            var dialog0 = scene0.Dialogues[0] as NpcDialogItem;
            var dialog1 = scene0.Dialogues[1] as PlayerDialogItem;
            Assert.AreEqual(dialog0.Pages.Count, 1);
            Assert.AreEqual(dialog0.Pages[0], "This is just a tribute");
            Assert.AreEqual(dialog1.Options.Count, 1);
            Assert.AreEqual(dialog1.Options[0].Option, "Agreed");
            Assert.AreEqual(dialog1.Options[0].ChangeSceneId, -1);
        }

        [TestMethod]
        public void ConversationTest_Non_concurrent_scenes()
        {
            var input = new List<string>();
            input.Add("<SCENE 1>");
            input.Add("NPC line 1");
            input.Add("NPC line 2");
            input.Add("    Player option 1");
            input.Add("    Player option 2");
            input.Add("NPC line 3");
            input.Add("<JUMP 3>");
            input.Add("");
            input.Add("<SCENE 3>");
            input.Add("NPC line 4");
            input.Add("<RETURN 4>");

            var c = new Conversation(input);
            Assert.AreEqual(c.Scenes.Count, 2);
            var scene1 = c.Scenes[0];
            Assert.AreEqual(scene1.SceneId, 1);
            Assert.AreEqual(scene1.ReturnValue, -1);
            Assert.AreEqual(scene1.JumpSceneId, 3);
            Assert.AreEqual(scene1.Dialogues.Count, 3);

            var scene3 = c.Scenes[1];
            Assert.AreEqual(scene3.SceneId, 3);
            Assert.AreEqual(scene3.JumpSceneId, -1);
            Assert.AreEqual(scene3.ReturnValue, 4);
            Assert.AreEqual(scene3.Dialogues.Count, 1);
            var dialog30 = scene3.Dialogues[0] as NpcDialogItem;
            Assert.AreEqual(dialog30.Pages.Count, 1);
            Assert.AreEqual(dialog30.Pages[0], "NPC line 4");
        }
    }
}
