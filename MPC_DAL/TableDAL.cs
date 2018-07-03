using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MPC_Persistence;

namespace DAL
{
    public class TableDAL
    {
        private string query;
        private MySqlDataReader reader;
        public Table GetById(int tableId)
        {
            query = @"select table_id, table_name, table_status
                        from Tables where table_id=" + tableId + ";";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecQuery(query);
            Table t = null;
            if (reader.Read())
            {
                t = GetTable(reader);
            }
            DBHelper.CloseConnection();
            return t;
        }

        internal Table GetById(int tableId, MySqlConnection connection)
        {
            query = @"select table_id, table_name, table_status
                        from Tables where table_id=" + tableId + ";";
            Table t = null;
            reader = (new MySqlCommand(query, connection)).ExecuteReader();
            if (reader.Read())
            {
                t = GetTable(reader);
            }
            reader.Close();
            return t;
        }
        private Table GetTable(MySqlDataReader reader)
        {
              Table c = new   Table();
            c.Table_Id = reader.GetInt32("table_id");
            c.TableName = reader.GetString("table_name");
            c.Status= reader.GetInt32("Status");
            return c;
        }
        public int Addtable(Table t)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("sp_createTable", DBHelper.OpenConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tableName", t.TableName);
                cmd.Parameters["@tableName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@tableStatus", MySqlDbType.Int16);
                cmd.Parameters["@customerAddress"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@tableId", MySqlDbType.Int32);
                cmd.Parameters["@tableId"].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = (int) cmd.Parameters["@tableId"].Value;
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