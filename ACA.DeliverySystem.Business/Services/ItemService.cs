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

        public async Task<Item> CreateItem(ItemAddModel item, CancellationToken token)
        {
            var mappedModel = _mapper.Map<Item>(item);
            await _uow.ItemRepository.Add(mappedModel, token);
            await _uow.Save(token);
            return mappedModel;
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var item = await _uow.ItemRepository.GetById(id, token);

            if (item == null)
                throw new KeyNotFoundException($"Item with ID {id} not found.");


            await _uow.ItemRepository.Delete(id, token);
            await _uow.Save(token);
        }

        public async Task<IEnumerable<ItemViewModel>> GetAll(CancellationToken token)
        {
            var items = await _uow.ItemRepository.GetAll(token);
            return _mapper.Map<IEnumerable<ItemViewModel>>(items);
        }

        public async Task<ItemViewModel> GetById(int id, CancellationToken token)
        {
            var item = await _uow.ItemRepository.GetById(id, token);
            return _mapper.Map<ItemViewModel>(item);
        }

        public async Task Update(int id, ItemUpdateModel model, CancellationToken token)
        {
            var oldItem = await _uow.ItemRepository.GetById(id, token);

            if (oldItem == null)
                throw new KeyNotFoundException($"Item with ID {id} not found.");

            // Code for review
            oldItem.Description = model.Description;
            oldItem.Price = model.Price;
            oldItem.Name = model.Name;

            await _uow.ItemRepository.Update(oldItem, token);
            await _uow.Save(token);
        }
    }
}
