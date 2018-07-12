using System;
using System.Collections.Generic;

namespace MPC_Persistence
{
	public static class OrderStatus
	{
		public const int CREATE_NEW_ORDER = 1;
		public const int UPDATE_ORDER = 2;
	}
	public class Order
	{
		public int total { get; set; }
		public List<int> listquantity { get; set; }

		public int OrderStatus { get; set; }

		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public Table OrderTable { get; set; }

		public Item OrderItem { get; set; }

		public List<Table> Tablelist { get; set; }
		public List<Item> ItemsList { get; set; }

		// public Table this[int i]
		// {
		// 	get
		// 	{
		// 		if (Tablelist == null || Tablelist.Count == 0 || i < 0 || Tablelist.Count < i) return null;
		// 		return Tablelist[i];
		// 	}
		// 	set
		// 	{
		// 		if (Tablelist == null) Tablelist = new List<Table>();
		// 		Tablelist.Add(value);
		// 	}
		// }

		public Item this[int index]
		{
			get
			{
				if (ItemsList == null || ItemsList.Count == 0 || index < 0 || ItemsList.Count < index) return null;
				return ItemsList[index];
			}
			set
			{
				if (ItemsList == null) ItemsList = new List<Item>();
				ItemsList.Add(value);
			}
		}

		public Order()
		{
			ItemsList = new List<Item>();
		}
	}
}