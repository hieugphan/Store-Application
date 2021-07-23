using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLogic
{
    public class DAL : IDAL
    {
        private DBContext _context;
        public DAL(DBContext p_context)
        { 
            _context = p_context;
        }

        public LineItem AddALineItem(LineItem p_lineItem)
        {
            _context.LineItems.Add(p_lineItem);
            _context.SaveChanges();
            return _context.LineItems.FirstOrDefault(li => li.OrderId == p_lineItem.OrderId
                                                                        && li.ProductId == p_lineItem.ProductId
                                                                        && li.Quantity == p_lineItem.Quantity);
        }

        public Order AddAnOrder(Order p_order)
        {
            _context.Orders.Add(p_order);
            _context.SaveChanges();
            return _context.Orders.FirstOrDefault(order => order.CustomerId == p_order.CustomerId
                                                                        && order.StoreFrontId == p_order.StoreFrontId
                                                                        && order.TotalPrice == p_order.TotalPrice);
        }

        public Customer AddCustomer(Customer p_customer)
        {
            _context.Customers.Add(p_customer);
            _context.SaveChanges();
            return _context.Customers.FirstOrDefault(cust => cust.Fname == p_customer.Fname
                                                                            && cust.Lname == p_customer.Lname
                                                                            && cust.Phone == p_customer.Phone
                                                                            && cust.Email == p_customer.Email);
        }

        public Inventory AddNewProductInventory(Inventory p_invt)
        {
            _context.Inventories.Add(p_invt);
            _context.SaveChanges();
            return _context.Inventories.FirstOrDefault(invt => invt.ProductId == p_invt.ProductId
                                                                                && invt.StoreFrontId == p_invt.StoreFrontId
                                                                                && invt.Quantity == p_invt.Quantity);
        }

        public Customer GetACustomer(int p_customerID)
        {
            return _context.Customers.FirstOrDefault(cust => cust.Id == p_customerID);
            //Find method requires the primary key
            //return _context.Customers.Find(p_customerID);
        }

        public List<Order> GetACustomerOrders(int p_customerId)
        {
            return _context.Orders.Where(order => order.CustomerId == p_customerId).Select(order => order).ToList();
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.Select(cust => cust).ToList();
        }

        public List<Inventory> GetAllInventories()
        {
            return _context.Inventories.Select(inv => inv).ToList();
        }

        public List<LineItem> GetAllLineItems()
        {
            return _context.LineItems.Select(li => li).ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Select(order => order).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Select(prod => prod).ToList();
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            return _context.StoreFronts.Select(sf => sf).ToList();
        }

        public List<Order> GetAStoreFrontOrders(int p_storeFrontId)
        {
            return _context.Orders.Where(order => order.StoreFrontId == p_storeFrontId).Select(order => order).ToList();
        }

        public List<Inventory> GetAStoreInventory(StoreFront p_storeFront)
        {
            return _context.Inventories.Where(inv => inv.StoreFrontId == p_storeFront.Id).Select(inv => inv).ToList();
        }

        public List<Inventory> GetAStoreInventory(int p_storeFrontId)
        {
            return _context.Inventories.Where(inv => inv.StoreFrontId == p_storeFrontId).Select(inv => inv).ToList();
        }


        public Inventory UpdateInventoryQuantity(Inventory p_inv)
        {
            _context.Inventories.Update(p_inv);
            _context.SaveChanges();
            //return _context.Inventories.FirstOrDefault(invt => invt == p_inv);
            return _context.Inventories.Find(p_inv.Id);
        }

        public Inventory UpdateInventoryQuantity(int p_invId, int p_purchasedQuantity)
        {
            Inventory theInventory = _context.Inventories.FirstOrDefault(inv => inv.Id == p_invId);
            theInventory.Quantity -= p_purchasedQuantity;
            _context.Inventories.Update(theInventory);
            _context.SaveChanges();
            return theInventory;

        }
    }
}