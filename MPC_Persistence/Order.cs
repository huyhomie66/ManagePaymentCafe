using System;
using System.Collections.Generic;

namespace MPC_Persistence
{
    public static class OrderStatus
    {
        public const int CREATE_NEW_ORDER = 1;
    }
    public class Order
    {
        private int? _OrderId ;
        private DateTime _OrderDate ;
        private Table _OrderTable ;
       private List<Item> _ItemsList ;
        public int? OrderId { 
        get
        {
            return _OrderId;
        }
         set
        {
           _OrderId=value;
        }
        }
        public DateTime OrderDate {
        get
        {
            return _OrderDate;
        } 
        set
        {
            _OrderDate=value;
        }
        }
        public Table OrderTable
        { 
        get
        {
            return _OrderTable;
        } 
        set
        {
            _OrderTable = value;
        }
        }
        public List<Item> ItemsList { get{
            return _ItemsList;
        } 
        set
        {
            _ItemsList= value;
        }
        }

        public Item this[int index]
        {
            get
            {
                if (ItemsList == null || ItemsList.Count == 0 || index < 0 || ItemsList.Count < index) return null;
                return ItemsList[index];
            }
            set
            {
                if (ItemsList == null) ItemsList = new List<Item>();
                ItemsList.Add(value);
            }
        }

        public Order()
        {
            ItemsList = new List<Item>();
        }
    }
}