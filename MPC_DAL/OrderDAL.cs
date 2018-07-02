using System;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace DAL
{
	public class OrderDAL
    {
        public bool CreateOrder(Order order)
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
            cmd.CommandText = "lock tables Tables write, Orders write, Items write, OrderDetails write;";
            cmd.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            MySqlDataReader reader = null;
            if (order.OrderTable == null || order.OrderTable.== null || order.OrderTable.TableName == "")
            {
                //set default customer with customer id = 1
                order.OrderTable = new Table() { Table_Id = 1 };
            }
            try
            {
                if (order.OrderTable.Table_Id == null)
                {
                    //Insert new Tables
                    cmd.CommandText = @"insert into Tables(table_name )
                            values ('" + order.OrderTable.TableName + "' );";
                    cmd.ExecuteNonQuery();
                    //Get new tables id
                    cmd.CommandText = "select table_id from Tables order by ta desc limit 1;";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.OrderTable.Table_Id = reader.GetInt32("table_id");
                    }
                    reader.Close();
                }
                else
                {
                    order.OrderTable = (new CustomerDAL()).GetById(order.OrderTable.tal ?? 0, DBHelper.GetConnection());
                }
                if (order.OrderCustomer == null || order.OrderCustomer.CustmerId == null)
                {
                    throw new Exception("Can't find Customer!");
                }
                //Insert Order
                cmd.CommandText = "insert into Orders(customer_id, order_status) values (@customerId, @orderStatus);";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@customerId", order.OrderCustomer.CustmerId);
                cmd.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATE_NEW_ORDER);
                cmd.ExecuteNonQuery();
                //get new Order_ID
                // cmd.CommandText = "select order_id from Orders order by order_id desc limit 1;";
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
                    if (item.ItemId == null || item.Amount <= 0)
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

                    //insert to Order Details
                    cmd.CommandText = @"insert into OrderDetails(order_id, item_id, unit_price, quantity) values 
                            (" + order.OrderId + ", " + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ");";
                    cmd.ExecuteNonQuery();

                    //update amount in Items
                    cmd.CommandText = "update Items set amount=amount-@quantity where item_id=" + item.ItemId + ";";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@quantity", item.Amount);
                    cmd.ExecuteNonQuery();
                }
                //commit transaction
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
                //unlock all tables;
                cmd.CommandText = "unlock tables;";
                cmd.ExecuteNonQuery();
                DBHelper.CloseConnection();
            }
            return result;
        }
    }
}