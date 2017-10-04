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
	using UnityEngine;

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
			var totalCrimeToDest = new Dictionary<MapCoordinate, CostMapNode>();
			var updateQueue = new Queue<MapCoordinate>();
			foreach (var validCoor in map.GetSurroundingCoordinates(destination))
			{
				updateQueue.Enqueue(validCoor);
			}

			totalCrimeToDest[destination] = new CostMapNode(map[destination].CrimeRating, destination, null);
			this.GenerateCrimeCostMap(updateQueue, totalCrimeToDest, map);

			this.Nodes = new List<MapCoordinate>();

			// Generate nodes
			var curNode = totalCrimeToDest[source];
			while (curNode.Coordinate != destination)
			{
				this.Nodes.Add(curNode.Coordinate);
				curNode = curNode.Source;
			}

			this.Nodes.Add(destination);

			this.Simplify();
		}

		/// <summary>
		/// Each node of the cost dictionary
		/// </summary>
		private class CostMapNode
		{
			/// <summary>
			/// The average crime rate
			/// </summary>
			public float RatingSum;

			/// <summary>
			/// The coordinate
			/// </summary>
			public MapCoordinate Coordinate;

			/// <summary>
			/// Which node does this node depend on
			/// </summary>
			public CostMapNode Source;

			/// <summary>
			/// A list of child nodes that depends on this node
			/// </summary>
			public List<CostMapNode> Children;

			/// <summary>
			/// Creates a new instance of the <see cref="CostMapNode"/> class 
			/// </summary>
			/// <param name="thisCrimeRating">The crime rating for this tile</param>
			/// <param name="coordinate">The coordinate for this node</param>
			/// <param name="from">Which node this node originated from</param>
			public CostMapNode(float thisCrimeRating, MapCoordinate coordinate, CostMapNode from = null)
			{
				var fromSum = from != null ? from.RatingSum : 0;
				this.RatingSum = fromSum + thisCrimeRating;
				this.Source = from;
				this.Children = new List<CostMapNode>();
				if (from != null)
				{
					from.Children.Add(this);
				}
				this.Coordinate = coordinate;
			}

			/// <summary>
			/// switch to a new source
			/// </summary>
			/// <param name="newSource">The new source</param>
			public void SwitchTo(CostMapNode newSource)
			{
				this.Source.Children.Remove(this);
				this.Source = newSource;
				newSource.Children.Add(this);
			}

			/// <summary>
			/// Calculates the potential average if this node comes from the given node
			/// </summary>
			/// <param name="from">originate node</param>
			/// <param name="thisCrimeRating">Crime rating for this tile</param>
			/// <returns>Potential average</returns>
			public float SumFrom(CostMapNode from, float thisCrimeRating)
			{
				var fromSum = from != null ? from.RatingSum : 0;
				return fromSum + thisCrimeRating;
			}

			public float AverageTo(float newCrimeRating)
			{
				return this.RatingSum + newCrimeRating;
			}
		}
		
		/// <summary>
		/// Updates all of the nodes in the queue
		/// </summary>
		/// <param name="updateQueue">the list of nodes to be updated</param>
		/// <param name="totalCrimeToDest">the hash map containing the result</param>
		/// <param name="map">the galaxy map data</param>
		private void GenerateCrimeCostMap(
			Queue<MapCoordinate> updateQueue, 
			Dictionary<MapCoordinate, CostMapNode> totalCrimeToDest, 
			GalaxyMapData map)
		{
			var pendingCoors = new HashSet<MapCoordinate>();

			while (updateQueue.Count > 0)
			{
				var curCoor = updateQueue.Dequeue();
				var curRating = map[curCoor].CrimeRating;

				CostMapNode winner = null;
				float winnerAverage = 0.0f ;

				var potentialImprovements = new List<CostMapNode>();
				foreach (var neighborCoor in map.GetSurroundingCoordinates(curCoor))
				{
					CostMapNode neighborNode;

					//  If the neighbor is null, check if it's already waiting in queue
					if (!totalCrimeToDest.TryGetValue(neighborCoor, out neighborNode))
					{
						if (!pendingCoors.Contains(neighborCoor))
						{
							pendingCoors.Add(neighborCoor);
							updateQueue.Enqueue(neighborCoor);
						}
						continue;
					}

					// Neighbor is available for comparison
					if (winner == null)
					{
						winner = neighborNode;
						winnerAverage = neighborNode.AverageTo(curRating);
						continue;
					}

					var neightborAvg = neighborNode.AverageTo(curRating);
					if (neightborAvg < winnerAverage)
					{
						potentialImprovements.Add(winner);
						winner = neighborNode;
						winnerAverage = neightborAvg;
					}
				}

				// Finish scan, winner is best route to take
				var curNode = new CostMapNode(curRating, curCoor, winner);
				totalCrimeToDest[curCoor] = curNode;

				// Go through potential nodes and see if they can be improved
				foreach (var potentialNode in potentialImprovements)
				{
					if (curNode.AverageTo(map[potentialNode.Coordinate].CrimeRating) < potentialNode.RatingSum)
					{
						potentialNode.SwitchTo(curNode);
						recurUpdate(potentialNode, map);
					}
				}
			}
		}

		private void recurUpdate(CostMapNode node, GalaxyMapData map)
		{
			if (node.Source == null)
			{
				return;
			}

			var potentialSum = node.AverageTo(map[node.Coordinate].CrimeRating);

			// Check if the current node is worth updating
			if (node.RatingSum <= potentialSum)
			{
				return;
			}

			node.RatingSum = potentialSum;
			foreach (var child in node.Children)
			{
				recurUpdate(child, map);
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
