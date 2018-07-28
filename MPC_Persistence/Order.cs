using System;
using System.Collections.Generic;

namespace MPC_Persistence
{
	public static class OrderStatus
	{
			public const int Pay_out =1;
		public const int Not_Pay_out= 0;
	}
	public class Order
	{ 
		public decimal Total{ get; set; }
		public int Orderstatus { get; set; }
		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public Item OrderItem{get;set;}
		public Table OrderTable { get; set; }
		public List<Order> OrderList{get;set;}
		public List<Item> ItemsList { get; set; }
		public Account OrderAccount {get;set;}
	

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