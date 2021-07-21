using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class CustomerVM
    {
        public CustomerVM()
        {
        }

        public CustomerVM(Customer p_cust)
        {
            Id = p_cust.Id;
            Fname = p_cust.Fname;
            Lname = p_cust.Lname;
            Address = p_cust.Address;
            City = p_cust.City;
            State = p_cust.State;
            Email = p_cust.Email;
            Phone = p_cust.Phone;
        }

        public int Id { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        //?????
        //[Phone]
        public string Phone { get; set; }
    }
}
