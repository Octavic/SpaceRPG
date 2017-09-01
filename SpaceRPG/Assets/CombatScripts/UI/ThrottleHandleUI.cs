//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerShip.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;
    using Ships;

    /// <summary>
    /// Controls the throttle handle
    /// </summary>
     public class ThrottleHandleUI : MonoBehaviour
    {
        /// <summary>
        /// The maximum height
        /// </summary>
        public float MaxHeight;

        /// <summary>
        /// The rect transform
        /// </summary>
        private RectTransform _rectTransform;

        /// <summary>
        /// Called in the beginning
        /// </summary>
        private void Start()
        {
            this._rectTransform = this.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            this._rectTransform.localPosition = new Vector3(0, this.MaxHeight * PlayerController.CurrentInstance.CurrentThrottle);
        }
    }
}
