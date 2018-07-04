using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace DAL
{ 
    public class AccountDAL
    {
       private string query;
        private MySqlDataReader reader;
        private MySqlConnection connection;

		
		private   Account GetAccount(MySqlDataReader reader)
        {
             Account a= new Account();
            a.Account_Id = reader.GetInt16("account_id");
            a.StaffName = reader.GetString("staffname");
            a.Username = reader.GetString("username");
            a.Password = reader.GetString("password");
            return a;
        }

        public Account Login(string username,string password)
        {
             Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUsername = regex.Matches(username);
            MatchCollection matchCollectionPassword = regex.Matches(password);
            if (matchCollectionUsername.Count <= 0 || matchCollectionPassword.Count <= 0)
            {
                return null;
            }

            query = @"select * from Account where username = '" + username + "' and password= '" + password + "';";

            Account a = null;
            using (connection = DBHelper.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                using (reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        a = GetAccount(reader);
                    }   
                }
            }

            if (a != null)
            {
              
            }

            return a;
        }
      

    }
}
