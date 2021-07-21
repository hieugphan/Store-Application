using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ProductVM
    {
        public ProductVM()
        {
        }

        public ProductVM(Product p_product)
        {
            Id = p_product.Id;
            Name = p_product.Name;
            Price = p_product.Price;
            Description = p_product.Description;
            Category = p_product.Category;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
