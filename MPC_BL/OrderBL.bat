public Order GetAllOrder()
		{
			return Odal.GetAllOrder();
		}
			// public bool CheckOrderById(int orderid)
		// {
		// 	return Odal.CheckOrderById(orderid);
		// }
			public Order GetOrderById(int orderid)
		{
			return Odal.GetOrderById(orderid);
		}
		public List<Order> GetListOrderById(int orderid)
		{
			return Odal.GetListOrderById(orderid);
		}
			// public void AddItemToUpdate(int ItemId, int quantity, Order o)
		// {
		// 	Item it = o.ItemsList.Where(item => item.ItemId == ItemId).FirstOrDefault();
		// 	if (it == null)
		// 	{
		// 		o.ItemsList.Add(new Item() { ItemId = ItemId, Amount = quantity });
		// 	}
		// 	else
		// 	{
		// 		it.Amount = it.Amount + quantity;
		// 	}
		// }