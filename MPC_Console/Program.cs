using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_BL;
using MPC_DAL;
using System.Text;

namespace PL_Console
{
	class Program
	{
		static void Main(string[] args)
		{
			MENU();
		}
		public static void MENU()
		{
			short mainChoose = 0;
			string[] login = { "Login", "Exit." };
			do
			{
				mainChoose = Validate.Menu("Welcome to Cafe Management System !", login);
				switch (mainChoose)
				{
					case 1:
						LoginConsole.Login();
						break;
				}
			} while (mainChoose != login.Length);

		}
		public static void CafeManagementSystem(Account a)
		{
			Console.Clear();
			short imChoose;
			string[] mainMenu = { "Order Management", "Exit" };
			do
			{
				imChoose = Validate.Menu(" Cafe Management System ", mainMenu);
				switch (imChoose)
				{
					case 1:
						OrderConsole.MenuOrder(a); 
						break;
				}
			} while (imChoose != mainMenu.Length);
		}

	}
}
