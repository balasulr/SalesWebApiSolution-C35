using Microsoft.EntityFrameworkCore;

namespace SalesWebApi.Models {
    
    public class AppDbContext : DbContext {
        // Defining Customer Model
        public virtual DbSet<Customer> Customers { get; set; } // Customers is the plural version
        // Constructor with one parameter since don't need the default constructor
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        // FLuentAPI - Makes non primary key column unique
        protected override void OnModelCreating(ModelBuilder builder) {

        }
    }
}
