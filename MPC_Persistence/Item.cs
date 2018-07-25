using System;

namespace MPC_Persistence
{
	public static class ItemStatus
	{
		public const int InStock = 1;
		public const int SoldOut = 0;
	}
	public class Item 
	{
		private int _ItemId;
		public int ItemId
		{
			get
			{
				return _ItemId;
			}
			set
			{
				while (value > 100 || value < 0)
				{
					Console.WriteLine("Cant find this Item for order!, please re-enter: ");
					value = Convert.ToInt32(Console.ReadLine());

				}
				_ItemId = value;
			}
		}
		public string ItemName { get; set; }
		public decimal ItemPrice { get; set; }
		private int _Amount;
		public int Amount
		{
			get { return _Amount; }
			set
			{
				while (value > 100)
				{
					Console.WriteLine("The amount you enter is too large compared to the stock, we can not respond, re - enter: ");
					value = Convert.ToInt32(Console.ReadLine());
				}
				while (value < 0)
				{
					Console.WriteLine("The amount you enter can not be negative ,please re-enter: ");
					value = Convert.ToInt32(Console.ReadLine());
				}
				_Amount = value;
			}
		}
		public int Status { get; set; }
	}
}