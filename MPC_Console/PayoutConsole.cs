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
		public static ItemBL ibl = new ItemBL();
		public static List<Order> orl = new List<Order>();
		public static AccountBL abl = new AccountBL();
		public static void Payout()
		{
			Console.Clear();
			DateTime now = DateTime.Now;
			DateTime date = now;

			Console.WriteLine("||=========================================||");
			Console.WriteLine("|| Table Id  || Table Name || Table Status ||");
			foreach (var table in tbl.DisplayListTable())
			{
				if (table.Status == 0)
				{
					string stt = "empty";
					Console.WriteLine("||{0,-11}||{1,-12}||{2,-14}||", table.Table_Id, table.TableName, stt);
				}
				else if (table.Status == 1)
				{
					string stt = "has some one";
					Console.WriteLine("||{0,-11}||{1,-12}||{2,-14}||", table.Table_Id, table.TableName, stt);
				}
			}
			Console.WriteLine("||=========================================||");
			while (true)
			{
				Console.Write("Input Id Table for pay: ");
				t.Table_Id = Validate.InputInt(Console.ReadLine());
				var result = obl.GetTableIdForPay(t.Table_Id);
				if (result.Count <= 0)
				{
					Console.WriteLine("This table not have order!");
					Console.WriteLine("Do you want to try again ?(Y/N)");
					char choice = Validate.InputToChar(Console.ReadLine());
					switch (choice)
					{
						case 'y':
							continue;
						case 'Y':
							continue;
						default:
							Console.WriteLine("Press any key to back the menu: ");
							Console.ReadKey();
							OrderConsole.MenuOrder(a);
							break;
					}
				}
				else
				{
					orl = obl.GetTableIdForPay(t.Table_Id);
					break;
				}
			}
			Console.WriteLine("Your Order: ");
			foreach (var order in orl)
			{
				Console.WriteLine("Item Name: " + ibl.GetItemById(order.OrderItem.ItemId).ItemName + " - " + order.OrderItem.Amount + " pieces");
			}
			var StaffName = abl.CheckAccountById(obl.GetOrderByTableID(t.Table_Id).OrderAccount.Account_Id).StaffName;

			decimal totalmoney = obl.Total(orl, t.Table_Id);
			decimal fax = totalmoney / 10;
			decimal grandtotal = totalmoney + fax;
			Console.WriteLine("Money to be pay = {0:C} ", grandtotal);
			decimal Money = 0;
			do
			{
				Console.Write("Input Money for pay: ");
				try
				{
					Money = Decimal.Parse(Console.ReadLine());
				}
				catch
				{
					Console.WriteLine("Wrong value or The characters you enter are too large for the allowed length!, please re-enter: ");
					continue;
				}
			} while (Money <= 0 || Money > 10000);

			while (true)
			{
				if (Money == grandtotal)
				{
					break;
				}
				else if (Money < grandtotal)
				{
					decimal moneyshortage = grandtotal - Money;
					Console.WriteLine("Money shortage : " + moneyshortage.ToString("C"));
					decimal a = 0;
					do
					{
						Console.WriteLine("Input money shortage to complete order! : ");
						try
						{
							a = Convert.ToDecimal(Console.ReadLine());
						}
						catch
						{
							Console.WriteLine("Wrong value or The characters you enter are too large for the allowed length!, please re-enter: ");
							continue;
						}
					} while (a <= 0 || a > 10000);

					if (a == moneyshortage)
					{
						break;
					}
					else
					{
						Console.WriteLine("Please enter the correct amount!");
						continue;
					}
				}
				else if (Money > grandtotal)
				{
					decimal excesscash = Money - grandtotal;
					Console.WriteLine("Excess Cash = {0:C} ", excesscash);
					break;
				}
			}
			Console.WriteLine("Pay Order: " + (obl.PayOrder(t,obl.GetOrderByTableID(t.Table_Id).OrderAccount.Account_Id) ? "successfully!" : "not successfully!"));

			Console.WriteLine("||==============================================================================||");
			Console.WriteLine("||=============================== Bill Infor ===================================||");
			Console.WriteLine("||Table: {0,2}                                                                     ||", t.Table_Id);
			Console.WriteLine("||Staff Name:{0,-5}                                                              ||", StaffName);
			Console.WriteLine("||Date Check Out: {0,-20}                                          ||", now);
			Console.WriteLine("||==============================================================================||");
			Console.WriteLine("||     Item Name    ||    Item Price    ||     Quantity     ||     Total        ||");
			Console.WriteLine("||==============================================================================||");
			foreach (var order in orl)
			{
				Console.WriteLine("||{0,-18}||{1,-18}||{2,-18}||{3,-18}||", ibl.GetItemById(order.OrderItem.ItemId).ItemName, order.OrderItem.ItemPrice.ToString("C"), order.OrderItem.Amount, order.Total.ToString("C"));
			}
			Console.WriteLine("||==============================================================================||");
			Console.WriteLine("||Total Money = {0,-6} , Tax(10%) = {1,-6}                                      ", totalmoney.ToString("C"), fax.ToString("C"));
			Console.WriteLine("||==============================================================================||");
			Console.WriteLine("||Grand Total(USD) = {0,-6}                                                     ", grandtotal.ToString("C"));
			Console.WriteLine("||===========================Thanks you very much !=============================||");
			Console.WriteLine("||==============================================================================||");
			Console.WriteLine("||Press any key to back the menu...");
			Console.ReadKey();
		}
	}
}