using System;
using System.Collections.Generic;
using MPC_Persistence;
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
			AccountBL account = new AccountBL();
			Account ap = new Account();
			Console.Write("Input user: ");
			ap.Username = Console.ReadLine();
			Console.Write("Input password: ");
			ap.Password = hidenpassword();
			while (true)
			{
				if (account.login(ap.Username, ap.Password) != null)
				{
					Console.WriteLine("login successfully!!!");
					break;
				}
				else
				{
					while (true)
					{
						Console.WriteLine("\n wrong value login, please re-enter: ");

						Console.Write("Input user: ");
						ap.Username = Console.ReadLine();
						Console.Write("Input password: ");
						ap.Password = hidenpassword();
						if (account.login(ap.Username, ap.Password) != null)
						{
							break;
						}
					}

				}
			}
		}
	}
}