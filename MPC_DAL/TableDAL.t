// public Order GetOrderByTable(int table_id, Table t)
		// {
		// 	query = @"select *from  Orders inner join OrderDetails where table_id =" + table_id + " and account_id =1;";
		// 	Order o = null;
		// 	using (connection = DbConfiguration.OpenDefaultConnection())
		// 	{
		// 		MySqlCommand command = new MySqlCommand(query, connection);
		// 		using (reader = command.ExecuteReader())
		// 		{
		// 			if (reader.Read())
		// 			{
		// 				o.OrderTable.Table_Id = reader.GetInt32("table_id");
		// 				o.OrderId = reader.GetInt32("order_id");
		// 			}
		// 			reader.Close();
		// 		}
		// 	}
		// 	return o;
	//	}
		// public bool UpdateTableStatus(int table_id)
		// {

		// 	query = @"Update Tables as t inner join Orders as o on t.table_id = o.table_id set t.table_status=1  where t.table_id =" + table_id + " ;";
		// 	bool t = false;
		// 	using (connection = DbConfiguration.OpenDefaultConnection())
		// 	{
		// 		MySqlCommand cmd = new MySqlCommand(query, connection);
		// 		using (reader = cmd.ExecuteReader())
		// 		{
		// 			if (reader.Read())
		// 			{
		// 				t = true;
		// 			}

		// 			reader.Close();
		// 		}
		// 	}
		// 	return t;
		// }