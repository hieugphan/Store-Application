using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Models
{
    public class StoreFront
    {
        private int _id;
        private string _name;
        private string _address;
        private string _city;
        private string _state;
        private List<Order> _orders = new List<Order>();
        private List<Inventory> _inventories = new List<Inventory>();
        public StoreFront()
        {
        }

        public int Id { get => _id; set => _id = value; }
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[A-Za-z0-9 .,'-]+$"))
                {
                    throw new Exception("Invalid Input [A-Za-z0-9 .,'-]");
                }
                _name = value;
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[A-Za-z0-9 .,-]+$"))
                {
                    throw new Exception("Invalid Input [A-Za-z0-9 .,-]");
                }
                _address = value;
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[A-Za-z .,'-]+$"))
                {
                    throw new Exception("Invalid Input [A-Za-z .,'-]");
                }
                _city = value;
            }
        }
        public string State
        {
            get => _state;
            set
            {
                if (!Regex.IsMatch(value, @"^(?-i:A[LKSZRAEP]|C[AOT]|D[EC]|F[LM]|G[AU]|HI|I[ADLN]|K[SY]|LA|M[ADEHINOPST]|N[CDEHJMVY]|O[HKR]|P[ARW]|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY])$"))
                {
                    throw new Exception("Invalid Input - 2[A-Z]");
                }
                _state = value;
            }
        }

        public List<Order> Orders { get => _orders; set => _orders = value; }
        public List<Inventory> Inventories { get => _inventories; set => _inventories = value; }

        public override string ToString()
        {

            return $"Id:[{Id}] Name: {Name} ||| Address: {Address} ||| City: {City} ||| State: {State}";
        }
    }
}