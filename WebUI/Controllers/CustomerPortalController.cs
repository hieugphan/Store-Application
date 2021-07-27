using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebUI.Models;
using Serilog;

namespace WebUI.Controllers
{
    public class CustomerPortalController : Controller
    {
        private IBL _BL;
        private static List<LineItemVM> shoppingCart = new List<LineItemVM>();
        private static string productName = "";
        private static double productPrice = 0.00;
        //var logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("CustomerPortalLog.txt").CreateLogger();
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
                Log.Information("User Fail To Register - Incorrect RegEx");
                ViewBag.Error = "Missing/Invalid Input --- Try Again";
                return View();
            }

            Log.Information("User Fail To Register");
            ViewBag.Error = "Missing/Invalid Input --- Try Again";
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string p_fname, string p_email)
        {
            try
            {
                CustomerVM theCust = new CustomerVM();
                List<Customer> listOfSearchedCustomers = _BL.SearchCustomers("fname", p_fname.ToUpper());
                bool flag = false;
                if(listOfSearchedCustomers.Count > 0)
                {
                    foreach (Customer c in listOfSearchedCustomers)
                    {
                        if(c.Email == p_email)
                        {
                            theCust = new CustomerVM(c);
                            flag = true;
                        }
                    }

                    if(flag)
                    {
                        TempData["fName"] = theCust.Fname;
                        TempData["customerId"] = theCust.Id;

                        return RedirectToAction("SignInOption");
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "DB Not Working";
                return View();
            }

            Log.Information("User Fail To Log In");
            ViewBag.Error = ViewBag.Error = "Customer Not Found";
            return View();
        }

        public IActionResult SignInOption()
        {
            return View();
        }

        

        public IActionResult FindAStore()
        {
            try
            {
                shoppingCart.Clear();
                return View(
                    _BL.GetAllStoreFronts()
                    .Select(sf => new StoreFrontVM(sf))
                    .ToList()
                );

            }
            catch (Exception)
            {
                shoppingCart.Clear();
                return View(nameof(SignInOption));
            }

        }

        public IActionResult DisplayAStoreInventory(int p_sfId)
        {
            try
            {
                TempData["sfId"] = p_sfId;
                ViewBag.Name = _BL.GetAStore(p_sfId).Name;
                List<Inventory> theStoreInventory = _BL.GetAStoreInventory(p_sfId);
                List<Product> listOfProducts = _BL.GetAllProducts();
                List<TheStoreInventoryVM> listOfTheStoreInventoryVM = new List<TheStoreInventoryVM>();
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
                    listOfTheStoreInventoryVM.Add(inventory);
                }
                return View(listOfTheStoreInventoryVM);

            }
            catch (Exception)
            {
                Log.Information("Fail To Display A Store Inventory");
                return View(nameof(SignInOption));
            }
        }

        public IActionResult CreateQuantity(TheStoreInventoryVM p_theStoreInventoryVM)
        {
            TempData["inventoryId"] = p_theStoreInventoryVM.Id;
            TempData["productId"] = p_theStoreInventoryVM.ProductId;
            productName = p_theStoreInventoryVM.ProductName;
            productPrice = p_theStoreInventoryVM.ProductPrice;
            return View(p_theStoreInventoryVM);
        }

        public IActionResult AddToCart(int p_quantity)
        {
            int sfId = (int)TempData["sfId"];
            LineItemVM liVM = new LineItemVM();
            liVM.CustomerId = (int)TempData["customerId"];
            liVM.StoreFrontId = sfId;
            liVM.InventoryId = (int)TempData["inventoryId"];
            liVM.ProductId = (int)TempData["productId"];
            liVM.ProductPrice = productPrice;
            liVM.ProductName = productName;
            liVM.Quantity = p_quantity;
            TempData.Keep("customerId");
            TempData.Keep("sfId");
            TempData.Keep("inventoryId");
            TempData.Keep("productId");
            TempData.Keep("productPrice");
            TempData.Keep("productName");
            shoppingCart.Add(liVM);
            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            return View(shoppingCart);
        }

        public IActionResult EmptyCart()
        {
            shoppingCart.Clear();
            return View(nameof(ViewCart));
        }

        public IActionResult CheckOut()
        {
            //calculate cart total
            double total = 0.0;
            foreach(LineItemVM liVM in shoppingCart)
            {
                total += liVM.ProductPrice * liVM.Quantity;
            }

            //Create a new order and write to DB
            Order newOrder = new Order();
            newOrder.CustomerId = (int)TempData["customerId"];
            newOrder.StoreFrontId = (int)TempData["sfId"];
            newOrder.TotalPrice = total;
            newOrder.Date = DateTime.Now;
            newOrder = _BL.AddAnOrder(newOrder);

            //Create lineItems in the cart and write to DB
            foreach (LineItemVM liVM in shoppingCart)
            {
                LineItem li = new LineItem();
                li.OrderId = newOrder.Id;
                li.ProductId = liVM.ProductId;
                li.Quantity = liVM.Quantity;
                li = _BL.AddALineItem(li);
            }

            //Update the store inventory and write to DB
            foreach (LineItemVM liVM in shoppingCart)
            {
                _BL.UpdateInventoryQuantity(liVM.InventoryId, liVM.Quantity);
            }


            TempData.Keep("customerId");
            TempData.Keep("sfId");

            List<LineItemVM> receipt = new List<LineItemVM>(shoppingCart);
            shoppingCart.Clear();
            return View(receipt);
        }

        public IActionResult ViewOrderHistory(string sortOrder)
        {
            try
            {
                int customerId = (int)TempData["customerId"];
                TempData.Keep("customerId");

                ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
                ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
                IEnumerable<OrderVM> listOfCustomerOrders = _BL.GetACustomerOrders(customerId).Select(order => new OrderVM(order)).ToList();

                switch (sortOrder)
                {
                    case "price":
                        listOfCustomerOrders = listOfCustomerOrders.OrderBy(o => o.TotalPrice);
                        break;
                    case "price_desc":
                        listOfCustomerOrders = listOfCustomerOrders.OrderByDescending(o => o.TotalPrice);
                        break;

                    case "date_desc":
                        listOfCustomerOrders = listOfCustomerOrders.OrderByDescending(o => o.Date);
                        break;
                    default:
                        listOfCustomerOrders = listOfCustomerOrders.OrderBy(o => o.Date);
                        break;
                }

                return View(listOfCustomerOrders);
            }
            catch (Exception)
            {
                Log.Information("Fail To Sort");
                return View(nameof(SignInOption));
            }
        }

        public IActionResult ViewOrderDetail(int p_orderId)
        {
            try
            {
                List<LineItemVM> listOfAnOrderLineItems = _BL.GetAnOrderLineItems(p_orderId).Select(li => new LineItemVM(li)).ToList();
                List<Product> listOfProducts = _BL.GetAllProducts();
                foreach(LineItemVM liVM in listOfAnOrderLineItems)
                {
                    foreach(Product p in listOfProducts)
                    {
                        if(liVM.ProductId == p.Id)
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
                Log.Information("Fail To Show Order Detail");
                return View(nameof(SignInOption));
            }
        }
    }
}
