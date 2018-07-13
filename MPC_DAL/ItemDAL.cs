using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace MPC_DAL
{
	public static class ItemFilter
	{
		public const int Get_Food = 0;
		public const int Get_Drink = 1;
	}
	public class ItemDAL
	{
		private string query;
		private MySqlDataReader reader;
		private MySqlConnection connection;
		public Item CheckAmount(int itemid, int amount)
		{
			query = @"select amount from Items where  amount >= " + amount + " and item_id =" + itemid + " ;";

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
					// else
					// {
					// 	throw new Exception("This Amount is wrong");
					// }
				}
			}
			return item;
		}

		public List<Item> GetItemsByCategory(int get_Food, int get_Drink, Item item)
		{
			throw new NotImplementedException();
		}
		public Item GetAmountByItemId(int itemId)
		{
			query = @"select amount from Items where item_id=" + itemId + ";";

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

		public Item GetItemById(int itemId)
		{
			query = @"select item_id, item_name, unit_price, amount from Items where item_id=" + itemId + ";";

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
			item.ItemPrice = reader.GetDecimal("unit_price");
			item.Amount = reader.GetInt32("amount");


			return item;
		}
		private List<Item> GetItemsByCategory(MySqlCommand command)
		{
			List<Item> lItem = new List<Item>();
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				lItem.Add(GetItem(reader));
			}
			DBHelper.CloseConnection();
			return lItem;
		}
		public List<Item> GetItemsByCategory(int itemFilter, Item item)
		{
			MySqlCommand command = new MySqlCommand("", DbConfiguration.OpenConnection());
			switch (itemFilter)
			{
				case ItemFilter.Get_Food:
					query = @"select Food from Items as i,
					Items_Category as ic 
					where ic.category_id=i.category_id and Food like concat('%',@Food,'%');";
					command.Parameters.AddWithValue("@Food", item.Food);
					break;
				case ItemFilter.Get_Drink:
					query = @"select Food from Items as i,
					Items_Category as ic 
					where ic.category_id=i.category_id and Drink like concat('%',@Drink,'%');";
					command.Parameters.AddWithValue("@Drink", item.Drink);
					break;
			}
			command.CommandText = query;
			return GetItemsByCategory(command);
		}
	}
}