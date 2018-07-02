using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace DAL
{
    public static class ItemFilter{
        public const int GET_ALL = 0;
        public const int FILTER_BY_ITEM_ID= 1;
    }
    public class ItemDAL
    {
        private string query;
        private MySqlDataReader reader;
        public Item GetItemById(int itemId)
        {
            query = @"select item_id, item_name, unit_price, amount, item_status,from Items where item_id=" + itemId + ";";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Item item = null;
            if (reader.Read())
            {
                item = GetItem(reader);
            }
            DBHelper.CloseConnection();
            return item;
        }
        private Item GetItem(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemId = reader.GetInt32("item_id");
            item.ItemName = reader.GetString("item_name");
            item.ItemPrice = reader.GetDecimal("unit_price");
            item.Amount = reader.GetInt32("amount");
            item.Status = reader.GetInt16("item_status");
          
            return item;
        }
        private List<Item> GetItems(MySqlCommand command)
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
        public List<Item> GetItems(int itemFilter, Item item)
        {
            MySqlCommand command = new MySqlCommand("", DBHelper.OpenConnection());
            switch(itemFilter)
            {
                case ItemFilter.GET_ALL:
                    query = @"select item_id, item_name, unit_price, amount, item_status from Items";
                    break;
                case ItemFilter.FILTER_BY_ITEM_ID:
                    query = @"select item_id, item_name, unit_price, amount from Items where item_name like concat('%',@itemName,'%');";
                    command.Parameters.AddWithValue("@itemName", item.ItemName);
                    break;
            }
            command.CommandText = query;
            return GetItems(command);
        }
    }
}