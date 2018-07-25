using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_BL;
using MPC_DAL;
using System.Text;

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
		public static bool Payout(Order o)
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
			Console.WriteLine("||=============== Bill List Infor ============||");
			Console.WriteLine("||============================================||");
			Console.WriteLine("|| Item ID || Item Price || Quantity || Total ||");
			Console.WriteLine("||============================================||");
			foreach (var order in orl)
			{
				Console.WriteLine("||{0,-9}||{1,-12}||{2,-10}||{3,-7}||", order.OrderItem.ItemId, order.OrderItem.ItemPrice.ToString("#.##"), order.OrderItem.Amount, order.total.ToString("#.##"));
			}
			Console.WriteLine("||============================================||");
			decimal totalmoney = obl.Total(orl, t.Table_Id);
			decimal fax = totalmoney / 10;
			decimal grandtotal = totalmoney + fax;
			Console.WriteLine("||Total Money: " + totalmoney.ToString("#.##") + "VND" + "   Fax: " + fax.ToString("#.##") + "VND");
			Console.WriteLine("||Grand total(VND): " + grandtotal.ToString("#.##") + "VND");
			Console.WriteLine("||============================================||");
			Console.Write("||Input Money for pay: ");
			decimal Money = Validate.InputInt(Console.ReadLine());
			while (true)
			{
				if (Money == grandtotal)
				{
					Console.WriteLine("||Pay Order: " + (obl.PayOrder(t.Table_Id) ? "successfully!" : "not successfully!"));
					break;
				}
				else if (Money < grandtotal)
				{
					decimal moneyshortage = grandtotal - Money;
					Console.WriteLine("||Money shortage : " + moneyshortage + "VND");
					Console.WriteLine("||Input money shortage to complete order! : ");
					decimal a = Convert.ToDecimal(Console.ReadLine());
					if (a == moneyshortage)
					{
						Console.WriteLine("||Pay Order: " + (obl.PayOrder(t.Table_Id) ? "successfully!" : "not successfully!"));
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
					Console.WriteLine("||Excess Cash: " + excesscash + "VND");
					Console.WriteLine("||Pay Order: " + (obl.PayOrder(t.Table_Id) ? "successfully!" : "not successfully!"));
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