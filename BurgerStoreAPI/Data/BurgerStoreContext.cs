using BurgerStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerStoreAPI.Data
{
    public class BurgerStoreContext : DbContext
    {
        public BurgerStoreContext(DbContextOptions<BurgerStoreContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.User)
            //    .WithMany(u => u.Orders)
            //    .HasForeignKey(o => o.UserId);
        }
    }
}
