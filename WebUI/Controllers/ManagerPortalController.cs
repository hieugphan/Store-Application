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
        private static string productName = "";
        private static double productPrice = 0.00;
        public ManagerPortalController(IBL p_BL)
        {
            _BL = p_BL;
        }

        public IActionResult OpenManagerPortal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OpenManagerPortal(string p_password)
        {
            if(p_password == "1234")
            {
                return View(nameof(ManagerOption));
            }
            else
            {
                return RedirectToAction("OpenManagerPortal");
            }
        }

        public IActionResult ManagerOption()
        {
            return View();
        }

        public IActionResult Customer()
        {
            return View();
        }


        public IActionResult CustomerDisplayAllCustomers()
        {
            //convert Customer obj to CustomerVM obj
            return View(
                   _BL.GetAllCustomers()
                   .Select(cust => new CustomerVM(cust))
                   .ToList()
            );
        }

        //This action method only show the view
        public IActionResult CustomerCreateNewCustomer()
        {
            return View();
        }

        //only enact this method when there a post request
        [HttpPost]
        public IActionResult CustomerCreateNewCustomer(CustomerVM p_custVM)
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




        public IActionResult CustomerEditCustomer(int p_id)
        {
            return View(new CustomerVM(_BL.GetACustomer(p_id)));
        }


        public IActionResult CustomerSearchForCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerSearchForCustomer(CustomerSearchVM p_searchVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("CustomerDisplayListOfSearchedCustomer", new CustomerSearchVM { Criteria = p_searchVM.Criteria, Value = p_searchVM.Value });
                }
            }
            catch (Exception)
            {
                return View();
            }

            return View();
        }

        public IActionResult CustomerDisplayListOfSearchedCustomer(CustomerSearchVM p_searchVM)
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

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult OrderDisplayAllstores()
        {
            return View(
                _BL.GetAllStoreFronts()
                .Select(sf => new StoreFrontVM(sf))
                .ToList()
            );
        }

        //public IActionResult OrderDisplayAStoreFrontOrders(int p_sfId)
        //{
        //    TempData["sfId"] = p_sfId;
        //    List<OrderVM> listOfStoreFrontOrders = _BL.GetAStoreFrontOrders(p_sfId).Select(order => new OrderVM(order)).ToList();
        //    return View(listOfStoreFrontOrders);           
        //}


        public IActionResult OrderDisplayAStoreFrontOrders(int p_sfId, string sortOrder)
        {
            TempData["sfId"] = p_sfId;
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            IEnumerable<OrderVM> listOfStoreFrontOrders = _BL.GetAStoreFrontOrders(p_sfId).Select(order => new OrderVM(order)).ToList();

            switch (sortOrder)
            {
                case "price_desc":
                    listOfStoreFrontOrders = listOfStoreFrontOrders.OrderByDescending(o => o.TotalPrice);
                    break;
                case "Date":
                    listOfStoreFrontOrders = listOfStoreFrontOrders.OrderBy(o => o.Date);
                    break;
                case "date_desc":
                    listOfStoreFrontOrders = listOfStoreFrontOrders.OrderByDescending(o => o.Date);
                    break;
                default:
                    listOfStoreFrontOrders = listOfStoreFrontOrders.OrderBy(o => o.TotalPrice);
                    break;
            }



            return View(listOfStoreFrontOrders);
        }

        public IActionResult OrderDisplayOrderDetail(int p_orderId)
        {
            List<LineItemVM> listOfAnOrderLineItems = _BL.GetAnOrderLineItems(p_orderId).Select(li => new LineItemVM(li)).ToList();
            List<Product> listOfProducts = _BL.GetAllProducts();
            foreach (LineItemVM liVM in listOfAnOrderLineItems)
            {
                foreach (Product p in listOfProducts)
                {
                    if (liVM.ProductId == p.Id)
                    {
                        liVM.ProductName = p.Name;
                        liVM.ProductPrice = p.Price;
                    }
                }
            }
            return View(listOfAnOrderLineItems);
        }
        public IActionResult Inventory()
        {
            return View();
        }

        public IActionResult InventoryDisplayAllstores()
        {
            return View(
                _BL.GetAllStoreFronts()
                .Select(sf => new StoreFrontVM(sf))
                .ToList()
            );
        }

        public IActionResult InventoryDisplayAStoreInventory(int p_sfId)
        {
            TempData["sfId"] = p_sfId;
            ViewBag.Name = _BL.GetAStore(p_sfId).Name;
            List<Inventory> listOfTheStoreInventory = _BL.GetAStoreInventory(p_sfId);
            List<Product> listOfAllProducts = _BL.GetAllProducts();
            List<TheStoreInventoryVM> listOfTheStoreInventoryVM = new List<TheStoreInventoryVM>();
            foreach (Inventory inv in listOfTheStoreInventory)
            {
                TheStoreInventoryVM inventoryVM = new TheStoreInventoryVM();
                inventoryVM.Id = inv.Id;
                inventoryVM.StoreFrontId = inv.StoreFrontId;
                inventoryVM.Quantity = inv.Quantity;
                foreach (Product p in listOfAllProducts)
                {
                    if (inv.ProductId == p.Id)
                    {
                        inventoryVM.ProductId = p.Id;
                        inventoryVM.ProductName = p.Name;
                        inventoryVM.ProductPrice = p.Price;
                    }
                }
                listOfTheStoreInventoryVM.Add(inventoryVM);
            }


            return View(listOfTheStoreInventoryVM);
        }

        public IActionResult InventoryReplenishInventory(TheStoreInventoryVM p_theStoreInventoryVM)
        {
            TempData["inventoryId"] = p_theStoreInventoryVM.Id;
            TempData["productId"] = p_theStoreInventoryVM.ProductId;
            productName = p_theStoreInventoryVM.ProductName;
            productPrice = p_theStoreInventoryVM.ProductPrice;
            return View(p_theStoreInventoryVM);
        }

        [HttpPost]
        public IActionResult InventoryReplenishInventory(int p_quantity)
        {
            int inventoryId = (int)TempData["inventoryId"];
            _BL.ReplenishInventory(inventoryId, p_quantity);
            int sfId = (int)TempData["sfId"];
            TempData.Keep("sfId");

            return RedirectToAction("InventoryDisplayAStoreInventory", new { p_sfId = sfId });

        }
    }
}
