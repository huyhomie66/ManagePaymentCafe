using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_BL;
using MPC_DAL;
using System.Text;

namespace PL_Console
{
	class PayoutConsole
	{
		public static void Pay(Account a)
		{
			// Console.WriteLine("Input Id Order for pay: ");
			// int orderid = Convert.ToInt32(Console.ReadLine());
			// try
			// {
			// 	orderid = o.OrderId;
			// 	while (true)
			// 	{
			// 	Console.WriteLine("Input Money for pay: ");
			// 	int Money = Convert.ToInt32(Console.ReadLine());
			// 	if (Money == o.total)
			// 	{
			// 		Console.WriteLine("Pay Out successfully !!!");
			// 		break;

			// 	}
			// 	else if (Money > o.total)
			// 	{
			// 		Console.WriteLine("excess cash is: " + (o.total - Money));

			// 	}
			// 	else if(Money < o.total)
			// 	{
			// 	  Console.WriteLine("The amount entered is not enough to pay!, pls re-enter");
			// 	  continue;
			// 	}
			// 	}

			// }
			// catch (System.Exception)
			// {

			// 	throw new Exception("Can't Find this order for pay, pls re-enter: ");
			// }
		}
		public static void Display()
		{

		}
		public static void Payout(Account a)
		{
			int imChoose2;
			string[] payout = { "Pay", "Display Bill", "Exit" };
			do
			{
				imChoose2 = OrderConsole.Menu("Payout", payout);
				switch (imChoose2)
				{
					case 1:
						Pay(a);
						break;
					case 2:
						Display();
						break;

				}
			} while (imChoose2 != payout.Length);
		}
	}
}