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