using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ManagerPortalController : Controller
    {
        private IBL _BL;
        public ManagerPortalController(IBL p_BL)
        {
            _BL = p_BL;
        }

        public IActionResult OpenManagerPortal()
        {
            return View();
        }


        public IActionResult DisplayAllCustomers()
        {
            //convert Customer obj to CustomerVM obj
            return View(
                   _BL.GetAllCustomers()
                   .Select(cust => new CustomerVM(cust))
                   .ToList()
            );
        }

        //This action method only show the view
        public IActionResult CreateNewCustomer()
        {
            return View();
        }

        //only enact this method when there a post request
        [HttpPost]
        public IActionResult CreateNewCustomer(CustomerVM p_custVM)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _BL.AddCustomer(new Customer
                    {
                        Fname = p_custVM.Fname.ToUpper(),
                        Lname = p_custVM.Lname.ToUpper(),
                        Address = p_custVM.Address.ToUpper(),
                        City = p_custVM.City.ToUpper(),
                        State = p_custVM.State.ToUpper(),
                        Email = p_custVM.Email,
                        Phone = p_custVM.Phone
                    }
                    );

                    // Go to the DisplayAllCustomers html of the Customer Controller
                    return RedirectToAction(nameof(OpenManagerPortal));
                }
            }
            catch (Exception)
            {
                return View();
            }

            return View();
        }




        public IActionResult Edit(int p_id)
        {
            return View(new CustomerVM(_BL.GetACustomer(p_id)));
        }


        public IActionResult SearchForCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchForCustomer(CustomerSearchVM p_searchVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("DisplayListOfSearchedCustomer", new CustomerSearchVM { Criteria = p_searchVM.Criteria, Value = p_searchVM.Value });
                }
            }
            catch (Exception)
            {
                return View();
            }

            return View();
        }

        public IActionResult DisplayListOfSearchedCustomer(CustomerSearchVM p_searchVM)
        {
            string value;
            if (p_searchVM.Criteria.ToString() == "email")
            {
                value = p_searchVM.Value;
            }
            else
            {
                value = p_searchVM.Value.ToUpper();
            }
            List<Customer> listOfSearchedCustomer = _BL.SearchCustomers(p_searchVM.Criteria.ToString(), value);
            List<CustomerVM> listOfSearchedCustomerVM = new List<CustomerVM>();
            foreach (Customer c in listOfSearchedCustomer)
            {
                listOfSearchedCustomerVM.Add(new CustomerVM(c));
            }
            return View(listOfSearchedCustomerVM);
        }

    }
}
