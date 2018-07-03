using System;

namespace MPC_Persistence 
{
    public static class ItemStatus{
        public const int NOT_ACTIVE = 0;
        public const int ACTIVE = 1;
    }
    public class Item : Item_Category
    {
        private int? _ItemId;
        public int? ItemId{
            get{
                return _ItemId;

            }
            set{
                _ItemId = value;
            }
        }
        private string _ItemName;
        public string ItemName{
            get{
                return _ItemName;

            }
            set{
                _ItemName= value;
            }
        }
        private decimal _ItemPrice;
        public decimal ItemPrice {
            get{
                return _ItemPrice;
            }
            set{
                _ItemPrice = value;
            }
        }
      
            
        private int _Amount;
        public int Amount {get{return _Amount;}
        set{_Amount=value;}}
        private int _Status;
        public int Status{get{
            return _Status;
        }
        set{
            _Status=value;
        }}

    }
}