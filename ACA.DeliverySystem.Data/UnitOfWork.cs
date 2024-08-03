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

        private IItemRepository? itemRepository;

        public IItemRepository ItemRepository
        {
            get
            {
                if (itemRepository is null)
                {
                    itemRepository = new ItemRepository(_context);
                }

                return itemRepository;
            }
            set => itemRepository = value;
        }

        private IOrderRepository? _orderRepository;
        public IOrderRepository OrderRepository
        {
            get
            {
                if (itemRepository is null)
                {
                    itemRepository = new ItemRepository(_context);
                }

                return _orderRepository;
            }
            set => _orderRepository = value;
        }


        public async Task Save(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
