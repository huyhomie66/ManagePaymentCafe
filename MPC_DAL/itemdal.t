// public  List<Item> GetAllItem()
		// {
		// 	query =@"select *from Items;";
		// 	List<Item> itemlist =new List<Item>();
		// 	using (connection = DbConfiguration.OpenDefaultConnection())
		// 	{
		// 		MySqlCommand cmd = new MySqlCommand(query, connection);
		// 		using (reader = cmd.ExecuteReader())
		// 		{
		// 			while (reader.Read())
		// 			{
		// 				Item i = new Item();
		// 				i = GetItem(reader);
		// 				itemlist.Add(i);
		// 			}
		// 		}
		// 	}
		// 	return itemlist;
		// }