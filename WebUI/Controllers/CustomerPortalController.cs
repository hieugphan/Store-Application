using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CustomerPortalController : Controller
    {
        private IBL _BL;
        public CustomerPortalController(IBL p_BL)
        {
            _BL = p_BL;
        }

        public IActionResult OpenCustomerPortal()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CustomerVM p_custVM)
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

                    return RedirectToAction(nameof(OpenCustomerPortal));
                }
            }
            catch (Exception)
            {
                return View();
            }

            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string p_fname, string p_email)
        {
            CustomerVM theCust = new CustomerVM();
            List<Customer> listOfSearchedCustomer = _BL.SearchCustomers("fname", p_fname.ToUpper());
            try
            {
                if(listOfSearchedCustomer.Count > 0)
                {
                    foreach (Customer c in listOfSearchedCustomer)
                    {
                        if(c.Email == p_email)
                        {
                            theCust = new CustomerVM(c);
                        }
                    }

                    TempData["fName"] = theCust.Fname;
                    TempData["customerId"] = theCust.Id;

                    return RedirectToAction("SignInOption");
                }
            }
            catch (Exception)
            {
                return View();
            }

            return View();
        }

        public IActionResult SignInOption()
        {

            return View();
        }

        public IActionResult FindAStore()
        {
            return View(
                _BL.GetAllStoreFronts()
                .Select(sf => new StoreFrontVM(sf))
                .ToList()
            );
        }

        public IActionResult DisplayAStoreInventory(int p_sfId)
        {
            TempData["sfId"] = p_sfId;
            //List<InventoryVM> theStoreInventory = _BL.GetAStoreInventory(p_sfId).Select(inv => new InventoryVM(inv)).ToList();
            List<Inventory> theStoreInventory = _BL.GetAStoreInventory(p_sfId);
            List<Product> listOfProducts = _BL.GetAllProducts();
            List<TheStoreInventoryVM> theStoreInventoryVM = new List<TheStoreInventoryVM>();
            foreach(Inventory inv in theStoreInventory)
            {
                TheStoreInventoryVM inventory = new TheStoreInventoryVM();
                inventory.Id = inv.Id;
                inventory.StoreFrontId = inv.StoreFrontId;
                inventory.Quantity = inv.Quantity;
                foreach(Product p in listOfProducts)
                {
                    if(inv.ProductId == p.Id)
                    {
                        inventory.ProductId = p.Id;
                        inventory.ProductName = p.Name;
                        inventory.ProductPrice = p.Price;
                    }
                }
                theStoreInventoryVM.Add(inventory);
            }

            //TempData["theStoreInventoryVM"] = theStoreInventoryVM;
            return View(theStoreInventoryVM);
        }
    }
}
