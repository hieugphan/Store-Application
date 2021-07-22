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

        public List<Inventory> GetAStoreInventory(StoreFront p_storeFront)
        {
            return _repo.GetAStoreInventory(p_storeFront);
        }

        public List<Inventory> GetAStoreInventory(int p_storeFrontId)
        {
            return _repo.GetAStoreInventory(p_storeFrontId);
        }

        public void PlaceOrder(Customer p_customer)
        {
            StoreFront theStore = new StoreFront();
            List<Inventory> theInventory = new List<Inventory>();
            List<Product> listOfProducts = this.GetAllProducts();
            Customer theCustomer = p_customer;
            int storeId;
            bool storeFlag = true;
            bool productFlag = true;
            bool quantityFlag = true;
            //Find a store
            System.Console.Write("Enter A Store ID: ");
            while(storeFlag)
            {
                try
                {
                    storeId = int.Parse(Console.ReadLine());
                    theStore = this.GetAStore(storeId);
                    if (theStore.Id != storeId)
                    {
                        System.Console.WriteLine("Store Front Not Found");
                        System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                        string string0 = Console.ReadLine().ToLower();
                        switch(string0)
                        {
                            case "quit":
                            storeFlag = false;
                            productFlag = false;
                            quantityFlag = false;
                            break;
                            default:
                            System.Console.WriteLine("Re-enter The Store ID:");
                            break;
                        }

                    }
                    else if (theStore.Id == storeId)
                    {
                        //Populates the current store's inventory
                        theInventory.Clear();
                        List<Inventory> listOfInventories = this.GetAllInventories();
                        foreach (Inventory inv in listOfInventories)
                        {
                            if(inv.StoreFrontId == theStore.Id)
                            {
                                theInventory.Add(inv);
                            }
                        }
                        //Displays the current store's inventory
                        System.Console.Clear();
                        System.Console.WriteLine("-----------------------------------------------------------------------");
                        System.Console.WriteLine("Store " + theStore.Name + " Inventory");
                        for (int i = 0; i < theInventory.Count; i++)
                        {
                            System.Console.WriteLine("Product ID: [" + theInventory[i].ProductId +
                                                        "] ||| Product Name: " + listOfProducts[theInventory[i].ProductId - 1].Name +
                                                        " ||| Product Price: $" + string.Format("{0:0.00}",(listOfProducts[theInventory[i].ProductId - 1].Price)) +
                                                        " ||| Inventory Quantity: " + theInventory[i].Quantity);
                        }
                        System.Console.WriteLine("-----------------------------------------------------------------------");
                        //Start the order                                               
                        int productId;
                        double total = 0.0;
                        List<LineItem> lineItems = new List<LineItem>();
                        //Creates a list of available product ID of the current store
                        List<int> availableProductId = new List<int>();
                        foreach (Inventory inv in theInventory)
                        {
                            availableProductId.Add(inv.ProductId);
                        }
                        System.Console.WriteLine("What Product Would You Like To Order?");
                        System.Console.Write("Use Product ID To Select Product: ");
                        while(productFlag)
                        {
                            try
                            {
                                productId = int.Parse(Console.ReadLine());
                                if(!availableProductId.Contains(productId))
                                {
                                    System.Console.WriteLine("Product ID Not Found");
                                    System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                                    string string0 = Console.ReadLine().ToLower();
                                    switch(string0)
                                    {
                                        case "quit":
                                        storeFlag = false;
                                        productFlag = false;
                                        quantityFlag = false;
                                        break;
                                        default:
                                        System.Console.WriteLine("Re-enter Product ID:");
                                        break;
                                    }
                                }
                                else if(availableProductId.Contains(productId))
                                {
                                    //Display the selected product
                                    LineItem lineItem = new LineItem();
                                    Product theProduct = new Product();
                                    Inventory theProductInventory = new Inventory();
                                    foreach(Product p in listOfProducts)
                                    {
                                        if(p.Id == productId)
                                        {
                                            theProduct = p;
                                        }
                                    }

                                    foreach (Inventory inv in theInventory)
                                    {
                                        if (inv.ProductId == productId)
                                        {
                                            theProductInventory = inv;
                                        }
                                    }

                                    System.Console.WriteLine(theProduct.ToString()+ " Available Quantity: " + theProductInventory.Quantity);
                                    System.Console.Write("Enter Your Quantity: ");
                                    quantityFlag = true;
                                    while(quantityFlag)
                                    {
                                        try
                                        {
                                            int quantity = int.Parse(Console.ReadLine());
                                            if (quantity <= 0 || quantity > theProductInventory.Quantity)
                                            {
                                                System.Console.WriteLine("Quantity Must Be Greater Than 0 and Less Than The Available Quantity");
                                                System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                                                string string0 = Console.ReadLine().ToLower();
                                                switch(string0)
                                                {
                                                    case "quit":
                                                    storeFlag = false;
                                                    productFlag = false;
                                                    quantityFlag = false;
                                                    break;
                                                    default:
                                                    System.Console.WriteLine("Re-enter Product Quantity:");
                                                    break;
                                                }
                                            }
                                            else if (theProductInventory.Quantity == 0)
                                            {
                                                System.Console.WriteLine("Out Of Stock");
                                                System.Console.Write("Select Another Product [ID]: ");
                                                quantityFlag = false;
                                            }
                                            else if(quantity > 0 && quantity <= theProductInventory.Quantity)
                                            {
                                                //Create a new LineItem and add to a list
                                                lineItem.ProductId = productId;
                                                lineItem.Quantity = quantity;
                                                lineItems.Add(lineItem);
                                                total += (theProduct.Price * quantity);
            
                                                System.Console.Clear();
                                                System.Console.WriteLine("-----------------------------------------------------------------------");
                                                System.Console.WriteLine("Product Name: " + theProduct.Name +
                                                                        " ||| Product Price: $" + theProduct.Price +
                                                                        " ||| Purchasing Quantity: " + quantity +
                                                                        " ||| Line Total: $" + (theProduct.Price*quantity));

                                                quantityFlag = false;
                                                System.Console.WriteLine("-----------------------------------------------------------------------");
                                                System.Console.WriteLine("You have " + lineItems.Count + " line item(s)");
                                                System.Console.WriteLine("Your Total Is: $" + total);
                                                System.Console.WriteLine("-----------------------------------------------------------------------");
                                                System.Console.WriteLine("What Would You Do Next?");
                                                System.Console.WriteLine("[1] Add More Product");
                                                System.Console.WriteLine("[2] Empty Cart");
                                                System.Console.WriteLine("[3] Check Out");
                                                System.Console.WriteLine("[0] Exit");
                                                System.Console.Write("Enter Your Choice: ");
                                                string string1 = Console.ReadLine();
                                                switch(string1)
                                                {
                                                    case "1":
                                                        System.Console.Clear();
                                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                                        System.Console.WriteLine("Store " + theStore.Name + " Inventory");
                                                        for (int i = 0; i < theInventory.Count; i++)
                                                        {
                                                            System.Console.WriteLine("Product ID: [" + theInventory[i].ProductId +
                                                                                        "] ||| Product Name: " + listOfProducts[theInventory[i].ProductId - 1].Name +
                                                                                        " ||| Product Price: $" + listOfProducts[theInventory[i].ProductId - 1].Price +
                                                                                        " ||| Inventory Quantity: " + theInventory[i].Quantity);
                                                        }
                                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                                        System.Console.WriteLine("Use Product ID To Select Product :");
                                                        break;
                                                    case "2":
                                                        lineItems.Clear();
                                                        total = 0.0;
                                                        System.Console.Clear();
                                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                                        System.Console.WriteLine("You have " + lineItems.Count + " line item(s)");
                                                        System.Console.WriteLine("Your Total Is: $" + total);
                                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                                        System.Console.WriteLine("Store " + theStore.Name + " Inventory");
                                                        for (int i = 0; i < theInventory.Count; i++)
                                                        {
                                                            System.Console.WriteLine("Product ID: " + theInventory[i].ProductId +
                                                                                        " ||| Product Name: " + listOfProducts[theInventory[i].ProductId - 1].Name +
                                                                                        " ||| Product Price: $" + listOfProducts[theInventory[i].ProductId - 1].Price +
                                                                                        " ||| Inventory Quantity: " + theInventory[i].Quantity);
                                                        }
                                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                                        System.Console.WriteLine("Use Product ID To Select Product :");
                                                        break;
                                                    case "3":
                                                        storeFlag = false;
                                                        productFlag = false;
                                                        quantityFlag = false;
                                                        //Validate that the current store inv can satisfy the order??????????

                                                        //Create and write the new order to DB to get its auto generated orderID
                                                        Order newOrder = new Order();
                                                        newOrder.CustomerId = theCustomer.Id;
                                                        newOrder.StoreFrontId = theStore.Id;
                                                        newOrder.TotalPrice = total;
                                                        newOrder = this.AddAnOrder(newOrder);
                                                        //Add the orderID into all lineItems
                                                        //Write all lineItems in the list to the DB
                                                        foreach(LineItem li in lineItems)
                                                        {
                                                            li.OrderId = newOrder.Id;
                                                            this.AddALineItem(li);
                                                            //adjust the store inv quantity
                                                            foreach (Inventory inv in theInventory)
                                                            {
                                                                if(inv.ProductId == li.ProductId)
                                                                {
                                                                    inv.Quantity -= li.Quantity;
                                                                    this.UpdateInventoryQuantity(inv);
                                                                }
                                                            }
                                                        }
                                                        //Print The Order Detail
                                                        Console.Clear();
                                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                                        System.Console.WriteLine("Order Submitted Successfully!!!");
                                                        System.Console.WriteLine("Order Detail:");
                                                        foreach(LineItem li in lineItems)
                                                        {
                                                            foreach(Product p in listOfProducts)
                                                            {
                                                                if(p.Id == li.ProductId)
                                                                {
                                                                    System.Console.WriteLine("Product Name: " + p.Name + 
                                                                                    " ||| Price: $" + p.Price + 
                                                                                    " ||| Purchased Quantity: " + li.Quantity +
                                                                                    " ||| Line Total: $" + (p.Price*li.Quantity));
                                                                }
                                                            }
                                                        }
                                                        System.Console.WriteLine("-----------------------------------------------------------------------Order Total: $" + newOrder.TotalPrice);
                                                        System.Console.Write("Enter To Continue");
                                                        System.Console.ReadLine();
                                                        lineItems.Clear();
                                                    break;
                                                    case "0":
                                                        lineItems.Clear();
                                                        storeFlag = false;
                                                        productFlag = false;
                                                        quantityFlag = false;
                                                        break;
                                                }
                                            }
                                        }
                                        catch (System.Exception e)
                                        {
                                            System.Console.WriteLine(e);
                                            System.Console.WriteLine("Input was not valid - Quantity Must Be A Number");
                                            System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                                            string string0 = Console.ReadLine().ToLower();
                                            switch(string0)
                                            {
                                                case "quit":
                                                storeFlag = false;
                                                productFlag = false;
                                                quantityFlag = false;
                                                break;
                                                default:
                                                System.Console.WriteLine("Re-enter Product Quantity: ");
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
                            catch (System.Exception)
                            {
                                System.Console.WriteLine("Input Was Not Valid - Product Id Must Be A Number");
                                System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                                string string0 = Console.ReadLine().ToLower();
                                switch(string0)
                                {
                                    case "quit":
                                    storeFlag = false;
                                    productFlag = false;
                                    quantityFlag = false;
                                    break;
                                    default:
                                    System.Console.WriteLine("Re-enter Product ID: ");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (System.Exception )
                {
                    System.Console.WriteLine("Input Was Not Valid - Store Id Must Be A Number");
                    System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                    string string0 = Console.ReadLine().ToLower();
                    switch(string0)
                    {
                        case "quit":
                        storeFlag = false;
                        productFlag = false;
                        quantityFlag = false;
                        break;
                        default:
                        System.Console.WriteLine("Re-enter The Store ID: ");
                        break;
                    }
                }
            } 
        }

        public void ReplenishAndDisplayInventory(StoreFront p_theStore, List<Product> listOfProducts)
        {
            List<int> listOfProductIDs = new List<int>();
            foreach (Product p in listOfProducts)
            {
                listOfProductIDs.Add(p.Id);
            }
            List<Inventory> theInventories = this.GetAStoreInventory(p_theStore);
            Inventory newInv = new Inventory();
            bool productFlag = true;
            bool quantityFlag = true;
            int productId;
            System.Console.Write("Enter Product [ID]: ");
            while(productFlag)
            {
                try
                {
                    string check = "addNewProduct";
                    productId = int.Parse(Console.ReadLine());
                    if(!listOfProductIDs.Contains(productId))
                    {
                        System.Console.WriteLine("Product ID Was Not Valid");
                        System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                        string string0 = Console.ReadLine().ToLower();
                        switch(string0)
                        {
                            case "quit":
                            productFlag = false;
                            quantityFlag = false;
                            break;
                            default:
                            System.Console.Write("Re-enter Product [ID]: ");
                            break;
                        }
                    }
                    else if(listOfProductIDs.Contains(productId))
                    {
                        foreach(Inventory inv in theInventories)
                        {
                            if(inv.ProductId == productId)
                            {
                                check = "replenish";
                            }
                        }
                        System.Console.Write("Enter Product Quantity: ");
                        quantityFlag = true;
                        while(quantityFlag)
                        {
                            try
                            {
                                int quantity = int.Parse(Console.ReadLine());
                                switch(check)
                                {
                                    case "addNewProduct":
                                        newInv.ProductId = productId;
                                        newInv.StoreFrontId = p_theStore.Id;
                                        newInv.Quantity = quantity;
                                        this.AddNewProductInventory(newInv);
                                        System.Console.Clear();
                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                        System.Console.WriteLine("Product ID[" + productId + "] ||| Quantity: " + quantity +" (Added)");
                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                        System.Console.WriteLine("Store " + p_theStore.Name + " Inventory");
                                        // Reset theInventories after adding new product
                                        theInventories = new List<Inventory>();
                                        List<Inventory> listOfInventories = this.GetAllInventories();
                                        foreach (Inventory inv in listOfInventories)
                                        {
                                            if(inv.StoreFrontId == p_theStore.Id)
                                            {
                                                theInventories.Add(inv);
                                            }
                                        }
                                        for (int i = 0; i < theInventories.Count; i++)
                                        {
                                            System.Console.WriteLine("Product ID: [" + theInventories[i].ProductId +
                                                                        "] ||| Product Name: " + listOfProducts[theInventories[i].ProductId - 1].Name +
                                                                        " ||| Product Price: $" + listOfProducts[theInventories[i].ProductId - 1].Price +
                                                                        " ||| Inventory Quantity: " + theInventories[i].Quantity);
                                        }
                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                        break;
                                    case "replenish":                                        
                                        newInv.ProductId = productId;
                                        newInv.StoreFrontId = p_theStore.Id;
                                        newInv.Quantity += quantity;
                                        this.UpdateInventoryQuantity(newInv);
                                        System.Console.Clear();
                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                        System.Console.WriteLine("Product ID[" + productId + "] ||| Quantity: " + quantity +" (Replenished)");
                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                        System.Console.WriteLine("Store " + p_theStore.Name + " Inventory");
                                        // Reset theInventories after adding new product
                                        theInventories = new List<Inventory>();
                                        listOfInventories = this.GetAllInventories();
                                        foreach (Inventory inv in listOfInventories)
                                        {
                                            if(inv.StoreFrontId == p_theStore.Id)
                                            {
                                                theInventories.Add(inv);
                                            }
                                        }
                                        // Sort the List base on its productID
                                        theInventories.Sort((x,y) => x.ProductId.CompareTo(y.ProductId));
                                        for (int i = 0; i < theInventories.Count; i++)
                                        {
                                            System.Console.WriteLine("Product ID: [" + theInventories[i].ProductId +
                                                                        "] ||| Product Name: " + listOfProducts[theInventories[i].ProductId - 1].Name +
                                                                        " ||| Product Price: $" + listOfProducts[theInventories[i].ProductId - 1].Price +
                                                                        " ||| Inventory Quantity: " + theInventories[i].Quantity);
                                        }
                                        System.Console.WriteLine("-----------------------------------------------------------------------");
                                        break;
                                }
                            
                            }
                            catch(System.Exception)
                            {
                                System.Console.WriteLine("Input Was Not Valid - Quantity Must Be A Number");
                                System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                                string string1 = Console.ReadLine().ToLower();
                                switch(string1)
                                {
                                    case "quit":
                                    productFlag = false;
                                    quantityFlag = false;
                                    break;
                                    default:
                                    System.Console.Write("Re-enter Quantity: ");
                                    break;
                                } 
                            }
                            System.Console.WriteLine("What Would You Like To Do?");
                            System.Console.WriteLine("[1] Replenish Inventory");
                            System.Console.WriteLine("[0] Go Back");
                            System.Console.Write("Enter Your Choice: ");
                            string string0 = Console.ReadLine().ToLower();
                            switch(string0)
                            {
                                case "1":
                                System.Console.Write("Enter Product [ID]: ");
                                quantityFlag = false;
                                break;
                                case "0":
                                productFlag = false;
                                quantityFlag = false;
                                break;
                                default:
                                productFlag = false;
                                quantityFlag = false;
                                break;
                            } 
                        }
                    }
                }
                catch(System.Exception)
                {
                    System.Console.WriteLine("Input Was Not Valid - Product ID Must Be A Number");
                    System.Console.WriteLine("Product ID Not Found");
                    System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                    string string0 = Console.ReadLine().ToLower();
                    switch(string0)
                    {
                        case "quit":
                        productFlag = false;
                        quantityFlag = false;
                        break;
                        default:
                        System.Console.Write("Re-enter Product ID: ");
                        break;
                    } 
                }
            }
        }

        public void SearchAndDisplayCustomer(string p_criteria)
        {
            List<Customer> listOfSearchedCustomer = new List<Customer>();
            bool repeat = true;
            string value;
            while(repeat)
            {
                try
                {
                    value = Console.ReadLine().ToUpper();
                    // listOfSearchedCustomer = _customerBL.SearchCustomers(criteria, value);
                    listOfSearchedCustomer = this.SearchCustomers(p_criteria, value);
                    if (listOfSearchedCustomer.Count == 0)
                    {
                        System.Console.WriteLine("No Results.");
                        System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                        string string0 = Console.ReadLine().ToLower();
                        switch(string0)
                        {
                            case "quit":
                            repeat = false;
                            break;
                            default:
                            System.Console.WriteLine("Re-enter Your " + p_criteria + " Search Value: ");
                            break;
                        }
                    }
                    else
                    {
                        System.Console.Clear();
                        System.Console.WriteLine("-----------------------------------------------------------------------");
                        System.Console.WriteLine("List of Results");
                        foreach(Customer c in listOfSearchedCustomer)
                        {
                            System.Console.WriteLine(c.ToString());
                        }
                        System.Console.WriteLine("-----------------------------------------------------------------------");
                        System.Console.Write("Enter To Continue");
                        System.Console.ReadLine();
                        repeat = false;
                    }
                }
                catch(System.Exception)
                {
                    System.Console.WriteLine("Input Was Not Valid");
                    System.Console.WriteLine("Customer Not Found");
                    System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                    string string0 = Console.ReadLine().ToLower();
                    switch(string0)
                    {
                        case "quit":
                        repeat = false;
                        break;
                        default:
                        System.Console.WriteLine("Re-enter Your " + p_criteria + " Search Value: ");
                        break;
                    }
                }
            }
        }

        public void SearchAndDisplayOrder(string p_criteria)
        {
            List<LineItem> theOrderLineItems = new List<LineItem>();
            List<Product> listOfProducts = this.GetAllProducts();
            List<Order> listOfSearchedOrders = new List<Order>();
            bool repeat = true;
            string value;
            while(repeat)
            {
                try
                {
                    value = Console.ReadLine();
                    listOfSearchedOrders = this.SearchOrders(p_criteria, value);
                    if (listOfSearchedOrders.Count == 0)
                    {
                        System.Console.WriteLine("No Results.");
                        System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                        string string0 = Console.ReadLine().ToLower();
                        switch(string0)
                        {
                            case "quit":
                            repeat = false;
                            break;
                            default:
                            System.Console.WriteLine("Re-enter Your " + p_criteria + " Search Value: ");
                            break;
                        }
                    }
                    else
                    {
                        System.Console.Clear();
                        System.Console.WriteLine("-----------------------------------------------------------------------");
                        foreach(Order o in listOfSearchedOrders)
                        {
                            System.Console.WriteLine(o.ToString());
                        }
                        System.Console.WriteLine("-----------------------------------------------------------------------");
                        repeat = false;
                        //if for displaying an order detail
                        if (p_criteria == "id")
                        {
                            double total = 0.0;
                            theOrderLineItems = this.GetAnOrderLineItems(int.Parse(value)); 
                            System.Console.WriteLine("Order Details");
                            foreach(LineItem li in theOrderLineItems)
                            {
                                foreach(Product p in listOfProducts)
                                {
                                    if(p.Id == li.ProductId)
                                    {
                                        System.Console.WriteLine("Product Name: " + p.Name + 
                                                        " ||| Price: $" + string.Format("{0:0.00}",p.Price) + 
                                                        " ||| Purchased Quantity: " + li.Quantity +
                                                        " ||| Line Total: $" +  string.Format("{0:0.00}",(p.Price*li.Quantity)));
                                        total += (p.Price*li.Quantity);
                                    }
                                }
                            }
                            System.Console.WriteLine("-----------------------------------------------------------------------Order Total: $" + string.Format("{0:0.00}",total));
                        }
                        System.Console.Write("Enter To Continue");
                        System.Console.ReadLine();                       
                    }
                }
                catch(System.Exception)
                {
                    System.Console.WriteLine("Input Was Not Valid");
                    System.Console.WriteLine("Order Not Found");
                    System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                    string string0 = Console.ReadLine().ToLower();
                    switch(string0)
                    {
                        case "quit":
                        repeat = false;
                        break;
                        default:
                        System.Console.WriteLine("Re-enter Your " + p_criteria + " Search Value: ");
                        break;
                    }
                }
            }
        }

        public void SearchAndDisplayStoreFront(string p_criteria)
        {
            List<StoreFront> ListOfSearchedStoreFront = new List<StoreFront>();
            bool repeat = true;
            string value;
            while(repeat)
            {
                try
                {
                    value = Console.ReadLine().ToUpper();
                    ListOfSearchedStoreFront = this.SearchStoreFronts(p_criteria, value);
                    if (ListOfSearchedStoreFront.Count == 0)
                    {
                        System.Console.WriteLine("No Results.");
                        System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                        string string0 = Console.ReadLine().ToLower();
                        switch(string0)
                        {
                            case "quit":
                            repeat = false;
                            break;
                            default:
                            System.Console.WriteLine("Re-enter Your " + p_criteria + " Search Value: ");
                            break;
                        }
                    }
                    else
                    {
                        System.Console.Clear();
                        System.Console.WriteLine("-----------------------------------------------------------------------");
                        System.Console.WriteLine("List of Results");
                        foreach(StoreFront sf in ListOfSearchedStoreFront)
                        {
                            System.Console.WriteLine(sf.ToString());
                        }
                        System.Console.WriteLine("-----------------------------------------------------------------------");
                        System.Console.Write("Enter To Continue");
                        System.Console.ReadLine();
                        repeat = false;
                    }
                }
                catch(System.Exception)
                {
                    System.Console.WriteLine("Input Was Not Valid");
                    System.Console.WriteLine("StoreFront Not Found");
                    System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                    string string0 = Console.ReadLine().ToLower();
                    switch(string0)
                    {
                        case "quit":
                        repeat = false;
                        break;
                        default:
                        System.Console.WriteLine("Re-enter Your \"" + p_criteria.ToUpper() + "\" Search Value: ");
                        break;
                    }
                }
            }
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

        public void ViewOrderHistory(Customer p_customer)
        {
            //Get all orders with cust ID            
            List<Order> listOfOrders = new List<Order>();
            listOfOrders = this.GetAllOrders();
            List<Order> myListOfOrders = new List<Order>();
            List<Product> listOfProducts = this.GetAllProducts();
            System.Console.Clear();
            System.Console.WriteLine("-----------------------------------------------------------------------");
            foreach(Order o in listOfOrders)
            {
                if(o.CustomerId == p_customer.Id)
                {
                    myListOfOrders.Add(o);
                }
            }
            if (myListOfOrders.Count == 0)
            {
                System.Console.WriteLine("You Have No Orders");
                System.Console.WriteLine("-----------------------------------------------------------------------");
                System.Console.Write("Enter To Go Back");
                System.Console.ReadLine();
            }
            else if (myListOfOrders.Count > 0)
            {
                foreach (Order o in myListOfOrders)
                {
                    System.Console.WriteLine(o.ToString());
                }
                System.Console.WriteLine("-----------------------------------------------------------------------");
                System.Console.WriteLine("What Would You Like To Do?");
                System.Console.WriteLine("[1] View An Order");
                System.Console.WriteLine("[0] Go Back");
                System.Console.Write("Enter Your Choice: ");
                string string0 = Console.ReadLine();
                switch(string0)
                {
                    case "1":
                        bool orderFlag = true;
                        double total = 0.0;
                        List<LineItem> lineItems = new List<LineItem>();
                        //Get LineItem with OrderID
                        System.Console.Write("Enter Order ID: ");
                        while(orderFlag)
                        {
                            try
                            {
                                int orderId = int.Parse(Console.ReadLine());
                                foreach(Order o in myListOfOrders)
                                {
                                    if(o.Id == orderId)
                                    {
                                        lineItems = this.GetAnOrderLineItems(o);
                                        total = o.TotalPrice;
                                    }
                                }
                                System.Console.Clear();
                                System.Console.WriteLine("-----------------------------------------------------------------------");
                                foreach(LineItem li in lineItems)
                                {
                                    foreach(Product p in listOfProducts)
                                    {
                                        if(p.Id == li.ProductId)
                                        {
                                            System.Console.WriteLine("Product Name: " + p.Name + 
                                                            " ||| Price: $" + string.Format("{0:0.00}",p.Price) + 
                                                            " ||| Purchased Quantity: " + li.Quantity +
                                                            " ||| Line Total: $" +  string.Format("{0:0.00}",(p.Price*li.Quantity)));
                                        }
                                    }
                                }
                                System.Console.WriteLine("-----------------------------------------------------------------------Order Total: $" + string.Format("{0:0.00}",total));
                                System.Console.Write("Enter To Continue");
                                System.Console.ReadLine();
                                orderFlag = false;
                            }
                            catch(System.Exception)
                            {
                                System.Console.WriteLine("Input Was Not Valid - Order ID Must Be A Number");
                                System.Console.WriteLine("Enter To Continue or 'quit' To Quit");
                                string string1 = Console.ReadLine().ToLower();
                                switch(string1)
                                {
                                    case "quit":
                                    orderFlag = false;
                                    break;
                                    default:
                                    System.Console.WriteLine("Re-enter Order ID: ");
                                    break;
                                }
                            }
                        }
                        break;
                    case "0":
                        break;
                        default:
                        System.Console.WriteLine("Input Was Not Valid");
                        System.Console.WriteLine("Enter To Go Back");
                        Console.ReadLine();
                        break;
                }
            }
        }
    
    
    }
}