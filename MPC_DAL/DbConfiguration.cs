using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;


namespace MPC_DAL
{
	public class DbConfiguration
	{
		private static MySqlConnection connection;
		private static string CONNECTION_STRING = "server=localhost;user id=huydev;password=123456789;port=3306;database=MPC;SslMode=None";
		public static MySqlConnection OpenDefaultConnection()
		{
			try
			{
				MySqlConnection connection = new MySqlConnection
				{
					ConnectionString = CONNECTION_STRING
				};
				connection.Open();
				return connection;
			}
			catch
			{
				return null;
			}
		}
		public static MySqlDataReader ExecQuery(string query)
		{
			MySqlCommand command = new MySqlCommand(query, connection);
			return command.ExecuteReader();
		}
		public static MySqlConnection OpenConnection()
		{
			try
			{
				string connectionString;
				FileStream fileStream = File.OpenRead("ConnectionString.txt");
				using (StreamReader reader = new StreamReader(fileStream))
				{
					connectionString = reader.ReadLine();
				}
				fileStream.Close();
				return OpenConnection(connectionString);
			}
			catch
			{
				return null;
			}
		}	
		public static MySqlConnection OpenConnection(string connectionString)
		{
			try
			{
				MySqlConnection connection = new MySqlConnection
				{
					ConnectionString = connectionString
				};
				connection.Open();
				return connection;
			}
			catch
			{
				return null;
			}
		}
		public static void CloseConnection()
		{
			if (connection != null) connection.Close();
		}
		public static bool ExecTransaction(List<string> queries)
		{
			bool result = true;
			OpenConnection();
			MySqlCommand command = connection.CreateCommand();
			MySqlTransaction trans = connection.BeginTransaction();

			command.Connection = connection;
			command.Transaction = trans;

			try
			{
				foreach (var query in queries)
				{
					command.CommandText = query;
					command.ExecuteNonQuery();
					trans.Commit();
				}
				result = true;
			}
			catch
			{
				result = false;
				try
				{
					trans.Rollback();
				}
				catch
				{
				}
			}
			finally
			{
				CloseConnection();
			}
			return result;
		}

		// public static MySqlConnection OpenConnection(string connectionString)
		// {
		// 	try
		// 	{
		// 		MySqlConnection connection = new MySqlConnection
		// 		{
		// 			ConnectionString = connectionString
		// 		};
		// 		connection.Open();
		// 		return connection;
		// 	}
		// 	catch
		// 	{
		// 		return null;
		// 	}
		// }
	}
}