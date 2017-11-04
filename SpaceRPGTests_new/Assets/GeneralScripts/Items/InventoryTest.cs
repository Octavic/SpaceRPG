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

        [TestMethod]
        public void Inventry_MoveItem_SelfConflict()
        {
            var i = new Inventory(4, 4);
            var item1 = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(3, 3), 10, ItemRarityEnum.Normal);

            Assert.AreEqual(i.CanAddItem(item1, new Vector2(1, 1)), true);
            i.AddItem(item1, new Vector2(1, 1));
            Assert.AreEqual(i.Occupied.Count, 9);
            Assert.AreEqual(i.Contents.Count, 1);
            Assert.AreEqual(i.CanAddItem(item1, new Vector2(0, 0)), true);
            i.TryRemoveItem(item1);
            i.AddItem(item1, new Vector2(0, 0));
            Assert.AreEqual(i.Occupied.Count, 9);
            Assert.AreEqual(i.Contents.Count, 1);
        }

        [TestMethod]
        public void Inventory_QuickAddItem_AddItems()
        {
            var i = new Inventory(2, 2);
            var smallItem1 = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(1, 1), 10, ItemRarityEnum.Normal);
            var smallItem2 = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(1, 1), 10, ItemRarityEnum.Normal);
            var bigItem = new NormalItem(0, "Gold nugget", new UnityEngine.Vector2(3, 3), 10, ItemRarityEnum.Normal);
            var result = i.CanQuickAddItem(smallItem1);
            Assert.AreEqual(result, new Vector2(0, 1));
            i.AddItem(smallItem1, result.Value);
            result = i.CanQuickAddItem(smallItem2);
            Assert.AreEqual(result, new Vector2(1, 1));
            i.AddItem(smallItem2, result.Value);
            Assert.AreEqual(i.CanQuickAddItem(bigItem), null);
        }
    }
}
