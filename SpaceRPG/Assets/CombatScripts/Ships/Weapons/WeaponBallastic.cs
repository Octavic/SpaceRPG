//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WeaponBallastic.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;

	/// <summary>
	/// A simple project
	/// </summary>
	public class WeaponBallastic : WeaponGeneratedObject
	{
		/// <summary>
		/// How much to move per second
		/// </summary>
		protected Vector3 _velocityPerSecond;

		/// <summary>
		/// Called when the projectile hits an enemy ship
		/// </summary>
		public override void OnHitEnemyShip()
		{
			this.DeathDelayInSeconds = 0.0f;
		}

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
