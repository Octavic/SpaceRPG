//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GalaxyMapPath.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.UI.GalaxyMap
{
	using Assets.GeneralScripts.Utility;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// A path from a given node to a target node
	/// </summary>
	public class GalaxyMapPath
	{
		/// <summary>
		/// A list of nodes that this path will travel through
		/// </summary>
		public IList<MapCoordinate> Nodes { get; private set; }

		/// <summary>
		/// Source of the trip
		/// </summary>
		public MapCoordinate Source
		{
			get
			{
				return this.Nodes.First();
			}
		}

		/// <summary>
		/// Gets the destination of the trip
		/// </summary>
		public MapCoordinate Destination
		{
			get
			{
				return this.Nodes.Last();
			}
		}

		/// <summary>
		/// Gets or sets the path priority
		/// </summary>
		public GalaxyMapPathPriorityEnum PathPriority
		{
			get
			{
				return this._priority;
			}
		}

		/// <summary>
		/// The amount of fuel cost if the trip goes uninterrupted 
		/// </summary>
		public int FuelCost
		{
			get
			{
				return this.Nodes.Count;
			}
		}

		/// <summary>
		/// The path's priority
		/// </summary>
		private GalaxyMapPathPriorityEnum _priority;

		/// <summary>
		/// Creates a new instance of the <see cref="GalaxyMapPath"/> class
		/// </summary>
		/// <param name="map">The galaxy map</param>
		/// <param name="source">Source of the trip</param>
		/// <param name="destination">Destination of the trip</param>
		/// <param name="priority"> The priority when generating the path</param>
		public GalaxyMapPath(GalaxyMapData map, MapCoordinate source, MapCoordinate destination, GalaxyMapPathPriorityEnum priority)
		{
			this._priority = priority;
			if (priority == GalaxyMapPathPriorityEnum.MostFuelEfficient)
			{
				this.Nodes = this.GenerateShortestPath(map, source, destination);
				return;
			}

			// A hash map of every single coordinate and the total crime rating when traveling from there to the destination
			var totalCrimeToDest = new Dictionary<MapCoordinate, float>();
			totalCrimeToDest[destination] = map.Tiles[destination.X, destination.Y].CrimeRating;
			this.GenerateCrimeCostMap(destination, totalCrimeToDest, map);

			// Generate nodes
			var curCoor = source;
			while (curCoor != destination)
			{
				this.Nodes.Add(curCoor);

				var curWinnerNode = curCoor + _availableMoves[0];
				float curWinnder = totalCrimeToDest[curWinnerNode];
				for (int i = 1; i < _availableMoves.Count(); i++)
				{
					var curCheckNode = curCoor + _availableMoves[i];
					var curValue = totalCrimeToDest[curCheckNode];
					if (curValue < curWinnder)
					{
						curWinnder = curValue;
						curWinnerNode = curCheckNode;
					}
				}

				this.Nodes.Add(curWinnerNode);
				curCoor = curWinnerNode;
			}

			this.Nodes.Add(destination);
			this.Simplify();
		}

		/// <summary>
		/// A list  of possible move directions from a node
		/// </summary>
		private static readonly MapCoordinate[] _availableMoves = {
			new MapCoordinate(0,1),
			new MapCoordinate(0,-1),
			new MapCoordinate(-1,1),
			new MapCoordinate(1,0)
		};

		public void GenerateCrimeCostMap(MapCoordinate cur, Dictionary<MapCoordinate, float> totalCrimeToDest, GalaxyMapData map)
		{
			var curValue = totalCrimeToDest[cur];
			foreach (var direction in _availableMoves)
			{
				var checkCoor = cur + direction;
				if (checkCoor.X < 0 || checkCoor.X >= map.Width || checkCoor.Y < 0 || checkCoor.Y >= map.Height)
				{
					continue;
				}

				var checkValue = curValue + map.Tiles[checkCoor.X, checkCoor.Y].CrimeRating;
				float existValue;
				if (!totalCrimeToDest.TryGetValue(checkCoor, out existValue) || existValue > curValue)
				{
					totalCrimeToDest[checkCoor] = curValue;
				}
			}

			foreach (var direction in _availableMoves)
			{
				var checkCoor = cur + direction;
				if (checkCoor.X < 0 || checkCoor.X >= map.Width || checkCoor.Y < 0 || checkCoor.Y >= map.Height)
				{
					continue;
				}

				GenerateCrimeCostMap(checkCoor, totalCrimeToDest, map);
			}
		}

		/// <summary>
		/// Initializes a default instance of the <see cref="GalaxyMapPath"/> class
		/// </summary>
		public GalaxyMapPath()
		{
			this.Nodes = new List<MapCoordinate>();
		}

		/// <summary>
		/// Simplies the nodes 
		/// </summary>
		public void Simplify()
		{
			for (int i = this.Nodes.Count - 2; i >= 0; i--)
			{
				var cur = this.Nodes[i];
				var next = this.Nodes[i + 1];
				if (cur == next)
				{
					this.Nodes.RemoveAt(i);
					continue;
				}

				if (i != 0)
				{
					var prev = this.Nodes[i - 1];
					if (prev == cur)
					{
						this.Nodes.RemoveAt(i);
						continue;
					}

					var prevStep = CalcStep(prev, cur);

					var curStep = CalcStep(cur, next);
					if (prevStep == curStep)
					{
						this.Nodes.RemoveAt(i);
					}
				}
			}
		}

		/// <summary>
		/// Generates the shortest path
		/// </summary>
		/// <param name="source">source coordinate</param>
		/// <param name="destination">destination< coordinate/param>
		/// <returns>A list of coordinates to get to the destination from source</returns>
		private IList<MapCoordinate> GenerateShortestPath(GalaxyMapData map, MapCoordinate source, MapCoordinate destination)
		{
			// If they are on the same line, then just connect and return
			if (source.X == destination.X || source.Y == destination.Y)
			{
				return new List<MapCoordinate>() { source, destination };
			}

			var midPointA = new MapCoordinate(source.X, destination.Y);
			var midPointB = new MapCoordinate(destination.X, source.Y);

			var pathA = new List<MapCoordinate>() { source, midPointA, destination };
			var pathB = new List<MapCoordinate>() { source, midPointB, destination };

			return CalcTotalCrimeRating(map, pathA) < CalcTotalCrimeRating(map, pathB) ? pathA : pathB;
		}

		/// <summary>
		/// Calculates the step that needed to be taken to  reach "to" from "from". These two coordinates must lie on the same line
		/// </summary>
		/// <param name="from">source coordinate</param>
		/// <param name="to">destination coordinate</param>
		/// <returns>coordinate of each step</returns>
		private static MapCoordinate CalcStep(MapCoordinate from, MapCoordinate to)
		{
			// Check to make sure they are on the same row/column
			if (from.X != to.X && from.Y != to.Y)
			{
				return null;
			}

			// Check to make sure they are different coordinates
			if (from == to)
			{
				return null; 
			}

			return new MapCoordinate(Math.Sign(to.X - from.X), Math.Sign(to.Y - from.Y));
		}

		/// <summary>
		/// Calculates the average safety rating with the given path
		/// </summary>
		/// <param name="map">galaxy map</param>
		/// <param name="nodes">the list of nodes</param>
		/// <returns></returns>
		public static float CalcTotalCrimeRating(GalaxyMapData map, IList<MapCoordinate> nodes)
		{
			float total = 0;

			// Loop through each node
			for (int i = 1; i < nodes.Count; i++)
			{
				MapCoordinate from = nodes[i - 1];
				MapCoordinate to = nodes[i];
				var step = CalcStep(from, to);
				var cur = new MapCoordinate(from);
				while (cur != to)
				{
					total += map.Tiles[cur.X, cur.Y].CrimeRating;
				}
			}

			return total;
		}
	}
}
