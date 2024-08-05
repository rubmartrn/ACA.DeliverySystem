using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _uow;

        public ItemService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Item> CreateItem(Item item, CancellationToken token)
        {
            await _uow.ItemRepository.Add(item, token);
            await _uow.Save(token);
            return item;
        }

        public async Task Delete(int id, CancellationToken token)
        {
            await _uow.ItemRepository.Delete(id, token);
            await _uow.Save(token);
        }

        public async Task<IEnumerable<Item>> GetAll(CancellationToken token)
        {
            return await _uow.ItemRepository.GetAllItem(token);
        }

        public async Task<Item> GetById(int id, CancellationToken token)
        {
            return await _uow.ItemRepository.GetItemById(id, token);
        }

        public async Task Update(Item item, CancellationToken token)
        {
            await _uow.ItemRepository.Update(item, token);
            await _uow.Save(token);
        }
    }
}
