using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace DAL
{
   public static class CategoryFilter{
        public const int GETFOOD = 0;
        public const int  GETDRINK= 1;
        public const int GETALL =2;
    }

    public class CategoryDAL
    {
        private string query;
        private MySqlDataReader reader;
        public Item_Category GetCategoryById(int CategoryId)
        {
            query = @"select category_id ,food,drink from Items_Category where category_id=" +  CategoryId + ";";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Item_Category category = null;
            if (reader.Read())
            {
                category = GetCategory(reader);
            }
            DBHelper.CloseConnection();
            return category;
        }
        private Item_Category GetCategory(MySqlDataReader reader)
        {
            Item_Category category = new Item_Category();
           category.Food = reader.GetString("food");
           category.Drink =reader.GetString("drink");
             return category;
        }
     private List<Item_Category> GetCategory(MySqlCommand command)
     {
         List<Item_Category> C = new List<Item_Category>();
         reader = command.ExecuteReader();
         while(reader.Read())
         {
             C.Add(GetCategory(reader));
         }
         DBHelper.CloseConnection();
         return C;
     }
        private List<Item_Category> GetCategory(int filter, Item_Category category)
        {
            MySqlCommand command = new MySqlCommand("",DBHelper.OpenConnection());
            switch (filter)
            {
                case CategoryFilter.GETFOOD:
                query= @"select category_id,food from Items_Category";
                break;
                case CategoryFilter.GETDRINK:
                query= @"select category_id, drink from Item_Category";
                break;
                case CategoryFilter.GETALL:
                 query= @"select category_id, drink, food from Item_Category";
                break;
                
            }
            command.CommandText=query;
            return GetCategory(command);
        }
    }
}