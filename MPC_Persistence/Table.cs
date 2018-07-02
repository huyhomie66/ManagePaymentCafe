using System;

namespace MPC_Persistence
{  
    	public class Table_Status
{
	const int empty_table =0;
	const int not_empty_table=1;
}
     public class Table
    {
       private int _Table_Id;
       public int Table_Id{
           get{
               return _Table_Id;
           }
           set{
               _Table_Id=value;
           }
       }
        private string _TableName ;

        public string  TableName{
            get{
                return _TableName;
            }
            set{
                _TableName=value;
            }
        }
	    private int _Status;
        public int Status{
            get{
                return _Status;
            }
            set{
                _Status = value;
            }
        }
     
	

    }
}
