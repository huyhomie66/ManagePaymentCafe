using System;

namespace MPC_Persistence
{
	public class Table_Status
	{
		const int empty_table = 0;
		const int not_empty_table = 1;
	}
	public class Table
	{

		public int Table_Id { get; set; }
		public string TableName { get; set; }

		public int Status { get; set; }


	}
}
