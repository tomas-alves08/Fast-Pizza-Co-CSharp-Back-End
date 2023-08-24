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
        }
    }
}
