using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Utility
{
	public class WeightedRandomList<T>
	{
		/// <summary>
		/// The item definition
		/// </summary>
		/// <typeparam name="T">Return type</typeparam>
		public class WeightedItem
		{
			/// <summary>
			/// Gets or sets the item
			/// </summary>
			public T Item { get; set; }

			/// <summary>
			/// Gets or sets the weight
			/// </summary>
			public int Weight { get; set; }

			/// <summary>
			/// Creates a new instance  of the <see cref="WeightedItem{T}"/> class 
			/// </summary>
			/// <param name="item">new item to be inserted</param>
			/// <param name="weight">weight of the item</param>
			public WeightedItem(T item, int weight)
			{
				this.Item = item;
				this.Weight = weight;
			}
		}

		/// <summary>
		/// A list of weighted items
		/// </summary>
		public IList<WeightedItem> Items { get; private set; }

		/// <summary>
		/// The current sum of all  weights in the list
		/// </summary>
		private int _total;

		/// <summary>
		/// Creates a new instance of the <see cref="WeightedRandomList{T}"/> class
		/// </summary>
		public WeightedRandomList()
		{
			this.Items = new List<WeightedItem>();
			this._total = 0;
		}

		/// <summary>
		/// Adds a new item
		/// </summary>
		/// <param name="item">new  item to be added to the weighted list</param>
		/// <param name="weight">Weight of this item</param>
		public void AddNewItem(T item, int weight)
		{
			this.Items.Add(new WeightedItem(item, weight));
			this._total += weight;
		}

		/// <summary>
		/// Gets a random item based on weight
		/// </summary>
		/// <returns>Result item</returns>
		public T GetRandomItem()
		{
			if (this.Items.Count == 0)
			{
				return default(T);
			}

			var rand = GlobalRandom.Next(this._total);
			foreach (var item in this.Items)
			{
				if (rand <= item.Weight)
				{
					return item.Item;
				}

				rand -= item.Weight;
			}

			return this.Items.Last().Item;
		}
	}
}
