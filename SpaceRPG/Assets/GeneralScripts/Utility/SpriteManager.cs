//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SpriteManager.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Prefab manager
    /// </summary>
    public class SpriteManager : MonoBehaviour
    {
        /// <summary>
        /// A collection of item sprites
        /// </summary>
        public List<Sprite> ItemSprites;

        /// <summary>
        /// Gets the current instance of the <see cref="SpriteManager"/> class
        /// </summary>
        public static SpriteManager CurrentInstance
        {
            get
            {
                if (SpriteManager._currentInstance == null)
                {
                    SpriteManager._currentInstance = GameObjectFinder.FindGameObjectWithTag(Tags.SpriteManager).GetComponent<SpriteManager>();
                }

                return SpriteManager._currentInstance;
            }
        }

        /// <summary>
        /// The current instance of the class
        /// </summary>
        private static SpriteManager _currentInstance;

        /// <summary>
        /// Cached sprites
        /// </summary>
        private Dictionary<string, Sprite> _cachedSprites = new Dictionary<string, Sprite>();

        /// <summary>
        /// Gets the sprite for the given item
        /// </summary>
        /// <param name="itemId">Id of the item</param>
        /// <returns>Sprite for the item, null if not found</returns>
        public Sprite GetItemSprite(int itemId)
        {
            if (itemId < 0 || itemId >= this.ItemSprites.Count)
            {
                return null;
            }

            return this.ItemSprites[itemId];
        }

        /// <summary>
        /// Gets the sprite by path
        /// </summary>
        /// <param name="path">path</param>
        /// <returns></returns>
        public Sprite GetSprite(string path)
        {
            Sprite result;

            // If sprite exists in the cache, just return that
            if (this._cachedSprites.TryGetValue(path, out result))
            {
                return result;
            }

            // Load sprite
            return result;
        }
    }
}
