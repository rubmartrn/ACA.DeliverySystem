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

        public async Task Create(UserAddModel user, CancellationToken token)
        {
            var mappedUser = _mapper.Map<User>(user);
            await _uow.UserRepository.Add(mappedUser, token);
            await _uow.Save(token);
        }
        public async Task Delete(int id, CancellationToken token)
        {
            await _uow.UserRepository.Delete(id, token);
            await _uow.Save(token);
        }

        public async Task<IEnumerable<UserViewModel>> GetAll(CancellationToken token)
        {
            var users = await _uow.UserRepository.GetAll(token);
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetById(int id, CancellationToken token)
        {
            var user = await _uow.UserRepository.GetById(id, token);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task Update(int id, UserUpdateModel model, CancellationToken token)
        {
            var oldUser = await _uow.UserRepository.GetById(id, token);

            if (oldUser == null)
                throw new KeyNotFoundException($"Item with ID {id} not found.");

            oldUser.Name = model.Name;
            oldUser.SureName = model.SureName;

            await _uow.UserRepository.Update(oldUser, token);
            await _uow.Save(token);
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersByUserId(int userId, CancellationToken token)
        {
            var orders = await _uow.OrderRepository.GetOrdersByUserId(userId, token);
            return orders.Select(x => _mapper.Map<OrderViewModel>(x));

        }
    }
}
