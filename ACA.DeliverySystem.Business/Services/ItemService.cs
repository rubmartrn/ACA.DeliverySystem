using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ItemService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
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
            return await _uow.ItemRepository.GetAll(token);
        }

        public async Task<Item> GetById(int id, CancellationToken token)
        {
            return await _uow.ItemRepository.GetById(id, token);
        }

        public async Task Update(int id, ItemUpdateModel model, CancellationToken token)
        {
            var oldItem = await GetById(id, token);
            // Code for review
            oldItem.Description = model.Description;
            oldItem.Price = model.Price;
            oldItem.Name = model.Name;
            //oldItem = _mapper.Map<Item>(model);
            await _uow.ItemRepository.Update(oldItem, token);
            await _uow.Save(token);
        }
    }
}
