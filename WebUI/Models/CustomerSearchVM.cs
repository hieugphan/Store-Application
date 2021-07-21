using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class CustomerSearchVM
    {
        public CustomerSearchVM()
        {
        }

        [Required]
        public SearchCriteria Criteria { get; set; }
        [Required]
        public string Value { get; set; }
    }


    public enum SearchCriteria
    {
        fname,
        lname,
        address,
        city,
        state,
        email,
        phone
    }
}
