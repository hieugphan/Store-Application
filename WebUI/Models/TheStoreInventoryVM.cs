using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class TheStoreInventoryVM
    {
        public TheStoreInventoryVM()
        {

        }

        public TheStoreInventoryVM(int id, int storeFrontId, int productId, string productName, double productPrice, int quantity)
        {
            Id = id;
            StoreFrontId = storeFrontId;
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public int StoreFrontId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
