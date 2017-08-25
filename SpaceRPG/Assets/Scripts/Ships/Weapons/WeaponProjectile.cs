//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WeaponProjectile.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Ships.Weapons
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;

	public class WeaponProjectile : MonoBehaviour
	{
		/// <summary>
		/// Speed of the projectile
		/// </summary>
		public float Speed;

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			var oldPos = this.transform.localPosition;
			this.transform.localPosition = new Vector3(oldPos.x + this.Speed * Time.deltaTime, oldPos.y);
		}
	}
}
