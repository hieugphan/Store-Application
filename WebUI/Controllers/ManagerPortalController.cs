using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using Serilog;

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
                Log.Information("Manager Fail To Log In");
                ViewBag.Error = "Incorrect Password";
                return View();
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
            try
            {
                //convert Customer obj to CustomerVM obj
                return View(
                       _BL.GetAllCustomers()
                       .Select(cust => new CustomerVM(cust))
                       .ToList()
                );
            }
            catch (Exception)
            {
                Log.Information("Fail To Get All Customer From DB");
                return View(nameof(Customer));
            }
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
                Log.Information("Fail To Add Customer");
                ViewBag.Error = "Missing/Invalid Input --- Try Again";
                return View();
            }

            Log.Information("ModelState Invalid");
            ViewBag.Error = "Missing/Invalid Input --- Try Again";
            return View();
        }


        //public IActionResult CustomerEditCustomer(int p_id)
        //{
        //    return View(new CustomerVM(_BL.GetACustomer(p_id)));
        //}


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
            try
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
            catch (Exception)
            {
                Log.Information("Fail To Search Customer");
                return View(nameof(CustomerSearchForCustomer));
            }
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult OrderDisplayAllstores()
        {
            try
            {
                return View(
                    _BL.GetAllStoreFronts()
                    .Select(sf => new StoreFrontVM(sf))
                    .ToList()
                );

            }
            catch (Exception)
            {
                Log.Information("Fail To Get All Store Fronts");
                return View(nameof(Order));
            }
        }

        public IActionResult OrderDisplayAStoreFrontOrders(int p_sfId, string sortOrder)
        {
            try
            {
                TempData["sfId"] = p_sfId;
                ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
                ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";


                IEnumerable<OrderVM> listOfStoreFrontOrders = _BL.GetAStoreFrontOrders(p_sfId).Select(order => new OrderVM(order)).ToList();

                switch (sortOrder)
                {
                    case "price":
                        listOfStoreFrontOrders = listOfStoreFrontOrders.OrderBy(o => o.TotalPrice);
                        break;
                    case "price_desc":
                        listOfStoreFrontOrders = listOfStoreFrontOrders.OrderByDescending(o => o.TotalPrice);
                        break;

                    case "date_desc":
                        listOfStoreFrontOrders = listOfStoreFrontOrders.OrderByDescending(o => o.Date);
                        break;
                    default:
                        listOfStoreFrontOrders = listOfStoreFrontOrders.OrderBy(o => o.Date);
                        break;
                }

                return View(listOfStoreFrontOrders);
            }
            catch (Exception)
            {
                Log.Information("Fail To Sort Order");
                return View(nameof(Order));
            }
        }

        public IActionResult OrderDisplayOrderDetail(int p_orderId)
        {
            try
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
            catch (Exception)
            {
                Log.Information("Fail To Display Order Detail");
                return View(nameof(Order));
            }
        }
        public IActionResult Inventory()
        {
            return View();
        }

        public IActionResult InventoryDisplayAllstores()
        {
            try
            {
                return View(
                    _BL.GetAllStoreFronts()
                    .Select(sf => new StoreFrontVM(sf))
                    .ToList()
                );
            }
            catch (Exception)
            {
                Log.Information("Fail To Get All Store Fronts");
                return View(nameof(Inventory));
            }
        }

        public IActionResult InventoryDisplayAStoreInventory(int p_sfId)
        {
            try
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
            catch (Exception)
            {
                Log.Information("Fail To Display A Store Inventory");
                return View(nameof(Inventory));
            }
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
                int sfId = (int)TempData["sfId"];
                TempData.Keep("sfId");
            try
            {
                int inventoryId = (int)TempData["inventoryId"];
                _BL.ReplenishInventory(inventoryId, p_quantity);

                return RedirectToAction("InventoryDisplayAStoreInventory", new { p_sfId = sfId });
            }
            catch (Exception)
            {
                Log.Information("Fail To Replenish Inventory");
                return RedirectToAction("InventoryDisplayAStoreInventory", new { p_sfId = sfId });
            }
        }
    }
}
