using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IUserRepository
    {
        Task Add(User user, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<User>> GetAllItem(CancellationToken token);
        Task<User> GetItemById(int id, CancellationToken token);
        Task Update(User user, CancellationToken token);
    }
}