using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IItemService
    {
        Task<Item> CreateItem(ItemAddModel item, CancellationToken token);
        Task<OperationResult> Delete(int id, CancellationToken token);
        Task<IEnumerable<ItemViewModel>> GetAll(CancellationToken token);
        Task<ItemViewModel> GetById(int id, CancellationToken token);
        Task Update(int id, ItemUpdateModel model, CancellationToken token);
    }
}