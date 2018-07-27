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
		public void TestFasleCreateOrder()
		{
			Item item = new Item();
			Order order = new Order();
			Account a = new Account();
			Table t = new Table();
			order.OrderTable = new Table();
			order.OrderTable.Table_Id = 1;
			order.ItemsList = new List<Item>();
			item.Amount = 9911111;
			item.ItemId = 11111;
			order.Orderstatus = 0;
			order.ItemsList.Add(item);
			bool result = odal.CreateOrder(order);
			Assert.False((bool)result);

		}
		[Fact]
		public void TestTrueCreateOrder()
		{
			Item item = new Item();
			Order order = new Order();
			Account a = new Account();
			Table t = new Table();
			order.OrderTable = new Table();
			order.OrderTable.Table_Id = 13;
			order.ItemsList = new List<Item>();
			item.Amount = 1;
			item.ItemId = 1;
			order.Orderstatus = 0;
			order.ItemsList.Add(item);
			bool result = odal.CreateOrder(order);
			Assert.False((bool)result);

		}
		[Fact]
		public void TestFasleUpdateOrder()
		{
			Item item = new Item();
			Order order = new Order();

			order.OrderId = 199;
			order.ItemsList = new List<Item>();
			item.ItemId = 199;
			item.Amount = 7766666;
			order.Orderstatus = 0;
			order.ItemsList.Add(item);
			bool result = odal.UpdateOrder(order);
			Assert.False((bool)result);

		}
		[Fact]
		public void TestTrueUpdateOrder()
		{
			Item item = new Item();
			Order order = new Order();

			order.OrderId = 1;
			order.ItemsList = new List<Item>();
			item.ItemId = 1;
			item.Amount = 1;
			//	order.Orderstatus = 1;
			order.ItemsList.Add(item);
			bool result = odal.UpdateOrder(order);
			Assert.True((bool)result);

		}
		[Fact]
		public void PayTestTrue()
		{
			Table t = new Table();
			t.Table_Id = 11;
			bool result = odal.PayOrder(t);
			Assert.True((bool)result);
		}

		[Fact]
		public void PayTestFalse()
		{
			Table t = new Table();
			t.Table_Id=-1;
			bool result = odal.PayOrder(t);
			Assert.False((bool)result);

		}


		[Theory]
		[InlineData(12)]
		[InlineData(11)]
		[InlineData(10)]
		[InlineData(9)]
		[InlineData(8)]
		[InlineData(7)]
		[InlineData(6)]
		[InlineData(5)]
		[InlineData(4)]
		[InlineData(3)]
		[InlineData(2)]
		[InlineData(1)]
		public void TestGetTableForPay(int table_id)
		{
			List<Order> result = odal.GetTableIdForPay(table_id);
			Assert.NotNull(result);
		}


		[Fact]
		public void TestGetListOrderForPay()
		{
			List<Order> result = odal.GetListOrderForShow();
			Assert.NotNull(result);
		}
		[Fact]
		public void TestPayOut()
		{
			Table t = new Table();
			t.Table_Id = 13;
			bool result = odal.PayOrder(t);
			Assert.False(result);
		}

	}


}