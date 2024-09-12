using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IUserService
    {
        Task AddOrderInUser(int userId, OrderAddModel model, CancellationToken token);
        Task<OperationResult> Create(UserAddModel user, CancellationToken token);
        Task<OperationResult> Delete(int id, CancellationToken token);
        Task<IEnumerable<UserViewModel>> GetAll(CancellationToken token);
        Task<SignInRequestModel> GetByEmail(string email, CancellationToken token);
        Task<UserViewModel> GetById(int id, CancellationToken token);
        Task<IEnumerable<OrderViewModel>> GetUserOrders(int userId, CancellationToken token);
        Task<OperationResult<SignInRequestModel>> SignIn(SignInRequestModel model, CancellationToken token);
        Task<OperationResult> Update(int id, UserUpdateModel model, CancellationToken token);
    }
}
