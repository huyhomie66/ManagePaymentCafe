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
			Item i = itdal.CheckItemId(1);
			Assert.Null(i);
		}
		[Fact]
		public void TestItem1()
		{
			Item i = itdal.GetItemById(99);
			Assert.Null(i);
		}
	}


}

