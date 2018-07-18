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
		public List<Order> GetAllListOrder()
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

		public Order GetAllOrder()
		{
			query = "select * from Orders inner join OrderDetails ;";
			Order order = null;
			//List<Order> lod = new List<Order>();
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						
						order = GetOrder(reader);
					
					}
					reader.Close();
				}
			}
			return order;
		}

		public Order GetOrderById(int OrderId)
		{

			query = @"select *from OrderDetails as od  inner join Orders as o where o.order_id=" + OrderId + ";";

			Order o = null;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						o = GetOrderForPay(reader);
					}
					reader.Close();
				}
			}
			return o;
		}
		public List<Order> GetListOrderById(int OrderId)
		{

			query = @"select *from Orders as o inner join OrderDetails as od on o.order_id = od.order_id  where o.table_id=" + OrderId + ";";

			List<Order> lod = new List<Order>();
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					if (reader.Read())
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
		private Order GetOrderForPay(MySqlDataReader reader)
		{
			Order order = new Order();
			order.OrderTable = new Table();
			order.OrderItem = new Item();
			order.OrderId = reader.GetInt32("order_id");
			order.OrderItem.ItemId = reader.GetInt32("item_id");
			order.OrderItem.ItemPrice = reader.GetInt32("item_price");
			order.OrderItem.Amount = reader.GetInt32("quantity");
			order.total = order.OrderItem.Amount * order.OrderItem.ItemPrice;
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
			o.total = o.OrderItem.ItemPrice * o.OrderItem.Amount;		
			return o;
		}
		public bool CheckOrderById(int order_id)
		{
			query = @"select * from Orders  where order_id = " + order_id + " and order_status =0;";


			bool t = false;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand cmd = new MySqlCommand(query, connection);
				using (reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						t = true;
					}
					reader.Close();
				}
			}
			return t;
		}

		public OrderDAL()
		{
			connection = DbConfiguration.OpenDefaultConnection();
		}
		public bool PayOrder(Order order)
		{

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
			//	MySqlDataReader reader = null;
			try
			{
				cmd.CommandText = @"UPDATE Tables INNER JOIN Orders ON Tables.table_id = Orders.table_id SET Tables.table_status = 0, Orders.order_status = 1 WHERE Tables.table_id =" + order.OrderTable.Table_Id + " and Orders.order_id = " + order.OrderId + ";";
				cmd.ExecuteNonQuery();
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
			cmd.CommandText = @"Update Orde;";
			cmd.ExecuteNonQuery();
			MySqlTransaction trans = connection.BeginTransaction();
			cmd.Transaction = trans;
			//MySqlDataReader reader = null;
			try
			{

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

					cmd.CommandText = @"insert into OrderDetails( order_id,item_id, item_price, quantity) values                             ( " + order.OrderId + "," + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ");";
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