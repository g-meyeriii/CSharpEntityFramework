using Microsoft.EntityFrameworkCore;
using System;

namespace CSharpEntityFramworkLibrary {
    public class AppDbContext : DbContext {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if (!builder.IsConfigured) {
                builder.UseLazyLoadingProxies();
                var connStr = @" server=localhost\sqlexpress;database=CustEfDb;trusted_connection=true;";
                builder.UseSqlServer(connStr);
            }
        }
        
    }
}
