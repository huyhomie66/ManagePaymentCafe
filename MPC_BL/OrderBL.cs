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
		private ItemDAL idal = new ItemDAL();
		private TableDAL tbl = new TableDAL();
		private AccountDAL adl = new AccountDAL();
		public bool CreateOrder(Order order)
		{
			bool result = Odal.CreateOrder(order);
			return result;
		}
		public List<Order> GetTableIdForPay(int Table_id)
		{
			return Odal.GetTableIdForPay(Table_id);
		}

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

		public Order GetOrderByTableID(Table t)
		{
			return Odal.GetOrderByTable(t);
		}
		public void AddItemToOrder(int itemid, int quantity, Order order)
		{
			foreach (Item i in order.ItemsList)
			{
				if (itemid == i.ItemId)
				{
					i.Amount += quantity;
					return;
				}
			}
			order.ItemsList.Add(idal.GetItemById(itemid));
			order.ItemsList[order.ItemsList.Count - 1].Amount = quantity;
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