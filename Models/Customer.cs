using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Models
{
    public class Customer
    {
        private int _id;
        private string _fname;
        private string _lname;
        private string _address;
        private string _city;
        private string _state;
        private string _email;
        private string _phone;
        private List<Order> _orders = new List<Order>();


        public Customer()
        {
        }

        public int Id { get => _id; set => _id = value; }

        // RegEx can be added here       
        public string Fname
        {
            get
            {
                return _fname;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[A-Za-z .']+$"))
                {
                    throw new Exception("Invalid Input [A-Za-z .']");
                }
                _fname = value;
            }
        }

        public string Lname
        {
            get
            {
                return _lname;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[A-Za-z .']+$"))
                {
                    throw new Exception("Invalid Input [A-Za-z .']");
                }
                _lname = value;
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
                if(!Regex.IsMatch(value, @"^[A-Za-z0-9 .,-]+$"))
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

        public string Email {  
            get
            {
                return _email;
            }
            set
            {
                if(!Regex.IsMatch(value, @"^[A-Za-z0-9.@]+$"))
                {
                    throw new Exception("Invalid Input [A-Za-z0-9.@]");
                }
                _email = value;
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                //if (!Regex.IsMatch(value, @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}"))
                if (!Regex.IsMatch(value, @"\d{10}"))
                {
                    throw new Exception("Invalid Input - 10 digits [0-9]");
                }
                _phone = value;
            }
        }

        public List<Order> Orders { get => _orders; set => _orders = value; }

        public override string ToString()
        {
            return $"ID: {Id} ||| First Name: {Fname} ||| Last Name: {Lname} ||| Address: {Address} ||| City: {City} ||| State: {State} ||| Email: {Email} ||| Phone: {Phone}";
        }

    }
}
