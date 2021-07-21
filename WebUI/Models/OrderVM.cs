using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class OrderVM
    {
        public OrderVM()
        {
        }

        public OrderVM(Order p_order)
        {
            Id = p_order.Id;
            CustomerId = p_order.CustomerId;
            StoreFrontId = p_order.StoreFrontId;
            TotalPrice = p_order.TotalPrice;
        }

        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int StoreFrontId { get; set; }
        [Required]
        public double TotalPrice { get; set; }
    }
}
