using System;
using System.Collections.Generic;
using MPC_Persistence;
using DAL;

namespace MPC_BL
{
    public class OrderBL
    {
        private OrderDAL Odal = new OrderDAL();
        public Order CreateOrder(Order Order)
		{
		bool result = Odal.CreateOrder(Order);
			return CreateOrder(result);
		}

		private Order CreateOrder(bool result)
		{
			throw new NotImplementedException();
		}
	}
}