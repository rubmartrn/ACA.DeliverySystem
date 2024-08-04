using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IItemService
    {
        Task<Item> CreateItem(Item item, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<Item>> GetAll(CancellationToken token);
        Task<Item> GetById(int id, CancellationToken token);
        Task Update(Item item, CancellationToken token);
    }
}