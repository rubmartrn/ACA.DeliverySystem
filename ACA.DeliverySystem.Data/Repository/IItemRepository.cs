using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IItemRepository
    {
        Task Add(Item item, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<Item>> GetAllItem(CancellationToken token);
        Task<Item> GetItemById(int id, CancellationToken token);
        Task Update(Item item, CancellationToken token);
    }
}