using System;
using System.Collections.Generic;

namespace MPC_Persistence
{
	public static class OrderStatus
	{
			public const int Pay_out =0;
		public const int Not_Pay_out= 1;
	}
	public class Order
	{
		public int total { get; set; }
//public List<int> listquantity { get; set; }as// toàn viết liên tha liên thiên

		public int Orderstatus { get; set; }

		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public Table OrderTable { get; set; }

		//public Item OrderItem { get; set; }

		// public List<Table> Tablelist { get; set; } // 1 hoas don co nhieu bang ? ak cho nay tao chua sua

		public List<Item> ItemsList { get; set; }
		public Account OrderAccount {get;set;}
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