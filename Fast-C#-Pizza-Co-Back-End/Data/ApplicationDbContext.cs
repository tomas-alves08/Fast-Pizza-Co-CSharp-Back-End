using Fast_C__Pizza_Co_Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Fast_C__Pizza_Co_Back_End.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<PizzaOrder> PizzaOrders { get; set;}
        public DbSet<PizzaObj> PizzaObj { get; set;}
        public DbSet<PizzaDataUnit> PizzaData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaOrder>().HasData(
                new PizzaOrder()
                {
                    Id = 1,
                    TotalCost = 36,
                    DeliveryTime = DateTime.Now,
                }
            );

            modelBuilder.Entity<PizzaObj>().HasData(
                    
                new PizzaObj()
                {
                    Id = 1,
                    Name = "Focaccia",
                    Price = 6,
                    Quantity = 2,
                    PizzaOrderId = 1,
                },
                new PizzaObj()
                {
                    Id=3,
                    Name = "Pizza Spinaci",
                    Price = 12,
                    Quantity = 2,
                    PizzaOrderId = 1,
                }
            );

            modelBuilder.Entity<PizzaDataUnit>().HasData(

                new PizzaDataUnit()
                {
                    Id = 1,
                    Name = "Focaccia",
                    Ingredients = "Bread with italian olive oil and rosemary",
                    Price = 6,
                    PhotoName = "pizzas/focaccia.jpg",
                    SoldOut = false,
                },
                new PizzaDataUnit()
                {
                    Id = 2,
                    Name = "Pizza Margherita",
                    Ingredients = "Tomato, basil and mozarella",
                    Price = 10,
                    PhotoName = "pizzas/margherita.jpg",
                    SoldOut = false,
                },
                new PizzaDataUnit()
                {
                    Id = 3,
                    Name = "Pizza Spinaci",
                    Ingredients = "Tomato, mozarella, spinach, and ricotta cheese",
                    Price = 12,
                    PhotoName = "pizzas/spinaci.jpg",
                    SoldOut = false,
                },
                new PizzaDataUnit()
                {
                    Id = 4,
                    Name = "Pizza Funghi",
                    Ingredients = "Tomato, mozarella, mushrooms, and onion",
                    Price = 13,
                    PhotoName = "pizzas/funghi.jpg",
                    SoldOut = false,
                },
                new PizzaDataUnit()
                {
                    Id = 6,
                    Name = "Pizza Salamino",
                    Ingredients = "Tomato, mozarella, mushrooms, and onion",
                    Price = 15,
                    PhotoName = "pizzas/funghi.jpg",
                    SoldOut = true,
                },
                new PizzaDataUnit()
                {
                    Id = 5,
                    Name = "Pizza Prosciutto",
                    Ingredients = "Tomato, mozarella, ham, aragula, and burrata cheese",
                    Price = 18,
                    PhotoName = "pizzas/prosciutto.jpg",
                    SoldOut = false,
                }
            );
        }
    }
}
