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
		
			order.OrderTable = new Table();
			order.OrderTable.Table_Id = 1;
			order.ItemsList = new List<Item>();
			
			item.ItemId = 1;
			item.Amount = 25;
			order.Orderstatus = OrderStatus.Not_Pay_out;
			order.ItemsList.Add(item);
			Assert.NotNull(odal.CreateOrder(order));

		}
		[Fact]
		public void UpdateOrder()
		{
			Item item = new Item();
			Order order = new Order();
		
			order.OrderTable = new Table();
			order.OrderTable.Table_Id = 1;
			order.ItemsList = new List<Item>();
			
			item.ItemId = 1;
			item.Amount = 25;
			order.Orderstatus = OrderStatus.Not_Pay_out;
			order.ItemsList.Add(item);
			Assert.NotNull(odal.UpdateOrder(order));

		}
	
	
	}


}