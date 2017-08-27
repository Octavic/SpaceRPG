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
		public float Speed;

		/// <summary>
		/// How far to move the bullet in the x direction
		/// </summary>
		public float StartDistance;

		/// <summary>
		/// How much to move per second
		/// </summary>
		protected Vector3 _offset;

		/// <summary>
		/// Called in the beginning for intialization
		/// </summary>
		protected void Start()
		{
			var angle = this.transform.eulerAngles.z*Mathf.Deg2Rad;
			this._offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Speed;
			this.transform.position = this.transform.position + this._offset.normalized * this.StartDistance;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected override void Update()
		{
			this.transform.position = this.transform.position + this._offset * Time.deltaTime;
			base.Update();
		}
	}
}
