using Models;
using DataAccessLogic;
using System.Collections.Generic;
using System;

namespace BusinessLogic
{
    public class BL : IBL
    {
        private IDAL _repo;

        public BL(IDAL p_repo)
        {
            _repo = p_repo;
        }

        public LineItem AddALineItem(LineItem p_lineItem)
        {
            LineItem found = _repo.AddALineItem(p_lineItem);
            if (found == null)
            {
                throw new Exception("LineItem Was Null");
            }
            return found;
        }

        public Order AddAnOrder(Order p_order)
        {
            Order found =_repo.AddAnOrder(p_order);
            if(found == null)
            {
                throw new Exception("Order Was Null");
            }
            return found;
        }

        public Customer AddCustomer(Customer p_customer)
        {
            Customer found = _repo.AddCustomer(p_customer);
            if (found == null)
            {
                throw new Exception("Customer Was Null");
            }
            return found;
        }

        public Inventory AddNewProductInventory(Inventory p_invt)
        {
            Inventory found = _repo.AddNewProductInventory(p_invt);
            if (found == null)
            {
                throw new Exception("Customer Was Null");
            }
            return found;
        }

        public Customer GetACustomer(int p_customerID)
        {
            Customer found = _repo.GetACustomer(p_customerID);
            if (found == null)
            {
                throw new Exception("Customer Was Null");
            }

            return found;
        }

        public List<Order> GetACustomerOrders(int p_customerId)
        {
            return _repo.GetACustomerOrders(p_customerId);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public List<Inventory> GetAllInventories()
        {
            return _repo.GetAllInventories();
        }

        public List<LineItem> GetAllLineItems()
        {
            return _repo.GetAllLineItems();
        }

        public List<Order> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }

        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            return _repo.GetAllStoreFronts();
        }

        public List<LineItem> GetAnOrderLineItems(Order p_order)
        {
            List<LineItem> listOfAllLineItems = this.GetAllLineItems();
            List<LineItem> listOfAnOrderLineItems = new List<LineItem>();
            foreach (LineItem li in listOfAllLineItems)
            {
                if(li.OrderId == p_order.Id)
                {
                    listOfAnOrderLineItems.Add(li);
                }
            }
            return listOfAnOrderLineItems;
        }

        public List<LineItem> GetAnOrderLineItems(int p_orderID)
        {
            List<LineItem> listOfAllLineItems = this.GetAllLineItems();
            List<LineItem> listOfAnOrderLineItems = new List<LineItem>();
            foreach (LineItem li in listOfAllLineItems)
            {
                if(li.OrderId == p_orderID)
                {
                    listOfAnOrderLineItems.Add(li);
                }
            }
            return listOfAnOrderLineItems;
        }

        public StoreFront GetAStore(int p_id)
        {
            List<StoreFront> listOfStoreFronts = this.GetAllStoreFronts();
            StoreFront theStore = new StoreFront();
            foreach (StoreFront sf in listOfStoreFronts)
            {
                if (sf.Id == p_id)
                {
                    theStore = sf;
                }
            }
            return theStore;
        }

        public List<Order> GetAStoreFrontOrders(int p_storeFrontId)
        {
            return _repo.GetAStoreFrontOrders(p_storeFrontId);
        }

        public List<Inventory> GetAStoreInventory(StoreFront p_storeFront)
        {
            return _repo.GetAStoreInventory(p_storeFront);
        }

        public List<Inventory> GetAStoreInventory(int p_storeFrontId)
        {
            return _repo.GetAStoreInventory(p_storeFrontId);
        }
        

        public Inventory ReplenishInventory(int p_invId, int p_replenishedQuantity)
        {
            return _repo.ReplenishInventory(p_invId, p_replenishedQuantity);
        }
                       

        public List<Customer> SearchCustomers(string p_criteria, string p_value)
        {
            List<Customer> listOfCustomers = this.GetAllCustomers();
            List<Customer> listOfSearchedCustomers = new List<Customer>();
            switch(p_criteria)
            {
                case "phone":
                    try
                    {
                        foreach(Customer c in listOfCustomers)
                        {
                            if (c.Phone == p_value)
                            {
                                listOfSearchedCustomers.Add(c);
                            }
                        }
                        return listOfSearchedCustomers;
                    }
                    catch(System.Exception e)
                    {
                        System.Console.WriteLine("Something Wrong");
                        System.Console.WriteLine(e);
                        return listOfSearchedCustomers;
                    }
                case "email":
                    try
                    {
                        foreach(Customer c in listOfCustomers)
                        {
                            if (c.Email == p_value)
                            {
                                listOfSearchedCustomers.Add(c);
                            }
                        }
                        return listOfSearchedCustomers;
                    }
                    catch(System.Exception e)
                    {
                        System.Console.WriteLine("Something Wrong");
                        System.Console.WriteLine(e);
                        return listOfSearchedCustomers;
                    }
                case "state":
                    try
                    {
                        foreach(Customer c in listOfCustomers)
                        {
                            if (c.State == p_value)
                            {
                                listOfSearchedCustomers.Add(c);
                            }
                        }
                        return listOfSearchedCustomers;
                    }
                    catch(System.Exception e)
                    {
                        System.Console.WriteLine("Something Wrong");
                        System.Console.WriteLine(e);
                        return listOfSearchedCustomers;
                    }
                case "city":
                    try
                    {
                        foreach(Customer c in listOfCustomers)
                        {
                            if (c.City == p_value)
                            {
                                listOfSearchedCustomers.Add(c);
                            }
                        }
                        return listOfSearchedCustomers;
                    }
                    catch(System.Exception e)
                    {
                        System.Console.WriteLine("Something Wrong");
                        System.Console.WriteLine(e);
                        return listOfSearchedCustomers;
                    }
                case "address":
                    try
                    {
                        foreach(Customer c in listOfCustomers)
                        {
                            if (c.Address == p_value)
                            {
                                listOfSearchedCustomers.Add(c);
                            }
                        }
                        return listOfSearchedCustomers;
                    }
                    catch(System.Exception e)
                    {
                        System.Console.WriteLine("Something Wrong");
                        System.Console.WriteLine(e);
                        return listOfSearchedCustomers;
                    }
                case "lname":
                    try
                    {
                        foreach(Customer c in listOfCustomers)
                        {
                            if (c.Lname == p_value)
                            {
                                listOfSearchedCustomers.Add(c);
                            }
                        }
                        return listOfSearchedCustomers;
                    }
                    catch(System.Exception e)
                    {
                        System.Console.WriteLine("Something Wrong");
                        System.Console.WriteLine(e);
                        return listOfSearchedCustomers;
                    }
                case "fname":
                    try
                    {
                        foreach(Customer c in listOfCustomers)
                        {
                            if (c.Fname == p_value)
                            {
                                listOfSearchedCustomers.Add(c);
                            }
                        }
                        return listOfSearchedCustomers;
                    }
                    catch(System.Exception e)
                    {
                        System.Console.WriteLine("Something Wrong");
                        System.Console.WriteLine(e);
                        return listOfSearchedCustomers;
                    }
                default:
                    //this would return empty list / count = 0
                    return listOfSearchedCustomers;
            }
        }

        public List<Order> SearchOrders(string p_criteria, string p_value)
        {
            List<Order> listOfOrders = this.GetAllOrders();
            List<Order> listOfSearchedOrders = new List<Order>();
            int value = int.Parse(p_value);
            switch(p_criteria)
            {
                case "id":
                foreach (Order o in listOfOrders)
                {
                    if(o.Id == value)
                    {
                        listOfSearchedOrders.Add(o);
                    }
                }
                return listOfSearchedOrders;
                case "customerID":
                foreach (Order o in listOfOrders)
                {
                    if(o.CustomerId == value)
                    {
                        listOfSearchedOrders.Add(o);
                    }
                }
                return listOfSearchedOrders;
                case "storeFrontID":
                foreach (Order o in listOfOrders)
                {
                    if(o.StoreFrontId == value)
                    {
                        listOfSearchedOrders.Add(o);
                    }
                }
                return listOfSearchedOrders;
                default:
                return listOfSearchedOrders;
            }
        }

        public List<StoreFront> SearchStoreFronts(string p_criteria, string p_value)
        {
            List<StoreFront> listOfStoreFronts = this.GetAllStoreFronts();
            List<StoreFront> listOfSearchedStoreFronts = new List<StoreFront>();
            switch(p_criteria)
            {
                case "id":
                    //loop through the listOfStoreFront, 
                    //if the storefront in the list match the p_value
                    //add that storefront to the listOfSearchedStoreFronts
                    foreach(StoreFront s in listOfStoreFronts)
                    {
                        if (s.Id == int.Parse(p_value))
                        {
                            listOfSearchedStoreFronts.Add(s);
                        }
                    }
                    return listOfSearchedStoreFronts;
                case "name":
                    for(int i = 0; i < listOfStoreFronts.Count; i++)
                    {
                        if (listOfStoreFronts[i].Name == p_value)
                        {
                            listOfSearchedStoreFronts.Add(listOfStoreFronts[i]);
                        }
                    }
                    return listOfSearchedStoreFronts;
                case "address":
                    foreach(StoreFront s in listOfStoreFronts)
                    {
                        if (s.Address == p_value)
                        {
                            listOfSearchedStoreFronts.Add(s);
                        }
                    }
                    return listOfSearchedStoreFronts;
                case "city":
                    foreach(StoreFront s in listOfStoreFronts)
                    {
                        if (s.City == p_value)
                        {
                            listOfSearchedStoreFronts.Add(s);
                        }
                    }
                    return listOfSearchedStoreFronts;
                case "state":
                    foreach(StoreFront s in listOfStoreFronts)
                    {
                        if (s.State == p_value)
                        {
                            listOfSearchedStoreFronts.Add(s);
                        }
                    }
                    return listOfSearchedStoreFronts;
                default:
                    return listOfSearchedStoreFronts;
            }
        }

        public Inventory UpdateInventoryQuantity(Inventory p_inv)
        {
            //Need null exception handler
            return _repo.UpdateInventoryQuantity(p_inv);
        }

        public Inventory UpdateInventoryQuantity(int p_invId, int p_purchasedQuantity)
        {
            return _repo.UpdateInventoryQuantity(p_invId, p_purchasedQuantity);
        }

    }
}