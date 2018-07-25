using System;

namespace MPC_Persistence
{
	public static class Table_Status
	{
		const int empty_table = 0;
		const int not_empty_table = 1;
	}
	public class Table
	{
		public int _Table_Id;
		public int Table_Id
		{
			get { return _Table_Id; }
			set
			{
				while (value> 36 || value < 0)
				{
					Console.WriteLine("Can't find this Table for order!, please re-enter: ");
					value = Convert.ToInt32(Console.ReadLine());
				}
				_Table_Id = value;
			}
		}
		public string TableName { get; set; }

		public int Status { get; set; }


	}
}
