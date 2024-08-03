using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IItemService
    {
        Task<Item> CreateItem(Item item, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<Item>> GetAllItems(CancellationToken token);
        Task<Item> GetItemById(int id, CancellationToken token);
        Task Update(Item item, CancellationToken token);
    }
}