using System;
using Xunit;
using MySql.Data.MySqlClient;
using MPC_DAL;

namespace DAL.Test
{

    public class DbConfiguratonTest
    {
	
        [Fact]
        public void OpenConnectionTest()
        {

            Assert.NotNull(DbConfiguration.OpenDefaultConnection());
        }

        [Theory]
        [InlineData("server=localhost;user id=huydev ;password=123456789;port=3306;database=MPC;SslMode=None")]
        public void OpenConnectionWithStringTest(string connectionString)
        {
            Assert.NotNull(DbConfiguration.OpenConnection(connectionString));
        }

        [Theory]
        [InlineData("server=localhost1;user id=huydev;password=546464;port=3306;database=MPC;SslMode=None")]
        [InlineData("server=localhost;user id=vtchuydeva231;password=fsdfdsf;port=3306;database=MPC;SslMode=None")]
        [InlineData("server=localhost;user id=huydev;password=gnfghudfhgkdfhgkdf;port=3306;database=MPC;SslMode=None")]
        [InlineData("server=localhost;user id=huydev;password=vtcacademy;port=3307;database=MPC;SslMode=None")]
        [InlineData("server=localhost;user id=huydev;password=vtcacademy;port=3306;database=MPC;SslMode=None")]
        [InlineData("server=localhost;user id=huydev;password=vtcacademy;port=3306;database=MPC;SslMode=Non")]
        [InlineData("server=localhost;user id=huydev;password=vtcacademy;port=3306;database=MPC")]
        public void OpenConnectionWithStringFailTest(string connectionString)
        {
            Assert.Null(DbConfiguration.OpenConnection(connectionString));
        }

        [Fact]
        public void OpenDefaultConnectionTest()
        {
            Assert.NotNull(DbConfiguration.OpenDefaultConnection());
        }
    }
}