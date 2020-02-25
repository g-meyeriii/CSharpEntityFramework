using CSharpEntityFramworkLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CSharpEntityFramworkLibrary {
    public class AppDbContext : DbContext {

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Orderline> Orderlines { get; set; }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if (!builder.IsConfigured) {
                builder.UseLazyLoadingProxies();
                var connStr = @" server=localhost\sqlexpress;database=CustEfDb;trusted_connection=true;";
                builder.UseSqlServer(connStr);
            }
        }
        protected override void OnModelCreating(ModelBuilder model) { //this replaces the the attributes that were on the product class 
            model.Entity<Product>(e => {
                e.ToTable("Products");
                e.HasKey(x => x.Id);
                e.Property(x => x.Code).HasMaxLength(10).IsRequired();
                e.Property(x => x.Name).HasMaxLength(30).IsRequired();
                e.Property(x => x.Price);
                e.HasIndex(x => x.Code).IsUnique();
            });
            model.Entity<Orderline>(e => {
                e.ToTable("OrderLines");
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Product).WithMany(x => x.Orderlines).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict); //foreingn key syntax for api, sequence is important
               
            });
        }  // model.Entity<Customer>(e => {
          //  )
        
    }
}
