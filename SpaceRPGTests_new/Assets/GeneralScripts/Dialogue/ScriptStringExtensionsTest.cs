using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Dialogue;

namespace SpaceRPGTests_new.Assets.GeneralScripts.Dialogue
{
	[TestClass]
	public class ScriptStringExtensionsTest
	{
		[TestMethod]
		public void ScriptStringExtension_TryGetTag_Head()
		{
			var s = "<RETURN 10>";
			string name;
			string value;
			Assert.AreEqual(s.TryGetTagNameValue(out name, out value), true);
			Assert.AreEqual(name, "RETURN");
			Assert.AreEqual(value, "10");
		}

		[TestMethod]
		public void ScriptStringExtension_TryGetTag_Tail()
		{
			var s = "    I will never do this! <JUMP 4>";
			string name;
			string value;
			Assert.AreEqual(s.TryGetTagNameValue(out name, out value), true);
			Assert.AreEqual(name, "JUMP");
			Assert.AreEqual(value, "4");
		}

		[TestMethod]
		public void ScriptStringExtension_TryGetTag_Invalid()
		{
			var s = "    I will never do this!";
			string name;
			string value;
			Assert.AreEqual(s.TryGetTagNameValue(out name, out value), false);
			Assert.AreEqual(name, null);
			Assert.AreEqual(value, null);
		}
	}
}
