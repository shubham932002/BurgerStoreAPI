using BurgerStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerStoreAPI.Data
{
    public class BurgerStoreContext : DbContext
    {
        public BurgerStoreContext(DbContextOptions<BurgerStoreContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>().HasData(
              new Menu() { Id = 1,Name= "Crispy Supreme", Type= "veg", Price=100,ImageUrl= "/IMG/burger1.png" },
                new Menu() { Id = 2, Name = "Surprise", Type = "veg", Price = 100, ImageUrl = "/IMG/burger2.png" },
                  new Menu() { Id = 3, Name = "Whopper", Type = "veg", Price = 100, ImageUrl = "/IMG/burger3.png" },
                    new Menu() { Id = 4, Name = "Chilli Cheese", Type = "veg", Price = 100, ImageUrl = "/IMG/burger4.png" },
                      new Menu() { Id = 5, Name = "Tandoor Gill", Type = "veg", Price = 100, ImageUrl = "/IMG/burger5.png" }
       );
        }
    }
}
