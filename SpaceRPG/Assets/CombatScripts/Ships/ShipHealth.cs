

namespace Assets.CombatScripts.Ships
{
	using Assets.CombatScripts.Utility;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Shows ship's health
	/// </summary>
	public class ShipHealth : MonoBehaviour
	{
		/// <summary>
		/// The target ship
		/// </summary>
		public Ship Target;

		/// <summary>
		/// The bar for the shield
		/// </summary>
		public ScaleBar ShieldBar;

		/// <summary>
		/// The bar for the hull
		/// </summary>
		public ScaleBar HullBar;

		/// <summary>
		/// The difference in position to the target
		/// </summary>
		protected Vector3 _posDiffToTarget;

		/// <summary>
		/// Called in the beginning
		/// </summary>
		protected void Start()
		{
			this._posDiffToTarget = this.transform.position - Target.transform.position;
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			this.ShieldBar.ratio = Target.CurrentShield / Target.MaxShield;
			this.HullBar.ratio = Target.CurrentHull / Target.MaxHull;
			this.transform.eulerAngles = new Vector3(0, 0, 0);
			this.transform.position = Target.transform.position + this._posDiffToTarget;
		}
	}
}
