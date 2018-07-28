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
		public static Order o = new Order();

		public static void CreateOrder(Account a)
		{
			Console.Clear();
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
				obl.AddItemToOrder(it.ItemId, it.Amount, o);
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
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pieces");
					}
					Console.WriteLine("Create By: " + a.StaffName);
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
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pieces");
					}
					Console.WriteLine("Create By: " + a.StaffName);
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

		public static void Update(Account a)
		{
			Console.Clear();
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
				Console.WriteLine("Input Table Id for Update: ");
				t.Table_Id = Validate.InputInt(Console.ReadLine());
				var result = obl.GetOrderByTableIDForUpdate(t);
				if (result != null)
				{
					o = obl.GetOrderByTableIDForUpdate(t);
					break;
				}
				else
				{

					Console.WriteLine("Cant Find this table for Update ,please re-enter: ");

					continue;
				}
			}
			while (true)
			{

				Console.WriteLine(" Input Item Id for Update or Add more: ");
				it.ItemId = Validate.InputInt(Console.ReadLine());
				it = itbl.GetItemById(it.ItemId);
				Console.WriteLine(it.ItemName);
				Console.WriteLine("Input quantity item : ");
				it.Amount = Validate.InputInt(Console.ReadLine());
				//o.ItemsList.Add(it);
				obl.AddItemToOrder(it.ItemId, it.Amount, o);
				Console.WriteLine("Do you want to continue Update or Add more Item? (Y/N) ");
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
			Console.WriteLine("Do you want to complete this order? (Y/N) ");
			char choice3 = Validate.InputToChar(Console.ReadLine());
			switch (choice3)
			{
				case 'y':
					Console.WriteLine("Update Order: " + (obl.UpdateOrder(o) ? "completed!" : "not complete!"));
					o.OrderDate = now;
					Console.WriteLine("Order Date: " + o.OrderDate);
					Console.WriteLine("Table: " + t.Table_Id);
					foreach (var item in o.ItemsList)
					{
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pieces");
					}
					Console.WriteLine("Update By: " + a.StaffName);
					irl.Clear();
					o.ItemsList.Clear();
					orl.Clear();
					Console.Write("Press any key to back the menu: ");
					Console.ReadKey();
					break;

				case 'Y':
					Console.WriteLine("Update Order: " + (obl.UpdateOrder(o) ? "completed!" : "not complete!"));
					o.OrderDate = now;
					Console.WriteLine("Order Date: " + o.OrderDate);
					Console.WriteLine("Table: " + t.Table_Id);
					foreach (var item in o.ItemsList)
					{
						Console.WriteLine("Item Name: " + item.ItemName + "  -  " + "Quantity: " + item.Amount + " pieces");
					}
					Console.WriteLine("Update By: " + a.StaffName);
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
		public static void ShowListOrder(Account a)
		{
			Console.Clear();
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
				Console.WriteLine("||{0,-14}||{1,-14}||{2,-16}||", order.OrderTable.TableName, order.OrderAccount.StaffName, order.OrderDate.ToString("dd/MM/yyyy HH:mm"));
			}

			Console.WriteLine("||================================================||");
			Console.WriteLine("||Press any key to back the menu...");
			Console.ReadKey();
		}
		public static void Delete()
		{

		}

		public static void MenuOrder(Account a)
		{
			Console.Clear();
			short imChoose1;

			string[] order = { "Create Order", "Update Order", "Show list Order", "PayOut", "Exit" };
			do
			{
				imChoose1 = Validate.Menu("Order Management", order);
				switch (imChoose1)
				{
					case 1:
						CreateOrder(a);
						break;
					case 2:
						Update(a);
						break;
					case 3:
						ShowListOrder(a);
						break;
					case 4:
						PayoutConsole.Payout();
						break;
					

				}
			} while (imChoose1 != order.Length);
		}
	}
}