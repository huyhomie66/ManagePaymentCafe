using System;
using System.Text;
using System.Security;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_BL;

namespace PL_Console
{
    class Validate
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

        
        public static int InputInt(string a)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollectionstr = regex.Matches(a);
            while ((matchCollectionstr.Count < a.Length) || (a == "") || (a == "0") || (a.Length > 9))
            {
                Console.Write("You must be input int , please re-enter: ");
                a = Console.ReadLine();
                matchCollectionstr = regex.Matches(a);
            }
            return Convert.ToInt32(a);
        }

        public static char InputToChar(string a)
        {
            Regex regex = new Regex("[a-zA-Z]");
            MatchCollection matchCollectionstr = regex.Matches(a);
            while ((matchCollectionstr.Count < a.Length) || (a != "Y" && a != "N" && a != "y" && a != "n") || (a.Length > 1))
            {
                Console.Write("Wrong value, please re-enter: ");
                a = Console.ReadLine();
                matchCollectionstr = regex.Matches(a);           
            }
            return Convert.ToChar(a);
        }
        public static string InputToDeciaml(decimal a)
        {
            string prices = a.ToString();
            string price = "";
            int balance = (prices.Length - 1) % 3;
            for (int i = 0; i < prices.Length; i++)
            {
                if (i == prices.Length - 1)
                {
                    price = price + prices[i];
                }
                else if ((i - balance) % 3 == 0)
                {
                    price = price + prices[i] + ".";
                }
                else
                {
                    price = price + prices[i].ToString();
                }
            }
            return price;
        }
    }
}