using Fast_C__Pizza_Co_Back_End.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Fast_C__Pizza_Co_Back_End.Repository.IRepository
{
    public interface IPizzaOrderRepository
    {
        Task<List<PizzaOrder>> GetAllAsync(Expression<Func<PizzaOrder, bool>> filter = null);
        Task<PizzaOrder> GetOneAsync(Expression<Func<PizzaOrder, bool>> filter = null, bool tracked = true);
        Task CreateAsync(PizzaOrder entity);
        Task UpdateAsync(PizzaOrder entity);
        Task RemoveAsync(PizzaOrder entity);
        Task SaveAsync();
    }
}
