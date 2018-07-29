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
		public static TableDAL tdal = new TableDAL();
		public static AccountDAL adal = new AccountDAL();
		public List<Order> GetTableIdForPay(int table_id)
		{
			query = "select *from Orders as o inner join OrderDetails as od  on o.order_id = od.order_id inner join Tables as t on t.table_id = o.table_id where o.order_status = 0  and t.table_status = 1 and o.table_id =" + table_id + ";";

			List<Order> lod = new List<Order>();
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Order order = new Order();
						order = GetOrderForPay(reader);
						lod.Add(order);
					}
					reader.Close();
				}
			}
			return lod;
		}
		public Order GetOrderByTableId(int table_id)
		{
			query = @"select *from Orders as o inner join OrderDetails as od where o.table_id= " + table_id + ";";
			Order o = null;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand cmd = new MySqlCommand(query, connection);
				using (reader = cmd.ExecuteReader())
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

		private Order GetOrderForPay(MySqlDataReader reader)
		{
			Order order = new Order();
			order.OrderTable = new Table();
			order.OrderAccount = new Account();
			order.OrderItem = new Item();
			order.OrderId = reader.GetInt32("order_id");
			order.OrderItem.ItemId = reader.GetInt32("item_id");
			order.OrderItem.ItemPrice = reader.GetDecimal("item_price");
			order.OrderItem.Amount = reader.GetInt32("quantity");
			order.OrderAccount.Account_Id = reader.GetInt32("account_id");
			order.OrderTable.Table_Id = reader.GetInt32("table_id");
			order.OrderDate = reader.GetDateTime("order_date");
			order.Total = order.OrderItem.Amount * order.OrderItem.ItemPrice;
			return order;
		}
		public List<Order> GetListOrderForShow()
		{
			query = @"select * from Orders ;";
			List<Order> orl = new List<Order>();
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Order order = new Order();
						order = GetOrderForShow(reader);
						orl.Add(order);
					}
					reader.Close();
				}
			}
			return orl;
		}
		private Order GetOrderForShow(MySqlDataReader reader)
		{
			Order o = new Order();
			o.OrderAccount = new Account();
			o.OrderTable = new Table();
			o.OrderAccount.Account_Id = reader.GetInt32("account_id");
			o.OrderTable.Table_Id = reader.GetInt32("table_id");
			o.OrderDate = reader.GetDateTime("order_date");
			o.OrderTable.TableName = tdal.GetTableById(o.OrderTable.Table_Id).TableName;
			o.OrderAccount.StaffName = adal.CheckAccountById(o.OrderAccount.Account_Id).StaffName;
			return o;
		}

		public OrderDAL()
		{
			connection = DbConfiguration.OpenDefaultConnection();
		}
		public Order GetOrderForUpdate(MySqlDataReader reader)
		{
			Order o = new Order();
			o.OrderTable = new Table();
			o.OrderTable.Table_Id = reader.GetInt32("table_id");
			o.OrderId = reader.GetInt32("order_id");
			return o;
		}
		public bool PayOrder(Table t,int account_id)
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
				cmd.CommandText = @"update Tables as t inner join Orders as o  on t.table_id = o.table_id set t.table_status = 0 , o.order_status = 1 where t.table_id = " + t.Table_Id + " and o.order_status = 0 and o.account_id="+account_id+";";
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
				connection.Close();
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
			MySqlTransaction trans = connection.BeginTransaction();
			cmd.Transaction = trans;
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

					cmd.CommandText = @"insert into OrderDetails( order_id,item_id, item_price, quantity) values ( " + order.OrderId + "," + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ")ON DUPLICATE KEY UPDATE quantity = quantity+" + item.Amount + ";";
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
				connection.Close();
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
				cmd.CommandText = @"Update Orders set order_status = 0 where order_id = " + order.OrderId + ";";
				cmd.ExecuteNonQuery();
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

					cmd.CommandText = @"insert into OrderDetails( order_id,item_id, item_price, quantity )values 
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
				connection.Close();
			}
			return result;
		}
		public Order GetOrderByTableForUpdate(Table t)
		{
			query = @"select *from  Orders as o inner join Tables as t on o.table_id = t.table_id  where o.order_status = 0 and o.table_id =" + t.Table_Id + " and t.table_status = 1;";
			Order o = null;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						o = GetOrderForUpdate(reader);
					}
					reader.Close();
				}
			}
			return o;
		}

		public bool cancel(int tableid)
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
				cmd.CommandText = @"update Tables as t inner join Orders as o  on t.table_id = o.table_id set t.table_status = 0 , o.order_status = 2 where t.table_id = " +tableid + " and o.order_status = 0;";
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
				connection.Close();
			}
			return result;
		}
	}
}