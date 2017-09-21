using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Utility;
using Assets.GeneralScripts.UI.GalaxyMap;
using static Assets.GeneralScripts.Utility.WeightedRandomList<string>;
using System.Collections.Generic;

namespace SpaceRPGTests_new.Assets.GeneralScripts.Utility
{
	[TestClass]
	public class WeightedRandomListTest
	{
		[TestMethod]
		public void WeightedItemConstructor()
		{
			var item = new WeightedItem<string>("data1", 10);
			Assert.AreEqual(item.Item, "data1");
			Assert.AreEqual(item.Weight, 10);
		}

		[TestMethod]
		public void WeightedItemListConstructor()
		{
			var item = new WeightedRandomList<string>();
			Assert.AreEqual(item.Items.Count, 0);
		}

		[TestMethod]
		public void WeightedItemList_AddNewItem()
		{
			var list = new WeightedRandomList<string>();
			list.AddNewItem("data1", 10);
			Assert.AreEqual(list.Items[0].Item, "data1");
		}

		[TestMethod]
		public void WeightedItemList_GetRandomItem()
		{
			var list = new WeightedRandomList<string>();
			list.AddNewItem("data1", 10);
			list.AddNewItem("data2", 100);
			int data1Count = 0;
			int data2Count = 0;
			for (int i = 0; i < 100; i++)
			{
				var item = list.GetRandomItem();
				Assert.IsTrue(item == "data1" || item == "data2");
				if (item == "data1")
				{
					data1Count++;
				}
				else if (item == "data2")
				{
					data2Count++;
				}
			}

			Assert.IsTrue(data1Count < data2Count);
		}

		public class MyClass
		{
		}

		[TestMethod]
		public void WeightedItemList_Empty()
		{
			var list = new WeightedRandomList<string>();
			Assert.AreEqual(list.GetRandomItem(), default(string));

			var list2 = new WeightedRandomList<MyClass>();
			Assert.AreEqual(list.GetRandomItem(), default(MyClass));
		}
	}
}
