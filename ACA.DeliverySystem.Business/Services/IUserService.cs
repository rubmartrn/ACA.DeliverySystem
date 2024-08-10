using ACA.DeliverySystem.Business.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IUserService
    {
        Task Create(UserAddModel user, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<UserViewModel>> GetAll(CancellationToken token);
        Task<UserViewModel> GetById(int id, CancellationToken token);
        Task Update(int id, UserUpdateModel model, CancellationToken token);
    }
}
