using System;
namespace Models
{
    public class LineItem
    {
        private int _id;
        private int _orderId;
        private int _productId;
        private int _quantity;

        public LineItem()
        {
        }

        public int Id { get => _id; set => _id = value; }
        public int OrderId { get => _orderId; set => _orderId = value; }
        public int ProductId { get => _productId; set => _productId = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }

        public override string ToString()
        {
            return "OrderID: " + OrderId + " ||| ProductID : " + ProductId + " ||| Quantity : " + Quantity;
        }
    }
}