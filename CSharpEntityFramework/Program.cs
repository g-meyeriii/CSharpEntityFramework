using CSharpEntityFramworkLibrary;
using CSharpEntityFramworkLibrary.Models;
using System;
using System.Linq;

namespace CSharpEntityFramework {
    class Program {
       // private static object[] custPk;

        static void Main(string[] args) {
            var context = new AppDbContext();
           // UpdateCustomers(context);
            //AddCustomer(context);

            // GetCustomerByPk(context);
            //UpdateCustomers(context);
            //DeleteCustomer(context);
           // GetAllCustomers(context);
            //AddOrder(context);
           AddProducts(context);
           
            Console.WriteLine($"Avg price is {context.Products.Average(x => x.Price)}");

            var top2 = context.Products.Where(x => x.Id > 7).ToList();

            var actCust = context.Customers.Where(x => x.Active).ToList();
        }
        static void UpdateCustomerSales(AppDbContext context) { //know  this syntax
            var CusOrdJoin = from c in context.Customers //if the var is not referenced not execute
                             join o in context.Orders
                             on c.Id equals o.CustomerId
                             where c.Id == 5
                             select new {
                                 Amount = o.Amount,
                                 Customer = c.Name,
                                 Order = o.Description
                             };
            var orderTotal = CusOrdJoin.Sum(c => c.Amount);
            var cust = context.Customers.Find(5);
            cust.Sales = orderTotal;
            context.SaveChanges();
        }


        static void DeleteCustomer(AppDbContext context) {
            var keyToDelete = 10;
            var cust = context.Customers.Find(keyToDelete);
            if (cust == null) throw new Exception("Customer not fount");
            context.Customers.Remove(cust);
            var rowsAffected = context.SaveChanges();
            if (rowsAffected != 1) throw new Exception("Delete Failed");
        }

        static void UpdateCustomers(AppDbContext context) {
            var custPk = 4;
            var cust = context.Customers.Find(custPk);
            if (cust == null) throw new Exception("Customer not found");
            cust.Name = "Amazon Inc.";
        }

        static void GetCustomerByPk(AppDbContext context) {
            var custPk = 3;
            var cust = context.Customers.Find(custPk);
            if (cust == null) throw new Exception("Customer not found");
            Console.WriteLine(cust);
            var rowsAffected = context.SaveChanges();
            if (rowsAffected != 1) throw new Exception("Failed to update");
            Console.WriteLine("Update successful");
        }
        static void GetAllCustomers(AppDbContext context) {
            var custs = context.Customers.ToList();
            foreach (var c in custs) {
                Console.WriteLine(c);

            }

        }
        static void AddCustomer(AppDbContext context) {
            var cust = new Customer {
                Id = 0,
                Name = "Amazon",
                Sales = 0,
                Active = true
            };
            context.Customers.Add(cust); //similar to a migration with out updating the db
            var rowsAffected = context.SaveChanges();
            if (rowsAffected == 0) throw new Exception("Add Failed");
            return;
        }
        //static void AddOrders(AppDbContext context){
       // var order 1 = new Order { Id = 0, Description = "Order 1, Amount = 100, CustomerId = 5
       //var order 2 = new Order { Id = 0, Description = "Order 2, Amount = 200, Customer Id =5
        //}  above is a multi-order syntax similar to that below
        static void AddOrder(AppDbContext context) {
            var ord = new Order {
                Id = 0,
                Description = "N/A",
                Amount = 500,
                CustomerId = 5
            };
            context.Orders.Add(ord);
            var rowsAffected = context.SaveChanges();
            if (rowsAffected == 0) throw new Exception("Order not added");
            return;
        }
        static void AddProducts(AppDbContext context) {
            var product1 = new Product { Id = 0, Code = "Echo", Name = "Echo", Price = 100 };
            var product2 = new Product { Id = 0, Code = "Book", Name = "Best Book", Price = 200 };
            var product3 = new Product { Id = 0, Code = "Tablet", Name = "Kindle", Price = 300 };
            var product4 = new Product { Id = 0, Code = "Kitchen", Name = "InstaPot", Price = 400 };
            var product5 = new Product { Id = 0, Code = "Clothig", Name = "Dress", Price = 500 };
            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);
            context.Products.Add(product4);
            context.Products.Add(product5);
            var rowsAffected = context.SaveChanges();
            if (rowsAffected != 5) throw new Exception("Add Products failes");
            return;
        
        }


    }
}
