using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ACA.DeliverySystem.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private DeliveryDbContext _context;

        public OrderRepository(DeliveryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAll(CancellationToken token)
        {
            return await _context.Orders.ToListAsync(token);
        }

        public async Task<Order> GetById(int id, CancellationToken token)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public void Add(Order model)
        {
            _context.Orders.Add(model);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, token);
            _context.Orders.Remove(order);
        }


        public async Task Update(Order order, CancellationToken token)
        {
            _context.Orders.Update(order);

        }
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId, CancellationToken token)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync(token);
        }
    }
}
