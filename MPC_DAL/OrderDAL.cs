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
		public OrderDAL()
		{
			connection = DbConfiguration.OpenDefaultConnection();
		}
		private Order GetOrder(MySqlDataReader reader)
		{
			Order o = new Order();
			o.OrderTable.Table_Id = reader.GetInt32("table_id");
			o.OrderItem.ItemId = reader.GetInt32("item_id");
			o.OrderItem.ItemPrice = reader.GetInt32("unit_price");
			o.OrderDate = reader.GetDateTime("order_date");
			o.OrderId = reader.GetInt32("order_id");
			return o;
		}

		public Order DeleteOrder(int tableid)
		{
			query = @"DELETE  FROM Orders WHERE table_id=" + tableid + ";";

			Order o = null;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						o = GetOrder(reader);
					}
					reader.Close();
				}
			}
			return o;
		}
		public bool UpdateOrder(Order order)
		{
			if (order == null || order.ItemsList == null || order.ItemsList.Count == 0)
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
			if (order.OrderTable == null || order.OrderTable.TableName == null || order.OrderTable.TableName == "")
			{
				order.OrderTable = new Table()
				{
					Table_Id = 1
				};
			}
			try
			{
				if (order.OrderTable == null || order.OrderTable.Table_Id == 0)
				{
					throw new Exception("Can't find table!");
				}
				cmd.CommandText = @"Update Orders set 
								table_id=@table_id 
								order_status=@orderstatus;";
				cmd.Parameters.Clear();
				cmd.Parameters.AddWithValue("@table_id", order.OrderTable.Table_Id);
				cmd.Parameters.AddWithValue("@orderstatus", OrderStatus.UPDATE_ORDER);
				cmd.ExecuteNonQuery();
				foreach (var item in order.ItemsList)
				{
					cmd.CommandText = @" Update OrderDetails 
									Set quantity= @quantity,
									item_id = @item
   									where order_id = @orderid;";
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@tableid", order.OrderTable.Table_Id);
					cmd.Parameters.AddWithValue("@itemid ", order.OrderItem.ItemId);
					cmd.Parameters.AddWithValue("@quantity", order.listquantity);
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
				DbConfiguration.CloseConnection();
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
			if (order.OrderTable == null || order.OrderTable.TableName == null || order.OrderTable.TableName == "")
			{
				order.OrderTable = new Table()
				{
					Table_Id = 1
				};
			}
			try
			{
				if (order.OrderTable == null || order.OrderTable.Table_Id == 0)
				{
					throw new Exception("Can't find table!");
				}
				//Insert Order

				cmd.CommandText = @"insert into Orders(table_id,order_status) values (@tableId, @orderStatus);";
				cmd.Parameters.Clear();
				cmd.Parameters.AddWithValue("@tableId", order.OrderTable.Table_Id);
				cmd.Parameters.AddWithValue("@orderStatus ", OrderStatus.CREATE_NEW_ORDER);
				cmd.ExecuteNonQuery();
				cmd.CommandText = @"update Tables set table_status=1 where table_id=" + order.OrderTable.Table_Id + " ;";
				cmd.ExecuteNonQuery();
				cmd.CommandText = "select LAST_INSERT_ID() as order_id";
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
					//   insert to Order Details
					cmd.CommandText = @"insert into OrderDetails(order_id, item_id, unit_price, quantity) values 
                            (" + order.OrderId + ", " + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ");";
					cmd.ExecuteNonQuery();


					//insert datetime
					cmd.CommandText = "insert into Orders(order_date) value(@date);";
					cmd.Parameters.AddWithValue("@date", order.OrderDate);
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
				DbConfiguration.CloseConnection();
			}
			return result;
		}
	}
}