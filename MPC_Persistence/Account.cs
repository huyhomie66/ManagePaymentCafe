using System;

namespace MPC_Persistence
{
	public class Account
	{

		public int Account_Id { get; set; }
		private string _Username;
		public string Username
		{
			get { return _Username; }
			set
			{
				while (value == "STAFF1"|| value == "Staff1" || value == "STaff1"||value =="STAff1"||value =="STAFf1"|| value == "Staff2" || value == "STaff2"||value =="STAff2"||value =="STAFf2"||value=="STAFF2"|| value == "Staff3" || value == "STaff3"||value =="STAff3"||value =="STAFf3"||value=="STAFF3"|| value == "Staff4" || value == "STaff4"||value =="STAff4"||value =="STAFf4"||value=="STAFF4"|| value == "Staff5" || value == "STaff5"||value =="STAff5"||value =="STAFf5"||value=="STAFF5"|| value == "Staff6" || value == "STaff6"||value =="STAff6"||value =="STAFf6"||value=="STAFF6"|| value == "Staff7" || value == "STaff7"||value =="STAff7"||value =="STAFf7"||value=="STAFF7"|| value == "Staff9" || value == "STaff9"||value =="STAff9"||value =="STAFf9"||value=="STAFF9"|| value == "Staff8" || value == "STaff8"||value =="STAff8"||value =="STAFf8"||value=="STAFF8")
				{
					Console.WriteLine("Wrong Username, please re-enter: ");
					value = Convert.ToString(Console.ReadLine());
				}
				_Username = value;
			}
		}


		public string Password { get; set; }

		public string StaffName { get; set; }
	}
}
