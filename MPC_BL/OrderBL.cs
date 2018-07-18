using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_DAL;

namespace MPC_BL
{
	public class OrderBL
	{
		private OrderDAL Odal = new OrderDAL();
		public bool CreateOrder(Order order)
		{
			bool result = Odal.CreateOrder(order);
			return result;
		}
		public List<Order> GetAllListOrder()
		{
			return Odal.GetAllListOrder();
		}
		public Order GetAllOrder()
		{
			return Odal.GetAllOrder();
		}
		public bool UpdateOrder(Order Order)
		{
			bool result = Odal.UpdateOrder(Order);
			return result;
		}
		public bool CheckOrderById(int orderid)
		{
			return Odal.CheckOrderById(orderid);
		}
		public Order GetOrderById(int orderid)
		{
			return Odal.GetOrderById(orderid);
		}
		public List<Order> GetListOrderById(int orderid)
		{
			return Odal.GetListOrderById(orderid);
		}
		public bool PayOrder(Order order)
		{
			return Odal.PayOrder(order);
		}
		private ItemDAL idal = new ItemDAL();
		private TableDAL tbl = new TableDAL();
		private AccountDAL adl = new AccountDAL();
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

		public decimal Total(List<Order> orl)
		{	OrderBL obl = new OrderBL();
			orl = Odal.GetAllListOrder();
			decimal totalmoney = 0;
			foreach (var o in orl)
			{
				totalmoney = totalmoney +  o.OrderItem.ItemPrice * o.OrderItem.Amount;
			}
			return totalmoney;
		}
	}
}