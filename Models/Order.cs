using System;
using System.Collections.Generic;
namespace Models
{
    public class Order
    {
        private int _id;
        private int _customerId;
        private int _storeFrontId;
        private double _totalPrice;
        private DateTime _date;
        private List<LineItem> _lineItems = new List<LineItem>();

        public Order()
        {
        }

        public int Id { get => _id; set => _id = value; }
        public int CustomerId { get => _customerId; set => _customerId = value; }
        public int StoreFrontId { get => _storeFrontId; set => _storeFrontId = value; }
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public List<LineItem> LineItems { get => _lineItems; set => _lineItems = value; }

        public override string ToString()
        {
            // return $"Order ID: [{Id}] ||| CustomerID: {CustomerId} ||| StoreFrontID: {StoreFrontId} ||| Total Price: ${TotalPrice}";
            return $"Order ID: [{Id}] ||| CustomerID: {CustomerId} ||| StoreFrontID: {StoreFrontId} ||| Total Price: $" + string.Format("{0:0.00}", TotalPrice);
        }
    }
}