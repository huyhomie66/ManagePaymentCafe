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
		public List<Table> display()
		{
			query = @"select * from Tables;";
			List<Table> tablelist = new List<Table>();
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand cmd = new MySqlCommand(query, connection);
				using (reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Table i = new Table();
						i = GetTable(reader);
						tablelist.Add(i);
					}
				}
			}
			return tablelist;
		}
	
		public Table GetTableById(int tableId)
		{
			query = @"select table_id,table_status,table_name from Tables where table_id = " + tableId + " ;";
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
		
		public bool CheckTableForPay(int tableid)
		{
			query = @"select * from Tables  where table_id =" + tableid + "  and table_status=1 ;";
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

		public bool CheckTableById(int tableid)
		{
			query = @"select * from Tables where table_id = " + tableid + " and table_status =0;";
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

		private Table GetTable(MySqlDataReader reader)
		{
			Table c = new Table();
			c.Table_Id = reader.GetInt32("table_id");
			c.TableName = reader.GetString("table_name");
			c.Status = reader.GetInt32("table_status");
			return c;
		}
	}
}