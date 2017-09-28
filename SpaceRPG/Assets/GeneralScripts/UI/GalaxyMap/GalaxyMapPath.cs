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
			totalCrimeToDest[destination] = map[destination].CrimeRating;
			this.GenerateCrimeCostMap(destination, totalCrimeToDest, map);

			this.Nodes = new List<MapCoordinate>();

			// Generate nodes
			var curCoor = source;
			while (curCoor != destination)
			{
				this.Nodes.Add(curCoor);

				// Get a list of available nodes
				var surroundCoors = map.GetSurroundingCoordinates(curCoor);
				var curWinnerCoor = surroundCoors[0];
				var curWinnerValue = totalCrimeToDest[curWinnerCoor];

				for (int i = 1; i < surroundCoors.Count; i++)
				{
					var curCheckCoor = surroundCoors[i];
					var curValue = totalCrimeToDest[curCheckCoor];
					if (curValue < curWinnerValue)
					{
						curWinnerValue = curValue;
						curWinnerCoor = curCheckCoor;
					}
				}

				this.Nodes.Add(curWinnerCoor);
				curCoor = curWinnerCoor;
			}

			this.Nodes.Add(destination);
			this.Simplify();
		}

		private enum MapNodeDirection
		{
			Up,
			Down,
			Left,
			Right,
			None
		}

		private class MapNodeData
		{
			private static List<MapCoordinate> directionVectors = new List<MapCoordinate>()
			{
				new MapCoordinate(0,1),
				new MapCoordinate(0,-1),
				new MapCoordinate(-1,0),
				new MapCoordinate(1,0),
				new MapCoordinate(0,0)
			};
			public float Total { get; set; }
			public MapNodeDirection SourceDirection { get; set; }
			public List<MapCoordinate> Dependencies { get; private set; }
			public MapNodeData(float total, MapNodeDirection sourceDirection)
			{
				this.Total = total;
				this.SourceDirection = sourceDirection;
				this.Dependencies = new List<MapCoordinate>();
			}
			public MapCoordinate GetSourceCoordiante()
			{
				return MapNodeData.directionVectors[(int)this.SourceDirection];
			}
		}

		private void GenerateCrimeCostMap(MapCoordinate cur, Dictionary<MapCoordinate, float> totalCrimeToDest, GalaxyMapData map)
		{
			var curValue = totalCrimeToDest[cur];
			var validCoors = map.GetSurroundingCoordinates(cur);

			for (int i = validCoors.Count - 1; i >= 0; i--)
			{
				var validCoor = validCoors[i];
				var checkValue = curValue + map[validCoor].CrimeRating;
				float existValue;
				if (totalCrimeToDest.TryGetValue(validCoor, out existValue))
				{
					validCoors.RemoveAt(i);
					if (existValue > curValue)
					{
						totalCrimeToDest[validCoor] = checkValue;
						this.UpdateCrimeCostMap(validCoor, totalCrimeToDest, map);
					}
				}
				else 
				{
					totalCrimeToDest[validCoor] = checkValue;
				}
			}

			foreach (var validCoor in validCoors)
			{
				GenerateCrimeCostMap(validCoor, totalCrimeToDest, map);
			}
		}

		/// <summary>
		/// Updates the crime sum map
		/// </summary>
		/// <param name="cur">node to be updated</param>
		/// <param name="totalCrimeToDest">the hash map</param>
		/// <param name="map">galaxy map</param>
		private void UpdateCrimeCostMap(MapCoordinate cur, Dictionary<MapCoordinate, float> totalCrimeToDest, GalaxyMapData map)
		{
			var curValue = map[cur];
			var curTotal = totalCrimeToDest[cur];
			var neighbors = map.GetSurroundingCoordinates(cur);
			foreach (var neighbor in neighbors)
			{
				var newPossible = curTotal + map[neighbor].CrimeRating;
				float neighborTotal;
				if (totalCrimeToDest.TryGetValue(neighbor, out neighborTotal) && neighborTotal > newPossible)
				{
					totalCrimeToDest[neighbor] = newPossible;
					this.UpdateCrimeCostMap(neighbor, totalCrimeToDest, map);
				}
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
		/// Simplifies the nodes 
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
		public static MapCoordinate CalcStep(MapCoordinate from, MapCoordinate to)
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
					total += map[cur].CrimeRating;
					cur = cur + step;
				}
			}

			return total;
		}
	}
}
