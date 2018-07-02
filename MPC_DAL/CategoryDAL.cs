using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace DAL
{
  
    
    public class CategoryDAL
    {
        private string query;
        private MySqlDataReader reader;
        public Item GetItemById(int CategoryId)
        {
            query = @"select food,drink from Items_Category where category_id=" +  CategoryId + ";";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Item_Category category = null;
            if (reader.Read())
            {
                category = GetItem(reader);
            }
            DBHelper.CloseConnection();
            return item;
        }
        private Item_Category GetItemCategory(MySqlDataReader reader)
        {
            Item_Category category = new Item();
           category.Food = reader.GetString("Food");
           category.Drink =reader.GetString("Drink");
            return category;
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