using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class StoreFrontVM
    {
        public StoreFrontVM()
        {
        }

        public StoreFrontVM(StoreFront p_sf)
        {
            Id = p_sf.Id;
            Name = p_sf.Name;
            Address = p_sf.Address;
            City = p_sf.City;
            State = p_sf.State;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
