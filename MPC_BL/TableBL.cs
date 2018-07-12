using System;
using System.Collections.Generic;
using MPC_Persistence;
using MPC_DAL;

namespace MPC_BL
{
    public class TableBL
    {
        private TableDAL cdal = new TableDAL();
        public Table checkById(int tableId)
        {
            return cdal.CheckTableById(tableId);
        }
        
      
    }
}