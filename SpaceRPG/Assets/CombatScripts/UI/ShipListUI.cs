//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShipListUI.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.CombatScripts.UI
{
	using Assets.CombatScripts.Ships;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using UnityEngine;
	using Utility;

	/// <summary>
	/// A list of ships
	/// </summary>
	public class ShipListUI : MonoBehaviour
	{
		/// <summary>
		/// prefab for the UI item 
		/// </summary>
		public GameObject ItemPrefab;

		/// <summary>
		/// The item difference in height
		/// </summary>
		public float ItemHeight = 25;

		/// <summary>
		/// Gets the current instance
		/// </summary>
		public static ShipListUI CurrentInstance
		{
			get
			{
				if (ShipListUI._currentInstance == null)
				{
					ShipListUI._currentInstance = GameObjectFinder.FindGameObjectWithTag(Tags.ShipListUI).GetComponent<ShipListUI>();
				}

				return ShipListUI._currentInstance;
			}
		}

		/// <summary>
		/// Gets the list of item 
		/// </summary>
		public IList<ShipListUIItem> Items { get; private set; }

		/// <summary>
		/// The current instance
		/// </summary>
		private static ShipListUI _currentInstance;

		/// <summary>
		/// if the list is sorted by distance currently
		/// </summary>
		private bool _isSortByDistance;

		/// <summary>
		/// Registers a new ship
		/// </summary>
		/// <param name="newShip">The new ship</param>
		public void RegisterShip(Ship newShip)
		{
			if (this.Items == null)
			{
				this.Items = new List<ShipListUIItem>();
			}

			var newItem = Instantiate(this.ItemPrefab, this.transform, true);
			newItem.transform.localPosition = new Vector3(0, - this.Items.Count * this.ItemHeight);
			var itemComp = newItem.GetComponent<ShipListUIItem>();
			itemComp.AssignShip(newShip);
			this.Items.Add(itemComp);
		}

		/// <summary>
		/// Destroys the target ship
		/// </summary>
		/// <param name="deadShip">The destroyed ship</param>
		public void DestroyShip(Ship deadShip)
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Target == deadShip)
				{
					var deadShipItem = this.Items[i];
					Destroy(deadShipItem.gameObject);
					this.Items.RemoveAt(i);
					this.RearrangeItems();
					return;
				}
			}
		}

		/// <summary>
		/// Sort the ships by the ship's attitude
		/// </summary>
		public void SortByAttitude()
		{
			this.Items = this.Items.OrderBy(item => item.Target.ShipAttitude).ToList();
			this.RearrangeItems();
			this._isSortByDistance = false;
		}

		/// <summary>
		/// Sort the ships by the ship's name
		/// </summary>
		public void SortByName()
		{
			this.Items = this.Items.OrderBy(item => item.Target.ShipName).ToList();
			this.RearrangeItems();
			this._isSortByDistance = false;
		}

		/// <summary>
		/// Sort the ships by ship distance
		/// </summary>
		public void SortByDistance()
		{
			this.Items = this.Items.OrderBy(item => item.Distance).ToList();
			this.RearrangeItems();
			this._isSortByDistance = true;
		}

		/// <summary>
		/// Called in the beginning
		/// </summary>
		protected void Start()
		{
			if (ShipListUI._currentInstance == null)
			{
				ShipListUI._currentInstance = this;
			}

			if (this.Items == null)
			{
				this.Items = new List<ShipListUIItem>();
			}
		}

		/// <summary>
		/// Called once per frame
		/// </summary>
		protected void Update()
		{
			// If it's currently sort by distance, the distance could be updated
			if (this._isSortByDistance)
			{
				this.SortByDistance();
			}
		}

		/// <summary>
		/// Rearranges the items in the list and move them to the right Y position
		/// </summary>
		private void RearrangeItems()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				var curItem = this.Items[i];
				curItem.transform.localPosition = new Vector3(0, -i * this.ItemHeight);
			}
		}
	}
}
