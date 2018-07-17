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
		public Table CheckTableById(int tableid)
		{
			query = @"select * from Tables where table_id = " + tableid + " and table_status =0;";
			
			
			Table	t = null;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand cmd = new MySqlCommand(query, connection);
				using (reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						t = GetTable(reader);
						// Console.WriteLine("Successful table selection");
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