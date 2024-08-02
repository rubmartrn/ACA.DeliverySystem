using ACA.DeliverySystem.Data.Models;
using ACA.DeliverySystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.DeliverySystem.Business
{
    public class ItemService:IItemService
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

        public async Task<IEnumerable<Item>> GetAllItems(CancellationToken token)
        {
            return await _uow.ItemRepository.GetAllItem(token);
        }

        public async Task<Item> GetItemById(int id, CancellationToken token)
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
