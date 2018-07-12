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
		public static void Add()
		{
			while (true)
			{


				AccountBL account = new AccountBL();
				TableBL table = new TableBL();
				Table t = new Table();
				Account ap = new Account();
				Item it = new Item();
				Order o = new Order();
				OrderBL ob = new OrderBL();
				TableBL tbl = new TableBL();
				ItemBL itbl = new ItemBL();
				while (true)
				{
					Console.WriteLine("Input table Id: ");
					int tableid= Convert.ToInt32(Console.ReadLine());
					if (tbl.checkById(tableid) != null)
					{
						break;
					}
					else
					{
						continue;
					}
				}

				// tbl.checkById(tableid);
				while (true)
				{
					Console.WriteLine(" Input Item Id: ");

					int itemid = Convert.ToInt32(Console.ReadLine());
					o.ItemsList.Add(itbl.GetItemById(itemid));
					Console.WriteLine("Input quantity item: ");
					int quantity = Convert.ToInt32(Console.ReadLine());
					ob.AddItemToOrder(itemid, quantity, o);
					Console.WriteLine("Do you want to continue Add Item? ");
					string choice = Console.ReadLine();
					if (choice == "Y" || choice == "y")
					{
						// Console.WriteLine("Create Order: " + (ob.CreateOrder(o) ? "completed!" : "not complete!"));
						continue;
					}
					else if (choice == "N" || choice == "n")
					{
						break;
					}

				}
				// Console.WriteLine("Input Date-Order: ");
				// DateTime date = Convert.ToDateTime(Console.ReadLine());
				Console.WriteLine("Do you want to continue Add Order");
				string choice1 = Console.ReadLine();
				if (choice1 == "Y" || choice1 == "y")
				{
					
					Console.WriteLine("Create Order: " + (ob.CreateOrder(o) ? "completed!" : "not complete!"));
					continue;
				}
				else if (choice1 == "N" || choice1 == "n")
				{
					break;
				}
			}

		}
		public static void Update()
		{

		}

		public static void Delete()
		{

		}

		public static void Order()
		{

			short imChoose1;

			string[] order = { "Add", "Update", "Delete", "Exit" };
			do
			{
				imChoose1 = Menu("Order Management", order);
				switch (imChoose1)
				{
					case 1:
						Add();
						break;
					case 2:
						Update();
						break;
					case 3:
						Delete();
						break;
				}
			} while (imChoose1 != order.Length);
		}

		public static short Menu(string title, string[] menuItems)
		{
			short choose = 0;
			string line = "========================================";
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