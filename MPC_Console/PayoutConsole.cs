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
		public static void Payout()
		{
			int imChoose2;
			string[] payout = { "Pay", "Display Bill", "Exit" };
			do
			{
				imChoose2 = OrderConsole.Menu("Payout", payout);
				switch (imChoose2)
				{
					case 1:
						break;
					case 2:
						break;

				}
			} while (imChoose2 != payout.Length);
		}
	}
}