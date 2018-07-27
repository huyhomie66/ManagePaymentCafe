using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_BL;
using MPC_DAL;
using System.Text;
using System.Globalization;

namespace PL_Console
{
	class PayoutConsole
	{
		public static Table t = new Table();
		public static Account a = new Account();
		public static OrderBL obl = new OrderBL();
		public static TableBL tbl = new TableBL();
		public static List<Order> orl = new List<Order>();
		public static AccountBL abl = new AccountBL();
		public static bool Payout()
		{


			while (true)
			{
				Console.Write("Input Id Table for pay: ");
				t.Table_Id = Validate.InputInt(Console.ReadLine());
				var result = obl.GetTableIdForPay(t.Table_Id);
				if (result != null)
				{
					orl = obl.GetTableIdForPay(t.Table_Id);
					break;
				}
				else
				{
					Console.WriteLine("Not Found this Table to pay!!!");
					continue;
				}
			}
			Console.WriteLine("||============================================||");
			Console.WriteLine("||================= Bill Infor ===============||");
			Console.WriteLine("||============================================||");
			Console.WriteLine("|| Item ID || Item Price || Quantity || Total ||");
			Console.WriteLine("||============================================||");
			foreach (var order in orl)
			{
				Console.WriteLine("||{0,-9}||{1,-12}||{2,-10}||{3,-7}||", order.OrderItem.ItemId, order.OrderItem.ItemPrice, order.OrderItem.Amount, order.Total);
			}
			Console.WriteLine("||============================================||");
			decimal totalmoney = obl.Total(orl, t.Table_Id);
			decimal fax = totalmoney / 10;
			decimal grandtotal = totalmoney + fax;
			Console.Write("||Total Money = {0:C} ", totalmoney);
			Console.WriteLine("Fax = {0:C} ",fax);
			Console.WriteLine("||Grand total = {0:C} ", grandtotal);
			Console.WriteLine("||============================================||");
			Console.Write("||Input Money for pay: ");
			decimal Money = Convert.ToDecimal(Console.ReadLine());
			while (true)
			{
				if (Money == grandtotal)
				{
					Console.WriteLine("||Pay Order: " + (obl.PayOrder(t) ? "successfully!" : "not successfully!"));
					break;
				}
				else if (Money < grandtotal)
				{
					decimal moneyshortage = grandtotal - Money;
					Console.WriteLine("||Money shortage : " + moneyshortage.ToString("F3", CultureInfo.InvariantCulture) + "VND");
					Console.WriteLine("||Input money shortage to complete order! : ");
					decimal a = Convert.ToDecimal(Console.ReadLine());
					if (a == moneyshortage)
					{
						Console.WriteLine("||Pay Order: " + (obl.PayOrder(t) ? "successfully!" : "not successfully!"));
						break;
					}
					else
					{
						continue;
					}
				}
				else if (Money > grandtotal)
				{
					decimal excesscash = Money - grandtotal;
					Console.WriteLine("||Excess Cash = {0:C} ",  excesscash);
					Console.WriteLine("||Pay Order: " + (obl.PayOrder(t) ? "successfully!" : "not successfully!"));
					break;

				}
			}
			Console.WriteLine("||===========================================||");
			Console.WriteLine("Press any key to back the menu...");
			Console.ReadKey();
			return true;
		}
	}
}