using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Create(User user, CancellationToken token)
        {
            await _uow.UserRepository.Add(user, token);
            await _uow.Save(token);
        }
        public async Task Delete(int id, CancellationToken token)
        {
            await _uow.UserRepository.Delete(id, token);
            await _uow.Save(token);
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken token)
        {
            return await _uow.UserRepository.GetAllItem(token);
        }

        public async Task<User> GetById(int id, CancellationToken token)
        {
            return await _uow.UserRepository.GetItemById(id, token);
        }

        public async Task Update(User user, CancellationToken token)
        {
            await _uow.UserRepository.Update(user, token);
            await _uow.Save(token);
        }
    }
}
