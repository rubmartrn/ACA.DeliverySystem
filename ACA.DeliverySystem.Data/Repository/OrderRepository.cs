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

        public void Add(Order model)
        {
            _context.Orders.Add(model);
        }

        public async Task<Order> Get(int id, CancellationToken token)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<IEnumerable<Order>> GetAll(CancellationToken token)
        {
            return await _context.Orders.ToListAsync(token);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, token);
            _context.Orders.Remove(order);
        }

    }
}
