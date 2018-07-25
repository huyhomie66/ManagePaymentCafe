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
		PayoutConsole pc = new PayoutConsole();
		OrderConsole oc = new OrderConsole();
		LoginConsole lc = new LoginConsole();
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
				mainChoose = OrderConsole.Menu("Welcom to management cafe restaurent system !", login);
				switch (mainChoose)
				{

					case 1:
						LoginConsole.Login();
						// CafeManagementSystem();
						break;
				}
			} while (mainChoose != login.Length);

		}
		public static void CafeManagementSystem(Account a)
		{
			Order o = new Order();
			short imChoose;
			string[] mainMenu = { "Order Management", "Payout ", "Exit" };
			do
			{
				imChoose = OrderConsole.Menu(" Cafe Management System ", mainMenu);
				switch (imChoose)
				{
					case 1:
						OrderConsole.Order(a,o); 
						break;

					case 2:
						PayoutConsole.Payout(o);
						break;
				}
			} while (imChoose != mainMenu.Length);
		}

	}
}
