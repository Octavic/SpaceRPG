//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipListUIItem.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.UI
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using UnityEngine.UI;
	using Ships;

	/// <summary>
	/// The ship list item
	/// </summary>
	public class ShipListUIItem : MonoBehaviour
	{
		/// <summary>
		/// The image's icon
		/// </summary>
		public Image Icon;
		
		/// <summary>
		/// Name of the ship
		/// </summary>
		public Text NameText;

		/// <summary>
		/// Distance from the ship to player
		/// </summary>
		public Text DistanceText;

		/// <summary>
		/// The icon for if any weapon target is targetting this
		/// </summary>
		public GameObject TargetIcon;

		/// <summary>
		/// Gets the target that this item representts
		/// </summary>
		public Ship Target { get; set; }

		/// <summary>
		/// Gets the distance from the target to the player
		/// </summary>
		public float Distance
		{
			get
			{
				if (this.Target == null)
				{
					return 0;
				}

				return (this.Target.transform.position - PlayerController.CurrentInstance.gameObject.transform.position).magnitude;
			}
		}

		/// <summary>
		/// Assigns a ship
		/// </summary>
		/// <param name="target">Target ship</param>
		public void AssignShip(Ship target)
		{
			this.Target = target;
			this.NameText.text = target.ShipName;
			this.Icon.color = Minimap.CurrentInstance.GetColor(target.ShipAttitude, true);
		}

		/// <summary>
		/// Called when the button is clicked
		/// </summary>
		public void OnClick()
		{
			PlayerController.CurrentInstance.SetTargetForCurrentWeaponSystem(this.Target);
		}

		/// <summary>
		/// Focuses the main camera on the target
		/// </summary>
		public void FocusCamera()
		{
			MainCamera.CurrentInstance.FocusTarget = this.Target.gameObject;
		}

		/// <summary>
		/// Checks the mouse event
		/// </summary>
		/// <param name="isEnter">If the mouse just entered or exited</param>
		public void CheckMouseEvent(bool isEnter)
		{
			GameController.CurrentInstance.SetHighlightStatus(this.Target, isEnter);
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			this.DistanceText.text = this.Distance.ToString(".00");
			this.TargetIcon.SetActive(PlayerController.CurrentInstance.IsShipTargeted(this.Target));
		}
	}
}
