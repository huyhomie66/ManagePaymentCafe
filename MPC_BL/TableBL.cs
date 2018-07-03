using System;
using System.Collections.Generic;
using MPC_Persistence;
using DAL;

namespace MPC_BL
{
    public class TableBL
    {
        private TableDAL cdal = new TableDAL();
        public Table GetById(int customerId)
        {
            return cdal.GetById(customerId);
        }

      
    }
}