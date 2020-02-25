using CSharpEntityFramworkLibrary;
using CSharpEntityFramworkLibrary.Models;
using System;
using System.Linq;

namespace CSharpEntityFramework {
    class Program {
       // private static object[] custPk;

        static void Main(string[] args) {
            var context = new AppDbContext();

            RecalcOrderAmount(3, context);
            //UpdateCustomers(context);
            //AddCustomer(context);
            //GetCustomerByPk(context);
            //UpdateCustomers(context);
            //DeleteCustomer(context);
            //GetAllCustomers(context);
            //AddOrder(context);
            //(context);
            // AddOrderline(context);

            //GetOrderlines(context);
            //Console.WriteLine($"Avg price is {context.Products.Average(x => x.Price)}");

            //var top2 = context.Products.Where(x => x.Id > 7).ToList();

            //var actCust = context.Customers.Where(x => x.Active).ToList();
        }

        
        //static void GetOrderlines(AppDbContext context) {
        //    var orderlines = context.Orderlines.ToList();
        //    orderlines.ForEach(line => Console.WriteLine($"{line.Quantity}|{line.Order.Description}| {line.Product.Name}")); //Lamda for foreach loop

        //static void AddOrderline(AppDbContext context) {
        //    var order = context.Orders.SingleOrDefault(o => o.Description == "Order 5");
        //    var product = context.Products.SingleOrDefault(p => p.Code == "Shovel");
        //    var orderline = new Orderline {
        //        Id = 0, ProductId = product.Id, OrderId = order.Id, Quantity = 3
        //    };
        //    context.Orderlines.Add(orderline);
        //    var rowsAffecte = context.SaveChanges();
        //    if (rowsAffecte != 1) throw new Exception("Orderline Failed");
        //}

        static void RecalcOrderAmount(int orderId, AppDbContext context) {//study this part for Capstone
            var order = context.Orders.Find(orderId);
            var total = context.Orderlines.Sum(ol => ol.Quantity * ol.Product.Price);
            order.Amount = total;
            var rc = context.SaveChanges();
            if (rc != 1) throw new Exception("Order update of amount failed");
            
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
                Description = "Shovel",
                Amount = 600,
                CustomerId = 5
            };
            context.Orders.Add(ord);
            var rowsAffected = context.SaveChanges();
            if (rowsAffected == 0) throw new Exception("Order not added");
            return;
        }
        static void AddProducts(AppDbContext context) {
            var product6 = new Product {  Id = 0, Code = "Shovel", Name = "Snow Shovel", Price = 600 };
            //var product1 = new Product { Id = 0, Code = "Echo", Name = "Echo", Price = 100 };
            //var product2 = new Product { Id = 0, Code = "Book", Name = "Best Book", Price = 200 };
            //var product3 = new Product { Id = 0, Code = "Tablet", Name = "Kindle", Price = 300 };
            //var product4 = new Product { Id = 0, Code = "Kitchen", Name = "InstaPot", Price = 400 };
            //var product5 = new Product { Id = 0, Code = "Clothig", Name = "Dress", Price = 500 };
            //context.Products.Add(product1);
            //context.Products.Add(product2);
            //context.Products.Add(product3);
            //context.Products.Add(product4);
            //context.Products.Add(product5);
            context.Products.Add(product6);
            var rowsAffected = context.SaveChanges();
            if (rowsAffected != 1) throw new Exception("Add Products failes");
            return;
        
        }


    }
}
