using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using MPC_Persistence;

namespace DAL
{
	public class OrderDAL
    {    
        	public bool UpdateOrder(Order order)
		{
            
            if (order == null || order.ItemsList == null || order.ItemsList.Count == 0)
            {
                return false;
            }
            bool result = true;
            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            //Lock update all tables
            cmd.CommandText = "lock tables Items_Category write, Account write, Tables write, Orders write, Items write, OrderDetails write;";
            cmd.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            MySqlDataReader reader = null;
            if (order.OrderTable == null || order.OrderTable.TableName == null || order.OrderTable.TableName == "")
            {
                order.OrderTable = new Table() { Table_Id = 1 };
            }
            try
            {
                if (order.OrderTable.Table_Id == null)
                {
                   
                    cmd.CommandText = @"insert into Tables(table_name, table_status )
                            values ('" + order.OrderTable.TableName + "','"+order.OrderTable.Status+"' );";
                    cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.OrderTable.Table_Id = reader.GetInt32("table_id");
                    }
                    reader.Close();
                }
                else
                {
                    order.OrderTable = (new TableDAL()).GetById(order.OrderTable.Table_Id ?? 0, DBHelper.GetConnection());
                }
                if (order.OrderTable == null || order.OrderTable.Table_Id== null)
                {
                    throw new Exception("Can't find table!");
                }

                  Table t = new Table();
                    List<Table> tb = new List<Table>();
                //Update Order

                cmd.CommandText = @"update Orders set table_id = @tableId where table_id =" + t.Table_Id + ";";
               
                cmd.Parameters.Clear();
                //check id table
                    reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                     throw new Exception("Wrong table id");
                 
                    } order.OrderTable.Table_Id = reader.GetInt32("table_id");
                    reader.Close();
                cmd.Parameters.AddWithValue("@tableId", order.OrderTable.Table_Id);
                cmd.Parameters.AddWithValue("@orderStatus", OrderStatus.UPDATE_ORDER);
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
                    if (item.ItemId == null || item.Amount <= 0 )
                    {
                        throw new Exception("Not Exists Item");
                    }
                    //get unit_price
                    cmd.CommandText = "select unit_price from Items where item_id=@itemId";
                    cmd.Parameters.Clear();
                 if (reader.Read())
                {
                    item.ItemId = reader.GetInt32("item_id");
                }
                reader.Close();
                cmd.Parameters.AddWithValue("@itemId", item.ItemId);
                reader = cmd.ExecuteReader();
                 if (!reader.Read())
                {
                    throw new Exception("Not Exists Item");
                }
                    item.ItemPrice = reader.GetDecimal("unit_price");
                    reader.Close();

                    //insert to Order Details
                    cmd.CommandText = @"insert into OrderDetails(order_id, item_id, unit_price, quantity) values 
                    (" + order.OrderId+ ", " + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ");";
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
		
        public bool CreateOrder( Order order)
        {
            // if (order == null || order.ItemsList == null || order.ItemsList.Count == 0)
            // {
            //     return false;
            // }
            bool result = true;
            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            //Lock update all tables
            cmd.CommandText = "lock tables Items_Category write, Account write, Tables write, Orders write, Items write, OrderDetails write;";
            cmd.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            MySqlDataReader reader = null;
            // if (order.OrderTable == null || order.OrderTable.TableName == null || order.OrderTable.TableName == "")
            // {
            //     order.OrderTable = new Table() { Table_Id = 1 };
            // }
            try
            {
                if (order.OrderTable.Table_Id == null)
                {
                   
                    cmd.CommandText = @"insert into Tables(table_name, table_status )
                            values ('" + order.OrderTable.TableName + "','"+order.OrderTable.Status+"' );";
                    cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.OrderTable.Table_Id = reader.GetInt32("table_id");
                    }
                    reader.Close();
                }
                else
                {
                    order.OrderTable = (new TableDAL()).GetById(order.OrderTable.Table_Id ?? 0, DBHelper.GetConnection());
                }
                if (order.OrderTable == null || order.OrderTable.Table_Id== null)
                {
                    throw new Exception("Can't find table!");
                }


                //Insert Order

                cmd.CommandText = "insert into Orders(table_id, order_status,) values (@tableId, @orderStatus);";
                cmd.Parameters.Clear();
                //check id table
                    // reader = cmd.ExecuteReader();
                    // if (!reader.Read())
                    // {
                    //  throw new Exception("Wrong table id");
                 
                    // } order.OrderTable.Table_Id = reader.GetInt32("table_id");
                    // reader.Close();
                cmd.Parameters.AddWithValue("@tableId", order.OrderTable.Table_Id);
                cmd.Parameters.AddWithValue("@orderStatus",order.status);
                //check id table
                    reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                     throw new Exception("Wrong table id");
                 
                    } order.OrderTable.Table_Id = reader.GetInt32("table_id");
                    reader.Close();
                // cmd.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATE_NEW_ORDER);
                cmd.ExecuteNonQuery();
                // cmd.CommandText = "select LAST_INSERT_ID() as order_id";
                // if (reader.Read())
                // {
                //     order.OrderId = reader.GetInt32("order_id");
                // }
                // reader.Close();

                //insert Order Details table
                foreach (var item in order.ItemsList)
                {
                    if (item.ItemId == null || item.Amount <= 0)
                    {
                        throw new Exception("Not Exists Item");
                    }
                    //get unit_price
                    cmd.CommandText = "select unit_price from Items where item_id=@itemId";
                    cmd.Parameters.Clear();
                 if (reader.Read())
                {
                    item.ItemId = reader.GetInt32("item_id");
                }
                reader.Close();
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
                    (" + order.OrderId+ ", " + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ");";
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