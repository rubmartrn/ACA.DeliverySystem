using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IUserRepository
    {
        Task Add(User user, CancellationToken token);
        Task<Order> AddOrderInUser(int userId, Order order, CancellationToken token);
        Task<OperationResult> Delete(int id, CancellationToken token);
        Task<IEnumerable<User>> GetAll(CancellationToken token);
        Task<User> GetById(int id, CancellationToken token);
        Task<IEnumerable<Order>> GetUserOrders(int id, CancellationToken token);
        Task Update(User user, CancellationToken token);
    }
}