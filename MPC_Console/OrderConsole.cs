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
		public static AccountBL abl = new AccountBL();
		public static Table t = new Table();
		public static Item it = new Item();
		public static List<Order> orl = new List<Order>();
		public static List<Item> irl = new List<Item>();
		public static List<Account> arl = new List<Account>();
		public static List<Table> trl = new List<Table>();
		public static OrderBL obl = new OrderBL();
		public static TableBL tbl = new TableBL();
		public static ItemBL itbl = new ItemBL();


		public static void Add(Account a, Order o)
		{
			DateTime now = DateTime.Now;
			o.OrderTable = new Table();
			o.OrderAccount = new Account();
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
			while (true)
			{
			Console.WriteLine("Input table Id: ");
			t.Table_Id = Validate.InputInt(Console.ReadLine()); 
			var result = tbl.CheckTableEmtpyById(t.Table_Id);
			if (result == true)
			{
					t = tbl.GetTableById(t.Table_Id);
					break;
			}
			else
			{
				Console.WriteLine("Cant Find this table or this table has some one, please re-enter: ");
				continue;
			}
			}
			o.OrderAccount.Account_Id = a.Account_Id;
			o.OrderTable.Table_Id = t.Table_Id;
			while (true)
			{

				Console.WriteLine(" Input Item Id: ");
				it.ItemId = Validate.InputInt(Console.ReadLine());
				it = itbl.GetItemById(it.ItemId);
				Console.WriteLine(it.ItemName);
				Console.WriteLine("Input quantity item: ");
				it.Amount = Validate.InputInt(Console.ReadLine());
				o.ItemsList.Add(it);
				Console.WriteLine("Do you want to continue Add Item? (Y/N) ");
				char choice2 = Validate.InputToChar(Console.ReadLine());
				if (choice2 == 'y' || choice2 == 'Y')
				{
					continue;
				}
				else if (choice2 == 'N' || choice2 == 'n')
				{
					break;
				}

			}
			foreach (var item in o.ItemsList)
			{
				Console.WriteLine(item.ItemName);
			}
			Console.WriteLine("Do you want to create order? (Y/N) ");
			char choice3 = Validate.InputToChar(Console.ReadLine());
			switch (choice3)
			{
				case 'y':
					Console.WriteLine("Create Order: " + (obl.CreateOrder(o) ? "completed!" : "not complete!"));
					o.OrderDate = now;
					Console.WriteLine("Order Date: " + o.OrderDate);
					Console.WriteLine("Table: " + t.Table_Id);
					foreach (var item in o.ItemsList)
					{
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");
					}
					Console.WriteLine("Order By: " + a.StaffName);
					irl.Clear();
					o.ItemsList.Clear();
					orl.Clear();
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;

				case 'Y':
					Console.WriteLine("Create Order: " + (obl.CreateOrder(o) ? "completed!" : "not complete!"));
					o.OrderDate = now;
					Console.WriteLine("Order Date: " + o.OrderDate);
					Console.WriteLine("Table: " + t.Table_Id);
					foreach (var item in o.ItemsList)
					{
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");
					}
					Console.WriteLine("Order By: " + a.StaffName);
					irl.Clear();
					o.ItemsList.Clear();
					orl.Clear();
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
		}

		public static void Edit(Account a, Order o)
		{

			DateTime now = DateTime.Now;
			o.OrderTable = new Table();
			o.OrderAccount = new Account();
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
			while (true)
			{
				Console.WriteLine("Input Table Id for edit: ");
				t.Table_Id = Validate.InputInt(Console.ReadLine());
				var result = obl.GetOrderByTableID(t);
				if (result != null)
				{
					o = obl.GetOrderByTableID(t);
					break;
				}
				else
				{
					Console.WriteLine("Cant Fins this table for Edit,pls re-enter: ");
					continue;
				}
			}
			while (true)
			{

				Console.WriteLine(" Input Item Id for edit or insert more: ");
				it.ItemId = Validate.InputInt(Console.ReadLine());
				it = itbl.GetItemById(it.ItemId);
				Console.WriteLine(it.ItemName);
				Console.WriteLine("Input quantity item : ");
				it.Amount = Validate.InputInt(Console.ReadLine());
				o.ItemsList.Add(it);
				Console.WriteLine("Do you want to continue Edit or Input more Item? (Y/N) ");
				char choice2 = Validate.InputToChar(Console.ReadLine());
				if (choice2 == 'y' || choice2 == 'Y')
				{
					continue;
				}
				else if (choice2 == 'N' || choice2 == 'n')
				{
					break;
				}
			}
			foreach (var item in o.ItemsList)
			{
				Console.WriteLine(item.ItemName);
			}
			Console.WriteLine("Do you want to complete this order? (Y/N) ");
			char choice3 = Validate.InputToChar(Console.ReadLine());
			switch (choice3)
			{
				case 'y':
					Console.WriteLine("Edit Order: " + (obl.UpdateOrder(o) ? "completed!" : "not complete!"));
					o.OrderDate = now;
					Console.WriteLine("Order Date: " + o.OrderDate);
					Console.WriteLine("Table: " + t.Table_Id);
					foreach (var item in o.ItemsList)
					{
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");
					}
					Console.WriteLine("Order By: " + a.StaffName);
					irl.Clear();
					o.ItemsList.Clear();
					orl.Clear();
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;

				case 'Y':
					Console.WriteLine("Create Order: " + (obl.UpdateOrder(o) ? "completed!" : "not complete!"));
					o.OrderDate = now;
					Console.WriteLine("Order Date: " + o.OrderDate);
					Console.WriteLine("Table: " + t.Table_Id);
					foreach (var item in o.ItemsList)
					{
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pecie");
					}
					Console.WriteLine("Order By: " + a.StaffName);
					irl.Clear();
					o.ItemsList.Clear();
					orl.Clear();
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
		}
		public static void ShowListOrder(Account a, Order o)
		{
			o.OrderTable = new Table();
			o.OrderAccount = new Account();
			orl = obl.GetListOrderForShow();
			Console.WriteLine("||================================================||");
			Console.WriteLine("||=================Order List Infor===============||");
			Console.WriteLine("||================================================||");
			Console.WriteLine("||  Table Name  ||  Staff Name  ||Date Order      ||");
			Console.WriteLine("||================================================||");
		
					foreach (var order in orl)
					{
						Console.WriteLine("||{0,-14}||{1,-14}||{2,-16}||", order.OrderTable.TableName,order.OrderAccount.StaffName, order.OrderDate.ToString("dd/MM/yyyy HH:mm"));
					}
		
			Console.WriteLine("||================================================||");
			Console.WriteLine("Press any key to back the menu...");
			Console.ReadKey();
		}
		public static void Delete()
		{

		}

		public static void Order(Account a, Order o)
		{
			short imChoose1;

			string[] order = { "Create Order", "Edit Order", "Show list Order", "Exit" };
			do
			{
				imChoose1 = Menu("Order Management", order);
				switch (imChoose1)
				{
					case 1:
						Add(a, o);
						break;
					case 2:
						Edit(a, o);
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