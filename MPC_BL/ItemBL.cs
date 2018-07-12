using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_DAL;

namespace MPC_BL
{
	public class ItemBL
	{
		private ItemDAL idal;
		public Item GetAmountByItemId(int itemId)
		{
			return idal.GetAmountByItemId(itemId);
		}
		public ItemBL()
		{
			idal = new ItemDAL();
		}
		public Item GetItemById(int itemId)
		{
			return idal.GetItemById(itemId);
		}
		public List<Item> GetItemsByCategory()
		{
			return idal.GetItemsByCategory(ItemFilter.Get_Food, ItemFilter.Get_Drink, null );
         
		}
		public List<Item> GetItemsByCategory(string food, string drink) => idal.GetItemsByCategory(ItemFilter.Get_Food, ItemFilter.Get_Drink, new Item { Food = food, Drink = drink });
		public  Item CheckAmount(int itemId, int amount)
		{
			return idal.CheckAmount(itemId, amount);
		}


	}
}
