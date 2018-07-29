using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_DAL;

namespace MPC_BL
{
	public class TableBL
	{
		private  static TableDAL tdal = new TableDAL();
		// public bool CheckTableEmtpyById(int tableId)
		// {
		// 	return tdal.CheckTableById(tableId);
		// }
		public Table GetTableById(int tableId)
		{
			return tdal.GetTableById(tableId);
		}
		public List<Table> DisplayListTable()
		{
			return tdal.DisplayAllTable();
		}
	}
}