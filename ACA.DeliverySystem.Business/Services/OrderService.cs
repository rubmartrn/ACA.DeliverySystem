using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrder(OrderAddModel model, CancellationToken token)
        {
            var order = _mapper.Map<Order>(model);
            await _uow.OrderRepository.Add(order, token);
            await _uow.Save(token);
            return order;
        }

        public async Task<OperationResult> Delete(int id, CancellationToken token)
        {
            var result = await _uow.OrderRepository.Delete(id, token);
            if (!result.Success)
            {
                return result;
            }
            await _uow.Save(token);
            return result;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAll(CancellationToken token)
        {
            var orders = await _uow.OrderRepository.GetAll(token);
            return orders.Select(x => _mapper.Map<OrderViewModel>(x));
        }

        public async Task<OrderViewModel> Get(int id, CancellationToken token)
        {
            var order = await _uow.OrderRepository.GetById(id, token);
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<bool> Update(int id, Order model, CancellationToken token)
        {
            var order = await _uow.OrderRepository.GetById(id, token);
            if (order == null)
            {
                return false;
            }
            if (order.ProgressEnum != ProgressEnum.Created)
            {
                throw new Exception("You can't change order.");
            }
            order = model;
            await _uow.OrderRepository.Update(order, token);
            await _uow.Save(token);
            return true;
        }

        public async Task<OperationResult> AddItemInOrder(int orderId, int itemId, CancellationToken token)
        {
            var result = await _uow.OrderRepository.AddItemInOrder(orderId, itemId, token);
            if (!result.Success)
            {
                return result;
            }
            await _uow.Save(token);
            return result;
        }

        public async Task<OperationResult> RemoveItemFromOrder(int orderId, int itemId, CancellationToken token)
        {
            var result = await _uow.OrderRepository.RemoveItemFromOrder(orderId, itemId, token);
            if (!result.Success)
            {
                return result;
            }
            await _uow.Save(token);
            return result;
        }

        public async Task PayForOrder(int orderId, decimal amount, CancellationToken token)
        {
            var order = await _uow.OrderRepository.GetById(orderId, token);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order or item with ID {order} not found.");
            }
            if (order.ProgressEnum != ProgressEnum.Created)
            {
                throw new Exception("The order is in progress or canceled.");
            }

            var amountToPay = order.Items.Sum(x => x.Price);

            if (amount != amountToPay)
            {
                throw new Exception($"You must pay {amountToPay}");
            }

            await _uow.OrderRepository.PayForOrder(orderId, amount, token);
            await _uow.Save(token);

        }

        public async Task OrderCompleted(int orderId, CancellationToken token)
        {
            var order = await _uow.OrderRepository.GetById(orderId, token);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order or item with ID {order} not found.");
            }
            else if (order.ProgressEnum != ProgressEnum.InProgress)
            {
                throw new Exception("Order must be in progress.");
            }
            await _uow.OrderRepository.OrderCompleted(orderId, token);
            await _uow.Save(token);
        }

        public async Task CancelOrder(int orderId, CancellationToken token)
        {
            var order = await _uow.OrderRepository.GetById(orderId, token);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order or item with ID {order} not found.");
            }
            if (order.ProgressEnum == ProgressEnum.Completed)
            {
                throw new Exception("You can't cancel completed order.");
            }
            if (order.ProgressEnum == ProgressEnum.Canceled)
            {
                throw new Exception("It's already canceled.");
            }
            await _uow.OrderRepository.CancelOrder(orderId, token);
            await _uow.Save(token);
        }
    }
}
