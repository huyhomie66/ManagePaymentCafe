using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MPC_Persistence;
using MPC_BL;
using MPC_DAL;
using System.Text;


namespace PL_Console
{
	class OrderConsole
	{
		public static bool Add(Account a)
		{
			Table t = new Table();
			Item it = new Item();
			Order o = new Order();
			OrderBL obl = new OrderBL();
			TableBL tbl = new TableBL();
			ItemBL itbl = new ItemBL();
			while (true)
			{
				Console.WriteLine("Input table Id: ");
				int tableid = Convert.ToInt32(Console.ReadLine());
				var result = tbl.CheckTableById(tableid);
				if (result != null)
				{
					t = tbl.GetTableById(tableid);
					break;
				}
				else if (result == null)
				{
					Console.WriteLine("Cant find this table or table is not empty!!!");
				}
			}
			while (true)
			{
				Console.WriteLine(" Input Item Id: ");
				int itemid = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Input quantity item: ");
				int quantity = Convert.ToInt32(Console.ReadLine());
				obl.AddItemToOrder(itemid, quantity, o);
				it = itbl.GetItemById(itemid);
				Console.WriteLine("Do you want to continue Add Item? ");
				string choice2 = Console.ReadLine();
				if (choice2 == "Y" || choice2 == "y")
				{
					continue;
				}
				else if (choice2 == "N" || choice2 == "n")
				{
					break;
				}
			}
			Console.WriteLine("Do you want to create order: ");
			char choice3 = Convert.ToChar(Console.ReadLine());
			switch (choice3)
			{
				case 'y':
					Console.WriteLine("Create Order: " + (obl.CreateOrder(o, a, t) ? "completed!" : "not complete!"));
					Console.WriteLine("Order ID: " + o.OrderId);
					Console.WriteLine("Order Date: " + o.OrderDate);

					foreach (var item in o.ItemsList)
					{
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");

					}
					//Console.WriteLine("Item: " + it.ItemId);
					Console.WriteLine("Order By: " + a.StaffName);
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;

				case 'Y':
					Console.WriteLine("Create Order: " + (obl.CreateOrder(o, a, t) ? "completed!" : "not complete!"));
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;
				case 'n':
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;
				case 'N':
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;
				default:
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;
			}
			return true;
		}

		public static bool Update(Account a, Order o)
		{
			OrderDAL obl = new OrderDAL();
			Console.WriteLine("Input Order Id to update: ");
			int orderid = Convert.ToInt32(Console.ReadLine());
			o = obl.GetOrderById(orderid);


			return true;
		}
		public static bool ShowListOrder(Account a, Order o)
		{
			// OrderDAL obl = new OrderDAL();
			// //o = obl.GetOrderById(orderid);
			// Console.WriteLine("================================================================================");
			// Console.WriteLine("============================= Order List Infor =================================");
			// Console.WriteLine("================================================================================");
			// Console.WriteLine("||Order ID    ||Item Id    ||Item Name    ||Quantity   ||Date Order   || Total  ");
			// Console.WriteLine("================================================================================");
			// Console.WriteLine();
			// foreach (var order in o.ItemsList)
			// {

			// 	Console.WriteLine("|{0,-10}||{1,-20}||{2,-16}||{3,-17}||{4,-7}|", order.OrderId, item.ItemId, item.ItemName, order.quantity, order.OrderDate);

			// }
			// Console.WriteLine("================================================================================");
			// Console.WriteLine("Press any key to back the menu...");
			// Console.ReadKey();
			return true;
		}
		public static void Delete()
		{

		}
		public static void Order(Account a)
		{
			short imChoose1;
			Table t = new Table();
			Item it = new Item();
			Order o = new Order();
			OrderBL obl = new OrderBL();
			TableBL tbl = new TableBL();
			ItemBL itbl = new ItemBL();

			string[] order = { "Create Order", "Edit Order", "Show list Order", "Exit" };
			do
			{
				imChoose1 = Menu("Order Management", order);
				switch (imChoose1)
				{
					case 1:
						Add(a);
						break;
					case 2:
						Update(a, o);
						break;
					case 3:
						ShowListOrder(a, o);
						break;
				}
			} while (imChoose1 != order.Length);
		}

		public static short Menu(string title, string[] menuItems)
		{
			short choose = 0;
			string line = "\n========================================";
			Console.WriteLine(line);
			Console.WriteLine(" " + title);
			Console.WriteLine(line);
			for (int i = 0; i < menuItems.Length; i++)
			{
				Console.WriteLine(" " + (i + 1) + ". " + menuItems[i]);
			}
			Console.WriteLine(line);
			do
			{
				Console.Write("Your choice: ");
				try
				{
					choose = Int16.Parse(Console.ReadLine());
				}
				catch
				{
					Console.WriteLine("Your Choose is wrong!");
					continue;
				}
			} while (choose <= 0 || choose > menuItems.Length);
			return choose;
		}
	}
}