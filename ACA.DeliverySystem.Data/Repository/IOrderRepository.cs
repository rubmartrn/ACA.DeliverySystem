using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order order, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<Order>> GetAll(CancellationToken token);

        Task<Order> GetById(int id, CancellationToken token);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId, CancellationToken token);
        Task Update(Order order, CancellationToken token);
    }
}