using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_DAL;

namespace MPC_BL
{
    public class AccountBL
    {
        private AccountDAL Adal = new AccountDAL();
     
		public Account login( string username, string password)
		
		{
			return Adal.Login(username, password);
		}
       
    }
}