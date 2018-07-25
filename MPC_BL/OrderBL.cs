using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MPC_Persistence;
using MPC_DAL;
using System.Linq;

namespace MPC_BL
{
	public class OrderBL
	{
		private OrderDAL Odal = new OrderDAL();
		public List<Item> listitem = new List<Item>();
		public bool CreateOrder(Order order)
		{
			bool result = Odal.CreateOrder(order);
			return result;
		}
		public List<Order> GetTableIdForPay( int Table_id)
		{
			return Odal.GetTableIdForPay(Table_id);
		}
		// 
		public bool UpdateOrder(Order Order)
		{
			bool result = Odal.UpdateOrder(Order);
			return result;
		}
	
	
		public List<Order> GetListOrderForShow()
		{
			return Odal.GetListOrderForShow();
		}
		public bool PayOrder(int Table_id)
		{
			return Odal.PayOrder(Table_id);
		}
		private ItemDAL idal = new ItemDAL();
		private TableDAL tbl = new TableDAL();
		private AccountDAL adl = new AccountDAL();
		
			public Order GetOrderByTableID(Table t)
			{
				return Odal.GetOrderByTable( t);
			}

		
		public void AddItemToUpdate(int ItemId, int quantity,Order o)
        {
		 Item it = o.ItemsList.Where(item => item.ItemId == ItemId).FirstOrDefault();
            if (it == null)
            {
                o.ItemsList.Add(new Item() { ItemId = ItemId, Amount =  quantity });
            }
            else 
            {
                it.Amount = it.Amount+ quantity;
            }
        }
		public decimal Total(List<Order> orl, int Table_id)
		{
			OrderBL obl = new OrderBL();
			orl = Odal.GetTableIdForPay(Table_id);
			decimal totalmoney = 0;
			foreach (var o in orl)
			{
				totalmoney = totalmoney + o.OrderItem.ItemPrice * o.OrderItem.Amount;
			}
			return totalmoney;
		}
	}
}