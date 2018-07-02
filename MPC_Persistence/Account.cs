using System;

namespace MPC_Persistence
{
     public class Account
    {   private int _Account_Id;
        public int Account_Id {get{
            return _Account_Id;
        }
        set{
            _Account_Id = value;
        }}
        private string _Username; 
        public string Username {get{
            return _Username;
        }set{
            _Username = value;
        }}
        private string _Password;
        public string Password{get{
            return _Password;
        }set{
            _Password =value;
        }} 
        private string _StaffName;
        public string StaffName{get{
            return _StaffName;
        }set{_StaffName=value;}}
    }
}
