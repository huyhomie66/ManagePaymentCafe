using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using MPC_Persistence;

namespace MPC_DAL
{
	public class OrderDAL
	{
		//private string query;
		//private MySqlDataReader reader;
		private MySqlConnection connection;
		public OrderDAL()
		{
			connection = DbConfiguration.OpenDefaultConnection();
		}
		// private Order GetOrder(MySqlDataReader reader)
		// {
		// 	Order o = new Order();// m xoa cai order item a// sai chả xoá
		// 	Item i = new Item();
		// 	o.OrderTable.Table_Id = reader.GetInt32("table_id");
		// 	o.ItemsList[0].ItemId = reader.GetInt32("item_id");
		// 	i.Amount
		// 	i.ItemPrice = reader.GetInt32("unit_price");
		// 	o.OrderDate = reader.GetDateTime("order_date");
		// 	o.OrderId = reader.GetInt32("order_id");
		// 	return o;
		// }

		// public Order DeleteOrder(int tableid)
		// {
		// 	query = @"DELETE  FROM Orders WHERE table_id=" + tableid + ";";

		// 	Order o = null;
		// 	using (connection = DbConfiguration.OpenDefaultConnection())
		// 	{
		// 		MySqlCommand command = new MySqlCommand(query, connection);
		// 		using (reader = command.ExecuteReader())
		// 		{
		// 			if (reader.Read())
		// 			{
		// 				o = GetOrder(reader);
		// 			}
		// 			reader.Close();
		// 		}
		// 	}
		// 	return o;
		// }
		public bool UpdateOrder(Order order)
		{

			if (order == null || order.ItemsList == null || order.ItemsList.Count == 0 || CreateOrder(order) == false|| order.OrderTable.Table_Id == 0|| order.OrderId == 0 )
			{
				return false;
			}
			bool result = true;
			//mở connection đến dbbase
			if (connection.State == System.Data.ConnectionState.Closed)
			{
				connection.Open();
			}
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "lock tables Items_Category write, Account write, Tables write, Orders write, Items write, OrderDetails write;";
			connection.CreateCommand();
			cmd.Connection = connection;
			cmd.ExecuteNonQuery();
			MySqlTransaction trans = connection.BeginTransaction();
			cmd.Transaction = trans;
			MySqlDataReader reader = null;
			try
			{
				if (order.OrderId == 0)
				{
					throw new Exception("Can't find this order!");
				}
				cmd.CommandText = @"Update Orders set account_id = "+order.OrderAccount.Account_Id+", table_id =1 where order_id = "+order.OrderId+" ;";
				cmd.Parameters.Clear();
			
				cmd.ExecuteNonQuery();
				foreach (var item in order.ItemsList)
				{
						if (item.ItemId == 0 || item.Amount <= 0)
					{
						throw new Exception("Not Exists Item");
					}
					//get unit_price
					cmd.CommandText = "select unit_price from Items where item_id=@itemId";
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@itemId", item.ItemId);
					reader = cmd.ExecuteReader();
					if (!reader.Read())
					{
						throw new Exception("Not Exists Item");
					}
					item.ItemPrice = reader.GetDecimal("unit_price");
					reader.Close();

					cmd.CommandText = @"Update OrderDetails set item_id = "+item.ItemId+", quantity= "+item.Amount+" where order_id = 1;";
					cmd.ExecuteNonQuery();
					//update amount in Items
					cmd.CommandText = "update Items set amount=amount-@quantity where item_id=" + item.ItemId + ";";
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@quantity", item.Amount);
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
			// MySqlConnection connection = DbConfiguration.OpenConnection();
			MySqlCommand cmd = connection.CreateCommand();
			cmd.Connection = connection;
			cmd.CommandText = "lock tables Items_Category write, Account write, Tables write, Orders write, Items write, OrderDetails write;";
			cmd.ExecuteNonQuery();
			MySqlTransaction trans = connection.BeginTransaction();
			cmd.Transaction = trans;
			MySqlDataReader reader = null;
			try
			{

				if (order.OrderTable == null || order.OrderTable.Table_Id == 0)
				{
					throw new Exception("Can't find table!");
				}
				//Insert Order
				cmd.CommandText = @"insert into Orders(table_id, account_id, order_status) values (" + order.OrderTable.Table_Id + ", " + order.OrderAccount.Account_Id + ", " + order.Orderstatus + ");";
				cmd.Parameters.Clear();
				order.Orderstatus = OrderStatus.Not_Pay_out;
				cmd.ExecuteNonQuery();
				cmd.CommandText = "select LAST_INSERT_ID() as order_id";
				reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					order.OrderId = reader.GetInt32("order_id");
				}
				reader.Close();
				//insert Order Details table
				foreach (var item in order.ItemsList)
				{
					if (item.ItemId == 0 || item.Amount <= 0)
					{
						throw new Exception("Not Exists Item");
					}
					//get unit_price
					cmd.CommandText = "select unit_price from Items where item_id=@itemId";
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@itemId", item.ItemId);
					reader = cmd.ExecuteReader();
					if (!reader.Read())
					{
						throw new Exception("Not Exists Item");
					}
					item.ItemPrice = reader.GetDecimal("unit_price");
					reader.Close();

					cmd.CommandText = @"insert into OrderDetails( order_id,item_id, unit_price, quantity) values 
                            ( " + order.OrderId + "," + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ");";
					cmd.ExecuteNonQuery();
					//update amount in Items
					cmd.CommandText = "update Items set amount=amount-@quantity where item_id=" + item.ItemId + ";";
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@quantity", item.Amount);
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