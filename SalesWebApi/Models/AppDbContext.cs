using Microsoft.EntityFrameworkCore;

namespace SalesWebApi.Models {
    
    public class AppDbContext : DbContext {
        // Defining Customer Model
        public virtual DbSet<Customer> Customers { get; set; } // Customers is the plural version
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderline> Orderlines { get; set; }
        
        // Constructor with one parameter since don't need the default constructor
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        // FLuentAPI - Makes non primary key column unique
        protected override void OnModelCreating(ModelBuilder builder) {

        }
    }
}
