using System;

namespace MPC_Persistence
{
   
    public class Item_Category
    {
        private string _Food;
        public string Food {get{
            return _Food;
        }set{
            _Food =value;
        }}
        private string _Drink;
        public string Drink {get{
            return _Drink;
        }set{_Drink=value;}}

    }
}