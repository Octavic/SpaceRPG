//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CalcTurnAngle.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.Utility
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Calculates how much to turn
	/// </summary>
	public static class CalcTurnAngle
	{
		/// <summary>
		/// Calculates how much to turn with the given position, rotation, and max turning allowed
		/// </summary>
		/// <param name="sourcePos">position of the source</param>
		/// <param name="targetPos">position of the target</param>
		/// <param name="oldRotation">The original rotation of the source</param>
		/// <param name="maxTurning">How much max turning is allowed</param>
		/// <returns>How much to turn</returns>
		public static float InDegree(
			Vector2 sourcePos,
			Vector2 targetPos,
			float oldRotation,
			float maxTurning)
		{
			// The angle that the target is at
			var targetAngle = (targetPos - sourcePos).ToAngleDegree();
			var angleDiff = CalcAngleDiff.InDegrees(oldRotation, targetAngle);
			float angleToTurn = angleDiff;

			// Check if the weapon is lined up. If the turning required is bigger than how much can be turned this frame, then weapon is no longer lined up
			if (Math.Abs(angleDiff) > maxTurning)
			{
				angleToTurn = Mathf.Sign(angleDiff) * maxTurning;
			}

			return angleToTurn;
		}
	}
}
