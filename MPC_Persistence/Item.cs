using System;

namespace MPC_Persistence
{
	public static class ItemStatus
	{
		public const int Get_Food = 0;
		public const int Get_Drink = 1;
	}
	public class Item // Item_Category
	{
		public int ItemId { get; set; }
		public string ItemName { get; set; }
		public decimal ItemPrice { get; set; }
		public int Amount { get; set; }
		public int Status { get; set; }
	}
}