using ACA.DeliverySystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ACA.DeliverySystem.Data.Repository
{
    public class ItemRepository : IItemRepository
    {
        readonly DeliveryDbContext _context;
        public ItemRepository(DeliveryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAll(CancellationToken token)
        {
            return await _context.Items.ToListAsync(token);
        }

        public async Task<Item> GetById(int id, CancellationToken token)
        {
            return await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Item> Add(Item item, CancellationToken token)
        {
            _context.Items.Add(item);
            return item;
        }

        public async Task Update(Item item, CancellationToken token)
        {
            _context.Items.Update(item);

        }
        public async Task<OperationResult> Delete(int id, CancellationToken token)
        {
            var item = await _context.Items.SingleOrDefaultAsync(e => e.Id == id, token);
            if (item == null)
            {
                return OperationResult.Error($"Item with id {id} not found", ErrorType.NotFound);
            }

            _context.Items.Remove(item);

            return OperationResult.Ok();
        }
    }
}
