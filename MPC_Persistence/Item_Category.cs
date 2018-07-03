using System;

namespace MPC_Persistence
{
   
    public class Item_Category
    {private int? _Category_ID;
    public int? Category_ID
    {
        get{  return _Category_ID;

        }
        set{_Category_ID=value;}
    }

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