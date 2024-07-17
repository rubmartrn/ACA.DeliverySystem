using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.DeliverySystem.Data.Repository
{
    public class OrderRepository
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

        public async Task<Order> Get(int id,CancellationToken token)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public async Task<IEnumerable<Order>> GetAll(CancellationToken token)
        {
            return await _context.Orders.ToListAsync(token);
        }

        public async Task Delete(int id,CancellationToken token)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x=>x.Id == id, token);
            _context.Orders.Remove(order);
        } 

    }
}
