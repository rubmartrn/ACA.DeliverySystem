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

        public async Task<Order?> GetById(int id, CancellationToken token)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(oi => oi.Item)
                .SingleOrDefaultAsync(x => x.Id == id, token);

        }

        public async Task<Order> Add(Order order, CancellationToken token)
        {
            _context.Orders.Add(order);
            return order;
        }

        public async Task<OperationResult> Delete(int id, CancellationToken token)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).SingleOrDefaultAsync(x => x.Id == id, token);

            if (order == null)
            {
                return OperationResult.Error($"Order with id {id} not found.", ErrorType.NotFound);
            }
            if (order.ProgressEnum == ProgressEnum.InProgress ||
               order.ProgressEnum == ProgressEnum.Completed)
            {
                return OperationResult.Error($"You can't delete it. Order is {order.ProgressEnum}", ErrorType.BadRequest);
            }
            if (order.OrderItems.Count != 0)
            {
                return OperationResult.Error($"Order have items in it. You can't delete it.", ErrorType.BadRequest);
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
            var order = await _context.Orders
                .Include(o => o.OrderItems).SingleOrDefaultAsync(x => x.Id == orderId);
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
                return OperationResult.Error($"You can't add item. Order is {order.ProgressEnum}", ErrorType.BadRequest);
            }
            //order.OrderItems.Add(item);
            //order.AmountToPay += item.Price;
            // Check if the item is already in the order
            var orderItem = order.OrderItems.SingleOrDefault(oi => oi.ItemId == itemId);

            if (orderItem != null)
            {
                // Item exists, increase quantity and price
                orderItem.Quantity++;
                orderItem.Price += item.Price;
            }
            else
            {
                // Item doesn't exist, add a new OrderItem with quantity 1
                order.OrderItems.Add(new OrderItem
                {
                    ItemId = item.Id,
                    OrderId = orderId,
                    ItemName = item.Name,
                    Price = item.Price,
                    Quantity = 1,
                    Item = item
                });
            }

            // Update the total amount to pay
            order.AmountToPay += item.Price;
            return OperationResult.Ok();
        }

        public async Task<OperationResult> RemoveItemFromOrder(int orderId, int itemId, CancellationToken token)
        {
            var item = await _context.Items.SingleOrDefaultAsync(x => x.Id == itemId);
            var order = await _context.Orders.Include(x => x.OrderItems).SingleOrDefaultAsync(x => x.Id == orderId);
            if (item == null)
            {
                return OperationResult.Error($"Item with id {itemId} not found.", ErrorType.NotFound);
            }
            if (order == null)
            {
                return OperationResult.Error($"Order with id {itemId} not found.", ErrorType.NotFound);
            }

            // Find the OrderItem to remove
            var orderItem = order.OrderItems.SingleOrDefault(oi => oi.ItemId == itemId);
            if (orderItem == null)
            {
                return OperationResult.Error($"You do not have that item in your list.", ErrorType.NotFound);
            }
            if (order.ProgressEnum != ProgressEnum.Created)
            {
                return OperationResult.Error($"You can't remove item from order. Order is {order.ProgressEnum}", ErrorType.BadRequest);
            }
            //order.OrderItems.Remove(item);
            //order.AmountToPay -= item.Price;

            // If the item's quantity is more than 1, just decrease the quantity
            if (orderItem.Quantity > 1)
            {
                orderItem.Quantity--;
                orderItem.Price -= item.Price;  // Update the price for the remaining quantity
                order.AmountToPay -= item.Price;  // Decrease the total amount to pay
            }
            else
            {
                // If the quantity is 1, remove the item from the order
                order.OrderItems.Remove(orderItem);
                order.AmountToPay -= item.Price;  // Decrease the total amount to pay
            }

            return OperationResult.Ok();
        }

        public async Task<OperationResult> PayForOrder(int orderId, decimal amount, CancellationToken token)
        {
            var order = await _context.Orders.Include(x => x.OrderItems).SingleOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
            {
                return OperationResult.Error($"Order with id {orderId} not found", ErrorType.NotFound);
            }
            var amountToPay = order.OrderItems.Sum(x => x.Price);
            if (amountToPay == 0)
            {
                return OperationResult.Error("You do not have items in your order.", ErrorType.BadRequest);
            }
            if (amount != amountToPay)
            {
                return OperationResult.Error($"You must pay {amountToPay.ToString("C")}.", ErrorType.BadRequest);
            }
            if (order.ProgressEnum != ProgressEnum.Created)
            {
                return OperationResult.Error("The order is in progress or canceled.", ErrorType.BadRequest);
            }
            order.PaidAmount = amount;
            order.AmountToPay = amountToPay - order.PaidAmount;
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
                return OperationResult.Error("Order must be in progress to mark it as completed.", ErrorType.BadRequest);
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
            if (order.ProgressEnum == ProgressEnum.InProgress)
            {
                return OperationResult.Error("You can't cancel order.It is in progress.", ErrorType.BadRequest);
            }
            order.ProgressEnum = ProgressEnum.Canceled;
            return OperationResult.Ok();

        }


    }
}
