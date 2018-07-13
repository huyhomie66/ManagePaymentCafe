using System;
using MPC_DAL;
using Xunit;
using MySql.Data.MySqlClient;
using MPC_Persistence;
namespace DAL.Test
{
	public class AccountDalUniTest
	{
		private AccountDAL acdal = new AccountDAL();

		[Fact]
		public void LoginTest1()
		{
			string username = "staff1";
			string password = "123456";
			Account a = acdal.Login(username, password);

			Assert.NotNull(a);
			Assert.Equal(username, a.Username);
		}
		[Fact]
		public void LoginTest2()
		{
			Assert.Null(acdal.Login("customer_01", "123456789"));
		}
        [Fact]
        public void LoginTest4()
        {
            Assert.Null(acdal.Login("'?^%'", "'.:=='"));
        }
		[Fact]
		public void TestCheckId()
		{
			int accountId = 10;
			Account a = acdal.CheckAccountById(accountId);
			Assert.Null(a);		
		}
	}
}

