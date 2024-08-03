using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<Order> Get(int id, CancellationToken token);
        Task<IEnumerable<Order>> GetAll(CancellationToken token);
        Task<bool> Update(int id, Order model, CancellationToken token);
    }
}