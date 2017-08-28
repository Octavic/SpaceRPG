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

	public class WeaponProjectile : DelayedSelfDestruct
	{
		/// <summary>
		/// Speed of the projectile
		/// </summary>
		public float Velocity;

		/// <summary>
		/// How much to move per second
		/// </summary>
		protected Vector3 _velocityPerSecond;

		/// <summary>
		/// Called in the beginning for intialization
		/// </summary>
		protected void Start()
		{
			var angle = this.transform.eulerAngles.z*Mathf.Deg2Rad;
			this._velocityPerSecond = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Velocity;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected override void Update()
		{
			this.transform.position = this.transform.position + this._velocityPerSecond * Time.deltaTime;
			base.Update();
		}
	}
}
