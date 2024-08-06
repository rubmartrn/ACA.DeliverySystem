using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<Order>> GetAll(CancellationToken token);
        Task<Order> Get(int id, CancellationToken token);
        Task Update(Order order, CancellationToken token);
    }
}