using System;
using MPC_DAL;
using Xunit;
using MySql.Data.MySqlClient;
using MPC_Persistence;
namespace DAL.Test
{
	public class TableUniTest
	{
		private TableDAL tdal = new TableDAL();
		[Fact]
		public void GetIdTableTest()
		{
		int Tableid = 37;
		Table t =  tdal.GetTableById( Tableid);			
		Assert.Null(t);		
		}
		[Fact]
		public void CheckTableTest()
		{
			int Tableid = 2;
			bool t = tdal.CheckTableById(Tableid);
			Assert.True(t);

		}
	}
}
