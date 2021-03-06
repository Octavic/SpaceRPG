﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WeaponMissile.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons
{
	using Assets.CombatScripts.Ships.Weapons;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;

	/// <summary>
	/// Describes a missile projectile
	/// </summary>
	public class WeaponMissile : WeaponGeneratedObject
	{
		/// <summary>
		/// The target of the missile
		/// </summary>
		public Ship Target;

		/// <summary>
		/// How fast the missile can turn per second
		/// </summary>
		public float MaxTurningPerSecond;

		/// <summary>
		/// How much time must pass until the missile start tracking
		/// </summary>
		public float TimeTillStartSeeking;

		/// <summary>
		/// Called when the projectile hits an enemy ship
		/// </summary>
		public override void OnHitEnemyShip()
		{
			this.Velocity = 0;
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.DeathDelayInSeconds = 0.3f;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected override void Update()
		{
			var oldRotation = this.transform.eulerAngles.z;
			float newAngle = oldRotation;

			if (this.TimeTillStartSeeking > 0)
			{
				this.TimeTillStartSeeking -= Time.deltaTime;
			}
			else
			{
				// Turn the missile if required
				if (this.Target != null)
				{
					var angleToTurn = CalcTurnAngle.InDegree(
						this.transform.position,
						Target.transform.position,
						oldRotation,
						this.MaxTurningPerSecond * Time.deltaTime);

					newAngle = oldRotation + angleToTurn;
					this.transform.eulerAngles = new Vector3(0, 0, oldRotation + angleToTurn);
				}
			}

			// Move missile forward
			newAngle *= Mathf.Deg2Rad;
			var moveVector = new Vector3(Mathf.Cos(newAngle), Mathf.Sin(newAngle)) * this.Velocity * Time.deltaTime;
			this.transform.position = this.transform.position + moveVector;
			base.Update();
		}
	}
}
