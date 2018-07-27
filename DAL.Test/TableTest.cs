using System;
using MPC_DAL;
using Xunit;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
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
		[Fact]
		public void DisplayAllTableTest()
		{
			List<Table> listable = new List<Table>();
			listable = tdal.DisplayAllTable();
			Assert.NotNull(listable);
			
		}
			[Theory]
		[InlineData(12)]
		[InlineData(11)]
		[InlineData(10)]
		[InlineData(9)]
		[InlineData(8)]
		[InlineData(7)]
		[InlineData(6)]
		[InlineData(5)]
		[InlineData(4)]
		[InlineData(3)]
		[InlineData(2)]
		[InlineData(1)]
		public void CheckTableForPayTest(int tableId)
		{
			bool result = tdal.CheckTableForPay(tableId);
			Assert.False(result);
		}
	}
}
