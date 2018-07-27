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
		public void GetItemByIdTest()
		{
			Item i = itdal.GetItemById(99);
			Assert.Null(i);
		}
		
	}


}

