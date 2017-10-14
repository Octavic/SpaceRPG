
namespace Assets.GeneralScripts.Item
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// The behavior script for an item
    /// </summary>
    public class ItemBehavior : MonoBehaviour
    {
        /// <summary>
        /// The item data
        /// </summary>
        public IItem ItemData;

        public void MoveIndexSquareToLocalCoordinate(Vector2 target)
        {
            this.transform.localPosition = target + ItemData.Dimensions / 2;
        }
    }
}
