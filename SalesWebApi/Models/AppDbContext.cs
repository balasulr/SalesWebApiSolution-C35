using Microsoft.EntityFrameworkCore;

namespace SalesWebApi.Models {
    
    public class AppDbContext : DbContext {
        // Defining the Classes in the Models folder that was created
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderline> Orderlines { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        
        // Constructor with one parameter since don't need the default constructor
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        // FLuentAPI - Makes non primary key column unique
        protected override void OnModelCreating(ModelBuilder builder) {

        }
    }
}
