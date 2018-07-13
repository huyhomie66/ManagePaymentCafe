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
		public static void Add(Account a)
		{




			TableBL table = new TableBL();
			Table t = new Table();
			Item it = new Item();
			Order o = new Order();
			OrderBL ob = new OrderBL();
			TableBL tbl = new TableBL();
			ItemBL itbl = new ItemBL();
			o.OrderTable = new Table();
			int c = 0;
			o.OrderAccount = new Account();
			Console.WriteLine("Input table Id: ");
			o.OrderTable.Table_Id = Convert.ToInt32(Console.ReadLine());
			tbl.checkById(o.OrderTable.Table_Id).Table_Id = o.OrderTable.Table_Id;

			o.OrderAccount.Account_Id = a.Account_Id;
		
			o.ItemsList = new List<Item>();
			while (true)
			{
				Console.WriteLine(" Input Item Id: ");

				it.ItemId= Convert.ToInt32(Console.ReadLine());
				o.ItemsList.Add(itbl.GetItemById(it.ItemId));
				Console.WriteLine("Input quantity item: ");
				int amount = Convert.ToInt32(Console.ReadLine());
				o.ItemsList[c].Amount = amount;
				c++;
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
					Console.WriteLine("Create Order: " + (ob.CreateOrder(o) ? "completed!" : "not complete!"));
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;

				case 'Y':
					Console.WriteLine("Create Order: " + (ob.CreateOrder(o) ? "completed!" : "not complete!"));
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
			}
		}


		public static void Update()
		{

		}

		public static void Delete()
		{

		}

		public static void Order(Account a)
		{

			short imChoose1;

			string[] order = { "Add", "Update", "Delete", "Exit" };
			do
			{
				imChoose1 = Menu("Order Management", order);
				switch (imChoose1)
				{
					case 1:
						Add(a);
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