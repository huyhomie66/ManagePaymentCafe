using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace MPC_DAL
{
	public class TableDAL
	{
		private string query;
		private MySqlDataReader reader;
		private MySqlConnection connection;
		public Table CheckTableById(int tableId)
		{
			// query = @"select table_name,table_status  as t
			// 						Order  as o
			// 						where  t.table_status=0 and
			// 						t.table_id = o.table_id = " + tableId + ";";
			query = @"select table_id, table_name,table_status  from Tables where table_id = "+tableId+";";		
			Table t = null;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						t = GetTable(reader);
					}
					reader.Close();
				}
			}
			return t;
		}

		private Table GetTable(MySqlDataReader reader)
		{
			Table c = new Table();
			c.Table_Id = reader.GetInt32("table_id");
			c.TableName = reader.GetString("table_name");
			c.Status = reader.GetInt32("table_status");
			return c;
		}
		// public bool InsertIdTable(int tableId, string  order_id, int amount)
		// {
		// 	// Thêm dữ liệu
		// 	query = "insert into order (table_id) values ('{0}')";
		// 	query = String.Format(query, tableId, order_id );
		// 	MySqlCommand cmd = new MySqlCommand();
		// 	cmd.ExecuteNonQuery();

		// 	// Set lại trạng thái table
		// 	query = "update Tables set table_status=1 where table_id='" + tableId + "'";
		// 	cmd = new MySqlCommand();
		// 	cmd.ExecuteNonQuery();

		// 	return true;
		// }
		public int Addtable(Table t)
		{
			int result = 0;
			MySqlCommand cmd = new MySqlCommand("sp_createTable", DbConfiguration.OpenConnection());
			try
			{
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@tableName", t.TableName);
				cmd.Parameters["@tableName"].Direction = System.Data.ParameterDirection.Input;
				cmd.Parameters.AddWithValue("@tableStatus", MySqlDbType.Int16);
				cmd.Parameters["@tableId"].Direction = System.Data.ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				result = (int)cmd.Parameters["@tableId"].Value;
			}
			catch
			{

			}
			finally
			{
				DBHelper.CloseConnection();
			}
			return result;
		}
	}
}