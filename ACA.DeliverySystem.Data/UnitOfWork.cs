using ACA.DeliverySystem.Data.Repository;

namespace ACA.DeliverySystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly DeliveryDbContext _context;
        public UnitOfWork(DeliveryDbContext context)
        {
            _context = context;
        }

        private IItemRepository? _itemRepository;
        private IUserRepository? _userRepository;
        private IOrderRepository? _orderRepository;


        public IItemRepository ItemRepository
        {
            get
            {
                if (_itemRepository is null)
                {
                    _itemRepository = new ItemRepository(_context);
                }

                return _itemRepository;
            }
            set => _itemRepository = value;
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository is null)
                {
                    _orderRepository = new OrderRepository(_context);
                }

                return _orderRepository;
            }
            set => _orderRepository = value;
        }


        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
            set => _userRepository = value;
        }


        public async Task Save(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
