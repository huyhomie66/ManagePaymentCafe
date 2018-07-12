using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_DAL;

namespace MPC_BL
{
	public class OrderBL
	{
		private OrderDAL Odal = new OrderDAL();
		public bool CreateOrder(Order Order)
		{
			bool result = Odal.CreateOrder(Order);
			return result;
		}
		public bool UpdateOrder(Order Order)
		{
			bool result = Odal.UpdateOrder(Order);
			return result;
		}
		private ItemDAL idal;
		public void AddItemToOrder(int itemid,int quantity, Order order)
        {
            foreach(Item b in order.ItemsList)
            {
                if (itemid  == b.ItemId)
                {
                    b.Amount += quantity;
                    return;
                }
            }
            order.ItemsList.Add(idal.GetItemById(itemid));
            order.ItemsList[order.ItemsList.Count-1].Amount = quantity;
        }

	}
}