using System;
using MPC_DAL;
using Xunit;
using MySql.Data.MySqlClient;
using MPC_Persistence;
namespace DAL.Test
{
	public class ItemDalUniTest
	{
		private ItemDAL itdal = new ItemDAL();
		[Fact]
		public void TestItem()
		{
			
			Item i = itdal.CheckAmount(1,500);
			Assert.Null(i);
			// Assert.Equal(itemid,i.ItemId);
		}

	}


}

