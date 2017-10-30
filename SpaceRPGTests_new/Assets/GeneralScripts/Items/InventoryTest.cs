using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GeneralScripts.Item;
using UnityEngine;

namespace SpaceRPGTests_new.Assets.GeneralScripts.Items
{
    [TestClass]
    public class InventoryTest
    {
        [TestMethod]
        public void Inventry_AddNormalItem()
        {
            var i = new Inventory(3,3);
            var smallItem = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(1, 1), 10, ItemRarityEnum.Normal);
            Assert.AreEqual(i.CanAddItem(smallItem, new UnityEngine.Vector2(0, 0)), true);
            i.AddItem(smallItem, new UnityEngine.Vector2(0, 0));
            Assert.AreEqual(i.Occupied.Count, 1);
            Assert.AreEqual(i.Occupied[new Vector2(0, 0)], smallItem);
            Assert.AreEqual(i.Contents.Count, 1);
            Assert.AreEqual(i.Contents[smallItem], new Vector2(0, 0));
        }

        [TestMethod]
        public void Inventry_AddBigItem()
        {
            var i = new Inventory(3, 3);
            var smallItem = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(2, 2), 10, ItemRarityEnum.Normal);
            Assert.AreEqual(i.CanAddItem(smallItem, new Vector2(1, 1)), true);
            i.AddItem(smallItem, new Vector2(1, 1));
            Assert.AreEqual(i.Occupied.Count, 4);
            Assert.AreEqual(i.Occupied[new Vector2(1, 1)], smallItem);
            Assert.AreEqual(i.Occupied[new Vector2(1, 2)], smallItem);
            Assert.AreEqual(i.Occupied[new Vector2(2, 1)], smallItem);
            Assert.AreEqual(i.Occupied[new Vector2(2, 2)], smallItem);
            Assert.AreEqual(i.Contents.Count, 1);
            Assert.AreEqual(i.Contents[smallItem], new Vector2(1, 1));
        }

        [TestMethod]
        public void Inventry_AddBigItem_TooBig()
        {
            var i = new Inventory(3, 3);
            var smallItem = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(4, 4), 10, ItemRarityEnum.Normal);
            Assert.AreEqual(i.CanAddItem(smallItem, new Vector2(1, 1)), false);
            Assert.AreEqual(i.Occupied.Count, 0);
            Assert.AreEqual(i.Contents.Count, 0);
        }

        [TestMethod]
        public void Inventry_AddBigItem_Conflict()
        {
            var i = new Inventory(3, 3);
            var item1 = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(2, 2), 10, ItemRarityEnum.Normal);
            var item2 = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(2, 2), 10, ItemRarityEnum.Normal);

            Assert.AreEqual(i.CanAddItem(item1, new Vector2(1, 1)), true);
            i.AddItem(item1, new Vector2(1, 1));
            Assert.AreEqual(i.CanAddItem(item2, new Vector2(0, 0)), false);
            Assert.AreEqual(i.Occupied.Count, 4);
            Assert.AreEqual(i.Contents.Count, 1);
        }
    }
}
