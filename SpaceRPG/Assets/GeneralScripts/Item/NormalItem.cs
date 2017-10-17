//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NormalItem.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Item
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// A normal item
    /// </summary>
    public class NormalItem : IItem
    {
        public int ItemId { get; private set; }

        public string ItemName { get; private set; }

        public Vector2 Dimensions{ get; private set; }

        public float BaseSellValue { get; private set; }

        public ItemRarityEnum Rarity { get; private set; }

        public NormalItem(int itemId, string itemName, Vector2 dimensions, float baseSellValue, ItemRarityEnum rarity)
        {
            this.ItemId = itemId;
            this.ItemName = itemName;
            this.Dimensions = dimensions;
            this.BaseSellValue = baseSellValue;
            this.Rarity = rarity;
        }
    }
}
