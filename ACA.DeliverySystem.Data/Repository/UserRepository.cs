using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ACA.DeliverySystem.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DeliveryDbContext _context;
        public UserRepository(DeliveryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAll(CancellationToken token)
        {
            return await _context.Users.ToListAsync(token);
        }

        public async Task<User> GetById(int id, CancellationToken token)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(User user, CancellationToken token)
        {
            _context.Users.Add(user);
        }

        public async Task Update(User user, CancellationToken token)
        {
            _context.Users.Update(user);

        }

        public async Task Delete(int id, CancellationToken token)
        {
            var user = await _context.Users.SingleAsync(e => e.Id == id, token);
            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<Order>> GetUserOrders(int id, CancellationToken token)
        {
            var user = await _context.Users.Include(x => x.Orders).SingleOrDefaultAsync(x => x.Id == id, token);
            return user.Orders;
        }

        public async Task<Order> AddOrderInUser(int userId, Order order, CancellationToken token)
        {
            var user = await _context.Users.Include(u => u.Orders).SingleAsync(e => e.Id == userId, token);
            user.Orders.Add(order);
            return order;

        }

    }
}
