//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MapCoordinate.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.UI.GalaxyMap
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Represents a coordinate in the map
	/// </summary>
	public class MapCoordinate
	{
		/// <summary>
		/// Gets or sets the X coordinate
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// Gets or sets the Y coordinate
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		/// Creates a new instance of the <see cref="MapCoordinate"/> class
		/// </summary>
		/// <param name="x">the X coordinate</param>
		/// <param name="y">the Y coordinate</param>
		public MapCoordinate(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Gets the magnitude of the coordinate
		/// </summary>
		public float Magnitude
		{
			get
			{
				return Mathf.Sqrt(this.X * this.X + this.Y * this.Y);
			}
		}

		/// <summary>
		/// Gets a  'unit' vector/coordinate that represents where this coordinate is heading with x/y values of either -1, 0, or 1
		/// </summary>
		public MapCoordinate Sign()
		{
			return new MapCoordinate(Math.Sign(this.X), Math.Sign(this.Y));
		}

		/// <summary>
		/// Adds two coordinates
		/// </summary>
		/// <param name="a">first coordinate</param>
		/// <param name="b">second coordinate</param>
		/// <returns>added coordinate</returns>
		public static MapCoordinate operator +(MapCoordinate a, MapCoordinate b)
		{
			return new MapCoordinate(a.X + b.X, a.Y + b.Y);
		}

		/// <summary>
		/// Gets the difference between two coordinates
		/// </summary>
		/// <param name="a">destination coordinate</param>
		/// <param name="b">source coordinate</param>
		/// <returns>difference between two coordinates</returns>
		public static MapCoordinate operator -(MapCoordinate a, MapCoordinate b)
		{
			return new MapCoordinate(a.X - b.X, a.Y - b.Y);
		}

		/// <summary>
		/// Scales the coordinate
		/// </summary>
		/// <param name="a">source coordinate</param>
		/// <param name="scale">how much to multiply it by</param>
		/// <returns>scaled coordinate</returns>
		public static MapCoordinate operator *(MapCoordinate a, int scale)
		{
			return new MapCoordinate(a.X * scale, a.Y * scale);
		}

		/// <summary>
		/// Scales the coordinate
		/// </summary>
		/// <param name="a">source coordinate</param>
		/// <param name="scale">how much to multiply it by</param>
		/// <returns>scaled coordinate</returns>
		public static MapCoordinate operator /(MapCoordinate a, int scale)
		{
			return new MapCoordinate(a.X / scale, a.Y / scale);
		}

		/// <summary>
		/// Overwrites the equals operator
		/// </summary>
		/// <param name="a">first coordinate</param>
		/// <param name="b">second coordinate</param>
		/// <returns>true if they are equal</returns>
		public static bool operator ==(MapCoordinate a, MapCoordinate b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		/// <summary>
		/// Overwrites the unequal operator
		/// </summary>
		/// <param name="a">first coordinate</param>
		/// <param name="b">second coordinate</param>
		/// <returns>true if they aren't equal</returns>
		public static bool operator !=(MapCoordinate a, MapCoordinate b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		/// <summary>
		/// Overrides the Equals() function
		/// </summary>
		/// <param name="obj">item to be compared with</param>
		/// <returns>True if the two objects are equal</returns>
		public override bool Equals(object obj)
		{
			if (obj is MapCoordinate)
			{
				return this == obj as MapCoordinate;
			}

			return base.Equals(obj);
		}

		/// <summary>
		/// Gets the hash code
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
