using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLogic
{
    // DBContext For Code First Approach
    public class DBContext : DbContext
    {
        //DbSet is a class that will encapsulate the models for this proj
        //Attaches a model to an entity?
        //Pluralizes the entity name
        //Allows to query without mapping the result
        public DbSet<Customer> Customers {get; set;}
        public DbSet<Inventory> Inventories {get; set;}
        public DbSet<LineItem> LineItems {get; set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<Product> Products {get; set;}
        public DbSet<StoreFront> StoreFronts {get; set;}


        // Liskov Substitution Principle
        public DBContext() : base()
        {
        }

        //DbContextOptions hold information we need for the DB
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        //Don't need connection string after the first migration ~~
        //The connection string is added to WebUI appsettings.json
        //Toggle this every time doing a new migration
        //protected override void OnConfiguring(DbContextOptionsBuilder p_options)
        //{
        //    p_options.UseNpgsql(@"Server=batyr.db.elephantsql.com;database=apdrwjmm;UserID=apdrwjmm;Password=aI7KAbVVIzhyeXHhvnAIeEQZmEschyFM");
        //}


        //This method dictates how the models is created in the DB
        protected override void OnModelCreating(ModelBuilder p_modelBuilder)
        {
            //It will auto generate the ID column for tables
            p_modelBuilder.Entity<Customer>()
                .Property(cust => cust.Id)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<Inventory>()
                .Property(invt => invt.Id)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<LineItem>()
                .Property(li => li.Id)
                .ValueGeneratedOnAdd();
            
            p_modelBuilder.Entity<Order>()
                .Property(ord => ord.Id)
                .ValueGeneratedOnAdd();
            
            p_modelBuilder.Entity<Product>()
                .Property(prod => prod.Id)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<StoreFront>()
                .Property(sf => sf.Id)
                .ValueGeneratedOnAdd();
        }
    }
}