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

        public async Task Delete(int id, CancellationToken token)
        {
            var order = await _uow.OrderRepository.GetById(id, token);
            if (order == null)
            {
                throw new KeyNotFoundException("Order with ID {id} not found.");
            }
            await _uow.OrderRepository.Delete(id, token);
            await _uow.Save(token);
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
            order = model;
            await _uow.OrderRepository.Update(order, token);
            await _uow.Save(token);
            return true;
        }

        public async Task AddItemInOrder(int orderId, int itemId, CancellationToken token)
        {
            var order = await _uow.OrderRepository.GetById(orderId, token);
            var item = await _uow.ItemRepository.GetById(itemId, token);
            if (order == null || item == null)
            {
                throw new KeyNotFoundException($"Order or item with ID {order} not found.");
            }
            await _uow.OrderRepository.AddItemInOrder(order.Id, item.Id, token);
            await _uow.Save(token);
        }
    }
}
