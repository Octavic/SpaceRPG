//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IShipWeaponFireMode.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships.Weapons.WeaponFireMode
{
	/// <summary>
	/// Fires one shot at a time
	/// </summary>
	public class SingleFire : IShipWeaponFireMode
	{
		/// <summary>
		/// The ship weapon
		/// </summary>
		private ShipWeapon _weapon;

		/// <summary>
		/// Creates a new instance of the <see cref="SingleFire"/> class
		/// </summary>
		/// <param name="weapon">The weapon that's being fired</param>
		public SingleFire(ShipWeapon weapon)
		{
			this._weapon = weapon;
		}

		/// <summary>
		/// Try to fire the weapon
		/// </summary>
		/// <returns>True if the weapon fired</returns>
		public bool TryFire()
		{
			this._weapon.GenerateProjectile();
			return true;
		}
	}
}
