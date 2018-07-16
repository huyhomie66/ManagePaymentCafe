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
		public static string hidenpassword()
		{
			StringBuilder sb = new StringBuilder();
			while (true)
			{
				ConsoleKeyInfo cki = Console.ReadKey(true);
				if (cki.Key == ConsoleKey.Enter)
				{
					Console.WriteLine();
					break;
				}

				if (cki.Key == ConsoleKey.Backspace)
				{
					if (sb.Length > 0)
					{
						Console.Write("\b\0\b");
						sb.Length--;
					}

					continue;
				}

				Console.Write('*');
				sb.Append(cki.KeyChar);
			}
			return sb.ToString();
		}
		
		public static void Login()
		{
			
			// o.OrderAccount=new Account();
			AccountBL account = new AccountBL();
			Account a= new Account();
			
			while (true)
			{
			
				Console.Write("Input user: ");
				a.Username = Console.ReadLine();
				Console.Write("Input password: ");
				a.Password = hidenpassword();
				var result =account.login(a.Username, a.Password);
				if (result != null)
				{
					Console.WriteLine("login successfully!!!");
					Program.CafeManagementSystem(account.login(a.Username, a.Password));
					break;
				}
				else if(result == null)
				{
					Console.WriteLine("Wrong value, pls re-enter ");
					
					
				}
			}
			
		}
	}
}