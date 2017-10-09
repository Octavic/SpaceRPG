//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PlayerOptioBehaviour.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Dialogue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Represents the player option
    /// </summary>
    public class PlayerOptioBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Provides call back
        /// </summary>
        public DialogBoxBehaviour DialogBox;

        /// <summary>
        /// The scene to jump to, -1 if none
        /// </summary>
        public int ChangeSceneId;

        /// <summary>
        /// Called when this option is clicked
        /// </summary>
        public void OnClick()
        {
            this.DialogBox.OnPlayerSelectOption(this.ChangeSceneId);
        }
    }
}
