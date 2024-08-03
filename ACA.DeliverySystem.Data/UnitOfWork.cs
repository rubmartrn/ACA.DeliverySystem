using ACA.DeliverySystem.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private IUserRepository? userRepository;

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

        public IUserRepository UserRepository
        {
            get
            {
                if(userRepository is null)
                {
                    userRepository = new UserRepository(_context);
                }
                return userRepository;
            }
            set => userRepository = value;
        }


        public async Task Save(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
