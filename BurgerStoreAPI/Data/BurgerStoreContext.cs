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
        public DbSet<Admin> Admins { get; set; }

        public DbSet<CartItem> Carts { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between User and Order
            modelBuilder.Entity<Order>()
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Menu>().Property(o => o.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");

            // Seed data for Menu table
            modelBuilder.Entity<Menu>().HasData(
                new Menu() { Id = 1, Name = "Crispy Supreme", Type = "veg", Price = 100, ImageUrl = "/IMG/burger1.png" },
                  new Menu() { Id = 2, Name = "Crispy Supreme", Type = "egg", Price = 150, ImageUrl = "/IMG/burger1.png" },
                    new Menu() { Id = 3, Name = "Crispy Supreme", Type = "nonveg", Price = 200, ImageUrl = "/IMG/burger1.png" },

                new Menu() { Id = 4, Name = "Surprise", Type = "veg", Price = 100, ImageUrl = "/IMG/burger2.png" },
                   new Menu() { Id = 5, Name = "Surprise", Type = "egg", Price = 150, ImageUrl = "/IMG/burger2.png" },
                      new Menu() { Id = 6, Name = "Surprise", Type = "nonveg", Price = 200, ImageUrl = "/IMG/burger2.png" },

                new Menu() { Id = 7, Name = "Whopper", Type = "veg", Price = 100, ImageUrl = "/IMG/burger3.png" },
                new Menu() { Id = 8, Name = "Whopper", Type = "egg", Price = 150, ImageUrl = "/IMG/burger3.png" },
                new Menu() { Id = 9, Name = "Whopper", Type = "nonveg", Price = 200, ImageUrl = "/IMG/burger3.png" },


                new Menu() { Id = 10, Name = "Chilli Cheese", Type = "veg", Price = 100, ImageUrl = "/IMG/burger4.png" },
                   new Menu() { Id = 11, Name = "Chilli Cheese", Type = "veg", Price = 150, ImageUrl = "/IMG/burger4.png" },
                      new Menu() { Id = 12, Name = "Chilli Cheese", Type = "veg", Price = 200, ImageUrl = "/IMG/burger4.png" },

                new Menu() { Id = 13, Name = "Tandoor Gill", Type = "veg", Price = 100, ImageUrl = "/IMG/burger5.png" },
                     new Menu() { Id = 14, Name = "Tandoor Gill", Type = "egg", Price = 150, ImageUrl = "/IMG/burger5.png" },
                          new Menu() { Id = 15, Name = "Tandoor Gill", Type = "veg", Price = 200, ImageUrl = "/IMG/burger5.png" }
            );
        }
    }
}