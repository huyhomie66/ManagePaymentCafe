using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace DAL
{ 
    public class AccountDAL
    {
        private string query;
        private MySqlDataReader reader;
        public Account GetById(int AccountId)
        {
            query = @"select account_id, username, password, staffname from Account where account_id="+AccountId+";";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Account a = null;
            if (reader.Read())
            {
                a = GetAccount(reader);
            }
            DBHelper.CloseConnection();
            return a;
        }

        internal Account GetById( int AccountId, MySqlConnection connection)
        {
           query = @"select account_id, username, password, staffname from Account where account_id="+AccountId+";";
    		Account a = null;
            reader = (new MySqlCommand(query, connection)).ExecuteReader();
            if (reader.Read())
            {
                a = GetAccount(reader);
            }
            reader.Close();
            return a;
        }   
		
		private   Account GetAccount(MySqlDataReader reader)
        {
             Account a= new Account();
            a.Account_Id = reader.GetInt16("account_id");
            a.StaffName = reader.GetString("staffname");
            a.Username = reader.GetString("username");
            a.Password = reader.GetString("password");
            return a;
        }
        

    }
}
