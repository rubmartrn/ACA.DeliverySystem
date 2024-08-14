using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IItemRepository
    {
        Task<Item> Add(Item item, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<Item>> GetAll(CancellationToken token);
        Task<Item> GetById(int id, CancellationToken token);
        Task Update(Item item, CancellationToken token);
    }
}