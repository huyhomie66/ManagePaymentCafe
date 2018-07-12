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
		public void CheckIdTableTest()
		{
		int Tableid = 37;
		Table t =  tdal.CheckTableById( Tableid);			
		Assert.Null(t);		
		}
	}
}
