using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class LineItemVM
    {
        public LineItemVM()
        {
        }

        public LineItemVM(LineItem p_li)
        {
            Id = p_li.Id;
            OrderId = p_li.OrderId;
            ProductId = p_li.ProductId;
            Quantity = p_li.Quantity;
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreFrontId { get; set; }
        public int InventoryId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { set; get; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
    }
}
