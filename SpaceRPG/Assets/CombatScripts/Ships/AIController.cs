//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="AIController.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Ships
{
    using GeneralScripts.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Utility;

    /// <summary>
    /// An AI controlled ship
    /// </summary>
    public class AIController : ShipController
    {
        /// <summary>
        /// The attitude of the AI towards the player
        /// </summary>
        public ShipAttitudeEnum ShipAttitude;

        /// <summary>
        /// The type of AI 
        /// </summary>
        public EnemyAITypes EnemyAIType;

        /// <summary>
        /// The minimal amount of distance that this enemy will try to keep from the player
        /// </summary>
        public float MinDistance;

        /// <summary>
        /// How far to zone from the player
        /// </summary>
        public float ZoneDistance;

        /// <summary>
        /// The player's ship
        /// </summary>
        private Ship _playerShip;

		/// <summary>
		/// Called in the beginning for initialization
		/// </summary>
		protected void Start()
		{
            // Rotate the ship randomly
            var randomZ = GlobalRandom.Next(360);
            this.transform.localEulerAngles = new Vector3(0, 0, randomZ);

            // Gives the ship a random initial momentum
            var oldVelocity = this.GetComponent<Rigidbody2D>().velocity;
            this.GetComponent<Rigidbody2D>().velocity = oldVelocity.normalized * GlobalRandom.Next(0, this.ShipComponent.MaximumSpeed, 100);

            // If the ship is enemy, target player with the weapon
            if (this.ShipAttitude == ShipAttitudeEnum.Hostile)
            {
                var playerShip = PlayerController.CurrentInstance.ShipComponent;
                this._playerShip = playerShip;

                for (int i = 0; i < this.WeaponSystems.Count; i++)
                {
                    var curSystem = this.WeaponSystems[i];
                    curSystem.CurrentTarget = playerShip;
                    curSystem.FireMode = Weapons.WeaponSystemAutoFireModeEnum.Rapid;
                }
            }
		}

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            if (this.ShipAttitude == ShipAttitudeEnum.Hostile)
            {
                var distanceToPlayer = (this._playerShip.transform.position - this.transform.position).magnitude;
                if (this.EnemyAIType == EnemyAITypes.Aggressive)
                {
                    if (distanceToPlayer > this.MinDistance)
                    {
                        this.ApproachPlayer();
                    }
                    else
                    {
                        this.RunAwayFromPlayer();
                    }
                }
                else if (this.EnemyAIType == EnemyAITypes.Escaping)
                {
                    this.RunAwayFromPlayer();
                }
                else if (this.EnemyAIType == EnemyAITypes.Zoning)
                {
                    if (distanceToPlayer > this.ZoneDistance)
                    {
                        this.ApproachPlayer();
                    }
                    else
                    {
                        this.RunAwayFromPlayer();
                    }
                }
            }
        }

        /// <summary>
        /// Adjusts the ship's throttle based on the amount that the ship wants to eventually turn
        /// </summary>
        /// <param name="turnAngle">How much to turn in degrees</param>
        private void AdjustThrottleBasedOnTurn(float turnAngle)
        {
            var absTurn = Math.Abs(turnAngle);

            if (absTurn < 5)
            {
                this.ShipComponent.CurrentThrottle = 1.0f;
            }
            else if (absTurn < 90)
            {
                this.ShipComponent.CurrentThrottle = 0.3f;
            }
            else if (absTurn < 150)
            {
                this.ShipComponent.CurrentThrottle = 0.2f;
            }
            else
            {
                this.ShipComponent.CurrentThrottle = 0.0f;
            }
        }

        private void ApproachPlayer()
        {
            var amountToTurn = CalcTurnAngle.InDegree(
                this.transform.position,
                this._playerShip.transform.position,
                this.transform.eulerAngles.z,
                180);

            this.AdjustThrottleBasedOnTurn(amountToTurn);
            var maxRotation = this.ShipComponent.RotationSpeed * Time.deltaTime;
            this.ShipComponent.TryTurn(amountToTurn.LimitTo(-maxRotation, maxRotation));
        }

        private void RunAwayFromPlayer()
        {
            var amountToTurn = CalcTurnAngle.InDegree(
                 this._playerShip.transform.position,
                 this.transform.position,
                 this.transform.eulerAngles.z,
                 this.ShipComponent.RotationSpeed * Time.deltaTime);

            this.AdjustThrottleBasedOnTurn(amountToTurn);
            var maxRotation = this.ShipComponent.RotationSpeed * Time.deltaTime;
            this.ShipComponent.TryTurn(amountToTurn.LimitTo(-maxRotation, maxRotation));
        }
    }
}
