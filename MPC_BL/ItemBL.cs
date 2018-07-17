using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_DAL;

namespace MPC_BL
{
	public class ItemBL
	{
		private ItemDAL idal;
			public ItemBL()
		{
			idal = new ItemDAL();
		}
		public Item GetItemById(int itemId)
		{
			return idal.GetItemById(itemId);
		}
		public Item CheckItemById(int itemid)
		{
			return idal.CheckItemId(itemid);
		}
		public List<Item> GetAll()
		{
			return idal.GetAllItem();
		}
	}
}
