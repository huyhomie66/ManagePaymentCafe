using System;
using System.Collections;
using System.Collections.Generic;
using MPC_Persistence;
using System.IO;
using MPC_BL;
using MPC_DAL;
using System.Text;

namespace PL_Console
{
	class LoginConsole
	{
		public static void Login()
		{
			AccountBL account = new AccountBL();
			Account a = new Account();

			while (true)
			{

				Console.Write("Username: ");
				a.Username = Console.ReadLine();
				Console.Write("Password: ");
				a.Password = Validate.hidenpassword();
				var result = account.login(a.Username, a.Password);
				if (result != null)
				{
					Console.WriteLine("Login successfully!!!");
					Program.CafeManagementSystem(account.login(a.Username, a.Password));
					break;
				}
				else if (result == null)
				{
					Console.WriteLine("Wrong Username or Password, please re-enter: ");
				}
			}

		}
	}
}