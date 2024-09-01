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
            return await _context.Orders.Include(x => x.Items).SingleOrDefaultAsync(x => x.Id == id, token);

        }

        public async Task<Order> Add(Order order, CancellationToken token)
        {
            _context.Orders.Add(order);
            return order;
        }

        public async Task<OperationResult> Delete(int id, CancellationToken token)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id, token);

            if (order == null)
            {
                return OperationResult.Error($"Order with id {id} not found.", ErrorType.NotFound);
            }
            _context.Orders.Remove(order);
            return OperationResult.Ok();
        }


        public async Task Update(Order order, CancellationToken token)
        {
            _context.Orders.Update(order);

        }

        public async Task<OperationResult> AddItemInOrder(int orderId, int itemId, CancellationToken token)
        {
            var item = await _context.Items.SingleOrDefaultAsync(x => x.Id == itemId);
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
            if (item == null)
            {
                return OperationResult.Error($"Item with id {itemId} not found.", ErrorType.NotFound);
            }
            if (order == null)
            {
                return OperationResult.Error($"Order with id {itemId} not found.", ErrorType.NotFound);
            }
            if (order.ProgressEnum != ProgressEnum.Created)
            {
                return OperationResult.Error("You can't add item from order.", ErrorType.BadRequest);
            }
            order.Items.Add(item);
            order.AmountToPay += item.Price;
            return OperationResult.Ok();
        }

        public async Task<OperationResult> RemoveItemFromOrder(int orderId, int itemId, CancellationToken token)
        {
            var item = await _context.Items.SingleOrDefaultAsync(x => x.Id == itemId);
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
            if (item == null)
            {
                return OperationResult.Error($"Item with id {itemId} not found.", ErrorType.NotFound);
            }

            if (order == null)
            {
                return OperationResult.Error($"Order with id {itemId} not found.", ErrorType.NotFound);
            }
            if (!order.Items.Contains(item))
            {
                return OperationResult.Error($"You do not have that item in your list.", ErrorType.NotFound);
            }
            if (order.ProgressEnum != ProgressEnum.Created)
            {
                return OperationResult.Error("You can't remove item from order.", ErrorType.BadRequest);
            }
            order.Items.Remove(item);
            order.AmountToPay -= item.Price;
            return OperationResult.Ok();
        }

        public async Task<OperationResult> PayForOrder(int orderId, decimal amount, CancellationToken token)
        {
            var order = await _context.Orders.Include(x => x.Items).SingleOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
            {
                return OperationResult.Error($"Order with id {orderId} not found", ErrorType.NotFound);
            }
            var amountToPay = order.Items.Sum(x => x.Price);
            if (amountToPay == 0)
            {
                return OperationResult.Error("You do not have items in your order.", ErrorType.BadRequest);
            }
            if (amount != amountToPay)
            {
                return OperationResult.Error($"You must pay {amountToPay}.", ErrorType.BadRequest);
            }
            if (order.ProgressEnum != ProgressEnum.Created)
            {
                return OperationResult.Error("The order is in progress or canceled.", ErrorType.BadRequest);
            }
            order.PaidAmount = amountToPay;
            order.AmountToPay -= order.PaidAmount;
            order.ProgressEnum = ProgressEnum.InProgress;
            return OperationResult.Ok();
        }

        public async Task<OperationResult> OrderCompleted(int orderId, CancellationToken token)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
            {
                return OperationResult.Error($"Order with id {orderId} not found", ErrorType.NotFound);
            }
            if (order.ProgressEnum != ProgressEnum.InProgress)
            {
                return OperationResult.Error("Order must be in progress.", ErrorType.BadRequest);
            }
            order.ProgressEnum = ProgressEnum.Completed;
            return OperationResult.Ok();
        }


        public async Task<OperationResult> CancelOrder(int orderId, CancellationToken token)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
            {
                return OperationResult.Error($"Order with id {orderId} not found.", ErrorType.NotFound);
            }
            if (order.ProgressEnum == ProgressEnum.Completed)
            {
                return OperationResult.Error("You can't cancel completed order.", ErrorType.BadRequest);
            }
            if (order.ProgressEnum == ProgressEnum.Canceled)
            {
                return OperationResult.Error("It's already canceled.", ErrorType.BadRequest);
            }
            order.ProgressEnum = ProgressEnum.Canceled;
            return OperationResult.Ok();

        }


    }
}
