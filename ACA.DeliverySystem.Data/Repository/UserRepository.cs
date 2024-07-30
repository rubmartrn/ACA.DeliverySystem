using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.DeliverySystem.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DeliveryDbContext _context;
        public UserRepository(DeliveryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllItem(CancellationToken token)
        {
            return await _context.Users.ToListAsync(token);
        }

        public async Task<User> GetItemById(int id, CancellationToken token)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(User user, CancellationToken token)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user, CancellationToken token)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(int id, CancellationToken token)
        {
            var user = await _context.Users.SingleAsync(e => e.Id == id, token);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(token);
        }
    }
}
