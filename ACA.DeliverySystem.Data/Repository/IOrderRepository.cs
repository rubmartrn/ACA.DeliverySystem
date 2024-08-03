using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IOrderRepository
    {
        Task Add(Order order, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<Order>> GetAll(CancellationToken token);
        Task<Order> Get(int id, CancellationToken token);
        Task Update(Order item, CancellationToken token);
    }
}