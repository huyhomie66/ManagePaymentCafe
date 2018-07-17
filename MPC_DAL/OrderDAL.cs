using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using MPC_Persistence;

namespace MPC_DAL
{
	public class OrderDAL
	{
		private string query;
		private MySqlDataReader reader;
		private MySqlConnection connection;
		public List<Order> GetAllOrder()
		{
			query = "select * from Orders inner join OrderDetails ;";
			// Order order = null;
			List<Order> lod = new List<Order>();
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Order order = new Order();
						order = GetOrder(reader);
						lod.Add(order);
					}
					reader.Close();
				}
			}
			return lod;
		}
		public Order GetOrderById(int OrderId)
		{

			query = @"select * from Items where order_id=" + OrderId + ";";

			Order order = null;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						order = GetOrder(reader);
					}
					reader.Close();
				}
			}
			return order;
		}
		private Order GetOrder(MySqlDataReader reader)
		{
			Order o = new Order();
			o.OrderTable = new Table();
			o.OrderItem = new Item();
			o.OrderAccount = new Account();
			o.OrderItem.Amount = reader.GetInt32("quantity");
			o.OrderItem.ItemPrice = reader.GetDecimal("item_price");
			o.OrderItem.ItemId = reader.GetInt32("item_id");
			o.OrderId = reader.GetInt32("order_id");
			o.Orderstatus = reader.GetInt32("order_status");
			o.OrderTable.Table_Id = reader.GetInt32("table_id");
			o.OrderAccount.Account_Id = reader.GetInt32("account_id");
			o.OrderDate = reader.GetDateTime("order_date");
			return o;
		}

		public OrderDAL()
		{
			connection = DbConfiguration.OpenDefaultConnection();
		}
	
		public bool UpdateOrder(Order order)
		{


			bool result = true;
			// //mở connection đến dbbase
			if (connection.State == System.Data.ConnectionState.Closed)
			{
				connection.Open();
			}
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "lock tables  Account write, Tables write, Orders write, Items write, OrderDetails write;";
			connection.CreateCommand();
			cmd.Connection = connection;
			cmd.ExecuteNonQuery();
			MySqlTransaction trans = connection.BeginTransaction();
			cmd.Transaction = trans;
			//MySqlDataReader reader = null;
			try
			{

				cmd.CommandText = @"Update OrdersDetail ";

				trans.Commit();
				result = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				result = false;
				try
				{
					trans.Rollback();
				}
				catch
				{

				}
			}
			finally
			{
				cmd.CommandText = "unlock tables;";
				cmd.ExecuteNonQuery();
				DBHelper.CloseConnection();
			}
			return result;
		}
		public bool CreateOrder(Order order)
		{
			if (order == null || order.ItemsList == null || order.ItemsList.Count == 0)
			{
				return false;
			}

			bool result = true;

			if (connection.State == System.Data.ConnectionState.Closed)
			{
				connection.Open();
			}

			MySqlCommand cmd = connection.CreateCommand();
			cmd.Connection = connection;
			cmd.CommandText = "lock tables Account write, Tables write, Orders write, Items write, OrderDetails write;";
			cmd.ExecuteNonQuery();
			MySqlTransaction trans = connection.BeginTransaction();
			cmd.Transaction = trans;
			MySqlDataReader reader = null;
			try
			{
				//Insert Order


				cmd.CommandText = @"insert into Orders(table_id, account_id, order_status) values (" + order.OrderTable.Table_Id + ", " + order.OrderAccount.Account_Id + ", " + order.Orderstatus + ");";
				cmd.Parameters.Clear();
				order.Orderstatus = OrderStatus.Not_Pay_out;
				cmd.ExecuteNonQuery();

				cmd.CommandText = @"Update Tables set table_status=1  where table_id =" + order.OrderTable.Table_Id + ";";
				cmd.ExecuteNonQuery();

				cmd.CommandText = "select LAST_INSERT_ID() as order_id";
				// cmd.ExecuteNonQuery();
				reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					order.OrderId = reader.GetInt32("order_id");
				}
				reader.Close();
				//insert Order Details table

				foreach (var item in order.ItemsList)
				{
					if (item.ItemId == 0)
					{
						throw new Exception("Not Exists Item");
					}
					//get unit_price
					cmd.CommandText = "select item_name , item_price from Items where item_id=" + item.ItemId + ";";
					reader = cmd.ExecuteReader();
					if (!reader.Read())
					{
						throw new Exception("Not Exists Item");
					}
					item.ItemPrice = reader.GetDecimal("item_price");
					item.ItemName = reader.GetString("item_name");
					reader.Close();

					cmd.CommandText = @"insert into OrderDetails( order_id,item_id, item_price, quantity) values 
                            ( " + order.OrderId + "," + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ");";
					cmd.ExecuteNonQuery();
					//update amount in Items
					cmd.CommandText = "update Items set  amount= amount - " + item.Amount + "  where item_id=" + item.ItemId + ";";
					cmd.ExecuteNonQuery();
				}
				trans.Commit();
				result = true;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				result = false;
				try
				{
					trans.Rollback();
				}
				catch
				{
				}
			}
			finally
			{
				cmd.CommandText = "unlock tables;";
				cmd.ExecuteNonQuery();
				DBHelper.CloseConnection();
			}
			return result;
		}
	}
}