	// public Order GetAllOrder()
		// {
		// 	query = "select * from Orders inner join OrderDetails ;";
		// 	Order order = null;
		// 	//List<Order> lod = new List<Order>();
		// 	using (connection = DbConfiguration.OpenDefaultConnection())
		// 	{
		// 		MySqlCommand command = new MySqlCommand(query, connection);
		// 		using (reader = command.ExecuteReader())
		// 		{
		// 			while (reader.Read())
		// 			{

		// 				order = GetOrder(reader);

		// 			}
		// 			reader.Close();
		// 		}
		// 	}
		// 	return order;
		// }
			// public Order GetOrderById(int OrderId)
		// {

		// 	query = @"select *from OrderDetails as od  inner join Orders as o where o.order_id=" + OrderId + ";";

		// 	Order o = null;
		// 	using (connection = DbConfiguration.OpenDefaultConnection())
		// 	{
		// 		MySqlCommand command = new MySqlCommand(query, connection);
		// 		using (reader = command.ExecuteReader())
		// 		{
		// 			if (reader.Read())
		// 			{
		// 				o = GetOrderForPay(reader);
		// 			}
		// 			reader.Close();
		// 		}
		// 	}
		// 	return o;
		// }
// public List<Order> GetListOrderById(int OrderId)
		// {

		// 	query = @"select *from Orders as o inner join OrderDetails as od on o.order_id = od.order_id  where o.table_id=" + OrderId + ";";

		// 	List<Order> lod = new List<Order>();
		// 	using (connection = DbConfiguration.OpenDefaultConnection())
		// 	{
		// 		MySqlCommand command = new MySqlCommand(query, connection);
		// 		using (reader = command.ExecuteReader())
		// 		{
		// 			if (reader.Read())
		// 			{
		// 				Order order = new Order();
		// 				order = GetOrder(reader);
		// 				lod.Add(order);
		// 			}
		// 			reader.Close();
		// 		}
		// 	}
		// 	return lod;
		// }
			// private Order GetOrder(MySqlDataReader reader)
		// {
		// 	Order o = new Order();
		// 	o.OrderTable = new Table();
		// 	o.OrderItem = new Item();
		// 	o.OrderAccount = new Account();
		// 	o.OrderItem.Amount = reader.GetInt32("quantity");
		// 	o.OrderItem.ItemPrice = reader.GetDecimal("item_price");
		// 	o.OrderItem.ItemId = reader.GetInt32("item_id");
		// 	o.OrderId = reader.GetInt32("order_id");
		// 	o.Orderstatus = reader.GetInt32("order_status");
		// 	o.OrderTable.Table_Id = reader.GetInt32("table_id");
		// 	o.OrderAccount.Account_Id = reader.GetInt32("account_id");
		// 	o.OrderDate = reader.GetDateTime("order_date");
		// 	o.total = o.OrderItem.ItemPrice * o.OrderItem.Amount;
		// 	return o;
		// }
			// public bool CheckOrderById(int order_id)
		// {
		// 	query = @"select * from Orders  where order_id = " + order_id + " and order_status =0;";


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
		