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
            return await _context.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id, token);

        }

        public async Task<Order> Add(Order order, CancellationToken token)
        {
            _context.Orders.Add(order);
            return order;
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

        public async Task<Item> AddItemInOrder(int orderId, int itemId, CancellationToken token)
        {
            var item = await _context.Items.SingleOrDefaultAsync(x => x.Id == itemId);
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
            order.Items.Add(item);
            return item;
        }

        public async Task RemoveItemFromOrder(int orderId, int itemId, CancellationToken token)
        {
            var item = await _context.Items.SingleOrDefaultAsync(x => x.Id == itemId);
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
            order.Items.Remove(item);
        }

        public async Task PayForOrder(int orderId, decimal amount, CancellationToken token)
        {
            var order = await _context.Orders.Include(x => x.Items).SingleOrDefaultAsync(x => x.Id == orderId);
            var amountToPay = order.Items.Sum(x => x.Price);
            order.PaidAmount = amountToPay;
            order.ProgressEnum = ProgressEnum.InProgress;
        }

        public async Task OrderCompleted(int orderId, CancellationToken token)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
            order.ProgressEnum = ProgressEnum.Completed;
        }


        public async Task CancelOrder(int orderId, CancellationToken token)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
            order.ProgressEnum = ProgressEnum.Canceled;
        }


    }
}
