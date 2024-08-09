using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IUserService
    {
        Task Create(User user, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<User>> GetAll(CancellationToken token);
        Task<User> GetById(int id, CancellationToken token);
        Task Update(int id, UserUpdateModel model, CancellationToken token);
    }
}
