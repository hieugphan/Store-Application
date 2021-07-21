using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class InventoryVM
    {
        public InventoryVM()
        {
        }

        public InventoryVM(Inventory p_invt)
        {
            Id = p_invt.Id;
            StoreFrontId = p_invt.StoreFrontId;
            ProductId = p_invt.ProductId;
            Quantity = p_invt.Quantity;
        }

        public int Id { get; set; }
        [Required]
        public int StoreFrontId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
