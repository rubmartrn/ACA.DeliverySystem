using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
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
            return await _uow.UserRepository.GetAll(token);
        }

        public async Task<User> GetById(int id, CancellationToken token)
        {
            return await _uow.UserRepository.GetById(id, token);
        }

        public async Task Update(int id, UserUpdateModel model, CancellationToken token)
        {
            var oldUser = await GetById(id, token);
            //oldUser = _mapper.Map<User>(model);
            oldUser.Name = model.Name;
            oldUser.SureName = model.SureName;
            await _uow.UserRepository.Update(oldUser, token);
            await _uow.Save(token);
        }
    }
}
