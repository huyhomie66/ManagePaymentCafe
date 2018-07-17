using System;
using MPC_DAL;
using Xunit;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using MPC_Persistence;

namespace DAL.Test
{
	public class OrderUniTest
	{
		private OrderDAL odal = new OrderDAL();
		private AccountDAL acdal = new AccountDAL();

		[Fact]
		public void CreateOrder()
		{
			Item item = new Item();
			Order order = new Order();
			Account a = new Account();
			Table t = new Table();
			order.OrderTable = new Table();
			order.OrderTable.Table_Id = 1;
			order.ItemsList = new List<Item>();
		//	order.quantity = 11111;
			item.Amount = 9911111;
			item.ItemId = 11111;
			order.Orderstatus = 0;
			order.ItemsList.Add(item);
			bool result = odal.CreateOrder(order);
			Assert.False((bool)result);

		}
		[Fact]
		public void UpdateOrder()
		{
			Item item = new Item();
			Order order = new Order();

			order.OrderId = 199;
			order.ItemsList = new List<Item>();
			item.ItemId = 199;
		//	order.quantity = 23666;
			item.Amount = 7766666;
			order.Orderstatus = 0;
			order.ItemsList.Add(item);
			bool result = odal.UpdateOrder(order);
			Assert.False((bool)result);

		}


	}


}