using Fast_C__Pizza_Co_Back_End.Data;
using Fast_C__Pizza_Co_Back_End.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fast_C__Pizza_Co_Back_End.Repository.IRepository
{
    public class PizzaOrderRepository : IPizzaOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public PizzaOrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(PizzaOrder entity)
        {
            await _db.PizzaOrders.AddAsync(entity);
            await SaveAsync();
        }
        public async Task UpdateAsync(PizzaOrder entity)
        {
            _db.PizzaOrders.Update(entity);
            await SaveAsync();
        }

        public async Task<PizzaOrder> GetOneAsync(Expression<Func<PizzaOrder, bool>> filter = null, bool tracked = true)
        {
            IQueryable<PizzaOrder> query = _db.PizzaOrders;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.Include(order => order.PizzaArr).FirstOrDefaultAsync();
        }

        public async Task<List<PizzaOrder>> GetAllAsync(Expression<Func<PizzaOrder, bool>> filter = null)
        {
            IQueryable<PizzaOrder> query = _db.PizzaOrders;

            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query
                            .Include(order => order.PizzaArr)
                            .OrderBy(order => order.DeliveryTime)
                            .ToListAsync();
        }

        public async Task RemoveAsync(PizzaOrder entity)
        {
            _db.PizzaOrders.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
