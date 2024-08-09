using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow) => _uow = uow;

        public async Task<Order> CreateOrder(Order order, CancellationToken token)
        {
            _uow.OrderRepository.Add(order);
            await _uow.Save(token);
            return order;
        }

        public async Task Delete(int id, CancellationToken token)
        {
            await _uow.OrderRepository.Delete(id, token);
            await _uow.Save(token);
        }

        public async Task<IEnumerable<Order>> GetAll(CancellationToken token)
        {
            return await _uow.OrderRepository.GetAll(token);
        }

        public async Task<Order> Get(int id, CancellationToken token)
        {
            return await _uow.OrderRepository.GetById(id, token);
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
    }
}
