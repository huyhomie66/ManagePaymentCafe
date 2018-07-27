using System;
using Xunit;
using MPC_BL;
using MPC_Persistence;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using MPC_DAL;
namespace BL.Test
{

	public class UnitTest1
	{
		public static OrderBL obl = new OrderBL();

		[Fact]
		public void CreateOrderTest()
		{
            Item item = new Item();
			Order order = new Order();
			Account a = new Account();
			order.OrderTable = new Table();
			order.OrderTable.Table_Id = 21;
			order.ItemsList = new List<Item>();
			item.Amount = 9911111;
			item.ItemId = 11111;
			order.Orderstatus = 0;
			order.ItemsList.Add(item);
			bool result = obl.CreateOrder(order);
			Assert.False((bool)result);
		}
	}
}