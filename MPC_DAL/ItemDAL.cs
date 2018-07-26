using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace MPC_DAL
{
	public class ItemDAL
	{
		private string query;
		private MySqlDataReader reader;
		private MySqlConnection connection;
		
		public Item GetItemById(int itemId)
		{
			
			query = @"select item_id,item_name, item_price, amount , item_status from Items where item_id=" + itemId + ";";

			Item item = null;
			using (connection = DbConfiguration.OpenDefaultConnection())
			{
				MySqlCommand command = new MySqlCommand(query, connection);
				using (reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						item = GetItem(reader);
					}
					reader.Close();
				}
			}
			return item;
		}
		private Item GetItem(MySqlDataReader reader)
		{
			Item item = new Item();
			item.ItemId = reader.GetInt32("item_id");
			item.ItemName = reader.GetString("item_name");
			item.ItemPrice = reader.GetDecimal("item_price");
			item.Amount = reader.GetInt32("amount");
			item.Status = reader.GetInt16("item_status");
			return item;
		}

	}
}