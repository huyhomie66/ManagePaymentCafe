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

		[Fact]
		public void CreateOrder()
		{
			Item item = new Item();
			Order order = new Order();
			order.Tablelist = new List<Table>();
			order.OrderTable = new Table();
			order.OrderTable.Table_Id = 1;
			order.ItemsList = new List<Item>();
			item.ItemId = 1;
			item.Amount = 25;
			order.OrderStatus = 1;

			order.ItemsList.Add(item);
			Assert.False(odal.CreateOrder(order));

		}
		[Fact]
		public void Update()
		{
		 Order order = new Order();
		 order.Tablelist = new List<Table>();
		 order.OrderTable = new Table();
		 order.OrderTable.Table_Id=1 ;
		 order.OrderStatus=2;
		 
		}
	}


}