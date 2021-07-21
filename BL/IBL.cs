using System;
using System.Collections.Generic;
using Models;

namespace BusinessLogic
{
    /// <summary>
    /// Handles all the business logic for the app
    /// </summary>
    public interface IBL
    {
        /// <summary>
        /// Gets a list of all customers from DB-Customers
        /// </summary>
        /// <returns>list of customers</returns>
        List<Customer> GetAllCustomers();

        /// <summary>
        /// Gets a customer using the customer's id from DB-Customers
        /// </summary>
        /// <param name="p_customerID">customer ID</param>
        /// <returns>obj customers</returns>
        Customer GetACustomer(int p_customerID);

        /// <summary>
        /// Adds obj Customers to DB-Customers
        /// </summary>
        /// <param name="p_customer"></param>
        /// <returns>obj customers</returns>
        Customer AddCustomer(Customer p_customer);

        /// <summary>
        /// Gets a list of customers that matches a criteria and its value from DB-Customers
        /// </summary>
        /// <returns>list of customers, empty or not</returns>
        List<Customer> SearchCustomers(string p_criteria, string p_value);

        /// <summary>
        /// Searchs and Displays any customers that match p_criteria
        /// </summary>
        /// <param name="p_criteria"></param>
        void SearchAndDisplayCustomer(string p_criteria);

        /// <summary>
        /// Places orders and writes to DB
        /// Affects many tables
        /// </summary>
        /// <param name="p_customer"></param>
        /// <param name="p_BL"></param>
        void PlaceOrder(Customer p_customer);

        /// <summary>
        /// Gets a list of all inventories from DB-Inventories
        /// </summary>
        /// <returns>list of inventories</returns>
        List<Inventory> GetAllInventories();

        /// <summary>
        /// Gets a specific store's inventory from DB-Inventories
        /// </summary>
        /// <param name="p_storeFront">storeFront obj</param>
        /// <returns>list of inventories</returns>
        List<Inventory> GetAStoreInventory (StoreFront p_storeFront);

        /// <summary>
        /// Gets a specific store's inventory from DB-Inventories
        /// </summary>
        /// <param name="p_storeFrontID">sf ID</param>
        /// <returns>list of inventories</returns>
        List<Inventory> GetAStoreInventory(int p_storeFrontId);

        /// <summary>
        /// Updates current quantity of an inventory (a product of a specific inventory) to p_quantity to DB-Inventories
        /// </summary>
        /// <param name="p_inv"></param>
        /// <param name="p_quantity"></param>
        /// <returns>the updated inventories obj</returns>
        Inventory UpdateInventoryQuantity(Inventory p_inv);

        /// <summary>
        /// Creates a record of a newly added product to DB-Inventories
        /// </summary>
        /// <param name="p_invt"></param>
        /// <returns></returns>
        Inventory AddNewProductInventory(Inventory p_invt);

        /// <summary>
        /// AddNewProductInventory or ReplenishInventory and Displays
        /// </summary>
        /// <param name="p_theStore"></param>
        /// <param name="listOfProducts"></param>
        void ReplenishAndDisplayInventory(StoreFront p_theStore, List<Product> listOfProducts);

        /// <summary>
        /// Gets all line items from DB-LineItems
        /// </summary>
        /// <returns>list of all line items</returns>
        List<LineItem> GetAllLineItems();

        /// <summary>
        /// Gets all line items of an Orders obj from DB-LineItems
        /// </summary>
        /// <param name="p_order"></param>
        /// <returns>list of the order's line items</returns>
        List<LineItem> GetAnOrderLineItems(Order p_order);

        /// <summary>
        /// Gets all line items of an Orders obj using the order's ID from DB-LineItems
        /// </summary>
        /// <param name="p_orderID"></param>
        /// <returns></returns>
        List<LineItem> GetAnOrderLineItems(int p_orderID);

        /// <summary>
        /// Adds a line item to the DB-LineItems
        /// </summary>
        /// <param name="p_lineItem"></param>
        /// <returns></returns>
        LineItem AddALineItem(LineItem p_lineItem);

        /// <summary>
        /// Gets all orders from DB-Orders
        /// </summary>
        /// <returns>list of all orders</returns>
        List<Order> GetAllOrders();

        /// <summary>
        /// Adds an order to DB-Orders
        /// </summary>
        /// <param name="p_order"></param>
        /// <returns>the added order with its new ID</returns>
        Order AddAnOrder(Order p_order);

        /// <summary>
        /// Searches for orders that match a criteria and value from DB-Orders
        /// </summary>
        /// <param name="p_criteria"></param>
        /// <param name="p_value"></param>
        /// <returns>list of Orders, empty or not</returns>
        List<Order> SearchOrders(string p_criteria, string p_value);

        /// <summary>
        /// Searches and Displays in a formatted way
        /// </summary>
        /// <param name="p_criteria"></param>
        /// <param name="_lineItemBL"></param>
        /// <param name="_productBL"></param>
        void SearchAndDisplayOrder(string p_criteria);

        /// <summary>
        /// Views order history in a formatted way
        /// </summary>
        /// <param name="p_customer"></param>
        /// <param name="p_lineItemBL"></param>
        /// <param name="p_prodBL"></param>
        void ViewOrderHistory(Customer p_customer);

        /// <summary>
        /// Gets a list of all products from DB-Products
        /// </summary>
        /// <returns>list of all products</returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Gets a list of all store fronts from DB-StoreFronts
        /// </summary>
        /// <returns>list of all storefronts</returns>
        List<StoreFront> GetAllStoreFronts();

        /// <summary>
        /// Gets a store using input id from DB-StoreFronts
        /// </summary>
        /// <param name="p_id"></param>
        /// <returns>a StoreFronts obj</returns>
        StoreFront GetAStore(int p_id);

        /// <summary>
        /// Gets a list of all store fronts that matches search criteria and value from DB-StoreFronts
        /// </summary>
        /// <param name="p_criteria"></param>
        /// <param name="p_value"></param>
        /// <returns></returns>
        List<StoreFront> SearchStoreFronts(string p_criteria, string p_value);

        /// <summary>
        /// Searches and Displays store fronts in a formatted way
        /// </summary>
        /// <param name="p_criteria"></param>
        void SearchAndDisplayStoreFront(string p_criteria);
    }
}
