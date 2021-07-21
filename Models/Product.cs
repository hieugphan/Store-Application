using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Models
{
    public class Product
    {
        private int _id;
        private string _name;
        private double _price;
        private string _description;
        private string _category;
        private List<Inventory> _inventories = new List<Inventory>();
        private List<LineItem> _lineItems = new List<LineItem>();

        public Product()
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
        public double Price { get => _price; set => _price = value; }
        public string Description 
        {
            get
            {
                return _description;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[A-Za-z0-9 .,'-]+$"))
                {
                    throw new Exception("Invalid Input [A-Za-z0-9 .,'-]");
                }
                _description = value;
            }
        }
        public string Category 
        {
            get
            {
                return _category;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^[A-Za-z0-9 .,'-]+$"))
                {
                    throw new Exception("Invalid Input [A-Za-z0-9 .,'-]");
                }
                _category = value;
            }
        }
        public List<Inventory> Inventories { get => _inventories; set => _inventories = value; }
        public List<LineItem> LineItems { get => _lineItems; set => _lineItems = value; }

        public override string ToString()
        {
            // return $"Product ID: {Id} ||| Product: {Name} ||| Price: ${Price} ||| Description: {Description} ||| Category: {Category} |||";
            return $"Product ID: {Id} ||| Product: {Name} ||| Price: $" + string.Format("{0:0.00}", Price) + $" ||| Description: {Description} ||| Category: {Category} |||";
        }
    }
}