using DataAccessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Xunit;

namespace UnitTest
{
    public class DALTest
    {
        //use this to set connection string
        private readonly DbContextOptions<DBContext> _options;

        //Constructors in unit test will always run before a test case
        //Create Inline memory database
        public DALTest()
        {
            //.UseSqlite method creates a sqlite database that is in-memory database storeed in your local storage
            _options = new DbContextOptionsBuilder<DBContext>().UseSqlite("Filename = Test.db").Options;
            this.Seed();
        }

        [Fact]
        public void GetAllCustomersShouldGetAllCustomers()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);
                List<Customer> customers;

                //Act
                customers = repo.GetAllCustomers();

                //Assert
                Assert.NotNull(customers);
                Assert.Equal(2, customers.Count);
            }

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetACustomerShouldGetACustomer(int p_custId)
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);
                Customer theCustomer = new Customer();

                //Act
                theCustomer = repo.GetACustomer(p_custId);

                //Assert
                Assert.NotNull(theCustomer);
                Assert.Equal(p_custId, theCustomer.Id);
            }
        }

        [Fact]
        public void GetAllStoreFrontsShouldGetAllStoreFronts()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);
                List<StoreFront> storeFronts;

                //Act
                storeFronts = repo.GetAllStoreFronts();

                //Assert
                Assert.NotNull(storeFronts);
                Assert.Equal(2, storeFronts.Count);
            }

        }

        [Fact]
        public void GetAllOrdersShouldGetAllOrders()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);
                List<Order> orders;

                //Act
                orders = repo.GetAllOrders();

                //Assert
                Assert.NotNull(orders);
                Assert.Equal(4, orders.Count);
            }
        }

        [Fact]
        public void GetAllLineItemsShouldGetAllLineItems()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);
                List<LineItem> lineItems;

                //Act
                lineItems = repo.GetAllLineItems();

                //Assert
                Assert.NotNull(lineItems);
                Assert.Equal(6, lineItems.Count);
            }
        }

        [Fact]
        public void GetAllProductsShouldGetAllProducts()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);
                List<Product> products;

                //Act
                products = repo.GetAllProducts();

                //Assert
                Assert.NotNull(products);
                Assert.Equal(3, products.Count);
                
            }
        }

        [Fact]
        public void GetAllInventoriesShouldGetAllInventories()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);
                List<Inventory> inventories;

                //Act
                inventories = repo.GetAllInventories();

                //Assert
                Assert.NotNull(inventories);
                Assert.Equal(6, inventories.Count);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAStoreInventoryShouldGetAStoreInventory(int p_storeFrontId)
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);
                List<Inventory> listOfTheInventoryProducts;

                //Act
                listOfTheInventoryProducts = repo.GetAStoreInventory(p_storeFrontId);

                //Assert
                Assert.NotNull(listOfTheInventoryProducts);
                Assert.Equal(3, listOfTheInventoryProducts.Count);
            }
        }

        [Theory]
        [InlineData(100)]
        [InlineData(150)]
        public void UpdateInventoryQuantityShouldUpdateQuantityOfAnInventory(int p_quantity)
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);

                //Act
                Inventory updatedInv = repo.UpdateInventoryQuantity(new Inventory()
                {
                    Id = 1,
                    StoreFrontId = 1,
                    ProductId = 1,
                    Quantity = p_quantity
                });

                //Assert
                Assert.NotNull(updatedInv);
                Assert.Equal(p_quantity, updatedInv.Quantity);
                Assert.Equal(1, updatedInv.Id);
            }
        }

        [Fact]
        public void OverloadedUpdateInventoryQuantityShouldUpdateInventory()
        {
            using (var context = new DBContext(_options))
            {
                //Arrange
                IDAL repo = new DAL(context);

                //Act
                Inventory updatedInv = repo.UpdateInventoryQuantity(1, 40);

                //Assert
                Assert.NotNull(updatedInv);
                Assert.Equal(10, updatedInv.Quantity);
            }
        }

        //Seeds or Populates the in-memory DB
        private void Seed()
        {
            //using block automatically close connection to db when done using resources
            using (var context = new DBContext(_options))
            {
                //We want to make sure our in-memory database gets deleted everytime before another test case runs it
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.AddRange(
                    new Customer
                    {
                        Id = 1,
                        Fname = "Hieu",
                        Lname = "Phan",
                        Address = "1234 Team Ct.",
                        City = "Arlington",
                        State = "TX",
                        Email = "hieu.phan@revature.net",
                        Phone = "6823319501",
                        //Orders = new List<Order>
                        //{
                        //    new Order
                        //    {
                        //        Id = 1,
                        //        CustomerId = 1,
                        //        StoreFrontId = 1,
                        //        TotalPrice = 99.99
                        //    },
                        //    new Order
                        //    {
                        //        Id = 2,
                        //        CustomerId = 1,
                        //        StoreFrontId = 2,
                        //        TotalPrice = 199.99
                        //    }
                        //}
                    },
                    new Customer
                    {
                        Id = 2,
                        Fname = "John",
                        Lname = "Smith",
                        Address = "4321 Team Ct.",
                        City = "Dallas",
                        State = "TX",
                        Email = "john.smith@revature.net",
                        Phone = "6823319502",
                    }
                );

                context.StoreFronts.AddRange(
                    new StoreFront
                    {
                        Id = 1,
                        Name = "ABC",
                        Address = "ABC Street",
                        City = "Arlington",
                        State = "TX"
                    },
                    new StoreFront
                    {
                        Id = 2,
                        Name = "XYZ",
                        Address = "XYZ Street",
                        City = "Dallas",
                        State = "TX"
                    }
                );

                context.Orders.AddRange(
                    new Order
                    {
                        Id = 1,
                        CustomerId = 1,
                        StoreFrontId = 1,
                        TotalPrice = 99.99
                    },
                    new Order
                    {
                        Id = 2,
                        CustomerId = 1,
                        StoreFrontId = 2,
                        TotalPrice = 199.99
                    },
                    new Order
                    {
                        Id = 3,
                        CustomerId = 2,
                        StoreFrontId = 1,
                        TotalPrice = 299.99
                    },
                    new Order
                    {
                        Id = 4,
                        CustomerId = 2,
                        StoreFrontId = 2,
                        TotalPrice = 399.99
                    }
                );

                context.LineItems.AddRange(
                    new LineItem
                    {
                        Id = 1,
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 1
                    },
                    new LineItem
                    {
                        Id = 2,
                        OrderId = 2,
                        ProductId = 1,
                        Quantity = 1
                    },
                    new LineItem
                    {
                        Id = 3,
                        OrderId = 2,
                        ProductId = 2,
                        Quantity = 1

                    },
                    new LineItem
                    {
                        Id = 4,
                        OrderId = 3,
                        ProductId = 3,
                        Quantity = 1
                    },
                    new LineItem
                    {
                        Id = 5,
                        OrderId = 4,
                        ProductId = 2,
                        Quantity = 1
                    },
                    new LineItem
                    {
                        Id = 6,
                        OrderId = 4,
                        ProductId = 3,
                        Quantity = 1
                    }
                );

                context.Products.AddRange(
                    new Product
                    {
                        Id = 1,
                        Name = "Prod99.99",
                        Price = 99.99
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Prod100.00",
                        Price = 100.00
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Prod299.99",
                        Price = 299.99
                    }
                );

                context.Inventories.AddRange(
                    new Inventory
                    {
                        Id = 1,
                        StoreFrontId = 1,
                        ProductId = 1,
                        Quantity = 50
                    },
                    new Inventory
                    {
                        Id = 2,
                        StoreFrontId = 1,
                        ProductId = 2,
                        Quantity = 50
                    },
                    new Inventory
                    {
                        Id = 3,
                        StoreFrontId = 1,
                        ProductId = 3,
                        Quantity = 50
                    },
                    new Inventory
                    {
                        Id = 4,
                        StoreFrontId = 2,
                        ProductId = 1,
                        Quantity = 100
                    },
                    new Inventory
                    {
                        Id = 5,
                        StoreFrontId = 2,
                        ProductId = 2,
                        Quantity = 100
                    },
                    new Inventory
                    {
                        Id = 6,
                        StoreFrontId = 2,
                        ProductId = 3,
                        Quantity = 100
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
